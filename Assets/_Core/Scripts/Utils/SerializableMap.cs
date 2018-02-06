using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BaseSerializableMap {
}

[Serializable]
public class SerializableMap <KeyType, Value>  : BaseSerializableMap, ISerializationCallbackReceiver {
	[SerializeField] KeyType[] keys;
	[SerializeField] Value[] values;

	Dictionary<KeyType, Value> map;

	public SerializableMap () {
		FillMap();
	}
	public void clear()
	{
		keys = null;
		values = null;
		map.Clear ();
	}

	public Value this[KeyType key] {
		get {
			Value value;
			map.TryGetValue(key, out value);
			return value;
		}
		set {
			map[key] = value;
			OnBeforeSerialize ();
		}
	}

	public void FillMap() {
		map = new Dictionary<KeyType, Value>();
	}

	public void OnAfterDeserialize() {
		FillMap();

		if (keys == null)
			return;

		for (int i = 0, count = keys.Length; i < count; ++i)
			map[keys[i]] = values[i];

		keys = null;
		values = null;
	}

	public void OnBeforeSerialize() {
		if (map == null)
			return;

		var count = map.Count;

		keys = new KeyType[count];
		values = new Value[count];

		var index = 0;

		foreach (var pair in map) {
			keys[index] = pair.Key;
			values[index] = pair.Value;
			++index;
		}
	}

}
