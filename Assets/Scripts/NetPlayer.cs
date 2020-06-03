using UnityEngine;
using Mirror;
using TMPro;
using System;

public class NetPlayer : NetworkBehaviour
{
    GameManager gm;

    public override void OnStartLocalPlayer()
    {
        gm = GameObject.FindGameObjectsWithTag("Game Manager")[0].GetComponent<GameManager>();

        if (isServer)
        {
            gm.isHost = true;
        }
        else
        {
            gm.isHost = false;
            CmdGrantAuthority(gm.gameObject);
        }
    }

    [Command]
    void CmdGrantAuthority(GameObject target)
    {
        target.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    }
}
