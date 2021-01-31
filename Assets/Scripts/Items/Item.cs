using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Item : MonoBehaviour
{
    [HideInInspector] public GameObject owner;
    public Sprite[] sprites;
    public static float timeToLiveSeconds = 5f;
    [HideInInspector]
    public bool delivered = false;

    private PlayerTrail playerTrail;
    private Transform target;
    private bool followingTarget;
    public SpriteRenderer spriteRenderer;

    #region Lifecycle
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        var randomItemIndex = UnityEngine.Random.Range(0, sprites.Length - 1);
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

    private void OnDestroy()
    {
        if (delivered)
        {
            print("My precious...");
        }
        else
        {
            print("You've failed Middle-earth");
            // decrease score
        }
    }
    #endregion

    #region Drop
    public void drop(GameObject owner)
    {
        this.owner = owner;
        transform.position = new Vector3(owner.transform.position.x, 0, owner.transform.position.z);
    }
    #endregion

    #region No Owner Handling

    public void ownerLeaving()
    {
        this.owner = GameObject.FindGameObjectWithTag("Balcony");
        StartCoroutine(FadeUntilDestroyed());
        StartCoroutine(DestroyAfterTimeToLiveSeconds());
    }

    public IEnumerator FadeUntilDestroyed()
    {
        float elapsedTime = 0f;
        float startValue = spriteRenderer.color.a;
        while (elapsedTime < timeToLiveSeconds)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, 0f, elapsedTime / timeToLiveSeconds);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }

    public IEnumerator DestroyAfterTimeToLiveSeconds()
    {
        yield return new WaitForSeconds(timeToLiveSeconds);

        if (playerTrail != null)
        {
            playerTrail.LeaderTrail.Remove(gameObject);
        }
        Destroy(gameObject);
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