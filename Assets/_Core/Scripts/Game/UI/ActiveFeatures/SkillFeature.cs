using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFeature : ActiveFeature
{
	[SerializeField]
	tk2dBaseSprite m_icon = null;

	protected BasicSkill m_skill = null;

	protected void OnDisable()
	{
		m_skill.OnStateChanged -= onStateChanged;
	}

	public override void initialize(Character character)
	{
		base.initialize(character);
		var skills = character.getSkills();
		m_skill = skills.Count > 0 ? skills[0] : null;
		m_skill.OnStateChanged += onStateChanged;
		m_icon.SetSprite(getIconName(character.getHero().type));
	}

	protected override void onFeatureActivated()
	{
		m_skill?.activate();
	}

	protected void onStateChanged(BasicSkill.State state)
	{
		switch (state)
		{
			case BasicSkill.State.ACTIVE:
				enableTouches(true);
				break;
			default:
				enableTouches(false);
				break;
		}
	}

	private string getIconName(GameData.HeroType heroType)
	{
		switch (heroType)
		{
			case GameData.HeroType.WARRIOR: return "ability_warrior";
			case GameData.HeroType.ROGUE: return "ability_rogue";
			case GameData.HeroType.MAGE: return "icon_magic_shield";//"ability_wizzard";
			default: return "ability_warrior";
		}
	}
}
