#if UNITY_CLOUD_BUILD

using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public static class CloudBuildHelper  {
	public static void PreExport(UnityEngine.CloudBuild.BuildManifestObject manifest)
	{
		Debug.LogWarning("PreExport() called");
		GenerateDBEditor.downloadAllGoogleSheetsDataAndGenerateDB ();
		Debug.LogWarning("PreExport() finished");

		var buildNumber = String.Format("0.0.{0}", manifest.GetValue("buildNumber", "unknown"));

		var filePath = Application.dataPath + "/" + "_Core/Scripts/General/GameSettings.cs";
		var text = File.ReadAllText (filePath);
		text = text.Replace ("BUILD_NUMBER_VALUE", buildNumber);
		File.WriteAllText(filePath, text);
		AssetDatabase.ImportAsset( "Assets/" + "_Core/Scripts/General/GameSettings.cs", ImportAssetOptions.ForceUpdate );
	}
}

#endif
