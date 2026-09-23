using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color _initialColor = Color.red;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = _initialColor;
    }

    public void SetRandomColor()
    {
        _renderer.material.color = new Color(
            Random.value,
            Random.value,
            Random.value
        );
    }

    public void ResetColor()
    {
        _renderer.material.color = _initialColor;
    }
}
