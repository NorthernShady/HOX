﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BaseScriptableObject <T> : ScriptableObject 
{
	[HideInInspector] [SerializeField] 
	public string SheetName = "";

	[HideInInspector] [SerializeField] 
	public string WorksheetName = "";

	// Note: initialize in OnEnable() not here.
	public T[] dataArray;

	void OnEnable()
	{
		//#if UNITY_EDITOR
		//hideFlags = HideFlags.DontSave;
		//#endif
		// Important:
		//    It should be checked an initialization of any collection data before it is initialized.
		//    Without this check, the array collection which already has its data get to be null 
		//    because OnEnable is called whenever Unity builds.
		// 
		if (dataArray == null)
			dataArray = new T[0];
	}

	//
	// Write a proper query methods for retrieving data.
	//
	//public $DataClassName FindByKey(string key)
	//{
	//    return Array.Find(dataArray, d => d.Key == key);
	//}
}
