using System.Collections;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [SerializeField]
    private CrowdUnit crowdUnitPrefab;

    [SerializeField]
    private CrowdSettings crowdSettings;

    public CrowdSpawnPoint[] spawnPoints;

    public Bounds playableArea;

    [HideInInspector]
    public int spawnedUnits = 0;
    private float currentSpawnRate;
    private int maxSpawnableUnits = 4;

    private void Awake()
    {
        spawnPoints = FindObjectsOfType<CrowdSpawnPoint>();
        currentSpawnRate = crowdSettings.spawnRate;
    }
    void Start()
    {
        StartCoroutine(SpawnObject());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnCrowdUnit();
        }
    }

    IEnumerator SpawnObject()
    {
        float spawnCountdown = crowdSettings.spawnRate;
        while (true)
        {
            yield return null;
            spawnCountdown -= Time.deltaTime;

            // Should a new object be spawned?
            if (spawnCountdown < 0)
            {
                spawnCountdown += currentSpawnRate;

                int numberUnits = Random.Range(1, getCurrentMaxSpawnableUnits());
                print("Spawning " + numberUnits +  "units");
                for (int i = 0; i < numberUnits; i++)
                {
                    SpawnCrowdUnit();
                }
                print("Current Spawned Units: " + spawnedUnits);
                updateSpawnRate();
            }
        }
    }


    private int getCurrentMaxSpawnableUnits()
    {
        if(crowdSettings.maxUnits - spawnedUnits >= maxSpawnableUnits)
        {
            return maxSpawnableUnits;
        }
        else
        {
            // At minimum, we should be able to spawn one unit
            return Mathf.Max(crowdSettings.maxUnits - spawnedUnits, 2);
        }
    }
    private void updateSpawnRate()
    {
        currentSpawnRate = (1 + ((float)spawnedUnits) / ((float)crowdSettings.maxUnits)) * crowdSettings.spawnRate;
        print("Current Spawn Rate: " + currentSpawnRate);
    }

    private void SpawnCrowdUnit()
    {
        if(spawnedUnits >= crowdSettings.maxUnits)
        {
            return;
        }

        var spawnPoint = spawnPoints.PickRandom();

        var behavior = crowdSettings.unitBehaviors.PickRandom();

        CrowdUnit unit = Instantiate(crowdUnitPrefab, spawnPoint.transform.position, Quaternion.identity,
            this.transform);

        unit.Setup(playableArea, spawnPoints, behavior, this);

        spawnedUnits++;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(playableArea.center, playableArea.size);
    }
}