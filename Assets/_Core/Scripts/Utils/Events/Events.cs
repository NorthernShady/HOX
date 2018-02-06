using UnityEngine;
using System.Collections;

namespace SoundEvents
{
	public enum Group {
		SOUND,
		MUSIC
	}

	public class SoundEvent : GameEvent
	{
		public static int UNDEFINED = -1;
	}
		
	public class Play : SoundEvent
	{
		public enum PlayType
		{
			NORMAL = 0,
			LOOP = 1
		}

		public AudioIDs audioID { get; private set; }

		public int audioSourceItemID { get; private set; }

		public float volume { get; private set; }

		public PlayType playType { get; private set; }

		public Group soundGroup { get; private set; }

		public Play (AudioIDs id, int audioSourceItemID = -1, float volume = -1, PlayType playType = PlayType.NORMAL, Group soundGroup = Group.SOUND)
		{
			this.audioID = id;
			this.volume = volume;
			this.audioSourceItemID = audioSourceItemID;
			this.playType = playType;
			this.soundGroup = soundGroup;
		}
	}

	public class Stop : SoundEvent
	{
		public Stop(int audioSourceItemId) {
			this.audioSourceItemId = audioSourceItemId;
		}

		public int audioSourceItemId { get; private set; }
	}

	public class UpdateVolume : SoundEvent
	{
		public int audioSourceItemID { get; private set; }

		public float volume { get; private set; }

		public UpdateVolume(int audioSourceItemID, float volume)
		{
			this.volume = volume;
			this.audioSourceItemID = audioSourceItemID;
		}
	}

	public class UpdateGroupVolume : SoundEvent
	{
		public Group soundGroup { get; private set; }

		public float volume { get; private set; }

		public UpdateGroupVolume(Group soundGroup, float volume)
		{
			this.volume = volume;
			this.soundGroup = soundGroup;
		}
	}


	public class Fade : SoundEvent
	{
		public int audioSourceItemID { get; private set; }

		public float endVolume { get; private set; }

		public float duration { get; private set; }

		public bool stopInEnd { get; private set; }

		public Fade (int audioSourceItemID, float endVolume, float duration, bool stopInEnd)
		{
			this.endVolume = endVolume;
			this.audioSourceItemID = audioSourceItemID;
			this.duration = duration;
			this.stopInEnd = stopInEnd;
		}
	}

	public class ChangePauseState : SoundEvent
	{
		public int audioSourceItemID { get; private set; }

		public bool isPaused { get; private set; }

		public ChangePauseState (int audioSourceItemID, bool isPaused)
		{
			this.isPaused = isPaused;
			this.audioSourceItemID = audioSourceItemID;
		}
	}
}

public class SceneLoadedEvent : GameEvent
{
	public bool GameplayLevel { get; private set; }

	public SceneLoadedEvent (bool isGameplayLevel)
	{
		GameplayLevel = isGameplayLevel;
	}
}

public class SoftCurrencyChangeEvent : GameEvent
{
	public int newValue { get; private set; }

	public SoftCurrencyChangeEvent (int newCurrencyValue)
	{
		newValue = newCurrencyValue;
	}
}

public class RankPointsChangeEvent : GameEvent
{
	public int newValue { get ; private set; }

	public RankPointsChangeEvent (int newValue)
	{
		this.newValue = newValue;
	}
}

public class HardCurrencyChangeEvent : GameEvent
{
	public int newValue { get; private set; }

	public HardCurrencyChangeEvent (int newCurrencyValue)
	{
		newValue = newCurrencyValue;
	}
}

public class XPChangeEvent : GameEvent
{
	public int newValue { get; private set; }

	public XPChangeEvent (int xp)
	{
		newValue = xp;
	}
}

public class OtherElementTouched : GameEvent
{

}

public class AllGoalsCompletedAndShown : GameEvent
{

}

public class BoosterCountUpdated : GameEvent
{

}

namespace GameInputEvents
{
	public class OnClickGameField : GameEvent
	{
		public Vector3 startWorldPosition;
		public Vector3 startScreenPosition;

		public Vector3 endWorldPosition;
		public Vector3 endScreenPosition;

		public OnClickGameField (Vector3 startWorldPosition, Vector3 startScreenPosition, Vector3 endWorldPosition, Vector3 endScreenPosition)
		{
			this.startWorldPosition = startWorldPosition;
			this.startScreenPosition = startScreenPosition;

			this.endWorldPosition = endWorldPosition;
			this.endScreenPosition = endScreenPosition;
		}
	}

	public class OnClick : GameEvent
	{
		public Vector3 startWorldPosition;
		public Vector3 startScreenPosition;

		public Vector3 endWorldPosition;
		public Vector3 endScreenPosition;

		public OnClick (Vector3 startWorldPosition, Vector3 startScreenPosition, Vector3 endWorldPosition, Vector3 endScreenPosition)
		{
			this.startWorldPosition = startWorldPosition;
			this.startScreenPosition = startScreenPosition;

			this.endWorldPosition = endWorldPosition;
			this.endScreenPosition = endScreenPosition;
		}
	}

	public class OnTapBegin : GameEvent
	{
		public Vector3 worldPosition;
		public Vector3 screenPosition;

		public OnTapBegin (Vector3 worldPosition, Vector3 screenPosition)
		{
			this.worldPosition = worldPosition;
			this.screenPosition = screenPosition;
		}
	}

	public class OnTapEnd : GameEvent
	{
		public Vector3 startWorldPosition;
		public Vector3 startScreenPosition;

		public Vector3 endWorldPosition;
		public Vector3 endScreenPosition;

		public OnTapEnd (Vector3 startWorldPosition, Vector3 startScreenPosition, Vector3 endWorldPosition, Vector3 endScreenPosition)
		{
			this.startWorldPosition = startWorldPosition;
			this.startScreenPosition = startScreenPosition;

			this.endWorldPosition = endWorldPosition;
			this.endScreenPosition = endScreenPosition;
		}
	}
}

