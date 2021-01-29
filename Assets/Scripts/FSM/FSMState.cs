using UnityEngine;

public abstract class FSMState<T>
{
    #region Constructors
    public FSMState(FSM<T> fsm)
    {
        _fsm = fsm;
        _owner = _fsm.owner;
    }
    #endregion

    #region Owner
    protected T _owner;

    public T owner
    {
        get { return _owner; }
    }
    #endregion

    #region FSM
    protected FSM<T> _fsm;

    public FSM<T> fsm
    {
        get { return _fsm; }
    }
    #endregion

    #region Events
    public virtual void Enter() { }
    public virtual void Exit() { }

    public virtual void FixedUpdate() { }
    public virtual void Update() { }

    public virtual void OnCollisionEnter(Collision collision) { }
    public virtual void OnCollisionStay(Collision collision) { }
    public virtual void OnCollisionExit(Collision collision) { }

    public virtual void OnCollisionEnter2D(Collision2D collision) { }
    public virtual void OnCollisionStay2D(Collision2D collision) { }
    public virtual void OnCollisionExit2D(Collision2D collision) { }

    public virtual void OnTriggerEnter(Collider collider) { }
    public virtual void OnTriggerStay(Collider collider) { }
    public virtual void OnTriggerExit(Collider collider) { }

    public virtual void OnTriggerEnter2D(Collider2D collider) { }
    public virtual void OnTriggerStay2D(Collider2D collider) { }
    public virtual void OnTriggerExit2D(Collider2D collider) { }
    #endregion
}