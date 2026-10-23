using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Bomb))]
public class BombView : MonoBehaviour
{
    private Material _material;
    private Bomb _bomb;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _bomb = GetComponent<Bomb>();
    }

    private void OnEnable()
    {
        _bomb.LifeProgressChanged += SetAlpha;
    }

    private void OnDisable()
    {
        _bomb.LifeProgressChanged -= SetAlpha;
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