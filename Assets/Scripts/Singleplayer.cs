using UnityEngine;
using Mirror;

public class Singleplayer : MonoBehaviour
{
    public NetworkManager singleNetManager;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Room Manager") == null)
        {
            if (singleNetManager == null)
            {
                singleNetManager = GameObject.FindGameObjectWithTag("Single Net Manager").GetComponent<NetworkManager>();
            }
            singleNetManager.gameObject.SetActive(true);
            singleNetManager.StartHost();
        }
    }
}
