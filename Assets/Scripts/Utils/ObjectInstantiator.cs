using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInstantiator : MonoBehaviour
{
    public void Instantiate(GameObject GO)
    {
        Instantiate(GO, transform);
    }

    public void Instantiate(GameObject GO, Transform container)
    {
        Instantiate(GO, container);
    }
}
