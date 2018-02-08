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
//	public static float General_HardCurrencyPriceInSoft {
//		get { return getFloatValue ("General_HardCurrencyPriceInSoft"); }
//	}
}
