using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFeature : ActiveFeature
{
	protected BasicSkill m_skill = null;

	protected override void onFeatureActivated()
	{
		m_skill.activate();
	}
}
