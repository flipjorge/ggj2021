public class PlayerStateIdle : FSMState<Player>
{
    #region Constructors
    public PlayerStateIdle(FSM<Player> fsm) : base(fsm) { }
    #endregion

    #region
    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
    #endregion
}