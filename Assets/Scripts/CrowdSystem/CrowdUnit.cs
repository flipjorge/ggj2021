using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class CrowdUnit : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurfaceReference walkableSurface;

    [SerializeField]
    private float lifeSpan = 15f;

    private NavMeshAgent agent;

    private Bounds playableArea;

    private CrowdSpawnPoint[] spawnPoints;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.destination = walkableSurface.Value.center;

        var routine = StartCoroutine(WalkAroundEnumerator());
    }

    public void Setup(Bounds playableArea, CrowdSpawnPoint[] spawnPoints)
    {
        this.spawnPoints = spawnPoints;
        this.playableArea = playableArea;
    }


    private WaitForSeconds Wait(float seconds) => new WaitForSeconds(seconds);

    private IEnumerator WalkAroundEnumerator()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(2f);
        yield return Wait(3);

        DateTime timePassed = DateTime.UtcNow;
        DateTime maxTime = timePassed.AddSeconds(lifeSpan);
        while (DateTime.UtcNow < maxTime)
        {
            agent.destination = PickRandomPositionInsidePlayableAre();
            yield return Wait(4f);
        }

        agent.destination = spawnPoints.PickRandom().transform.position;
    }

    private Vector3 PickRandomPositionInsidePlayableAre()
    {
        float x = playableArea.center.x + Random.Range(-playableArea.extents.x, playableArea.extents.x);
        float z = playableArea.center.z + Random.Range(-playableArea.extents.z, playableArea.extents.z);
        
        return new Vector3(x, 0, z);
    }
}