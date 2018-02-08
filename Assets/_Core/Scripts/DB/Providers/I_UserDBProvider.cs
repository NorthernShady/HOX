using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_UserDBProvider {
	User getUser ();
	XPLevel getXPLevel (int levelId);
	CreepsConfig getCreepsConfig(string creepName, int level);
	HeroesConfig getHeroesConfig(string heroName, int level);
}
