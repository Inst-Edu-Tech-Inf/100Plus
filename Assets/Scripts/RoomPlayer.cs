using UnityEngine;
using Mirror;
using TMPro;

public class RoomPlayer : NetworkRoomPlayer
{
    NetRoom netRoom;

    private void Awake()
    {
        netRoom = GameObject.FindGameObjectWithTag("Room Manager").GetComponent<NetRoom>();
        GameObject.FindGameObjectWithTag("Main Canvas").GetComponent<MultiplayerUI>().roomPlayer = this;
    }
}
