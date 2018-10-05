using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfEnhancement : BasicSkill
{
	[SerializeField]
	GameObject m_visualEffectPrefab = null;
	[SerializeField]
	float m_cooldown = 10.0f;
	[SerializeField]
	float m_duration = 5.0f;

	CommonTraits m_traits = new CommonTraits();
	GameObject m_visualEffect = null;

	protected void Awake()
	{
		initializeInternal();
	}

	public override void activate()
    {
		setState(State.ACTIVE);
		m_visualEffect = GameObject.Instantiate(m_visualEffectPrefab, Vector3.zero, Quaternion.identity);
		m_visualEffect.transform.SetParent(transform, false);
    }

    public override void deactivate()
    {
		setState(State.COOLDOWN);
		Destroy(m_visualEffect);
		StartCoroutine(workingTime());
		StartCoroutine(cooldown());
    }

    public override CommonTraits getTraits()
    {
        return new CommonTraits();
    }

    public override void enhanceTraits(CommonTraits traits)
    {
		if (m_state == State.ACTIVE)
			traits.add(m_traits);
    }

	private void initializeInternal()
	{
		switch (m_type)
		{
			case GameData.SkillType.RAGE:
				m_traits[TraitsType.ATTACK_PERCENT] = 0.3f;
				break;
			case GameData.SkillType.SHIELD:
				m_traits[TraitsType.DEFENCE_PERCENT] = 0.3f;
				break;
			case GameData.SkillType.SPEED_UP:
				m_traits[TraitsType.MOVE_SPEED_PERCENT] = 0.3f;
				break;
			default:
				break;
		}
	}

	private IEnumerator workingTime()
	{
		yield return new WaitForSeconds(m_duration);
		deactivate();
	}

	private IEnumerator cooldown()
	{
		yield return new WaitForSeconds(m_cooldown);
		setState(State.AVAILABLE);
	}
}
