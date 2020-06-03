using UnityEngine;
using Mirror;
using Mirror.Discovery;

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
	public NetworkDiscovery discovery;

    private void Start()
    {
		discovery = GameObject.FindGameObjectWithTag("Room Manager").GetComponent<NetworkDiscovery>();
    }

    public void CreateGame()
	{
		roomManager.StartHost();
		discovery.AdvertiseServer();
	}

	public void JoinGame()
	{
		discovery.StartDiscovery();
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
