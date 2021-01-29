public class PlayerFSM : FSM<Player>
{
    #region Constructor
    public PlayerFSM(Player owner) : base(owner)
    {
        this._createStates();
        this._connectStates();
    }
    #endregion

    #region States
    public PlayerStateIdle idleState;
    #endregion

    #region Create States
    private void _createStates()
    {
        idleState = new PlayerStateIdle(this);
    }
    #endregion

    #region Connect States
    private void _connectStates()
    {
        //e.g. nameState.onEventName = this.otherState
    }
    #endregion
}