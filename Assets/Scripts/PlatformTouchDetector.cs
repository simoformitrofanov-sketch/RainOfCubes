using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlatformTouchDetector : MonoBehaviour
{
    public event Action Touched;

    private bool _hasTouched;

    private void OnCollisionEnter(Collision collision)
    { 
        if (_hasTouched)
            return;

        if (collision.gameObject.TryGetComponent<Platform>(out _) == false)
            return;

        _hasTouched = true;
        Touched?.Invoke();
    }

    public void ResetTouch()
    {
        _hasTouched = false;
    }
}

