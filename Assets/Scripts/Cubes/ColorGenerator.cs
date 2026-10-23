using UnityEngine;

public class ColorGenerator
{
    public Color Generate()
    {
        return new Color(
            Random.value,
            Random.value,
            Random.value);
    }
}