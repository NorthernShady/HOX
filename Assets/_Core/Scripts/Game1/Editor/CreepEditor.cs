using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Creep))]
public class CreepEditor : Editor {

	public override void OnInspectorGUI()
	{
		var creep = (Creep)target;

		DrawDefaultInspector();

		if (GUILayout.Button("Load")) {
			creep.updateVisual();
		}
	}
}
