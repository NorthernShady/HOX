using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.Linq;

using GDataDB;
using GDataDB.Linq;

using UnityQuickSheet;

// to resolve TlsException error.
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

using Google.GData.Client;
using Google.GData.Spreadsheets;
using System;

public class LoadDBData
{

	static string TUTORIAL_PREFABS_PATH = "Assets/_Core/Prefabs/Tutorial/Stages/";

	static DataAssetsHolder m_dataAssetsHolder;
	static ConfigSheetAccessor m_configSheetAccessor;

	public static bool Validator (object sender, X509Certificate certificate, X509Chain chain,
	                             SslPolicyErrors sslPolicyErrors)
	{
		return true;
	}

	public static void loadGoogleSheet<T,D> (T tableAsset) where T: BaseScriptableObject<D> where D:new()
	{   
		// resolves TlsException error
		ServicePointManager.ServerCertificateValidationCallback = Validator;

		GoogleDataSettings settings = GoogleDataSettings.Instance;
		if (settings != null) {
			if (string.IsNullOrEmpty (settings.OAuth2Data.client_id) ||
			    string.IsNullOrEmpty (settings.OAuth2Data.client_secret))
				Debug.LogWarning ("Client_ID and Client_Sceret is empty. Reload .json file.");

			if (string.IsNullOrEmpty (settings._AccessCode))
				Debug.LogWarning ("AccessCode is empty. Redo authenticate again.");
		} else {
			Debug.LogError ("Failed to get google data settings. See the google data setting if it has correct path.");
			return;
		}
		
		T targetData = tableAsset;
		var client = new DatabaseClient ("", "");
		string error = string.Empty;
		var db = client.GetDatabase (targetData.SheetName, ref error);	
		var table = db.GetTable<D> (targetData.WorksheetName) ?? db.CreateTable<D> (targetData.WorksheetName);

		List<D> myDataList = new List<D> ();

		var all = table.FindAll ();
		foreach (var elem in all) {
			D data = new D ();

			data = Cloner.DeepCopy<D> (elem.Element);
			myDataList.Add (data);
		}

		targetData.dataArray = myDataList.ToArray ();
		EditorUtility.SetDirty (targetData);
		AssetDatabase.SaveAssets ();
	}
		
	// Use this for initialization
	public static void Load (DataService dataService)
	{
		const int userId = 0;

		m_dataAssetsHolder = Resources.Load<DataAssetsHolder> (k.Resources.DATA_ASSETS_HOLDER);
		m_configSheetAccessor = new ConfigSheetAccessor (m_dataAssetsHolder);

		loadGeneralData(dataService);
		loadUserData(dataService);
		loadHeroesData(dataService);
		loadCreepsData(dataService);

		PlayerPrefs.DeleteAll ();
	}

	public static void downloadAllGoogleSheetsData ()
	{
		m_dataAssetsHolder = Resources.Load<DataAssetsHolder> (k.Resources.DATA_ASSETS_HOLDER);
		loadGoogleSheet<UserRepresentation, UserRepresentationData> (m_dataAssetsHolder.getUserRepresentationAsset());
		loadGoogleSheet<GeneralRepresentation, GeneralRepresentationData> (m_dataAssetsHolder.getGeneralRepresentationAsset());
		loadGoogleSheet<HeroConfigRepresentation, HeroConfigRepresentationData>(m_dataAssetsHolder.getHeroRepresentationAsset());
		loadGoogleSheet<CreepConfigRepresentation, CreepConfigRepresentationData>(m_dataAssetsHolder.getCreepRepresentationAsset());
	}

	static void loadGeneralData (DataService dataService)
	{
		var generalRepresentation = m_dataAssetsHolder.getGeneralRepresentationAsset ();

		foreach (var row in generalRepresentation.dataArray) {
			if (row.Name.Length == 0)
				continue;
			dataService.connection.InsertAll (new[] {
				new Config {
					ConfigId = row.Configid,
					Name = row.Name,
					Data = row.Data
				},
			});
		}
	}

	static void loadUserData (DataService dataService)
	{
		var userDataRepresentation = m_dataAssetsHolder.getUserRepresentationAsset ();

		foreach (var row in userDataRepresentation.dataArray) {
			if (row.Name.Length == 0)
				continue;
			dataService.connection.InsertAll (new[] {
				new User {
					Id = row.Id,
					Name = row.Name
				},
			});
		}
	}

	static void loadHeroesData(DataService dataService)
	{
		var heroDataRepresentation = m_dataAssetsHolder.getHeroRepresentationAsset();

		foreach (var row in heroDataRepresentation.dataArray) {
			if (row.Name.Length == 0)
				continue;
			dataService.connection.InsertAll(new[] {
				new HeroConfig {
					Id = row.Id,
					Name = row.Name,
					Level = row.Level,
					HP = row.HP,
					Attack = row.Attack,
					Defence = row.Defence,
					Speed = row.Speed,
					AttackSpeed = row.Attackspeed
				}
			});
		}
	}

	static void loadCreepsData(DataService dataService)
	{
		var creepDataRepresentation = m_dataAssetsHolder.getCreepRepresentationAsset();

		foreach (var row in creepDataRepresentation.dataArray) {
			if (row.Name.Length == 0)
				continue;
			dataService.connection.InsertAll(new[] {
				new CreepConfig {
					Id = row.Id,
					Name = row.Name,
					Level = row.Level,
					HP = row.HP,
					Attack = row.Attack,
					Defence = row.Defence,
					Speed = row.Speed,
					AttackSpeed = row.Attackspeed
				}
			});
		}
	}

//	static void loadXPLevel (DataService dataService)
//	{
////		var xpLevelRepresentation = m_dataAssetsHolder.getXPLevelRepresentationAsset ();
//
//		// I substract previous xp to get only xp needed to achive the next level.
//		// This allows me use convinient representation of xp in code and conviniet
//		// representation of data in google sheets for Jorge.
//		// for exapmle, 1000/2000/5000 is transformed to 1000/1000/3000
////		int previousXP = 0;
//
////		foreach (var item in xpLevelRepresentation.dataArray) {
////			dataService.connection.InsertAll (new[] {
////				new XPLevel {
////					Id = item.Xplevel,
////					RequiredXP = item.Requiredxp - previousXP
////				},
////			});
////			previousXP = item.Requiredxp;
////		}
//	}
}