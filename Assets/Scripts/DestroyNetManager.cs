using UnityEngine;

public class DestroyNetManager : MonoBehaviour
{
    private void Start()
    {
        GameObject roomManager = GameObject.FindGameObjectWithTag("Room Manager");

        if (roomManager != null)
        {
            Destroy(roomManager);
        }
    }
}
