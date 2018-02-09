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

	public tk2dTextMesh nameField;

	bool isFindRooms = false;

	List<LobbyRoomButton> m_roomButtons;

	// Use this for initialization
	void Awake () {
		PhotonNetwork.automaticallySyncScene = true;
		m_roomButtons = new List<LobbyRoomButton> ();
		joinButton.SetActive (false);
		createButton.SetActive (false);
		nameField.text = "";
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
//		foreach (var room in m_roomButtons) {
//			Destroy (room);
//		}
//		m_roomButtons.Clear ();
		var roomsInfo = PhotonNetwork.GetRoomList ();
		float x = -2.21f;
		float y = 1.32f;
		float dy = -1.0f;
		float dx = 3.0f;
		int index = 0;
		foreach (var roomInfo in roomsInfo) {
			index++;
			var lobbyButton = Instantiate (lobbyButtonPrefab, new Vector3(x, y, 0), Quaternion.identity);
			if (index % 3 == 0) {
				y += dy;
				x = -2.21f;
			} else {
				x += dx;
			}

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
		nameField.text = roomName;
		RoomOptions roomOptions = new RoomOptions ();
		roomOptions.MaxPlayers = 2;
		PhotonNetwork.CreateRoom (roomName, roomOptions, null);
	}

	void findRoom()
	{
		isFindRooms = true;
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
		PhotonNetwork.JoinLobby ();
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
		PhotonNetwork.Disconnect ();
		SceneManager.LoadScene(k.Scenes.HERO_PICK);
	}

	void Update()
	{
		if (isFindRooms) {
			updateRooms ();
		}
	}

	public override void OnJoinedLobby()
	{
		joinButton.SetActive (true);
		createButton.SetActive (true);
	}
}
