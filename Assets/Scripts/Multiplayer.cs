﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using Mirror;
using System.Net;
using System.Net.Sockets;
using System;
//using Mirror.Transport;

//using Mirror.Discovery;

public class Multiplayer : MonoBehaviour
{
    [Header("Background"), SerializeField]
    public Image backgroundImage;
    public InputField input;
    public Dropdown gameConditionsList;
    public GameObject pvpPanel;
    public GameObject restartPanel;
    public GameObject aiDifficultyPanel;
    public Button readyButton;
    public Text gameModeText;
    public Button soloButton;
    public Button aiButton;
    public Button pvpButton;
    public Button startButton;
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;
    public Button impossibleButton;
    public Text sciezka;
    public Text adresIP;
    public NetworkRoomManager kopiaRoom;
    public Text restartText;
    //public Text gameModeText;
    
    public NetworkRoomManager roomManagerShowGUI;
    //public NetworkRoomPlayer roomManagerShowGUI;
    NetworkRoomPlayer roomPlayerShowGUI;
    Transport servTransport;
    //Uri actualServerUri;

   /* private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log(string.Format("The level '{0}' was loaded.", scene.name));
    }*/

    // Start is called before the first frame update
    void Start()
    {
       // SystemLanguage iLang = Application.systemLanguage;
        //changeBackground();
        //string pom2 = Application.streamingAssetsPath + "/" + SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name + ".jpg";
        //backgroundImage.sprite = Resources.Load<Sprite>(pom2);
        //StartCoroutine(GetWWWTexture("http://ieti.pl/portal/images/Autumn.jpg"));
        gameConditionsList.value = SkinManager.instance.ActivePlayerMode;
       /* switch (iLang)
        {
            case SystemLanguage.English:
                gameModeText.text = SkinManager.MENU_EN[SkinManager.TRYB_GRY];
                
                break;
            case SystemLanguage.Polish:
                gameModeText.text = SkinManager.MENU_PL[SkinManager.TRYB_GRY];
                break;
            default:
                gameModeText.text = SkinManager.MENU_EN[SkinManager.TRYB_GRY];
                break;
        }*/
        gameModeText.text = SkinManager.instance.MenuLang[SkinManager.TRYB_GRY];
        restartText.text = SkinManager.instance.MenuLang[SkinManager.RESTART_GRY];

        //readyButton.interactable = false;
        readyButton.gameObject.SetActive(false);
        adresIP.gameObject.SetActive(false);

        if (SkinManager.instance.ActivePlayerMode == GameManager.GAME_CONDITION_SOLO)
        {
            pvpPanel.SetActive(false);
            aiDifficultyPanel.SetActive(false);
            soloButton.interactable = false;
            if (!SkinManager.instance.MiddlePass)
            {
                aiButton.interactable = false;
                pvpButton.interactable = false;
            }
            else
            {
                aiButton.interactable = true;
                pvpButton.interactable = true;
            }
            startButton.gameObject.SetActive(true);
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
                
                startButton.gameObject.SetActive(true);
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
                    startButton.gameObject.SetActive(false);
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
                        startButton.gameObject.SetActive(false);
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
            sciezka.text = pom;
            StartCoroutine(GetWWWTexture(pom));
        }
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string pom3 = SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name + ".jpg";//
            //string pom = SkinManager.instance.tla[LocalActiveBackground].Name + ".jpg";//
            pom3 = System.IO.Path.Combine(Application.dataPath + "/Raw", pom3);
            sciezka.text = pom3;
            StartCoroutine(GetWWWTexture(pom3));
        }
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            string pom2 = Application.streamingAssetsPath + "/" + SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name + ".jpg";
            sciezka.text = pom2;
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
            {
                Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                // Texture2D texture2D = new Texture2D(myTexture.width, myTexture.height, TextureFormat.RGBA32, false);
                Texture2D texture2D = ((DownloadHandlerTexture)www.downloadHandler).texture as Texture2D;
                Sprite fromTex = Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
                backgroundImage.sprite = fromTex;
                //input.text = myTexture.width.ToString();
            }
        }



    }

    public void AIDifficultyEasy()
    {
        SkinManager.instance.SetAIDifficulty(SkinManager.AI_EASY);
        PlayerPrefs.SetInt("AIDifficulty", SkinManager.AI_EASY);
        easyButton.interactable = false;
        mediumButton.interactable = true;
        hardButton.interactable = true;
        impossibleButton.interactable = true;
    }

    public void AIDifficultyMedium()
    {
        SkinManager.instance.SetAIDifficulty(SkinManager.AI_MEDIUM);
        PlayerPrefs.SetInt("AIDifficulty", SkinManager.AI_MEDIUM);
        easyButton.interactable = true;
        mediumButton.interactable = false;
        hardButton.interactable = true;
        impossibleButton.interactable = true;
    }

    public void AIDifficultyHard()
    {
        SkinManager.instance.SetAIDifficulty(SkinManager.AI_HARD);
        PlayerPrefs.SetInt("AIDifficulty", SkinManager.AI_HARD);
        easyButton.interactable = true;
        mediumButton.interactable = true;
        hardButton.interactable = false;
        impossibleButton.interactable = true;
    }

    public void AIDifficultyImpossible()
    {
        SkinManager.instance.SetAIDifficulty(SkinManager.AI_IMPOSSIBLE);
        PlayerPrefs.SetInt("AIDifficulty", SkinManager.AI_IMPOSSIBLE);
        easyButton.interactable = true;
        mediumButton.interactable = true;
        hardButton.interactable = true;
        impossibleButton.interactable = false;
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
        startButton.gameObject.SetActive(true);
        //roomManagerShowGUI.showRoomGUI = false;
        Start();
    }

    public void AIGamePressed()
    {
        SkinManager.instance.SetActivePlayerMode(GameManager.GAME_CONDITION_AI);
        PlayerPrefs.SetInt("ActivePlayerMode", GameManager.GAME_CONDITION_AI);
        soloButton.interactable = true;
        aiButton.interactable = false;
        pvpButton.interactable = true;
        startButton.gameObject.SetActive(true);
        //roomManagerShowGUI.showRoomGUI = false;
        Start();
    }

    public void PVPGamePressed()
    {
        SkinManager.instance.SetActivePlayerMode(GameManager.GAME_CONDITION_PVP);
        PlayerPrefs.SetInt("ActivePlayerMode", GameManager.GAME_CONDITION_PVP);
        soloButton.interactable = true;
        aiButton.interactable = true;
        pvpButton.interactable = false;
        startButton.gameObject.SetActive(false);
        //roomManagerShowGUI.showRoomGUI = true;
        Start();
    }

    public string GetLocalIPv4()
    {
       // return Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
        return Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();//(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();

        //return Dns.GetHostEntry(Dns.GetHostName()).AddressList.First().ToString();
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene("Menu");
    }

    public void CheckMultiplayerServer()
    {
        //after solo game
        //Debug.Log("Sprawdzamy" + GameObject.FindGameObjectWithTag("Room Manager"));
        //Debug.Log("RoomManager:" + roomManagerShowGUI);
        //GameObject.FindGameObjectWithTag("Single Net Manager").GetComponent<NetworkManager>().StopHost();
       /* if (GameObject.FindGameObjectWithTag("Room Manager") == null)
        {
            if (singleNetManager == null)
            {
                singleNetManager = GameObject.FindGameObjectWithTag("Single Net Manager").GetComponent<NetworkManager>();
            }
            singleNetManager.gameObject.SetActive(true);
            singleNetManager.StartHost();
        }*/
      /*  if (GameObject.FindGameObjectWithTag("Single Net Manager") != null)
        {
            NetworkManager singleNetManager = GameObject.FindGameObjectWithTag("Single Net Manager").GetComponent<NetworkManager>();
            singleNetManager = kopiaRoom;
            singleNetManager.gameObject.SetActive(true);
            singleNetManager.StartHost();
        }*/
  /*      if (GameObject.FindGameObjectWithTag("Room Manager") == null)
        {
            GameObject roomManager = GameObject.FindGameObjectWithTag("Single Net Manager");

            if (roomManager != null) 
            {
                Destroy(roomManager);
            }
            //NetworkRoomManager netRoomManager = GameObject.FindGameObjectWithTag("Room Manager").GetComponent<NetworkRoomManager>();
            roomManagerShowGUI = kopiaRoom;
            roomManagerShowGUI.gameObject.SetActive(true);
            roomManagerShowGUI.StartHost();//cant multiple network managers on scene
            Debug.Log("netRoomManager exists" + roomManagerShowGUI);//+ netRoomManager);
        }*/

        //USUWA Single NEt Manager i tyle
        /*if (GameObject.FindGameObjectWithTag("Single Net Manager") != null)
        {
            GameObject roomManager = GameObject.FindGameObjectWithTag("Single Net Manager");

            if (roomManager != null)
            {
                Destroy(roomManager);
            }
    
        }*/
        if (GameObject.FindGameObjectWithTag("Single Net Manager") != null)
        {
            restartPanel.SetActive(true);
        }
    }

    public void ReadyButtonAvailable()
    {
        //readyButton.gameObject.SetActive(true);
        //roomPlayerShowGUI = roomManagerShowGUI.roomSlots[0]; //
        //roomPlayerShowGUI.showRoomGUI = true; 

        string pom = "localhost";
        string pom2 = "localhost";
        string hostName = System.Net.Dns.GetHostName();
        string localIP = System.Net.Dns.GetHostEntry(hostName).AddressList[0].ToString();
        IPAddress tmpAdres = System.Net.Dns.GetHostEntry(hostName).AddressList[0];
        //adresIP.text = localIP;
        //IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPAddress ipAddress2 = System.Net.Dns.GetHostEntry(hostName).AddressList[0];
        for (int i = 0; i < System.Net.Dns.GetHostEntry(hostName).AddressList.Length ; ++i)
        {
            ipAddress2 = System.Net.Dns.GetHostEntry(hostName).AddressList[i];
            //Debug.Log(ipAddress2.MapToIPv4().ToString());
            pom2 = ipAddress2.MapToIPv4().ToString();
            if ((pom2[0] == '1') && (pom2[1] == '9') && (pom2[2] == '2'))
            {
                if (pom2[pom2.Length - 3].ToString() != ".")
                {
                    if (pom2[pom2.Length - 2].ToString() != ".")
                    {
                        pom = pom2[pom2.Length - 3].ToString() + pom2[pom2.Length - 2].ToString() + pom2[pom2.Length - 1].ToString();
                    }
                    else//przedostatni kropka
                    {
                        pom = pom2[pom2.Length - 3].ToString() + pom2[pom2.Length - 1].ToString() +" ";
                    }
                }
                else//jest kropka pomijamy
                {
                    pom = pom2[pom2.Length - 2].ToString() + pom2[pom2.Length - 1].ToString() + " ";

                }
            }
            adresIP.text += ipAddress2.MapToIPv4().ToString()+"\n";
        }
        //Debug.Log(ipAddress2.MapToIPv4().ToString());
        //adresIP.text = tmpAdres.MapToIPv4().ToString();

        string IP = NetworkManager.singleton.networkAddress;
        string ipv4 = IPManager.GetIP(ADDRESSFAM.IPv4);
        //string ipv6 = IPManager.GetIP(ADDRESSFAM.IPv6);
        //IP = ipv4;

        
        byte rColor, gColor, bColor;
        int ipValue;
        if (pom == "localhost")
            pom = "123456";
        try
        {
            Debug.Log("Pom:" + pom + " " + pom.Length);
            if (pom[pom.Length - 3].ToString() != ".")
            {
                if (pom[pom.Length - 2].ToString() != ".")
                {
                    pom = pom[pom.Length - 3].ToString() + pom[pom.Length - 2].ToString() + pom[pom.Length - 1].ToString();
                }
                else//przedostatni kropka
                {
                    pom = pom[pom.Length - 3].ToString() + pom[pom.Length - 1].ToString();
                }
            }
            else//jest kropka pomijamy
            {
                pom = pom[pom.Length - 2].ToString() + pom[pom.Length - 1].ToString();

            }
            ipValue = int.Parse(pom);
            if (ipValue % 3 == 0)
                rColor = 0;
            else
                if (ipValue % 3 == 1)
                    rColor = 150;
                else
                    rColor = 255;
            if (ipValue % 4 == 0)
                gColor = 0;
            else
                if (ipValue % 4 == 1)
                    gColor = 150;
                else
                    gColor = 255;
            if ((ipValue % 5 <= 1) && ((rColor == 0) || (gColor == 0)))
                bColor = 255;
            else
                if ((ipValue % 5 <= 3) && ((rColor == 0) || (gColor == 0)))
                    bColor = 150;
                else
                    bColor = 0;

            adresIP.color = new Color32(rColor, gColor, bColor, 255);
            adresIP.gameObject.SetActive(true);
        }

        catch (Exception ex)
        {
            //blad kolorow
            Debug.Log("Błąd kolorów IP:" + ex);
        }
    }

    public void StartNotPVPGame()
    {
        SceneManager.LoadScene("Game");
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
