using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlatformTouchDetector))]
[RequireComponent(typeof(ColorChanger))]
[RequireComponent(typeof(LifeTimer))]
public class FallingCube : MonoBehaviour
{
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    private Rigidbody _rigidbody;
    private PlatformTouchDetector _touchDetector;
    private ColorChanger _colorChanger;
    private LifeTimer _lifeTimer;

    public event Action<FallingCube> Expired;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _touchDetector = GetComponent<PlatformTouchDetector>();
        _colorChanger = GetComponent<ColorChanger>();
        _lifeTimer = GetComponent<LifeTimer>();

        _touchDetector.Touched += OnPlatformTouched;
        _lifeTimer.Expired += OnTimerExpired;
    }

    private void OnDestroy()
    {
        Expired = null;
        _touchDetector.Touched -= OnPlatformTouched;
        _lifeTimer.Expired -= OnTimerExpired;
    }

    private void OnPlatformTouched()
    {
        _colorChanger.SetRandomColor();
        _lifeTimer.StartCountdown(UnityEngine.Random.Range(_minLifeTime, _maxLifeTime));
    }

    private void OnTimerExpired()
    {
        Expired?.Invoke(this);
    }

    public void ResetState()
    {
        _touchDetector.ResetTouch();
        _colorChanger.ResetColor();
        _lifeTimer.StopCountdown();
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
}
