using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetRoom : NetworkRoomManager
{
    TextMeshProUGUI readyText;
    //Text adresIPText;
    int readyPlayers = 0;

    public override void Start()
    {
        readyText = GameObject.FindGameObjectWithTag("Ready Text").GetComponent<TextMeshProUGUI>();
    //    adresIPText = GameObject.FindGameObjectWithTag("AdresIP").GetComponent<Text>();
        base.OnStartHost();
    }


    private void Update()
    {
        readyPlayers = 0;
        foreach (NetworkRoomPlayer player in this.roomSlots)
        {
            if (player.readyToBegin)
                readyPlayers++;
        }
        UpdateReadyText();
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        NetworkClient.Shutdown();
        NetworkServer.Shutdown();
        SceneManager.LoadScene("Menu");
    }

    /*public override void OnServerConnect(NetworkConnection conn)
    {
        //Debug.Log("NetRoom :"+Transport.activeTransport.ServerUri().Host);
     //   adresIPText.text = Transport.activeTransport.ServerUri().Host;
        //NetworkClient.Shutdown();
        //NetworkServer.Shutdown();
        //SceneManager.LoadScene("Menu");
    }*/



    public void UpdateReadyText()
    {
        readyText.text = $"Ready {readyPlayers} / {this.roomSlots.Count}";
    }
}