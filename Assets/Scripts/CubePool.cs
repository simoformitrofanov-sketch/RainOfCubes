using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    [SerializeField] private FallingCube _cubePrefab;
    [SerializeField] private int _initialSize = 20;

    private readonly Queue<FallingCube> _available = new Queue<FallingCube>();

    private void Awake()
    {
        for (int i = 0; i < _initialSize; ++i)
        {
            _available.Enqueue(CreateCube());
        }
    }

    public FallingCube GetCube()
    {
        FallingCube cube = _available.Count > 0 ? _available.Dequeue() : CreateCube();
        cube.gameObject.SetActive(true);
        return cube;
    }

    public void ReturnCube(FallingCube cube)
    {
        cube.ResetState();
        cube.gameObject.SetActive(false);
        _available.Enqueue(cube);
    }

    private FallingCube CreateCube()
    {
        FallingCube cube = Instantiate(_cubePrefab, transform);
        cube.gameObject.SetActive(false);
        cube.Expired += ReturnCube;
        return cube;
    }
}
