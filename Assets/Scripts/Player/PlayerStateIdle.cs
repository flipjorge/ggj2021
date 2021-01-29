using UnityEngine;

public class PlayerStateIdle : FSMState<Player>
{
    #region Constructors
    public PlayerStateIdle(FSM<Player> fsm) : base(fsm) { }
    #endregion

    #region
    public override void Enter()
    {
        base.Enter();

        _camera = Camera.main;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        owner.rigidbody.velocity = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * owner.movementDirection * owner.velocity * Time.fixedDeltaTime;
    }

    public override void Exit()
    {
        base.Exit();
    }
    #endregion

    #region Camera
    private Camera _camera;
    #endregion
}