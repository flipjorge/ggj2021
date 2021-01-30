using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private static GameObject player;
    private static PlayerTrail playerTrail;
    [SerializeField]
    private GameObject owner;

    private GameObject target;
    private bool followingTarget;


    private void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerTrail = player.GetComponent<PlayerTrail>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        followingTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (followingTarget)
        {
            float followSharpness = 0.1f;
            float offsetFloat = 2f;
            Vector3 offset = target.transform.forward * offsetFloat;
            transform.LookAt(target.transform);
            transform.position += ((target.transform.position - transform.position) - offset) * followSharpness;
        }
    }

    void LateUpdate()
    {
        if (followingTarget)
        {
            if (playerTrail.LeaderTrail.Count == 0 || playerTrail.LeaderTrail.IndexOf(gameObject) == 0)
            {
                target = player;
            }
            else
            {
                target = playerTrail.LeaderTrail[playerTrail.LeaderTrail.IndexOf(gameObject) - 1];
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!followingTarget && collision.gameObject.CompareTag("Player"))
        {
            followingTarget = true;
            playerTrail.LeaderTrail.Add(gameObject);
            
            // Probably no need to disable this, but anywaaaaay
            gameObject.GetComponent<Rigidbody>().Sleep();
            gameObject.GetComponent<Collider>().isTrigger = true;
        }
    }

    public void SetOwner(GameObject owner)
    {
        print("You're the boss of me now " + owner);
        this.owner = owner;
    }

    public GameObject GetOwner()
    {
        return owner;
    }
}
