using UnityEngine;
using Mirror;

public class RoomPlayer : NetworkRoomPlayer
{
	private void Awake()
	{
		GameObject.FindGameObjectWithTag("Main Canvas").GetComponent<MultiplayerUI>().roomPlayer = this;
	}
}
