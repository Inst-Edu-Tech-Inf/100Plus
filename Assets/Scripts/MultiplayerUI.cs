using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Mirror.Discovery;
using TMPro;
using System;

public class MultiplayerUI : NetworkBehaviour//MonoBehaviour
{
    const string BASIC = "Room Manager";
    const string COPY = "JustRoom";
	public RoomPlayer roomPlayer;
	public NetworkRoomManager roomManager
	{
        //if (GameObject.FindGameObjectWithTag("Room Manager").GetComponent<NetworkRoomManager>() != null)

		//get => GameObject.FindGameObjectWithTag("Room Manager").GetComponent<NetworkRoomManager>();
        get => GameObject.FindGameObjectWithTag(BASIC).GetComponent<NetworkRoomManager>();
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
        //Debug.Log("MultiUI :"+Transport.activeTransport.ServerUri().Host);
        //print ("URI"+Transport.activeTransport.ServerUri().Host);


	}

	public void OnServerFound(ServerResponse info)
    {
        bool czyTakiSam = false;
        
		//print("server found " + info.uri.Host +":child:"+list.content.childCount);
		if (list.content.childCount >= 1)
        {
            //print("ForEach");
			foreach (Transform go in list.content.transform)
			{
                //print("lista:"+go.GetComponentInChildren<Text>().text );
				if (go.GetComponentInChildren<Text>().text != info.uri.Host)
				{
                    //print("go:"+go.GetComponentInChildren<Text>().text + (go.GetComponentInChildren<Text>().text != info.uri.Host)+(go.GetComponentInChildren<Text>().text == info.uri.Host));
                    //print("uri:"+info.uri.Host);
					//AppendListButton(info);
				}
                if (go.GetComponentInChildren<Text>().text == info.uri.Host)
				{
					czyTakiSam = true;
				}
			}
            //print("CZY:"+czyTakiSam);
            if (!czyTakiSam){
                AppendListButton(info);
            }
		}
		else
        {
            //print("lista1:");
			AppendListButton(info);
		}
    }

	void AppendListButton(ServerResponse info)
    {
        string pom = info.uri.Host;
        byte rColor = 0;
        byte  gColor = 0;
        byte bColor = 0;
        int ipValue;       
		float btnY = list.content.transform.position.y-15f;
        try
        {
            if (pom[pom.Length - 3].ToString() != ".")
            {
                if (pom[pom.Length - 2].ToString() != ".")
                {
                    pom = pom[pom.Length - 3].ToString() + pom[pom.Length - 2].ToString() + pom[pom.Length - 1].ToString();
                }
                else//przedostatni kropka
                {
                    pom = pom[pom.Length - 3].ToString() + pom[pom.Length - 1].ToString();
                }
            }
            else//jest kropka pomijamy
            {
                pom = pom[pom.Length - 2].ToString() + pom[pom.Length - 1].ToString();

            }
            ipValue = int.Parse(pom);
            if (ipValue%3 == 0)
                rColor = 0;
            else
                if (ipValue%3 == 1)
                    rColor = 150;
                else
                    rColor = 255;
            if (ipValue%4 == 0)
                gColor = 0;
            else
                if (ipValue%4 == 1)
                    gColor = 150;
                else
                    gColor = 255;
            if ((ipValue%5 <= 1) && ((rColor == 0) || (gColor == 0)))
                bColor = 255;
            else
                if ((ipValue%5 <=3) && ((rColor == 0) || (gColor == 0)))
                    bColor = 150;
                else
                    bColor = 0;
        }
        catch (Exception ex)
        {
            //blad kolorow
            Debug.Log(ex);
        }


		if (list.content.childCount >= 1)
        {
			btnY = list.content.GetChild(list.content.childCount - 1).transform.position.y - 65f;
        }
        
		GameObject btn = Instantiate(listButton.gameObject);
		btn.GetComponentInChildren<Text>().text = info.uri.Host;
        

        btn.GetComponentInChildren<Text>().color = new Color32(rColor, gColor, bColor, 255);
		btn.GetComponent<Button>().onClick.AddListener(delegate { ListButtonOnClick(info); });
		btn.transform.SetParent(list.content, false);
		//btn.transform.position = new Vector2(list.transform.position.x, btnY);
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
       // if ( )
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
		/*if (!roomPlayer.readyToBegin)
		{
			//roomPlayer.readyToBegin = true;
            roomPlayer.readyToBegin = !roomPlayer.readyToBegin;
			roomManager.CheckReadyToBegin();
		}
		else
		{
			roomPlayer.readyToBegin = false;
		}*/
        roomPlayer.readyToBegin = true;
		roomManager.CheckReadyToBegin();
        //CmdChangeReadyState(true);
	}

   /* [Command]
    public void CmdSetReady(bool value)
    {
        roomPlayer.readyToBegin = value;
    }*/
}
