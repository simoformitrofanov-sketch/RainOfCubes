using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.CubeExpired += SpawnAt;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeExpired -= SpawnAt;
    }

    public void SpawnAt(Vector3 position)
    {
        Bomb bomb = Spawn(position);
        bomb.Expired += OnExpired;
        bomb.BeginLife();
    }

    private void OnExpired(Bomb bomb)
    {
        bomb.Expired -= OnExpired;
        Return(bomb);
    }
}