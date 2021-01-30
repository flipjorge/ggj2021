using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    #region Lifecycle
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        trail = GetComponentInChildren<PlayerTrail>();

        fsm = new PlayerFSM(this);
    }

    private void Start()
    {
        fsm.goToState(fsm.idleState);
    }

    private void Update()
    {
        fsm.currentState.Update();

        //set animator velocity parameter
        animator.SetFloat("velocity", rigidbody.velocity.magnitude);

        //set sprite direction
        if (spriteRenderer.flipX && movementDirection.x < 0) spriteRenderer.flipX = false;
        else if (!spriteRenderer.flipX && movementDirection.x > 0) spriteRenderer.flipX = true;
    }

    private void FixedUpdate()
    {
        fsm.currentState.FixedUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        fsm.currentState.OnCollisionEnter(collision);
    }
    #endregion

    #region FSM
    private PlayerFSM fsm;
    #endregion

    #region Rigidbody
    [HideInInspector] public new Rigidbody rigidbody;
    #endregion

    #region Visual
    [HideInInspector] public Animator animator;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    #endregion

    #region Movement
    public float velocity = 1;
    [HideInInspector] public Vector3 movementDirection;
    #endregion

    #region Trail
    [HideInInspector] public PlayerTrail trail;
    #endregion
}