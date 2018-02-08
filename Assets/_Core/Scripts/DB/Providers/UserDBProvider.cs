using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DBProvider : I_UserDBProvider  {

	public User getUser ()
	{
		return dataService.connection.Table<User> ().First();
	}

	public XPLevel getXPLevel (int levelId) {
		string cmdText = "SELECT * FROM XPLevel WHERE Id = ?";

		return dataService.connection.Query<XPLevel> (cmdText, levelId)[0];
	}

	public AnalyticsData getAnalyticsData()
	{
		return dataService.connection.Table<AnalyticsData> ().First();
	}

	public BoosterConfig getBoosterConfig(string boosterName)
	{
		string cmdText = "SELECT * FROM BoosterConfig WHERE Name = ?";

		return dataService.connection.Query<BoosterConfig> (cmdText, boosterName)[0];
	}
}
