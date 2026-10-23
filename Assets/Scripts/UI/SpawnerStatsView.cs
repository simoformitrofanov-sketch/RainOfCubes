using UnityEngine;
using TMPro;

public abstract class SpawnerStatsView<T> : MonoBehaviour where T : Component
{
    [SerializeField] private Spawner<T> _spawner;
    [SerializeField] private TMP_Text _spawnedText;
    [SerializeField] private TMP_Text _createdText;
    [SerializeField] private TMP_Text _activeText;

    protected virtual void OnEnable()
    {
        _spawner.CountsChanged += UpdateView;
    }

    protected virtual void OnDisable()
    {
        _spawner.CountsChanged -= UpdateView;
    }

    protected virtual void Start()
    {
        UpdateView();
    }

    private void UpdateView()
    {
        _spawnedText.text = $"Spawned: {_spawner.SpawnedCount}";
        _createdText.text = $"Created: {_spawner.CreatedCount}";
        _activeText.text = $"Active: {_spawner.ActiveCount}";
    }
}