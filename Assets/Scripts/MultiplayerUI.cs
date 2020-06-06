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
        //gm.isHostTurn = true;
        //I'm Host
	}

	public void JoinGame()
	{
		discovery.StartDiscovery();
        //gm.isHostTurn = from synchronize
        //I'm not Host
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
