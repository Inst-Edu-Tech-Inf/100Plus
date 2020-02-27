using UnityEngine;
using TMPro;

public class DebugTools : MonoBehaviour
{
    public GameObject debugPanel;
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI fpsText;

    private void Awake()
    {
        mainText.text = Application.productName + " v" + Application.version + "\n" + SystemInfo.operatingSystem + "\n" + SystemInfo.graphicsDeviceName;
    }

    void Update()
    {
        fpsText.text = (Mathf.Floor(1f / Time.deltaTime)).ToString() + " FPS";

        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (debugPanel.activeSelf != true)
            {
                debugPanel.SetActive(true);
            }
            else
            {
                debugPanel.SetActive(false);
            }
        }
    }
}
