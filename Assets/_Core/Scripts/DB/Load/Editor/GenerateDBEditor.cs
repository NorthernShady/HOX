using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GenerateDBEditor : MonoBehaviour {

	[MenuItem( "Edit/DB/Generate DB" )]
	public static void generateDB()
	{
		Debug.LogWarning("generateDB() called");
		DBProvider.instance<I_DBProvider> ().createDB ();
		LoadDBData.Load (DBProvider.instance<DBProvider> ().getDataService());
		DBProvider.instance<DBProvider> ().backupDB ();
		DBProvider.instance<DBProvider> ().clear ();
		Debug.LogWarning("generateDB() finished");
	}

	[MenuItem( "Edit/DB/Download All Google Sheets Data And Generate DB")]
	public static void downloadAllGoogleSheetsDataAndGenerateDB()
	{
		LoadDBData.downloadAllGoogleSheetsData ();
		generateDB ();
	}
}
