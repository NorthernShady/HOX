using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] class HeroVisual : TypedMap<GameData.HeroType, GameObject> {}

public class Hero : Character, IPunObservable {

	[SerializeField]
	HeroVisual m_heroVisual = null;

	[SerializeField]
	GameData.HeroType m_type = GameData.HeroType.WARRIOR;

	[SerializeField]
	int m_team = 0;

	public Vector2 startPosition {
		get {
			return new Vector2(transform.position.x, transform.position.z);
		}
	}

	public GameData.HeroType type {
		get {
			return m_type;
		}
		set {
			m_type = value;
			updateVisual();
		}
	}

	public int team {
		get {
			return m_team;
		}
	}

	GameObject m_activeVisual = null;

	public void initialize(Vector2 position, int team, GameData.HeroType type = GameData.HeroType.WARRIOR)
	{
		m_type = type;
		m_team = team;
		transform.position = new Vector3(position.x, 0.0f, position.y);
		updateVisual();
	}

	public void updateVisual()
	{
		if (m_activeVisual != null)
			DestroyImmediate(m_activeVisual.gameObject);

		m_activeVisual = GameObject.Instantiate(m_heroVisual[m_type], transform, false);
	}

	#region IPunObservable implementation
	void IPunObservable.OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
	{
		if (photonView.isMine) {
			stream.SendNext (team);
		} else {
			m_team = (int)stream.ReceiveNext ();
		}
	}
	#endregion
}
