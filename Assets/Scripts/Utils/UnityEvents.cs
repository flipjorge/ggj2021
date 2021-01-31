using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BoolUnityEvent : UnityEvent<bool> { }
[Serializable]
public class FloatUnityEvent : UnityEvent<float> { }
[Serializable]
public class IntUnityEvent : UnityEvent<int> { }
[Serializable]
public class StringUnityEvent : UnityEvent<string> { }
[Serializable]
public class StringListUnityEvent : UnityEvent<List<string>> { }
[Serializable]
public class GameObjectUnityEvent : UnityEvent<GameObject> { }
[Serializable]
public class TransformUnityEvent : UnityEvent<Transform> { }
[Serializable]
public class ComponentUnityEvent : UnityEvent<Component> { }
[Serializable]
public class TextureUnityEvent : UnityEvent<Texture> { }
[Serializable]
public class GuidUnityEvent : UnityEvent<Guid> { }
[Serializable]
public class GuidIntUnityEvent : UnityEvent<Guid, int> { }
[Serializable]
public class ObjectUnityEvent : UnityEvent<UnityEngine.Object> { }
[Serializable]
public class SystemObjectUnityEvent : UnityEvent<object> { }
