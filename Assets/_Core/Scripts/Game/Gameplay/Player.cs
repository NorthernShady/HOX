using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : Photon.PunBehaviour, IPunObservable {

	private GameInputController m_gameInputController = null;
	private Hero m_hero = null;

	private bool m_shouldSendPosition = false;
	private Vector3 m_position = Vector3.zero;
	private int m_team = -1;
	private bool m_isInitialized = false;

	void Awake()
	{
		m_gameInputController = FindObjectOfType<GameInputController>();
	}

	void subscribe()
	{
		m_gameInputController.OnTap -= onTap;
		m_gameInputController.OnTap += onTap;
	}

	void OnDisable()
	{
		m_gameInputController.OnTap -= onTap;
	}

	public void initialize(Hero hero, int team)
	{
		m_hero = hero;
		m_team = team;

		if (PhotonHelper.isMine(this))
		{
			subscribe();
			gameObject.AddComponent<CameraFollower>();
		}
	}
	
	void onTap(Vector3 position)
	{
		if (PhotonHelper.isMaster()) {
			m_hero.moveTo(position);
		}
		else {
			m_shouldSendPosition = true;
			m_position = position;
		}
	}

	#region PUN callbacks

	protected void photonUpdate(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting && !PhotonHelper.isMaster()) {
			stream.SendNext(m_team);
			stream.SendNext(m_shouldSendPosition);
			stream.SendNext(m_position);
			m_shouldSendPosition = false;
			return;
		}

		if (stream.isReading && PhotonHelper.isMaster()) {
			m_team = (int)stream.ReceiveNext();
			var positionChanged = (bool)stream.ReceiveNext();
			var position = (Vector3)stream.ReceiveNext();

			if (!m_isInitialized)
			{
				Debug.Log("hero");
				findHero();
				m_isInitialized = true;
			}

			if (positionChanged)
			{
				m_hero?.moveTo(position);
			}
		}
	}

	void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
		photonUpdate(stream, info);
    }

	protected void photonInit()
	{
	}

	private void findHero()
	{
		var heroes = FindObjectsOfType<Hero>().ToList();
		m_hero = heroes.Find(x => x.team == m_team);
		transform.SetParent(m_hero.transform, false);
	}

	#endregion
}
