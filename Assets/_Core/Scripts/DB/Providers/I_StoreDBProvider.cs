using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_StoreDBProvider {
	InAppItem getInAppItem(int id);
	List<InAppItem> getInAppItems ();
	InAppItem getInAppItemByGooglePlayId (string googlePlayInAppId);
}
