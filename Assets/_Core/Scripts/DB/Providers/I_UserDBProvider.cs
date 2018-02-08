using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_UserDBProvider {
	User getUser ();
	XPLevel getXPLevel (int levelId);
	CreepConfig getCreepConfig(string creepName, int level);
	HeroConfig getHeroConfig(string heroName, int level);
}
