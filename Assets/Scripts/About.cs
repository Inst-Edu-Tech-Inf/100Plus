using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class About : MonoBehaviour
{
    [Header("Background"), SerializeField]
    public Image backgroundImage;
    public Text programisciText;
    public Text graficyText;
    public Text testerzyText;
    public Text koncepcjaText;
    public Text versionText;
    public Text tlumaczText;
    public Scrollbar scrollBar;
    public Button upArrow;
    public Button downArrow;
    public RectTransform Content;

    IEnumerator GetWWWTexture(string pathWithPrefix)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(pathWithPrefix);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Texture2D texture2D = ((DownloadHandlerTexture)www.downloadHandler).texture as Texture2D;
            Sprite fromTex = Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
            backgroundImage.sprite = fromTex;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //changeBackground();
        versionText.text = Application.version;
       /* SystemLanguage iLang = Application.systemLanguage;
        switch (iLang)
        {
            case SystemLanguage.English:
                programisciText.text = SkinManager.MENU_EN[SkinManager.PROGRAMISCI];
                graficyText.text = SkinManager.MENU_EN[SkinManager.GRAFICY];
                testerzyText.text = SkinManager.MENU_EN[SkinManager.TESTERZY];
                koncepcjaText.text = SkinManager.MENU_EN[SkinManager.KONCEPCJA_GRY];
                tlumaczText.text = SkinManager.MENU_EN[SkinManager.TLUMACZE];
                break;
            case SystemLanguage.Polish:
                programisciText.text = SkinManager.MENU_PL[SkinManager.PROGRAMISCI];
                graficyText.text = SkinManager.MENU_PL[SkinManager.GRAFICY];
                testerzyText.text = SkinManager.MENU_PL[SkinManager.TESTERZY];
                koncepcjaText.text = SkinManager.MENU_PL[SkinManager.KONCEPCJA_GRY];
                tlumaczText.text = SkinManager.MENU_PL[SkinManager.TLUMACZE];
                break;
            default:
                programisciText.text = SkinManager.MENU_EN[SkinManager.PROGRAMISCI];
                graficyText.text = SkinManager.MENU_EN[SkinManager.GRAFICY];
                testerzyText.text = SkinManager.MENU_EN[SkinManager.TESTERZY];
                koncepcjaText.text = SkinManager.MENU_EN[SkinManager.KONCEPCJA_GRY];
                tlumaczText.text = SkinManager.MENU_EN[SkinManager.TLUMACZE];
                break;
        }*/
        programisciText.text = SkinManager.instance.MenuLang[SkinManager.PROGRAMISCI];
        graficyText.text = SkinManager.instance.MenuLang[SkinManager.GRAFICY];
        testerzyText.text = SkinManager.instance.MenuLang[SkinManager.TESTERZY];
        koncepcjaText.text = SkinManager.instance.MenuLang[SkinManager.KONCEPCJA_GRY];
        tlumaczText.text = SkinManager.instance.MenuLang[SkinManager.TLUMACZE];
    }

    // Update is called once per frame
    void Update()
    {
        if(scrollBar.value * Content.sizeDelta.y <= 40)
        {
            downArrow.gameObject.SetActive(false);
            upArrow.gameObject.SetActive(true);
        }
        else if(scrollBar.value * Content.sizeDelta.y >= Content.sizeDelta.y - 40)
        {
            downArrow.gameObject.SetActive(true);
            upArrow.gameObject.SetActive(false);
        }
        else
        {
            downArrow.gameObject.SetActive(true);
            upArrow.gameObject.SetActive(true);
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    void changeBackground()
    {
//        backgroundImage.sprite = Resources.Load<Sprite>(SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name);
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            string pom2 = Application.streamingAssetsPath + "/" + SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name + ".jpg";
            StartCoroutine(GetWWWTexture(pom2));
        }
        /*iOS uses Application.dataPath + "/Raw",
Android uses files inside a compressed APK
/JAR file, "jar:file://" + Application.dataPath + "!/assets".*/
        if (Application.platform == RuntimePlatform.Android)
        {
            string pom = SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name + ".jpg";//
            pom = System.IO.Path.Combine("jar:file://" + Application.dataPath + "!/assets", pom);
            StartCoroutine(GetWWWTexture(pom));
        }
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string pom3 = SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name + ".jpg";//
            //string pom = SkinManager.instance.tla[LocalActiveBackground].Name + ".jpg";//
            pom3 = System.IO.Path.Combine(Application.dataPath + "/Raw", pom3);
            StartCoroutine(GetWWWTexture(pom3));
        }
    }
}
