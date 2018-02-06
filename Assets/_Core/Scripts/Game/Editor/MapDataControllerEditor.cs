using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapDataController))]
public class MapDataControllerEditor : Editor {

	public override void OnInspectorGUI()
	{
		MapDataController mapDataController = (MapDataController)target;

		DrawDefaultInspector();

		if (GUILayout.Button("Save")) {
			var asset = Resources.Load<MapData>(mapDataController.mapDataName);
			EditorUtility.SetDirty(asset);
			mapDataController.saveMapData();
			AssetDatabase.SaveAssets();
		}

		if (GUILayout.Button("Load")) {
			mapDataController.loadMapData();
		}
	}
}
