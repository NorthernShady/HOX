using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonHelper {

	public static T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation, byte group = 0) where T:Object
	{
		return (PhotonNetwork.connected) ?
			(Object)PhotonNetwork.Instantiate(prefab.name, position, rotation, group) as T :
			GameObject.Instantiate(prefab, position, rotation);
	}

	public static void Destroy(GameObject targetObject)
	{
		if (PhotonNetwork.connected)
			PhotonNetwork.Destroy(targetObject);
		else
			Object.Destroy(targetObject);
	}

	public static bool isMine(Photon.PunBehaviour script)
	{
		return (!PhotonNetwork.connected || script.photonView.isMine);
	}

	public static bool isMaster()
	{
		return (!PhotonNetwork.connected || PhotonNetwork.isMasterClient);
	}

	public static bool isConnected()
	{
		return PhotonNetwork.connected;
	}
}
