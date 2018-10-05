using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayerPreInit : Photon.PunBehaviour, IPunObservable {

    bool m_isInit = false;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //if (stream)
    }

}
