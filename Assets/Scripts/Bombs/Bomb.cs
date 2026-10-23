using System;
using UnityEngine;

[RequireComponent(typeof(LifeTimer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Exploder))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    private LifeTimer _lifeTimer;
    private Rigidbody _rigidbody;
    private Exploder _exploder;

    public event Action<Bomb> Expired;
    public event Action<float> LifeProgressChanged;

    private void Awake()
    {
        _lifeTimer = GetComponent<LifeTimer>();
        _rigidbody = GetComponent<Rigidbody>();
        _exploder = GetComponent<Exploder>();
    }

    private void OnEnable()
    {
        _lifeTimer.Expired += OnTimerExpired;
        _lifeTimer.ProgressChanged += OnProgressChanged;
    }

    private void OnDisable()
    {
        _lifeTimer.Expired -= OnTimerExpired;
        _lifeTimer.ProgressChanged -= OnProgressChanged;
        _lifeTimer.StopCountdown();
    }

    public void BeginLife()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        float duration = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);
        _lifeTimer.StartCountdown(duration);
    }

    private void OnTimerExpired()
    {
        _exploder.Explode();
        Expired?.Invoke(this);
    }

    private void OnProgressChanged(float progress)
    {
        LifeProgressChanged?.Invoke(progress);
    }
}