using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reference<T> : ScriptableObject
{

    [SerializeField]
    private T value;
    
    public T Value
    {
        get => value;
        set => this.value = value;
    }
}
