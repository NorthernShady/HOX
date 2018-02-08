using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DBProvider : I_StoreDBProvider  {

	public InAppItem getInAppItem(int id) {
		string cmdText = "SELECT * FROM InAppItem WHERE Id = ?";

		return dataService.connection.Query<InAppItem> (cmdText, id)[0];
	}

	public List<InAppItem> getInAppItems() {
		string cmdText = "SELECT * FROM InAppItem";

		return dataService.connection.Query<InAppItem> (cmdText);
	}

	public InAppItem getInAppItemByGooglePlayId (string googlePlayInAppId) {
		string cmdText = "SELECT * FROM InAppItem WHERE GooglePlayId = ?";

		return dataService.connection.Query<InAppItem> (cmdText, googlePlayInAppId)[0];
	}

}
