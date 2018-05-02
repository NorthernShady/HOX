using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_UserDBProvider {
	User getUser ();
	XPLevel getXPLevel (int levelId);
	CommonConfig getCreepConfig(string creepName, int level);
	CommonConfig getHeroConfig(string heroName, int level);
	CommonConfig getItemConfig(string itemName);
}
