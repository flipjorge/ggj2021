using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFinderService
{
	public Dictionary<Guid, GameObject> lookups;
	public Dictionary<Guid, List<Action<GameObject>>> callbacks;

	public ObjectFinderService()
	{
		lookups = new Dictionary<Guid, GameObject>();
		callbacks = new Dictionary<Guid, List<Action<GameObject>>>();
	}

	public void RegisterObject(Guid guid, GameObject gameObject)
	{
		lookups[guid] = gameObject;

		if (callbacks.TryGetValue(guid, out List<Action<GameObject>> actions))
		{
			// Cache the action list as the unsubscribe can be called when invoking an action.
			List<Action<GameObject>> cachedList = new List<Action<GameObject>>(actions);
			actions.Clear();

			for (int i = 0; i < cachedList.Count; i++)
			{
				cachedList[i].Invoke(gameObject);
			}
		}
	}

	public void UnregisterObject(Guid guid)
	{
		lookups.Remove(guid);
	}

	public void Subscribe(Guid guid, Action<GameObject> callback)
	{
		if (lookups.TryGetValue(guid, out GameObject gameObject))
		{
			callback.Invoke(gameObject);
		}
		else
		{
			if (!callbacks.TryGetValue(guid, out List<Action<GameObject>> actions))
			{
				actions = new List<Action<GameObject>>();
				callbacks[guid] = actions;
			}

			actions.Add(callback);
		}
	}

	public void Unsubscribe(Guid guid, Action<GameObject> callback)
	{
		if (callbacks.TryGetValue(guid, out List<Action<GameObject>> actions))
		{
			actions.Remove(callback);
		}
	}

	public void Dispose()
	{
		foreach (var item in callbacks)
		{
			item.Value.Clear();
		}

		callbacks.Clear();
		lookups.Clear();
	}
}

public class ObjectFinderServiceFactory : IServiceFactory<ObjectFinderService>
{
	public ObjectFinderService Create()
	{
		return new ObjectFinderService();
	}
}
