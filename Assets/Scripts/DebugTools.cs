using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DebugTools : MonoBehaviour
{
    public GameObject debugPanel;
    public TextMeshProUGUI mainText;

    private void Awake()
    {
        mainText.text = Application.productName + " v" + Application.version + "\n";
    }

    void Update()
    {
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

    public void ReloadScene()
    {
        SceneManager.LoadScene("Game");
    }
}
