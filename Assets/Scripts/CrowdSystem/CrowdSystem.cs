using System;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{

    [SerializeField]
    private CrowdUnit crowdUnitPrefab;

    public CrowdSpawnPoint[] spawnPoints;
    
    private void Awake()
    {
        spawnPoints = FindObjectsOfType<CrowdSpawnPoint>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnCrowdUnit();
        }
    }

    private void SpawnCrowdUnit()
    {
        var spawnPoint = spawnPoints.PickRandom();

        CrowdUnit unit = Instantiate(crowdUnitPrefab, spawnPoint.transform.position, Quaternion.identity);
    }
}