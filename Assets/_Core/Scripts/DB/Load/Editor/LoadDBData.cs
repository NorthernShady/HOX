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
//		targetData.SheetName = "MP2";
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

		m_dataAssetsHolder = null;//Resources.Load<DataAssetsHolder> (k.Resources.DATA_ASSETS_HOLDER);
		m_configSheetAccessor = new ConfigSheetAccessor (m_dataAssetsHolder);

		loadGeneralConfigData (dataService);

		loadXPLevel (dataService);
		loadInAppItem (dataService);
		loadAnalyticsData (dataService);
		loadUserData (dataService);
		loadBoosterConfig(dataService);

		PlayerPrefs.DeleteAll ();
	}

	public static void downloadAllGoogleSheetsData ()
	{
		m_dataAssetsHolder = null;//Resources.Load<DataAssetsHolder> (k.Resources.DATA_ASSETS_HOLDER);
		loadGoogleSheet<UserRepresentation, UserRepresentationData> (m_dataAssetsHolder.getUserRepresentationAsset ());

		loadGoogleSheet<GeneralConfigRepresentation, GeneralConfigRepresentationData> (m_dataAssetsHolder.getGeneralConfigRepresentationAsset ());
		loadGoogleSheet<XPLevelRepresentation, XPLevelRepresentationData> (m_dataAssetsHolder.getXPLevelRepresentationAsset ());

		loadGoogleSheet<InAppItemRepresentation, InAppItemRepresentationData> (m_dataAssetsHolder.getInAppItemRepresentation ());
		loadGoogleSheet<BoosterConfigRepresentation, BoosterConfigRepresentationData>(m_dataAssetsHolder.getBoosterConfigRepresentationAsset());

		loadGoogleSheet<NotificationsRepresentation, NotificationsRepresentationData> (m_dataAssetsHolder.getNotificationsRepresentationAsset ());

		loadGoogleSheet<LevelListRepresentation, LevelListRepresentationData> (m_dataAssetsHolder.getLevelListRepresentationAsset ());
	}

	static void loadGeneralConfigData (DataService dataService)
	{
		var generalConfigRepresentation = m_dataAssetsHolder.getGeneralConfigRepresentationAsset ();

		foreach (var row in generalConfigRepresentation.dataArray) {
			if (row.Name.Length == 0)
				continue;
			dataService.connection.InsertAll (new[] {
				new Config {
					ConfigId = row.Configid,
					Name = row.Name,
					Data = row.Data,
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
					Name = row.Name,
					CurrentLevel = 0,
					SoftCurrency = row.Softcurrency,
					HardCurrency = row.Hardcurrency,
					ShuffleBoosterCount = 0,
					HammerBoosterCount = 0,
					RocketBoosterCount = 0,
					Lives = row.Lives,
					MaxLives = row.Maxlives,
					LifeRestorationTime = row.Liferestorationtime,
					LastLifeUpdateTime = 0,
					NewMechanicsShown = false,
					TutorialInGameBoostersGiven = false,
					LastLevelCompleted = false,
				},
			});
		}
	}


	static void loadXPLevel (DataService dataService)
	{
		var xpLevelRepresentation = m_dataAssetsHolder.getXPLevelRepresentationAsset ();

		// I substract previous xp to get only xp needed to achive the next level.
		// This allows me use convinient representation of xp in code and conviniet
		// representation of data in google sheets for Jorge.
		// for exapmle, 1000/2000/5000 is transformed to 1000/1000/3000
		int previousXP = 0;

		foreach (var item in xpLevelRepresentation.dataArray) {
			dataService.connection.InsertAll (new[] {
				new XPLevel {
					Id = item.Xplevel,
					RequiredXP = item.Requiredxp - previousXP
				},
			});
			previousXP = item.Requiredxp;
		}
	}

	static void loadInAppItem (DataService dataService)
	{
		var inAppItemRepresentation = m_dataAssetsHolder.getInAppItemRepresentation ();

		foreach (var item in inAppItemRepresentation.dataArray) {
			dataService.connection.InsertAll (new[] {
				new InAppItem {
					Id = item.Id,
					Name = item.Name,
					RewardType = item.Rewardtype,
					RewardAmount = item.Rewardamount,
					GooglePlayId = item.Googleplayid,
					PriceUSD = item.Priceusd
				},
			});
		}
	}

	static void loadAnalyticsData (DataService dataService)
	{

		UserRepresentation userRepresentation = m_dataAssetsHolder.getUserRepresentationAsset ();
		var user = userRepresentation.dataArray.ToList ().Find (x => x.Id == 0);

		dataService.connection.InsertAll (new[] {
			new AnalyticsData {
				Id = 0,
				Uuid = "",
				Last_time_login = 0,
				Days_after_install = 0,
				Current_free_hard = user.Hardcurrency,
				Current_free_soft = user.Softcurrency,
				Total_free_hard_got = user.Hardcurrency,
				Total_hard_got = user.Hardcurrency,
				Total_free_soft_got = user.Softcurrency,
				Total_soft_got = user.Softcurrency,
				Hard_boosters_state = ""
			},
		});
	}

	static void loadBoosterConfig(DataService dataService)
	{
		var boosterConfigRepresentation = m_dataAssetsHolder.getBoosterConfigRepresentationAsset();

		foreach (var item in boosterConfigRepresentation.dataArray) {
			dataService.connection.InsertAll (new[] {
				new BoosterConfig {
					Id = item.Id,
					Name = item.Name,
					UnlockLevel = item.Unlocklevel,
					PurchaseAmount = item.Purchaseamount,
					Cost = item.Cost,
				},
			});
		}
	}

//	static void loadLevelListData ()
//	{
//		var levelListDataRepresentation = m_dataAssetsHolder.getLevelListRepresentationAsset ();
//
//		LevelList gameLevelList = Resources.Load<LevelList> (k.Resources.LEVEL_LIST);
//
//		gameLevelList.gameLevels = new List<GameLevelInfo> ();
//
//		foreach (var row in levelListDataRepresentation.dataArray) {
//			if (row.Levelname == "")
//				continue;
//			GameLevelInfo levelInfo = new GameLevelInfo ();
//			levelInfo.levelBackName = new string[row.Levelbackname.Length];
//			Array.Copy (row.Levelbackname, levelInfo.levelBackName, row.Levelbackname.Length);
//			levelInfo.levelDataName = new string[row.Leveldataname.Length];
//			Array.Copy (row.Leveldataname, levelInfo.levelDataName, row.Leveldataname.Length);
//			levelInfo.levelTopologyName = new string[row.Leveltopologyname.Length];
//			Array.Copy (row.Leveltopologyname, levelInfo.levelTopologyName, row.Leveltopologyname.Length);
//			levelInfo.levelName = row.Levelname;
//			levelInfo.tutorialImageName = row.Tutorialimagename;
//			levelInfo.newElement = row.Newelements;
//
//			if (row.Tutorialprefab != "") {
//				GameObject tutorialPrefab = AssetDatabase.LoadAssetAtPath<GameObject> (TUTORIAL_PREFABS_PATH + row.Tutorialprefab + ".prefab");
//				if (tutorialPrefab == null) {
//					Debug.LogError ("WRONG TUTORIAL PREFAB NAME IN LEVEL LIST -> " + row.Tutorialprefab);
//				}
//				levelInfo.tutorialPrefab = tutorialPrefab;
//			}
//			gameLevelList.gameLevels.Add (levelInfo);
//		}
//
//		EditorUtility.SetDirty (gameLevelList);
//		AssetDatabase.SaveAssets ();
//	}
}