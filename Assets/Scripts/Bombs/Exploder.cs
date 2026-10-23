using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Exploder : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;
    [SerializeField] private float _force = 10f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Explode()
    {
        Vector3 position = transform.position;
        Collider[] colliders = Physics.OverlapSphere(position, _radius);

        foreach (Collider collider in colliders)
        {
            Rigidbody body = collider.attachedRigidbody;

            if (body == null || body == _rigidbody)
                continue;

            body.AddExplosionForce(
                _force,
                position,
                _radius,
                0f,
                ForceMode.Impulse);
        }
    }
}