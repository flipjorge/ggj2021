using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CrowdSettings", menuName = "ScriptableObjects/Crowd Settings", order = 1)]
public class CrowdSettings : ScriptableObject
{
    [Header("Spawn Settings")]
    [SerializeField]
    private float _spawnRate;

    [Header("Unit Behaviors")]
    [SerializeField]
    private UnitBehavior[] _unitBehaviors;

    public float spawnRate => _spawnRate;

    public UnitBehavior[] unitBehaviors => _unitBehaviors;
}

[Serializable]
public struct UnitBehavior
{
    [Range(0f, 1f)]
    public float chanceOfCheckingStore;

    public bool stopsAtDestinationBeforeContinue;

    public float intervalBetweenDestinationChange;

    public float lifeSpan;

    public float unitSpeed;

    public float chanceOfDroppingItem;
}