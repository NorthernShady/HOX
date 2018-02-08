using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterConfigDBHelper {

	public static BoosterConfig getBoosterConfig(string boosterName) {
		return DBProvider.instance<I_UserDBProvider>().getBoosterConfig(boosterName);
	}

//	public static BoosterConfig getBoosterConfig(MenuData.BoosterType booster) {
//		return getBoosterConfig(booster.ToString());
//	}
}
