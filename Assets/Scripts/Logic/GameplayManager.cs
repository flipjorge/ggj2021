using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayManager : Singleton<GameplayManager>
{
    public int Score { get; private set; }

    public TimeSpan TimeLeft { get; private set; }
    
    public int ObjectsDelivered { get; private set; }

    public int ObjectsNotDelivered { get; private set; }

    //  Got to wait for the implementation to understand what is the best type for this List
    public List<GameObject> CarryingObjects { get; private set; }

    public UnityEvent OnObjectCatched;
    public UnityEvent OnObjectDelivered;
    public UnityEvent OnObjectDropped;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        Score = 0;
        TimeLeft = new TimeSpan(0,0,GameManager.Instance.SessionTimeInSeconds);
        ObjectsDelivered = 0;
        ObjectsNotDelivered = 0;
        CarryingObjects.Clear();
    }
}
