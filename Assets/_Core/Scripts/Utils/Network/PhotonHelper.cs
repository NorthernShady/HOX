using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonHelper {
    
	public static T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation, byte group = 0) where T:Object
	{
        return (PhotonNetwork.inRoom) ?
            (Object)PhotonNetwork.Instantiate(prefab.name, position, rotation, group) as T :
                                 GameObject.Instantiate(prefab, position, rotation);
			
	}

    public static GameObject InstantiateNew(string prefabName, Vector3 position, Quaternion rotation, byte group = 0)
    {
        return PhotonNetwork.Instantiate(prefabName, position, rotation, group);

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
m