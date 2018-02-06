using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialize : MonoBehaviour {

	[System.Serializable]
	public abstract class SystemInitializer : MonoBehaviour
	{
		public abstract void initialize();
	}

	[SerializeField]
	List<SystemInitializer> m_systemInitializerList = new List<SystemInitializer>();

	void Start () {
		StartCoroutine(initializeSystems());
	}

	IEnumerator initializeSystems()
	{
		yield return new WaitForSeconds (0.5f);
		m_systemInitializerList.ForEach(x => x.initialize());
		Physics.Simulate(Time.fixedDeltaTime);
		SceneManager.LoadScene(k.Scenes.MAIN_MENU);
	}
}
