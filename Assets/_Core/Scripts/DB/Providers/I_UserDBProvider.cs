using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_UserDBProvider {
	User getUser ();
	XPLevel getXPLevel (int levelId);
	AnalyticsData getAnalyticsData ();
	BoosterConfig getBoosterConfig(string boosterName);
}
