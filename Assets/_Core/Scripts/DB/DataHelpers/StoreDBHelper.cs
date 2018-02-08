using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreDBHelper {

	public static InAppItem getInAppItem(int id) {
		return DBProvider.instance<I_StoreDBProvider>().getInAppItem(id);
	}

	public static List<InAppItem> getInAppItems() {
		return DBProvider.instance<I_StoreDBProvider>().getInAppItems();
	}

	public static InAppItem getInAppItemByGooglePlayId(string googlePlayInAppId) {
		return DBProvider.instance<I_StoreDBProvider>().getInAppItemByGooglePlayId(googlePlayInAppId);
	}
}
