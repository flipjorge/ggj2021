using System;
using UnityEngine;

public class Reference<T> : ScriptableObject
{

    [SerializeField]
    private T value;

    public T Value
    {
        get => value;
        set
        {
            this.value = value;
            onValueChanged?.Invoke(value);
        }
    }

    public event Action<T> onValueChanged;
}