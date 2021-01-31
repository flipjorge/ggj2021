using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Must be attached to Player object
public class PlayerTrail : MonoBehaviour
{
    public List<GameObject> LeaderTrail;
    public int maxTrailSize = 5;
    public ItemReference nextItemReference;

    private void Start()
    {
        LeaderTrail = new List<GameObject>();
    }

    public void addItem(Item item)
    {
        LeaderTrail.Add(item.gameObject);
        item.startFollowing(this);

        //update next item
        if (LeaderTrail.Count == 1) nextItemReference.Value = item;
    }

    public void deliverFirst()
    {
        if (LeaderTrail.Count > 0)
        {
            GameObject first = LeaderTrail[0];
            first.GetComponent<Item>().delivered = true;
            LeaderTrail.Remove(first);
            Destroy(first);

            //update next item
            nextItemReference.Value = LeaderTrail.FirstOrDefault()?.GetComponent<Item>() ?? null;
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
