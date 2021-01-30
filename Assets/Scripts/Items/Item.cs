using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject owner;

    private PlayerTrail playerTrail;
    private Transform target;
    private bool followingTarget;

    #region Lifecycle
    void Start()
    {
        followingTarget = false;
    }

    private void FixedUpdate()
    {
        if (target != null && followingTarget)
        {
            float followSharpness = 0.1f;
            float offsetFloat = 2f;
            Vector3 offset = target.forward * offsetFloat;
            transform.LookAt(target);
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
