using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
//using System;
//using System.Data;
//using MySql.Data;
//using MySql.Data.MySqlClient;

public class Liga : MonoBehaviour
{
    [Header("Background"), SerializeField]
    public Image backgroundImage;
    public Text programisciText;
    public Text graficyText;
    public Text testerzyText;
    public Text koncepcjaText;
    public Text szkolaText;

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
        SystemLanguage iLang = Application.systemLanguage;
        switch (iLang)
        {
            case SystemLanguage.English:
                programisciText.text = SkinManager.MENU_EN[SkinManager.PROGRAMISCI];
                graficyText.text = SkinManager.MENU_EN[SkinManager.GRAFICY];
                testerzyText.text = SkinManager.MENU_EN[SkinManager.TESTERZY];
                koncepcjaText.text = SkinManager.MENU_EN[SkinManager.KONCEPCJA_GRY];

                break;
            case SystemLanguage.Polish:
                programisciText.text = SkinManager.MENU_PL[SkinManager.PROGRAMISCI];
                graficyText.text = SkinManager.MENU_PL[SkinManager.GRAFICY];
                testerzyText.text = SkinManager.MENU_PL[SkinManager.TESTERZY];
                koncepcjaText.text = SkinManager.MENU_PL[SkinManager.KONCEPCJA_GRY];
                break;
            default:
                programisciText.text = SkinManager.MENU_EN[SkinManager.PROGRAMISCI];
                graficyText.text = SkinManager.MENU_EN[SkinManager.GRAFICY];
                testerzyText.text = SkinManager.MENU_EN[SkinManager.TESTERZY];
                koncepcjaText.text = SkinManager.MENU_EN[SkinManager.KONCEPCJA_GRY];
                break;
        }

        
     /*
         string connStr = "server=localhost;user=root;database=world;port=3306;password=******";
         MySqlConnection conn = new MySqlConnection(connStr);
         try
         {
             Console.WriteLine("Connecting to MySQL...");
             conn.Open();

             string sql = "SELECT COUNT(*) FROM Country";
             MySqlCommand cmd = new MySqlCommand(sql, conn);
             object result = cmd.ExecuteScalar();
             if (result != null)
             {
                 int r = Convert.ToInt32(result);
                 Console.WriteLine("Number of countries in the world database is: " + r);
             }

         }
         catch (Exception ex)
         {
             Console.WriteLine(ex.ToString());
         }

         conn.Close();
         Console.WriteLine("Done.");*/
     
    }

    // Update is called once per frame
    void Update()
    {

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
