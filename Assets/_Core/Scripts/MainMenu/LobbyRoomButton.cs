using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyRoomButton : MonoBehaviour {

	string m_name;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setName(string name)
	{
		m_name = name;
	}

	public void onClick()
	{
		PhotonNetwork.JoinRoom (m_name);
	}
}
