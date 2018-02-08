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
}
