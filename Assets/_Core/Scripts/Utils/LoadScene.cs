using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	[SerializeField]
	int sceneID;
	[SerializeField]
	public string levelTopologyName;
	[SerializeField]
	public string levelDataName;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onClick()
	{
		SceneManager.LoadScene (sceneID);
	}
	public void restartScene()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}
	public void loadGameLevel()
	{
		//var proxy = FindObjectOfType<LevelDataProxy> ();
		//proxy.levelTopologyName = new string[] { levelTopologyName };
		//proxy.levelDataName = new string[] { levelDataName };
		//SceneManager.LoadScene (k.Scenes.GAME_SCENE_MAIN);
	}
}
