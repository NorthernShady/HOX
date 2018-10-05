using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerNetworkCopy : Photon.PunBehaviour, IPunObservable
{
    public Player player = null;
    public int team = -1;

    List<KeyValuePair<int, List<object>>> commands = new List<KeyValuePair<int, List<object>>>();
    bool m_isInitialized = false;

    public void addCommands(List<KeyValuePair<int, List<object>>> cmds)
    {
        commands.AddRange(cmds.ToList());
    }

    void findPlayer()
    {
        var list = FindObjectsOfType<Player>();
        foreach (var p in list) {
            if (p.m_team == team) {
                player = p;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting) {
            stream.SendNext(team);
            stream.SendNext(commands.Count);
            foreach (var cmd in commands) {
                stream.SendNext(cmd.Key);
                foreach (var p in cmd.Value) {
                    stream.SendNext(p);
                }
            }
            commands.Clear();
            return;
        }

        if (stream.isReading) {
            team = (int)stream.ReceiveNext();
            if (!m_isInitialized) {
                findPlayer();
                m_isInitialized = true;
            }

            var cmdsCount = (int)stream.ReceiveNext();
            for (int i = 0; i < cmdsCount; i++) {
                int cmd = (int)stream.ReceiveNext();
                Debug.Log("Get command from origin PLayer, cmd: " + cmd.ToString());
                player?.handleCommand(cmd, stream);
            }


        }
    }
}
