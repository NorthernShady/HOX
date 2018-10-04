using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : Photon.PunBehaviour, IPunObservable {

	private GameInputController m_gameInputController = null;
	private Hero m_hero = null;
	private CameraFollower m_cameraFollower = null;
	private bool m_shouldSendPosition = false;
	private Vector3 m_position = Vector3.zero;
	private int m_team = -1;
	private bool m_isInitialized = false;

    public const int CMD_OPEN_LOOT_POPUP = 1;
    public const int CMD_UPDATE_INVENTORY = 2;

    public List<KeyValuePair<int, List<object>>> commands = new List<KeyValuePair<int, List<object>>>();

    void Awake()
	{
		m_gameInputController = FindObjectOfType<GameInputController>();
	}

	void subscribe()
	{
		m_gameInputController.OnTap -= onTap;
		m_gameInputController.OnTap += onTap;
		m_hero.OnFightStarted += onFightStarted;
		m_hero.OnFightFinished += onFightFinished;
	}

	void OnDisable()
	{
		m_gameInputController.OnTap -= onTap;
		m_hero.OnFightFinished -= onFightFinished;
		m_hero.OnFightStarted -= onFightStarted;
	}

	public void initialize(Hero hero, int team)
	{
		m_hero = hero;
		m_team = team;

		if (PhotonHelper.isMine(this))
		{
			subscribe();
			m_cameraFollower = gameObject.AddComponent<CameraFollower>();
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

	void onFightStarted()
	{
		m_cameraFollower.runCloseAnimation();
	}

	void onFightFinished()
	{
		m_cameraFollower.runDistanceAnimation();
	}

    void handleCommand(int cmd, PhotonStream stream)
    {
        switch (cmd) {
            case CMD_OPEN_LOOT_POPUP:
                var charId = (int)stream.ReceiveNext();
                m_hero.openLootPopup(charId);
                break;
            case CMD_UPDATE_INVENTORY:
                updateInventory(stream);
                break;
        }
    }

    public void addCommand(int cmd, List<object> parameters)
    {
        commands.Add(new KeyValuePair<int, List<object>>(cmd, parameters.ToList<object>()));
    }

    public void setItems(List<Item> items) 
    {
        if (PhotonHelper.isMaster()) {
            setItemToHero(items);
        } else {
            var parameters = new List<object>();
            parameters.Add(items.Count);
            foreach (var item in items) {
                parameters.Add(JsonUtility.ToJson(item));
            }
            addCommand(CMD_UPDATE_INVENTORY, parameters);
        }

    }

    void setItemToHero(List<Item> items)
    {
        m_hero.inventory.setItems(items);
    }

    void updateInventory(PhotonStream stream)
    {
        var items = new List<Item>();
        int count = (int)stream.ReceiveNext();
        for (int i = 0; i < count; i++) {
            var item = JsonUtility.FromJson<Item>((string)stream.ReceiveNext());
            items.Add(item);
        }
        setItemToHero(items);
    }

    #region PUN callbacks

    protected void photonUpdate(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting && !PhotonHelper.isMaster()) {
			stream.SendNext(m_team);
			stream.SendNext(m_shouldSendPosition);
			stream.SendNext(m_position);
            stream.SendNext(commands.Count);
            foreach (var cmd in commands) {
                stream.SendNext(cmd.Key);
                foreach (var p in cmd.Value) {
                    stream.SendNext(p);
                }
            }
            commands.Clear();


            m_shouldSendPosition = false;
			return;
		}

		if (stream.isReading && PhotonHelper.isMaster()) {
			m_team = (int)stream.ReceiveNext();
			var positionChanged = (bool)stream.ReceiveNext();
			var position = (Vector3)stream.ReceiveNext();
            var cmdsCount = (int)stream.ReceiveNext();
            for (int i = 0; i < cmdsCount; i++) {
                int cmd = (int)stream.ReceiveNext();
                handleCommand(cmd, stream);
            }

            if (!m_isInitialized)
			{
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
        m_hero.m_player = this;
	}

	#endregion
}
