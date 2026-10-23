using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlatformTouchDetector))]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(LifeTimer))]
public class FallingCube : MonoBehaviour
{
    [SerializeField] private Color _initialColor = Color.red;
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    private Rigidbody _rigidbody;
    private PlatformTouchDetector _touchDetector;
    private LifeTimer _lifeTimer;
    private Material _material;
    private readonly ColorGenerator _colorGenerator = new ColorGenerator();

    public event Action<FallingCube> Expired;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _touchDetector = GetComponent<PlatformTouchDetector>();
        _lifeTimer = GetComponent<LifeTimer>();
        _material = GetComponent<Renderer>().material;
        _material.color = _initialColor;

        _touchDetector.Touched += OnPlatformTouched;
        _lifeTimer.Expired += OnTimerExpired;
    }

    private void OnDestroy()
    {
        _touchDetector.Touched -= OnPlatformTouched;
        _lifeTimer.Expired -= OnTimerExpired;

        if (_material != null)
            Destroy(_material);
    }

    public void ResetState()
    {
        _touchDetector.ResetTouch();
        _material.color = _initialColor;
        _lifeTimer.StopCountdown();
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private void OnPlatformTouched()
    {
        _material.color = _colorGenerator.Generate();
        _lifeTimer.StartCountdown(UnityEngine.Random.Range(_minLifeTime, _maxLifeTime));
    }

    private void OnTimerExpired()
    {
        Expired?.Invoke(this);
    }
}
