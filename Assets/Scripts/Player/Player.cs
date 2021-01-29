using UnityEngine;

public class Player : MonoBehaviour
{
    #region Lifecycle
    private void Awake()
    {
        fsm = new PlayerFSM(this);
    }

    private void Start()
    {
        fsm.goToState(fsm.idleState);
    }
    #endregion

    #region FSM
    private PlayerFSM fsm;
    #endregion
}