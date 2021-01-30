using System.Collections.Generic;
using UnityEngine;

// Must be attached to Player object
public class PlayerTrail : MonoBehaviour
{
    public List<GameObject> LeaderTrail;

    private void Start()
    {
        LeaderTrail = new List<GameObject>();
    }

    public void addItem(Item item)
    {
        LeaderTrail.Add(item.gameObject);
        item.startFollowing(this);
    }

    public void destroyFirst()
    {
        if (LeaderTrail.Count > 0)
        {
            GameObject first = LeaderTrail[0];
            LeaderTrail.RemoveAt(0);
            Destroy(first);
        }
    }

    public bool isFirstItemFrom(GameObject npc)
    {
        if (LeaderTrail.Count > 0)
        {
            GameObject owner = LeaderTrail[0].GetComponent<Item>().owner;
            return npc.Equals(owner);
        }

        return false;
    }
}
