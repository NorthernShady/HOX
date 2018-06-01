using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Obstacle))]
public class ObstacleEditor : Editor {

	public override void OnInspectorGUI()
	{
		var obstacle = (Obstacle)target;

		DrawDefaultInspector();

		if (GUILayout.Button("Load")) {
			obstacle.updateVisual();
		}
	}
}
