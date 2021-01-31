using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
	[SerializeField]
	private float delay = 1;

	protected void Start()
	{
		if (delay > 0)
		{
			Destroy(gameObject, delay);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
