using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_UserDBProvider {
	User getUser ();
	XPLevel getXPLevel (int levelId);
	CharacterNorm getCharacterNorm(int level);
	CommonConfig getCreepConfig(string creepName, int level);
	CommonConfig getHeroConfig(string heroName, int level);
	ItemConfig getItemConfig(string itemName);
}
