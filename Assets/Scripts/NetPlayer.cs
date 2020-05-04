using UnityEngine;
using Mirror;

public class NetPlayer : NetworkBehaviour
{
    GameManager gm;

    private void Awake()
    {
        gm = GameObject.FindGameObjectsWithTag("Game Manager")[0].GetComponent<GameManager>();
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        print(isServer);
        if (isServer)
        {
            gm.playerID = 1;
        }
        else
        {
            gm.playerID = 2;
        }
    }
}
