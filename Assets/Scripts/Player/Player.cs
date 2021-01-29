using UnityEngine;

public class Player : MonoBehaviour
{
    #region Lifecycle
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        fsm = new PlayerFSM(this);
    }

    private void Start()
    {
        fsm.goToState(fsm.idleState);
    }

    private void Update()
    {
        fsm.currentState.Update();
    }

    private void FixedUpdate()
    {
        fsm.currentState.FixedUpdate();
    }
    #endregion

    #region FSM
    private PlayerFSM fsm;
    #endregion

    #region Rigidbody
    [HideInInspector] public new Rigidbody rigidbody;
    #endregion

    #region Movement
    public float velocity = 1;
    [HideInInspector] public Vector3 movementDirection;
    [HideInInspector] public Quaternion rotationDirection;
    #endregion
}