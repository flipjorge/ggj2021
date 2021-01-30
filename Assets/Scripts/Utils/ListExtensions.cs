using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions 
{
    public static T PickRandom<T>(this List<T> list)
    {
        int randomIndex = Random.Range(0, list.Count);

        return list[randomIndex];
    }
    
    public static T PickRandom<T>(this T[] array)
    {
        int randomIndex = Random.Range(0, array.Length);

        return array[randomIndex];
    }
}
