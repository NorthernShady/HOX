using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BaseTypedMap {
}

[Serializable]
public class TypedMap<EnumType, Value> : BaseTypedMap, ISerializationCallbackReceiver where EnumType : struct, IComparable, IFormattable, IConvertible {
	[SerializeField] EnumType[] keys;
	[SerializeField] Value[] values;

	Dictionary<EnumType, Value> map =  null;

	public Dictionary<EnumType, Value> getDictionary(){
		if (map == null) {
			FillMap(); 
		}
		return map;
	}

	public TypedMap () {
		FillMap();
	}

	public Value this[EnumType key] {
		get {
			return map [key];

		}
		set {
			map[key] = value;
		}
	}

	public void FillMap() {
		map = new Dictionary<EnumType, Value>();
		foreach (EnumType key in Enum.GetValues(typeof(EnumType))) {
			

			map[key] = default(Value);
		}
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

		keys = new EnumType[count];
		values = new Value[count];

		var index = 0;

		foreach (var pair in map) {
			keys[index] = pair.Key;
			values[index] = pair.Value;

			++index;
		}
	}
}
