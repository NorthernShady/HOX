using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectingToGame : Photon.PunBehaviour {

	public string levelName = "GameScene";

	const string m_gameVersion = "0.1";

	public LobbyRoomButton lobbyButtonPrefab;

	public GameObject joinButton;
	public GameObject createButton;


	List<LobbyRoomButton> m_roomButtons;

	// Use this for initialization
	void Awake () {
		PhotonNetwork.automaticallySyncScene = true;
		m_roomButtons = new List<LobbyRoomButton> ();
		joinButton.SetActive (false);
		createButton.SetActive (false);
	}
	
	// Update is called once per frame
	void Start () {
		conncetToServer ();
	}

	void conncetToServer()
	{
		PhotonNetwork.ConnectUsingSettings (m_gameVersion);
	}

	void updateRooms()
	{
		foreach (var room in m_roomButtons) {
			Destroy (room);
		}
		m_roomButtons.Clear ();
		var roomsInfo = PhotonNetwork.GetRoomList ();
		float x = 320.0f;
		float y = 75.0f;
		float dy = 100.0f;
		foreach (var roomInfo in roomsInfo) {
			var lobbyButton = Instantiate (lobbyButtonPrefab, new Vector3(x, y, 0), Quaternion.identity);
			y += dy;
			lobbyButton.setName (roomInfo.Name);
			m_roomButtons.Add (lobbyButton);
		}

	}


	void createRoom()
	{
		GameObject.FindObjectOfType<GameDataProxy> ().team = 0;
		joinButton.SetActive (false);
		createButton.SetActive (false);
		string roomName = "name_";
		for (int i = 0; i < Random.Range (5, 9); i++) {
			roomName += Random.Range (0, 10).ToString ();
		}
		RoomOptions roomOptions = new RoomOptions ();
		roomOptions.MaxPlayers = 2;
		PhotonNetwork.CreateRoom (roomName, roomOptions, null);
	}

	void findRoom()
	{
		GameObject.FindObjectOfType<GameDataProxy> ().team = 1;
		joinButton.SetActive (false);
		createButton.SetActive (false);
		updateRooms ();
	}

	override public void OnConnectedToMaster ()
	{
		if (!PhotonNetwork.connected) {
			Debug.Log ("No connection");
			return;
		}
		joinButton.SetActive (true);
		createButton.SetActive (true);
	}

	override public void OnReceivedRoomListUpdate ()
	{
		updateRooms ();
	}

	override public void OnPhotonPlayerConnected (PhotonPlayer newPlayer)
	{
		PhotonNetwork.LoadLevel (levelName);
	}
		

	override public void OnConnectedToPhoton ()
	{
		joinButton.SetActive (true);
		createButton.SetActive (true);
	}

	public void onBackButtonClick()
	{
		SceneManager.LoadScene(k.Scenes.HERO_PICK);
	}
}
