using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSkill : MonoBehaviour
{
    public System.Action OnActivated;
    public System.Action OnDeactivated;
    public System.Action<State> OnStateChanged;

    public enum State
	{
		ABSENT,
		LOCKED,
		COOLDOWN,
        AVAILABLE,
		ACTIVE
	}

    [SerializeField]
    protected GameData.SkillType m_type = GameData.SkillType.NONE;

    protected State m_state = State.AVAILABLE;

    public virtual void activate()
    {
    }

    public virtual void deactivate()
    {
    }

    public virtual CommonTraits getTraits()
    {
        return new CommonTraits();
    }

    public virtual void enhanceTraits(CommonTraits traits)
    {
    }

    protected void setState(State state)
    {
        m_state = state;
        OnStateChanged?.Invoke(state);
    }
}

[System.Serializable]
public class Skills
{
    public List<BasicSkill> skills = new List<BasicSkill>();
}
