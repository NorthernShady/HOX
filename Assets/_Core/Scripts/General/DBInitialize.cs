using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System.IO;
using SQLite4Unity3d;

public class DBInit : Initialize.SystemInitializer
{
	public override void initialize()
	{
		#if HOX_DEBUG
		int version = PlayerPrefs.GetInt ("Version", 1);

		if (version != GameSettings.BALANCE_VERSION) {
			#if !UNITY_EDITOR
			var DBPath = string.Format("{0}/{1}", Application.persistentDataPath, "HOX_db.sqlite");
			if (File.Exists(DBPath))
			{
				File.Delete(DBPath);
			}
			PlayerPrefs.SetInt ("Version",GameSettings.BALANCE_VERSION);
			PlayerPrefs.SetInt("LevelNumber", 0);
			#endif
		}
		#endif
		//var provider = DBProvider.instance <DBProvider> ();
	}

}
