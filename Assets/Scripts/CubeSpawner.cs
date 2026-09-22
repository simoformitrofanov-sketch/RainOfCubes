using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubePool _pool;
    [SerializeField] private Transform _spawnArea;
    [SerializeField] private float _spawnInterval = 1f;
    [SerializeField] private float _spawnHeight = 20f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnOne), _spawnInterval, _spawnInterval);
    }

    private void SpawnOne()
    {
        FallingCube cube = _pool.GetCube();
        float halfX = _spawnArea.localScale.x * 0.5f;
        float halfZ = _spawnArea.localScale.z * 0.5f;
        float x = Random.Range(-halfX, halfX);
        float z = Random.Range(-halfZ, halfZ);

        cube.transform.position = _spawnArea.position + new Vector3(x, _spawnHeight, z);
    }
}
