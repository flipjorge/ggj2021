using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshSurfaceRegister : MonoBehaviour
{
 
    [SerializeField]
    private NavMeshSurfaceReference navmeshReference;

    private void Awake()
    {
        NavMeshSurface navMeshSurface = GetComponent<NavMeshSurface>();
        navmeshReference.Value = navMeshSurface;
    }
}
