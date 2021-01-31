using UnityEngine;

public class PlayerStateIdle : FSMState<Player>
{

    private GameManager gameManager;

    #region Constructors
    public PlayerStateIdle(FSM<Player> fsm) : base(fsm) { }
    #endregion

    #region
    public override void Enter()
    {
        _camera = Camera.main;
        gameManager = (GameManager)GameObject.Find("GM").GetComponent("GameManager");
    }

    public override void Update()
    {
    }

    public override void FixedUpdate()
    {
        owner.rigidbody.velocity = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * owner.movementDirection * owner.velocity * Time.fixedDeltaTime;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Item"))
        {
            if (owner.trail.LeaderTrail.Count < owner.trail.maxTrailSize)
            {
                var item = collision.collider.GetComponent<Item>();
                owner.trail.addItem(item);
            }
            else
            {
                //TODO: probably notify the player that he can't pick another item
            }
        }
        else if (collision.collider.CompareTag("NPC"))
        {
            if (owner.trail.isFirstItemFrom(collision.collider.gameObject))
            {
                owner.trail.deliverFirst();
                //TODO: give it to the owner and score
                gameManager.ChangeScore(ScoreValue.DeliveredOwner);
            }
        }
        else if (collision.collider.CompareTag("Balcony"))
        {
            if (owner.trail.isFirstItemFrom(collision.collider.gameObject))
            {
                owner.trail.deliverFirst();
                //TODO: give it to the balcony and score
                gameManager.ChangeScore(ScoreValue.DeliveredBalcony);
            }
        }
    }

    public override void Exit()
    {

    }
    #endregion

    #region Camera
    private Camera _camera;
    #endregion
}