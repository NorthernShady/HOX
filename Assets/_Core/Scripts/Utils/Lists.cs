using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public static class Lists
{
	public static List<T> RepeatedDefault<T>(int count)
	{
		return Repeated(default(T), count);
	}

	public static List<T> Repeated<T>(T value, int count)
	{
		List<T> ret = new List<T>(count);
		ret.AddRange(Enumerable.Repeat(value, count));
		return ret;
	}

	public static List<T> RepeatedClass<T>(int count) where T: new()
	{
		List<T> ret = new List<T>(count);
		for (int i = 0; i < count; i++) {
			ret.Add (new T ());
		}
		return ret;
	}

	public static void AddUnique<T> (List<T> list, T element) where T: class
	{
		int index = list.FindIndex (x => x == element);
		if (index == -1) {
			list.Add (element);
		}
	}

	public static void AddUnique(List<int> list, int element)
	{
		int index = list.FindIndex (x => x == element);
		if (index == -1) {
			list.Add (element);
		}
	}

	public static void ShuffleList<T> (List<T> list) where T: class
	{
		for (int i = 0; i < list.Count; i++) {
			var temp = list [i];
			int randomIndex = UnityEngine.Random.Range (i, list.Count);
			list [i] = list [randomIndex];
			list [randomIndex] = temp;
		}
	}

	public static void ShuffleList (List<int> list)
	{
		for (int i = 0; i < list.Count; i++) {
			var temp = list [i];
			int randomIndex = UnityEngine.Random.Range (i, list.Count);
			list [i] = list [randomIndex];
			list [randomIndex] = temp;
		}
	}
}
