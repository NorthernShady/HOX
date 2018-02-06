using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SerializableDictionary<TKey, TValue>{

	[SerializeField]
	private List<TKey> keysList = new List<TKey>();

	[SerializeField]
	private List<TValue> valuesList = new List<TValue>();

	public SerializableDictionary()
	{

	}

	public SerializableDictionary(Dictionary<TKey, TValue> dic)
	{
		keysList.Clear ();
		valuesList.Clear ();
		foreach (var pair in dic) {
			keysList.Add (pair.Key);
			valuesList.Add (pair.Value);
		}
	}

	public Dictionary<TKey, TValue> getDictionary()
	{
		var dic = new Dictionary<TKey, TValue> ();
		var keyArray = keysList.ToArray();
		var valueArray = valuesList.ToArray ();
		int index = 0;
		while (index < keyArray.Length && index < valueArray.Length) {
			if (!dic.ContainsKey(keyArray[index])) {
				dic.Add (keyArray [index], valueArray [index]);
			}
			index++;
		}
		return dic;
	}

	public void add(TKey key, TValue value)
	{
		keysList.Add (key);
		valuesList.Add (value);
	}

}

