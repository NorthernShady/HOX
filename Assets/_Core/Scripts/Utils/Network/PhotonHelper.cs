using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonHelper {
    
    public static GameObject Instantiate<T>(T prefab, Vector3 position, Quaternion rotation, byte group = 0) where T:MonoBehaviour
	{
        return (PhotonNetwork.inRoom) ?
            PhotonNetwork.Instantiate(prefab.name, position, rotation, group) :
                         (GameObject.Instantiate(prefab, position, rotation)).gameObject;
			
	}

	public static void Destroy(GameObject targetObject)
	{
		if (PhotonNetwork.inRoom)
			PhotonNetwork.Destroy(targetObject);
		else
			Object.Destroy(targetObject);
	}

	public static bool isMine(Photon.PunBehaviour script)
	{
		return (!PhotonNetwork.inRoom || script.photonView.isMine);
	}

	public static bool isMaster()
	{
		return (!PhotonNetwork.inRoom || PhotonNetwork.isMasterClient);
	}

	public static bool isConnected()
	{
		return PhotonNetwork.inRoom;
	}
}
