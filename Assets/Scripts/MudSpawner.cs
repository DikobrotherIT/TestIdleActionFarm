using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private float _secondsBetweenSpawn;

    public float PlantTimer => _secondsBetweenSpawn;

    private void Start()
    {
        foreach (var item in _spawnPoints)
        {
            item.GetComponent<Cell>().PlantGrowth();
        }
    }
}
