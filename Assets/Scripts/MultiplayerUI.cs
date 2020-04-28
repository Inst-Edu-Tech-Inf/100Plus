using UnityEngine;
using Mirror;

public class MultiplayerUI : NetworkBehaviour
{
    public NetworkRoomManager roomManager;

    public void LocalPlayerReady()
    {
        NetworkRoomPlayer localPlayer = ClientScene.localPlayer.GetComponent<NetworkRoomPlayer>();
        
        if (!localPlayer.readyToBegin)
        {
            localPlayer.readyToBegin = true;
        }
        else
        {
            localPlayer.readyToBegin = false;
        }
    }
}
