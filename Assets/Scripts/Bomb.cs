using System;
using UnityEngine;

[RequireComponent(typeof(LifeTimer))]
[RequireComponent(typeof(Rigidbody))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 10f;

    private LifeTimer _lifeTimer;
    private Rigidbody _rigidbody;

    public event Action<Bomb> Expired;

    private void Awake()
    {
        _lifeTimer = GetComponent<LifeTimer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _lifeTimer.Expired += OnTimerExpired;
    }

    private void OnDisable()
    {
        _lifeTimer.Expired -= OnTimerExpired;
        _lifeTimer.StopCountdown();
    }

    public void BeginLife()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        float duration = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);
        _lifeTimer.StartCountdown(duration);
    }

    private void Explode()
    {
        Vector3 position = transform.position;
        Collider[] colliders = Physics.OverlapSphere(position, _explosionRadius);

        foreach (Collider collider in colliders)
        {
            Rigidbody body = collider.attachedRigidbody;

            if (body == null || body == _rigidbody)
                continue;

            body.AddExplosionForce(
                _explosionForce,
                position,
                _explosionRadius,
                0f,
                ForceMode.Impulse);
        }
    }

    private void OnTimerExpired()
    {
        Explode();
        Expired?.Invoke(this);
    }
}