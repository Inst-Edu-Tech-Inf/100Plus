using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [Header("DebugText"), SerializeField]
    public Text debugText;
    public GameObject iapPanel;
    public GameObject mainPanel;
    // Start is called before the first frame update
    void Start()
    {
        OpenIAP();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Succed()
    {
        debugText.text = debugText + "OK";
        SceneManager.LoadScene("Skins");
    }

    public void Fail()
    {
        debugText.text = debugText + "FAIL";
    }

    public void ShowMe()
    {
        debugText.text = SkinManager.instance.DebugToShow;
    }

    //mainPanel.SetActive (true);
		//iapPanel.SetActive (false);

    public void OpenIAP()
    {
        mainPanel.SetActive(false);
        iapPanel.SetActive(true);
    }

    public void CloseIAP()
    {
        mainPanel.SetActive(true);
        iapPanel.SetActive(false);
    }
}
