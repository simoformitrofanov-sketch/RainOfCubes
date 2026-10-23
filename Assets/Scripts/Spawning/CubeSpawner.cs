using System;
using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<FallingCube>
{
    [SerializeField] private Transform _spawnArea;
    [SerializeField] private float _spawnInterval = 1f;
    [SerializeField] private float _spawnHeight = 20f;

    private Coroutine _spawnRoutine;

    public event Action<Vector3> CubeExpired;

    private void OnEnable()
    {
        _spawnRoutine = StartCoroutine(SpawnLoop());
    }

    private void OnDisable()
    {
        if (_spawnRoutine == null)
            return;

        StopCoroutine(_spawnRoutine);
        _spawnRoutine = null;
    }

    private void SpawnOne()
    {
        float halfX = _spawnArea.localScale.x * 0.5f;
        float halfZ = _spawnArea.localScale.z * 0.5f;
        float x = UnityEngine.Random.Range(-halfX, halfX);
        float z = UnityEngine.Random.Range(-halfZ, halfZ);

        Vector3 position = _spawnArea.position + new Vector3(x, _spawnHeight, z);
        FallingCube cube = Spawn(position);
        cube.Expired += OnCubeExpired;
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);
            SpawnOne();
        }
    }

    private void OnCubeExpired(FallingCube cube)
    {
        Vector3 position = cube.transform.position;

        cube.Expired -= OnCubeExpired;
        cube.ResetState();
        Return(cube);

        CubeExpired?.Invoke(position);
    }
}
