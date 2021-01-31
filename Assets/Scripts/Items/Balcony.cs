using System;
using UnityEngine;

public class Balcony : MonoBehaviour
{
   
    public GameObject arrow;
    public ItemReference nextItemReference;

    public Collider collider;
    
    private void Awake()
    {
        nextItemReference.onValueChanged += _nextItemChanged;

    }

    private void Update()
    {
        _nextItemChanged(nextItemReference.Value);
    }

    private void _nextItemChanged(Item obj)
    {
        if (obj != null && obj.owner == this.collider)
        {
            arrow.SetActive(true);
        }
        else
        {
            arrow.SetActive(false);
        }
    }
    
    private void OnDestroy()
    {
        nextItemReference.onValueChanged -= _nextItemChanged;
    }
}
