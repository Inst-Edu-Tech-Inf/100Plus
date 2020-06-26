using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Mirror.Discovery;
using TMPro;

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
	public Button listButton;
	public ScrollRect list;
	public TextMeshProUGUI readyText;

    private void Start()
    {
		discovery = GameObject.FindGameObjectWithTag("Room Manager").GetComponent<NetworkDiscovery>();
	}

	public void OnServerFound(ServerResponse info)
    {
		print("server found " + info.uri.Host);
		if (list.content.childCount >= 1)
        {
			foreach (Transform go in list.content.transform)
			{
				if (go.GetComponentInChildren<Text>().text != info.uri.Host)
				{
					AppendListButton(info);
				}
			}
		}
		else
        {
			AppendListButton(info);
		}
    }

	void AppendListButton(ServerResponse info)
    {
		float btnY = list.content.transform.position.y;
		if (list.content.childCount >= 1)
        {
			btnY = list.content.GetChild(list.content.childCount - 1).transform.position.y - 35f;
        }
        
		GameObject btn = Instantiate(listButton.gameObject);
		btn.GetComponentInChildren<Text>().text = info.uri.Host;
		btn.GetComponent<Button>().onClick.AddListener(delegate { ListButtonOnClick(info); });
		btn.transform.SetParent(list.content, false);
		btn.transform.position = new Vector2(list.transform.position.x, btnY);
	}

	void ListButtonOnClick(ServerResponse info)
    {
		roomManager.networkAddress = info.uri.Host;
		roomManager.StartClient();
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
			//roomPlayer.readyToBegin = true;
            roomPlayer.readyToBegin = !roomPlayer.readyToBegin;
			roomManager.CheckReadyToBegin();
		}
		else
		{
			roomPlayer.readyToBegin = false;
		}
	}
}
