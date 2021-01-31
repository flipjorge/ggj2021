using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventComponent : MonoBehaviour
{
	[SerializeField]
	private UnityEvent onStart = null;
	[SerializeField]
	private UnityEvent onEnable = null;
	[SerializeField]
	private UnityEvent onDisable = null;
	[SerializeField]
	private UnityEvent onEndOfFrame = null;

	protected IEnumerator Start()
	{
		onStart.Invoke();
		yield return new WaitForEndOfFrame();
		onEndOfFrame.Invoke();
	}

	protected void OnEnable()
	{
		onEnable.Invoke();
	}

	protected void OnDisable()
	{
		onDisable.Invoke();
	}
}
