using UnityEngine;
using Mirror;

public class MultiplayerUI : MonoBehaviour
{
	public RoomPlayer roomPlayer;
	public NetworkRoomManager roomManager
	{
		get => GameObject.FindGameObjectWithTag("Room Manager").GetComponent<NetworkRoomManager>();
		set
		{
			roomManager = value;
		}
	}

	public void CreateGame()
	{
		roomManager.StartHost();
	}

	public void JoinGame()
	{
		roomManager.StartClient();
	}

	public void Ready()
	{
		if (!roomPlayer.readyToBegin)
		{
			roomPlayer.readyToBegin = true;
			roomManager.CheckReadyToBegin();
		}
		else
		{
			roomPlayer.readyToBegin = false;
		}
	}
}
