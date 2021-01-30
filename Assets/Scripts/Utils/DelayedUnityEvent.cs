using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayedUnityEvent : MonoBehaviour
{
	[SerializeField]
	private float delay = 1;
	[SerializeField]
	private bool runOnStart = true;
	[SerializeField]
	private UnityEvent events = null;

	protected void Start()
	{
		if (runOnStart)
		{
			StartCountdown();
		}
	}

	public void StartCountdown()
	{
		Invoke("ExecuteEvents", delay);
	}

	public void ExecuteEvents()
	{
		events.Invoke();
	}
}
