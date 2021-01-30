using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class CrowdUnit : MonoBehaviour
{
    [SerializeField]
    private Item itemPrefab;

    [SerializeField]
    private NavMeshSurfaceReference walkableSurface;

    [SerializeField]
    private UnitBehavior unitBehavior;

    private NavMeshAgent agent;

    private Bounds playableArea;

    private CrowdSpawnPoint[] spawnPoints;

    private Coroutine routine;

    private Item droppedItem;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.destination = walkableSurface.Value.center;

        routine = StartCoroutine(WalkAroundEnumerator());
    }

    public void Setup(Bounds playableArea, CrowdSpawnPoint[] spawnPoints, UnitBehavior unitBehavior)
    {
        this.spawnPoints = spawnPoints;
        this.playableArea = playableArea;
        this.unitBehavior = unitBehavior;

        agent.speed = unitBehavior.unitSpeed;
    }

    private WaitForSeconds Wait(float seconds) => new WaitForSeconds(seconds);

    private IEnumerator WalkAroundEnumerator()
    {
        yield return Wait(6);

        DateTime timePassed = DateTime.UtcNow;
        DateTime maxTime = timePassed.AddSeconds(unitBehavior.lifeSpan);
        while (DateTime.UtcNow < maxTime)
        {
            agent.destination = PickRandomPositionInsidePlayableAre();

            var random = Random.Range(0f, 1f);
            if (random < unitBehavior.chanceOfDroppingItem && droppedItem == null)
            {
                droppedItem = Instantiate<Item>(itemPrefab);
                droppedItem.drop(gameObject);
            }

            if (unitBehavior.stopsAtDestinationBeforeContinue)
            {
                while (!HasArrivedToDestination())
                {
                    yield return null;
                }
            }

            yield return Wait(unitBehavior.intervalBetweenDestinationChange);
        }

        agent.destination = spawnPoints.PickRandom().transform.position;

        while (!HasArrivedToDestination())
        {
            yield return null;
        }

        //unit left the building

        Destroy(this.gameObject);
    }

    private bool HasArrivedToDestination()
    {
        float dist = Vector3.Distance(agent.transform.position, agent.destination);
        return (dist <= 1.5f);
    }

    private Vector3 PickRandomPositionInsidePlayableAre()
    {
        float x = playableArea.center.x + Random.Range(-playableArea.extents.x, playableArea.extents.x);
        float z = playableArea.center.z + Random.Range(-playableArea.extents.z, playableArea.extents.z);

        return new Vector3(x, 0, z);
    }

    private void OnDestroy()
    {
        StopCoroutine(routine);

        if(droppedItem != null)
        {
            droppedItem.GetComponent<Item>().ownerLeaving();
        }
    }
}