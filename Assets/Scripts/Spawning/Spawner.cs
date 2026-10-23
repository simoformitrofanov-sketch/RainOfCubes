using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T _prefab;

    private ObjectPool<T> _pool;

    public event Action CountsChanged;

    public int SpawnedCount { get; private set; }
    public int CreatedCount { get; private set; }
    public int ActiveCount => _pool.CountActive;

    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: CreateObject,
            actionOnGet: null,
            actionOnRelease: OnReturned,
            actionOnDestroy: DestroyObject);
    }

    protected T Spawn(Vector3 position)
    {
        T instance = _pool.Get();
        instance.transform.position = position;
        instance.gameObject.SetActive(true);
        SpawnedCount++;
        CountsChanged?.Invoke();

        return instance;
    }

    protected void Return(T instance)
    {
        _pool.Release(instance);
        CountsChanged?.Invoke();
    }

    private T CreateObject()
    {
        T instance = Instantiate(_prefab, transform);
        instance.gameObject.SetActive(false);
        CreatedCount++;

        return instance;
    }

    private void OnReturned(T instance)
    {
        instance.gameObject.SetActive(false);
    }

    private void DestroyObject(T instance)
    {
        Destroy(instance.gameObject);
    }
}