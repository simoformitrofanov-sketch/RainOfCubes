using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(LifeTimer))]
public class BombView : MonoBehaviour
{
    private Material _material;
    private LifeTimer _lifeTimer;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _lifeTimer = GetComponent<LifeTimer>();
    }

    private void OnEnable()
    {
        _lifeTimer.ProgressChanged += SetAlpha;
    }

    private void OnDisable()
    {
        _lifeTimer.ProgressChanged -= SetAlpha;
    }

    private void OnDestroy()
    {
        if (_material != null)
            Destroy(_material);
    }

    private void SetAlpha(float alpha)
    {
        Color color = _material.color;
        color.a = Mathf.Clamp01(alpha);
        _material.color = color;
    }
}