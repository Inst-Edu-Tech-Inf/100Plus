using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Multiplayer : MonoBehaviour
{
    [Header("Background"), SerializeField]
    public Image backgroundImage;
    public InputField input;
    public Dropdown gameConditionsList;
    public GameObject pvpPanel;
    public GameObject aiDifficultyPanel;
    public Button readyButton;
    public Button soloButton;
    public Button aiButton;
    public Button pvpButton;


    // Start is called before the first frame update
    void Start()
    {
        //changeBackground();
        gameConditionsList.value = SkinManager.instance.ActivePlayerMode;
        //readyButton.interactable = false;
        readyButton.gameObject.SetActive(false);
        if (SkinManager.instance.ActivePlayerMode == GameManager.GAME_CONDITION_SOLO)
        {
            pvpPanel.SetActive(false);
            aiDifficultyPanel.SetActive(false);
            soloButton.interactable = false;
            aiButton.interactable = true;
            pvpButton.interactable = true;
        }
        else
        {
            if (SkinManager.instance.ActivePlayerMode == GameManager.GAME_CONDITION_AI)
            {
                pvpPanel.SetActive(false);
                aiDifficultyPanel.SetActive(true);
                soloButton.interactable = true;
                aiButton.interactable = false;
                pvpButton.interactable = true;
            }
            else
            {
                if (SkinManager.instance.ActivePlayerMode == GameManager.GAME_CONDITION_PVP)
                {
                    pvpPanel.SetActive(true);
                    aiDifficultyPanel.SetActive(false);
                    soloButton.interactable = true;
                    aiButton.interactable = true;
                    pvpButton.interactable = false;
                }
                else
                {
                    if (SkinManager.instance.ActivePlayerMode == GameManager.GAME_CONDITION_LEAGUE)
                    {
                        pvpPanel.SetActive(true);
                        aiDifficultyPanel.SetActive(false);
                        soloButton.interactable = true;
                        aiButton.interactable = true;
                        pvpButton.interactable = true;
                    }
                }
            }
        }
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
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            string pom2 = Application.streamingAssetsPath + "/" + SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name + ".jpg";
            StartCoroutine(GetWWWTexture(pom2));
        }
    }

    public void wgrajGrafike()
    {
        //backgroundImage.sprite =
        StartCoroutine(GetWWWTexture(input.text));
    }

    public void wstawIETI()
    {
        input.text = "http://ieti.pl/portal/images/Autumn.jpg";
    }

    public void wstawJAR()
    {
        string pom = SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name + ".jpg";//"/Background/"
        pom = System.IO.Path.Combine("jar:file://" + Application.dataPath + "!/assets", pom);
        input.text = pom;
    }

    IEnumerator GetWWWTexture(string pathWithPrefix)
    {
        /*Texture2D texture2D = null;
        WWW reader = new WWW(pathWithPrefix);
        yield return reader;
          texture2D = reader.texture;*/
        /* void Start() {
         StartCoroutine(GetTexture());
     }*/


        UnityWebRequest www = UnityWebRequestTexture.GetTexture(pathWithPrefix);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
           // Texture2D texture2D = new Texture2D(myTexture.width, myTexture.height, TextureFormat.RGBA32, false);
            Texture2D texture2D = ((DownloadHandlerTexture)www.downloadHandler).texture as Texture2D;
            Sprite fromTex = Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
            backgroundImage.sprite = fromTex;
            //input.text = myTexture.width.ToString();
        }



    }

    public void AIDifficultyEasy()
    {
        SkinManager.instance.SetAIDifficulty(SkinManager.AI_EASY);
        PlayerPrefs.SetInt("AIDifficulty", SkinManager.AI_EASY);
    }

    public void AIDifficultyHard()
    {
        SkinManager.instance.SetAIDifficulty(SkinManager.AI_IMPOSSIBLE);
        PlayerPrefs.SetInt("AIDifficulty", SkinManager.AI_IMPOSSIBLE);
    }

    public void PlayerModeChange()
    {
        SkinManager.instance.SetActivePlayerMode(gameConditionsList.value);
        PlayerPrefs.SetInt("ActivePlayerMode", gameConditionsList.value);
        Start();
    }

    public void SoloGamePressed()
    {
        SkinManager.instance.SetActivePlayerMode(GameManager.GAME_CONDITION_SOLO);
        PlayerPrefs.SetInt("ActivePlayerMode", GameManager.GAME_CONDITION_SOLO);
        soloButton.interactable = false;
        aiButton.interactable = true;
        pvpButton.interactable = true;
        Start();
    }

    public void AIGamePressed()
    {
        SkinManager.instance.SetActivePlayerMode(GameManager.GAME_CONDITION_AI);
        PlayerPrefs.SetInt("ActivePlayerMode", GameManager.GAME_CONDITION_AI);
        soloButton.interactable = true;
        aiButton.interactable = false;
        pvpButton.interactable = true;
        Start();
    }

    public void PVPGamePressed()
    {
        SkinManager.instance.SetActivePlayerMode(GameManager.GAME_CONDITION_PVP);
        PlayerPrefs.SetInt("ActivePlayerMode", GameManager.GAME_CONDITION_PVP);
        soloButton.interactable = true;
        aiButton.interactable = true;
        pvpButton.interactable = false;
        Start();
    }

    public void ReadyButtonAvailable()
    {
        //readyButton.interactable = false;//true after connection of 2nd player
        readyButton.gameObject.SetActive(true);
    }
    /*iOS uses Application.dataPath + "/Raw",
Android uses files inside a compressed APK
/JAR file, "jar:file://" + Application.dataPath + "!/assets".*/


    //yield return StartCoroutine(TestTimeRoutine("UWR Texture", iterations, TestTimeForUwrTexture));
    /*
     * IEnumerator TestTimeRoutine(string testTitle, int iterations, System.Func<IEnumerator> innerRoutine)
{
    System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
    watch.Start();
    for (int i = 0; i < iterations; i++)
    {
        yield return StartCoroutine(innerRoutine());
    }
    watch.Stop();
 
    Debug.LogFormat("{0} - iterations: {1} - total time: {2} ms - average time: {3} ms",
        testTitle, iterations, watch.ElapsedMilliseconds, watch.ElapsedMilliseconds / iterations);
}*/
    //////


    //#if UNITY_ANDROID
    //   if(Application.platform == RuntimePlatform.Android)
    // {
    /* Do Stuff */
    /*      string pom = SkinManager.instance.tla[LocalActiveBackground].Name + ".jpg";//"/Background/"
          pom = System.IO.Path.Combine("jar:file://" + Application.dataPath + "!/assets", pom);// SkinManager.instance.tla[LocalActiveBackground].Name + ".jpg";//"/Background/"
          //GetUwrTexture(pom);
          GetWWWTexture(pom);
          Debug.Log("Button Pressed in Android");
      }*/
    //#endif

    //#if UNITY_EDITOR
    /*    if (Application.platform == RuntimePlatform.WindowsEditor)
        {
           // rawImageA.GetComponent<VideoPlayer>().url = System.IO.Path.Combine(Application.streamingAssetsPath, SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + Kolor + ".mp4");
            string pom2 = System.IO.Path.Combine(Application.streamingAssetsPath, SkinManager.instance.tla[LocalActiveFrame].Name + ".jpg");
            backgroundImage.sprite = Resources.Load<Sprite>(pom2);
            //Debug.Log(backgroundImage.sprite);
            Debug.Log(pom2);
        }*/
    //#endif

    /*       //backgroundImage.sprite = Resources.Load<Sprite>(System.IO.Path.Combine(Application.streamingAssetsPath,"Background/" + SkinManager.instance.tla[LocalActiveBackground].Name) + ".jpg");//.Name

    byte[] pngBytes = System.IO.File.ReadAllBytes(pom);

    //Creates texture and loads byte array data to create image
    Texture2D tex = new Texture2D(2, 2);
    tex.LoadImage(pngBytes);

    //Creates a new Sprite based on the Texture2D
    Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

    //Assigns the UI sprite
        
    backgroundImage.sprite = fromTex;*/
    //rawImageA.GetComponent<VideoPlayer>().url = System.IO.Path.Combine(Application.streamingAssetsPath, SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + Kolor + ".mp4");
    //Debug.Log(backgroundImage.sprite);
}
