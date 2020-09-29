using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetPlayer : NetworkBehaviour
{
    [SyncVar(hook = nameof(ChangeVictoryConditions))]
    int victoryConditions = 0;

    public GameManager gm;

    void ChangeVictoryConditions(int oldValue, int newValue)
    {
        PlayerPrefs.SetInt("ActiveVictoryConditions", newValue);
        GameSetting gs = FindObjectOfType<GameSetting>();

        if (gs == null)
        {
            gs = gameObject.AddComponent<GameSetting>();
        }

        gs.VictoryConditionsChange(newValue);
    }

    public override void OnStartLocalPlayer()
    {
        gm = GameObject.FindGameObjectsWithTag("Game Manager") [0].GetComponent<GameManager>();

        if (isServer)
        {
            gm.isHost = true;
        }
        else
        {
            gm.isHost = false;
            CmdGrantAuthority(gm.gameObject);
        }

        if (isServer)
        {
            victoryConditions = PlayerPrefs.GetInt("ActiveVictoryConditions");
        }
    }

    [Command]
    void CmdGrantAuthority(GameObject target)
    {
        target.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    }
}