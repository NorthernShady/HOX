using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Hero))]
public class HeroEditor : Editor {

	public override void OnInspectorGUI()
	{
		var hero = (Hero)target;

		DrawDefaultInspector();

		if (GUILayout.Button("Load")) {
			hero.updateVisual();
		}
	}
}