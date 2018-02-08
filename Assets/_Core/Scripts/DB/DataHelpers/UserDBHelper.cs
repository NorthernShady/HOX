using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CargoStorageChanedEvent: GameEvent
{

}

public class UserDBHelper : MonoBehaviour
{

	private static User m_generalUser = null;

	public class SoftCurrencyLackEvent : GameEvent
	{

	}

	public class HardCurrencyLackEvent : GameEvent
	{

	}

	static string hashsetToStr (HashSet<int> hashset)
	{
		string str = "";
		foreach (int item in hashset) {
			str += item.ToString () + ", ";
		}
		return str;
	}

	public static User getPlayerData ()
	{
		if (m_generalUser == null) {
			m_generalUser = DBProvider.instance<I_UserDBProvider> ().getUser ();
		}
		return m_generalUser;
	}

	public static void updatePlayer (User userData)
	{
		//userData.LastCommandTime = System.DateTime.Now.Ticks;
		DBProvider.instance<I_DBProvider> ().updateRowValue (userData);
		m_generalUser = userData;
	}

//	public static bool trySpendPlayerSoftCorrency (int spendValue)
//	{
//		var user = getPlayerData ();
//		if (user.SoftCurrency < spendValue) {
//			EventManager.Instance.Raise (new SoftCurrencyLackEvent ());
//			return false;
//		}
//
//		EventManager.Instance.Raise (new SoundEvents.Play(AudioIDs.SPEND_COINS));
//		GameAnalyticsController.spendSoftCurrency (spendValue);
//
//		user.SoftCurrency -= spendValue;
//		DBProvider.instance<I_DBProvider> ().updateRowValue (user);
//		m_generalUser = user;
//		EventManager.Instance.Raise (new SoftCurrencyChangeEvent (user.SoftCurrency));
//		return true;
//	}
//
//	public static bool trySpendPlayerHardCurrency (int spendValue, int gainValue, string reason, int level)
//	{
//		var user = getPlayerData ();
//		if (user.HardCurrency < spendValue) {
//			var lack = spendValue - user.HardCurrency;
//			GameAnalyticsController.lackOfHardCurrency (lack, reason, level);
//			EventManager.Instance.Raise (new HardCurrencyLackEvent ());
//			return false;
//		}
//
//		EventManager.Instance.Raise (new SoundEvents.Play(AudioIDs.SPEND_COINS));
//		GameAnalyticsController.spendHardCurrency (spendValue, gainValue, reason, level);
//
//		user.HardCurrency -= spendValue;
//		DBProvider.instance<I_DBProvider> ().updateRowValue (user);
//		m_generalUser = user;
//		EventManager.Instance.Raise (new HardCurrencyChangeEvent (user.HardCurrency));
//		return true;
//	}
//
//	public static bool tryConsumeLife()
//	{
//		var user = getPlayerData();
//		if (user.Lives > 0) {
//			EventManager.Instance.Raise(new LifeGameEvents.ConsumeLives());
//			return true;
//		}
//
//		return false;
//	}
//
//	public static void addSoftCurrency (int value, bool isFreeCurrency = true)
//	{
//		var user = getPlayerData ();
//		user.SoftCurrency = Mathf.Clamp (user.SoftCurrency + value, 0, 99999999);
//		DBProvider.instance<I_DBProvider> ().updateRowValue (user);
//		m_generalUser = user;
//		EventManager.Instance.Raise (new SoftCurrencyChangeEvent (user.SoftCurrency));
//		if (isFreeCurrency) {
//			GameAnalyticsController.addFreeSoftCurrency (value);
//		} else {
//			GameAnalyticsController.addPurchasedSoftCurrency (value);
//		}
//	}
//
//	public static void addHardCurrency (int value, bool isFreeCurrency = true)
//	{
//		var user = getPlayerData ();
//		user.HardCurrency += value;
//		DBProvider.instance<I_DBProvider> ().updateRowValue (user);
//		m_generalUser = user;
//		EventManager.Instance.Raise (new HardCurrencyChangeEvent (user.HardCurrency));
//		if (isFreeCurrency) {
//			GameAnalyticsController.addFreeHardCurrency (value);
//		} else {
//			GameAnalyticsController.addPurchasedHardCurrency (value);
//		}
//	}
//
//	public static void addPaidHardCurrency(int value) {
//		addHardCurrency(value, false);
//	}
//
//	public static void spendPlayerInGameBooster (MenuData.BoosterType type)
//	{
//		var user = getPlayerData ();
//		switch (type) {
//		case MenuData.BoosterType.HAMMER:
//			{
//				user.HammerBoosterCount -= 1;
//				break;
//			}
//		case MenuData.BoosterType.ROCKET:
//			{
//				user.RocketBoosterCount -= 1;
//				break;
//			}
//		case MenuData.BoosterType.SHUFFLE:
//			{
//				user.ShuffleBoosterCount -= 1;
//				break;
//			}
//		default:
//			{
//				break;
//			}
//		}
//		int level = user.CurrentLevel + 1;
//		EventManager.Instance.Raise(new AnalyticsGameEvent.HardBoosterSpent(type, level));
//		DBProvider.instance<I_DBProvider> ().updateRowValue (user);
//		m_generalUser = user;
//
//		EventManager.Instance.Raise(new BoosterCountUpdated());
//	}
//
//	public static void addPlayerInGameBooster(MenuData.BoosterType type, string gainType, int amount)
//	{
//		var user = getPlayerData ();
//		switch (type) {
//		case MenuData.BoosterType.HAMMER:
//			{
//				user.HammerBoosterCount += amount;
//				break;
//			}
//		case MenuData.BoosterType.ROCKET:
//			{
//				user.RocketBoosterCount += amount;
//				break;
//			}
//		case MenuData.BoosterType.SHUFFLE:
//			{
//				user.ShuffleBoosterCount += amount;
//				break;
//			}
//		default:
//			{
//				break;
//			}
//		}
//		int level = user.CurrentLevel + 1;
//		EventManager.Instance.Raise(new AnalyticsGameEvent.HardBoosterGot(type, gainType, level));
//		DBProvider.instance<I_DBProvider> ().updateRowValue (user);
//		m_generalUser = user;
//
//		EventManager.Instance.Raise(new BoosterCountUpdated());
//	}
//
//	public static void addTutorialInGameBoosters()
//	{
//		var user = getPlayerData();
//		if (!user.TutorialInGameBoostersGiven) {
//			var userData = Resources.Load<DataAssetsHolder> (k.Resources.DATA_ASSETS_HOLDER).getUserRepresentationAsset();
//			addPlayerInGameBooster(MenuData.BoosterType.HAMMER, "free", userData.dataArray[0].Hammerboostercount);
//			addPlayerInGameBooster(MenuData.BoosterType.ROCKET, "free", userData.dataArray[0].Rocketboostercount);
//			addPlayerInGameBooster(MenuData.BoosterType.SHUFFLE, "free", userData.dataArray[0].Shuffleboostercount);
//
//			user.TutorialInGameBoostersGiven = true;
//			DBProvider.instance<I_DBProvider> ().updateRowValue (user);
//			m_generalUser = user;
//		}
//
//		if (user.HammerBoosterCount <= 0)
//			addPlayerInGameBooster(MenuData.BoosterType.HAMMER, "free", 1);
//
//		if (user.RocketBoosterCount <= 0)
//			addPlayerInGameBooster(MenuData.BoosterType.ROCKET, "free", 1);
//
//		if (user.ShuffleBoosterCount <= 0)
//			addPlayerInGameBooster(MenuData.BoosterType.SHUFFLE, "free", 1);
//	}
//
//	public static void levelCompleted()
//	{
//		var user = getPlayerData();
//		user.CurrentLevel += 1;
//		updatePlayer(user);
//	}
//
//	public static int getCurrentLevel()
//	{
//		return getPlayerData().CurrentLevel;
//	}
//
//	public static void setCurrentLevel(int level)
//	{
//		var user = getPlayerData();
//		user.CurrentLevel = level;
//		updatePlayer(user);
//	}
}
