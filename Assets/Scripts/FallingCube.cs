using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FallingCube : MonoBehaviour
{
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;
    [SerializeField] private Color _initialColor = Color.red;

    private bool _hasTouchedPlatform;
    private Rigidbody _rigidbody;
    private Renderer _renderer;

    public event Action<FallingCube> Expired;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = _initialColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasTouchedPlatform)
            return;

        if (collision.gameObject.TryGetComponent<Platform>(out _) == false)
            return;

        _hasTouchedPlatform = true;

        _renderer.material.color = new Color(
            UnityEngine.Random.value,
            UnityEngine.Random.value,
            UnityEngine.Random.value
        );

        float lifeTime = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);

        Invoke(nameof(Expire), lifeTime);
    }

    private void Expire()
    {
        Expired?.Invoke(this);
    }

    public void ResetState()
    {
        _hasTouchedPlatform = false;
        _renderer.material.color = _initialColor;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
}
