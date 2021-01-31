using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class CrowdUnit : MonoBehaviour
{
    private CrowdSystem crowdSystem;

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

    private bool isDropper;
    private bool alreadyDropped;
    private Item droppedItem;

    [HideInInspector] public Animator animator;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public new Rigidbody rigidbody;

    public ItemReference nextItemReference;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody>();

        nextItemReference.onValueChanged += _nextItemChanged;
    }

    private void Start()
    {
        agent.destination = walkableSurface.Value.center;

        routine = StartCoroutine(WalkAroundEnumerator());

        var random = Random.Range(0f, 1f);
        isDropper = random < unitBehavior.chanceOfDroppingItem;
    }

    private void Update()
    {
        //set animator velocity parameter
        animator.SetFloat("velocity", agent.velocity.magnitude);

        //set sprite direction
        if (!spriteRenderer.flipX && agent.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (spriteRenderer.flipX && agent.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    public void Setup(Bounds playableArea, CrowdSpawnPoint[] spawnPoints, UnitBehavior unitBehavior, CrowdSystem crowdSystem)
    {
        this.spawnPoints = spawnPoints;
        this.playableArea = playableArea;
        this.unitBehavior = unitBehavior;
        this.crowdSystem = crowdSystem;

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

            if (unitBehavior.stopsAtDestinationBeforeContinue)
            {
                while (!HasArrivedToDestination())
                {
                    yield return null;
                }
            }

            yield return Wait(unitBehavior.intervalBetweenDestinationChange);

            if (isDropper && !alreadyDropped)
            {
                droppedItem = Instantiate<Item>(itemPrefab);
                droppedItem.drop(gameObject);
                alreadyDropped = true;
            }
        }

        agent.destination = spawnPoints.PickRandom().transform.position;

        while (!HasArrivedToDestination())
        {
            yield return null;
        }

        //unit left the building
        crowdSystem.spawnedUnits--;
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

        if (droppedItem != null)
        {
            droppedItem.ownerLeaving();
        }

        nextItemReference.onValueChanged -= _nextItemChanged;

        transform.DOKill();
    }

    private void _nextItemChanged(Item obj)
    {
        if (obj != null && droppedItem == obj)
        {
            transform.DOShakeScale(999);
        }
        else
        {
            transform.DOKill();
            transform.localScale = Vector3.one;
        }
    }
}