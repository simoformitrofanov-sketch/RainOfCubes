using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color _initialColor = Color.red;

    private Material _material;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _material.color = _initialColor;
    }

    private void OnDestroy()
    {
        if (_material != null)
            Destroy(_material);
    }

    public void SetRandomColor()
    {
        _material.color = new Color(
            Random.value,
            Random.value,
            Random.value
        );
    }

    public void ResetColor()
    {
        _material.color = _initialColor;
    }
}