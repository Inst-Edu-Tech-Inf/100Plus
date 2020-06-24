using UnityEngine;
using Mirror;
using TMPro;
using System.Collections.Generic;

public class NetRoom : NetworkRoomManager
{
    TextMeshProUGUI readyText;
    int readyPlayers = 0;

    public override void Start()
    {
        readyText = GameObject.FindGameObjectWithTag("Ready Text").GetComponent<TextMeshProUGUI>();
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

    public void UpdateReadyText()
    {
        readyText.text = $"Ready {readyPlayers} / {this.roomSlots.Count}";
    }
}