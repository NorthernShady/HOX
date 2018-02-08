using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigDBHelper {

	private static float getFloatValue(string name) {
		return DBProvider.instance<I_ConfigDBProvider> ().getFloatValue (name);
	}

	private static int getIntValue(string name) {
		return DBProvider.instance<I_ConfigDBProvider> ().getIntValue (name);
	}

	private static int[] getIntArrayValue(string name) {
		return DBProvider.instance<I_ConfigDBProvider> ().getIntArrayValue (name);
	}

	private static float[] getFloatArrayValue(string name) {
		return DBProvider.instance<I_ConfigDBProvider> ().getFloatArrayValue (name);
	}
		
	// General Config
	public static float General_HardCurrencyPriceInSoft {
		get { return getFloatValue ("General_HardCurrencyPriceInSoft"); }
	}

	public static int General_RefillLivesCost {
		get { return getIntValue ("General_RefillLivesCost"); }
	}

	public static int General_AddMovesCost {
		get { return getIntValue ("General_AddMovesCost"); }
	}

	public static int General_HardCurrencyWinReward {
		get { return getIntValue("General_HardCurrencyWinReward"); }
	}

	public static int General_HardCurrencyLastLevelWinReward {
		get { return getIntValue("General_HardCurrencyLastLevelWinReward"); }
	}

	public static float General_HardCurrencyWinRewardMultiplier {
		get { return getFloatValue("General_HardCurrencyWinRewardMultiplier"); }
	}
}
