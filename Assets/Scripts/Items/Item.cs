using System.Net;
using UnityEngine;

public class Item : MonoBehaviour
{
    [HideInInspector] public GameObject owner;
    public Sprite[] sprites;

    private PlayerTrail playerTrail;
    private Transform target;
    private bool followingTarget;
    private SpriteRenderer spriteRenderer;

    #region Lifecycle
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        var randomItemIndex = Random.Range(0, sprites.Length - 1);
        spriteRenderer.sprite = sprites[randomItemIndex];
    }

    private void FixedUpdate()
    {
        if (target != null && followingTarget)
        {
            float followSharpness = 0.3f;
            float offsetFloat = 2f;
            Vector3 offset = target.right * offsetFloat;
            transform.rotation = Quaternion.LookRotation(-target.right, transform.up) * Quaternion.Euler(new Vector3(0, 90, 0));
            transform.position += ((target.position - transform.position) - offset) * followSharpness;
        }
    }

    void LateUpdate()
    {
        if (playerTrail != null && followingTarget)
        {
            if (playerTrail.LeaderTrail.Count == 0 || playerTrail.LeaderTrail.IndexOf(gameObject) == 0)
            {
                target = playerTrail.transform;
            }
            else
            {
                target = playerTrail.LeaderTrail[playerTrail.LeaderTrail.IndexOf(gameObject) - 1].transform;
            }
        }
    }
    #endregion

    #region Drop
    public void drop(GameObject owner)
    {
        this.owner = owner;
        transform.position = owner.transform.position;
    }
    #endregion

    #region Trail
    public void startFollowing(PlayerTrail trail)
    {
        playerTrail = trail;
        followingTarget = true;

        // Probably no need to disable this, but anywaaaaay
        gameObject.GetComponent<Rigidbody>().Sleep();
        gameObject.GetComponent<Collider>().isTrigger = true;
    }
    #endregion
}