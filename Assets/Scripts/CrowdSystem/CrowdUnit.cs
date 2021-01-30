using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CrowdUnit : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurfaceReference walkableSurface;

    [SerializeField]
    private float lifeSpan = 15f;

    private NavMeshAgent agent;

    private Bounds playableArea;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.destination = walkableSurface.Value.center;

        var routine = StartCoroutine(WalkAroundEnumerator());
    }

    public void SetPlayableArea(Bounds playableArea)
    {
        this.playableArea = playableArea;
    }

    private WaitForSeconds Wait(float seconds) => new WaitForSeconds(seconds);

    private IEnumerator WalkAroundEnumerator()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(2f);
        yield return Wait(2);
    }
    
}