using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrowdUnit : MonoBehaviour
{
   [SerializeField]
   private NavMeshSurfaceReference walkableSurface;

   private NavMeshAgent agent;

   private void Awake()
   {
      agent = GetComponent<NavMeshAgent>();
   }

   private void Start()
   {
      agent.destination = walkableSurface.Value.center;
   }
}
