public class FSM<T>
{
    #region Constructors
    public FSM(T owner)
    {
        _owner = owner;
    }
    #endregion

    #region Owner
    protected T _owner;

    public T owner
    {
        get { return _owner; }
    }
    #endregion

    #region CurrentState
    private FSMState<T> _currentState;

    public FSMState<T> currentState
    {
        get { return _currentState; }
    }
    #endregion

    #region Go
    public void goToState(FSMState<T> state)
    {
        if (_currentState != null) _currentState.Exit();
        _currentState = state;
        if (_currentState != null) _currentState.Enter();
    }
    #endregion
}