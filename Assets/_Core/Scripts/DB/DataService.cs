using UnityEngine;
using System.Collections;
using System.IO;
using SQLite4Unity3d;
using System.Collections.Generic;

public class DataService
{
	private SQLiteConnection _connection;
	private string _DBName;
	private string _DBBackupName;


	public SQLiteConnection connection {
		get {
			return _connection;
		}
	}

	public DataService (string DatabaseName)
	{

		_DBName = DatabaseName;
		_DBBackupName = "Backup_" + DatabaseName;
		openConection (_DBBackupName, _DBName);

	}

	void openConection (string DBBackupName, string DBName)
	{
#if UNITY_EDITOR
		var dbPath = string.Format (@"Assets/StreamingAssets/{0}", DBName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DBName);
        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID 
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DBName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DBName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DBName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
			var loadDb = Application.dataPath + "/StreamingAssets/" + DBName;  // this is the path to your StreamingAssets in iOS
			// then save to Application.persistentDataPath
			File.Copy(loadDb, filepath);
#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
		_connection = new SQLiteConnection (dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
		Debug.Log ("Final PATH: " + dbPath);   
	}

	public bool tableExists<T> ()
	{
		const string cmdText = "SELECT name FROM sqlite_master WHERE type='table' AND name=?";
		return _connection.ExecuteScalar<string> (cmdText, typeof(T).Name) != null;
	}

	public void DBBackup ()
	{
		DBBackup (_DBBackupName, _DBName);
	}

	void DBBackup (string DBBackupName, string DBName)
	{

#if UNITY_EDITOR
		var DBBackupPath = string.Format (@"Assets/StreamingAssets/{0}", DBBackupName);
		var DBPath = string.Format (@"Assets/StreamingAssets/{0}", DBName);
		if (File.Exists (DBBackupPath)) {
			File.Delete (DBBackupPath);
		}
		File.Copy (DBPath, DBBackupPath);

#endif

	}

	void restoreDB (string DBBackupName, string DBName)
	{
		_connection.Close ();
#if UNITY_EDITOR
		var DBBackupPath = string.Format (@"Assets/StreamingAssets/{0}", DBBackupName);
		var DBPath = string.Format (@"Assets/StreamingAssets/{0}", DBName);
		if (File.Exists (DBPath)) {
			try {
				File.Delete (DBPath);
			} catch (System.Exception e) {
				Debug.Log (e);
			}
		}

		try {
			File.Copy (DBBackupPath, DBPath);
		} catch (System.Exception e) {
			Debug.Log (e);
		}
#else
        var DBPath = string.Format("{0}/{1}", Application.persistentDataPath, DBName);
        if (File.Exists(DBPath))
        {
            File.Delete(DBPath);
        }
#endif
		openConection (DBBackupName, DBName);
	}

	public void restoreDB ()
	{
		restoreDB (_DBBackupName, _DBName);
	}
}
