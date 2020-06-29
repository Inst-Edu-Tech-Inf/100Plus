//#define HTML5

using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using Mirror;
using UnityEngine.PlayerLoop;
using Mirror.Websocket;

public class GameManager : NetworkBehaviour
{
/*UNITY_IOS	
UNITY_ANDROID
UNITY_STANDALONE_WIN
<Project Path>/Assets/mcs.rsp*/

/*#if UNITY_EDITOR
         string adUnitId = "unused";
#elif UNITY_ANDROID
         string adUnitId = "ca-app-pub-6616097464062************";
#elif UNITY_IPHONE
             string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
#else
    string adUnitId = "unexpected_platform";
#endif*/

    public const int GAME_CONDITION_SOLO = 0;
    public const int GAME_CONDITION_AI = 1;
    public const int GAME_CONDITION_PVP = 2;
    public const int GAME_CONDITION_LEAGUE = 3;
    public const int KARTA_STATYCZNA = 1;
    public const int KARTA_DYNAMICZNA = 2;
    public const int KARTA_RAMKA = 3;
    public const int BACKGROUND_STATIC = 4;
    public const int BACKGROUND_DYNAMIC = 5;
    public const int SOUND_BACKGROUND = 6;
    public const int SFX = 7;
    public const string RED_TEXT = "Red";
    public const string GREEN_TEXT = "Green";
    public const string BLUE_TEXT = "Blue";
    public const string VICTORY = "VICTORY";
    public const string DEFEAT = "DEFEAT";
    public const int COLOR_NUMBER = 3;
    public const float COINS_SPEED = 250.0f;
    public const float COINS_RANGE_SQUARED = 35.0f * 35.0f;
    public string[] COLORS_ARRAY = new string[] { RED_TEXT , GREEN_TEXT, BLUE_TEXT };
    public const int AI_IDLE = 0;
    public const int AI_END_TURN = 1;
    public const int AI_SET_ACTIVE_CARD = 2;
    public const int AI_SHOW_POINTS_RED = 3;
    public const int AI_DISCARD_TASK = 4;
    public const int AI_DISCARD_AI_PLAYER_CARD_RED = 5;
    public const int AI_DISCARD_AI_POWERUP_CARD = 6;
    public const int AI_PLAY_AI_PLAYER_CARD = 7;
    public const int AI_PLAY_AI_POWERUP_CARD = 8;
    public const int AI_SHOW_POINTS_GREEN = 9;
    public const int AI_SHOW_POINTS_BLUE = 10;
    public const int AI_DISCARD_AI_PLAYER_CARD_GREEN = 11;
    public const int AI_DISCARD_AI_PLAYER_CARD_BLUE = 12;
    public const int AI_COLLECT_POINTS = 13;
    public const int AI_SHOW_POWERUP = 14;
    public const int PVP_IDLE = 0;
    public const int PVP_SET_ACTIVE_CARD = 1;
    public const int PVP_SHOW_PLAYER_CARD = 2;
    public const int PVP_DRAW_TASK = 3;
    public const int PVP_DISCARD_TASK = 4;
    public const int PVP_SHOW_POWERUP_CARD = 5;
    public const int PVP_COLLECT_POINTS = 6;
    public const int PVP_RED = 1;
    public const int PVP_GREEN = 2;
    public const int PVP_BLUE = 3;
    const float ACHIEVEMENT_PANEL_SMALL_SCALE = 0.5f;
    const float RESULT_PENALTY = 0.51f;

    
    public GameObject activeCardSpace;
    public GameObject collectPointsBtn;
    public GameObject trashArea;
    public GameObject endTurnBtn;
    public GameObject tasks;
    public GameObject hands;
    public GameObject handsP2;
    public GameObject powerUps;
    public GameObject playerCardPrefab;
    public GameObject taskCardPrefab;
    public GameObject powerUpCardPrefab;
    public GameObject coinsPrefab;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timerPlayerTurnText;
    public TextMeshProUGUI victoryPoints;
    public TextMeshProUGUI victoryPointsP2;
    public GameObject card;
    public GameObject cardPowerUp;
    public GameObject coin;
    public Slider slider;
    public GameObject rerollPanel;
    public Text rerollCostText;
    public Image backgroundImage;
    public AudioSource soundBackground;
    public AudioSource endTurnSFX;
    public AudioSource exitSFX;
    public AudioSource coinSingleSFX;
    public AudioSource coinFewSFX;
    public AudioSource coinMoreSFX;
    public AudioSource coinManySFX;
    public AudioSource coinMissSFX;
    public AudioSource trashSFX;
    public AudioSource taskResignSFX;
    public AudioSource victorySFX;
    public Text victoryText;
    public Text defeatVictoryText;
    public Text victoryScoreText;
    public Text defeatVictoryScoreText;
    public Text victoryConditionsText;
    public Text defeatVictoryConditionsText;
    public GameObject victoryPanel;
    public GameObject defeatVictoryPanel;
    public GameObject achievementPanel;
    public TextMeshProUGUI ActualVictoryPointsText;
    public Text achievementNameText;
    public Text achievementPointsText;
    public Image helpTask;
    public Image helpTurn;
    public Image helpCard;
    public Image helpEndTask;
    public Image helpTrash;
    //public TextMeshProUGUI closeText;
    public Button closeText;
    public Image timerImage;
    public Image timerPlayerTurnImage;
    public Image scoreImage;
    public GameObject transparentPlayerCardPanel;
    public GameObject transparentPowerUpCardPanel;
    public GameObject closeConfirmationPanel;
    public Button transparentButton;
    public Image iaTurnImage;
    public Image p2TurnImage;
    public GameObject transparentAllPanel;
    public Text infoText;

    List<GameObject> playerCards = new List<GameObject>();
    //List<GameObject> playerAICardsToRemove = new List<GameObject>();
    List<GameObject> playerAICardsRed = new List<GameObject>();
    List<GameObject> playerAICardsGreen = new List<GameObject>();
    List<GameObject> playerAICardsBlue = new List<GameObject>();
    List<int> playerAICardsRedToRemove = new List<int>();
    List<int> playerAICardsGreenToRemove = new List<int>();
    List<int> playerAICardsBlueToRemove = new List<int>();
    List<int> powerUpAICardsToRemove = new List<int>();
    int taskCardAIToRemove = 0;
    List<GameObject> taskCards = new List<GameObject>();
    public List<GameObject> powerUpCards = new List<GameObject>();
    public List<GameObject> powerUpAICards = new List<GameObject>();
    List<GameObject> coins = new List<GameObject>();
    List<int> aiCommands = new List<int>();

    [Header("Game Settings")]
    public int maxPlayerCards;
    public int maxPlayerCardsAddMidle;
    public int maxPlayerCardsAddLate;
    public int maxTaskCards;
    public int maxTaskCardsAddMiddle;
    public int maxTaskCardsAddLate;  
    public int maxPowerUpCards;
    public int maxPowerUpCardsAddLate;
    public int playerCardsOnStart;
    public int taskCardsOnStart;
    public int powerUpCardsOnStart;
    public int playerCardsToDraw;
    public int taskCardsToDraw;
    public int powerUpCardsToDraw;
    public float maxGameTimeInSeconds;
    public float maxPlayerTurnInSeconds;
    public int earlyGamePoint;
    public int middleGamePoint;
    public int lateGamePoint;
    public int earlyGamePlayerCardMax;
    public int middleGamePlayerCardMax;
    public int lateGamePlayerCardMax;
    public int earlyGameTaskCardMax;
    public int middleGameTaskCardMax;
    public int lateGameTaskCardMax;
    public int earlyChanceOnMiddle;//%
    public int earlyChanceOnLate;//%
    public int middleChanceOnLate;//%
    public GameObject activeCard;
    public float userActivityTime = 0.0f;
    public float AIActivityTime = 0.0f;
    

    public Texture2D wybranaRamka;
    public Sprite wybranyBlack;
    public Sprite wybranyRed;
    public Sprite wybranyGreen;
    public Sprite wybranyBlue;
    public Sprite wybranyRedStatic;
    public Sprite wybranyGreenStatic;
    public Sprite wybranyBlueStatic;
    public VideoClip wybranyClipRed;
    public VideoClip wybranyClipGreen;
    public VideoClip wybranyClipBlue;
    public GameObject coinGlobal;
    public bool isHost = true;
    //public bool isHostTurn = true;

    //then (int)ptr displays the memory address and *ptr displays the value at that memory address

    bool isVictoryPointFirst = false;
    bool isVictoryTimePass = true;
    bool isVictory = false;
    bool isVictoryResult = true;
    bool isVictorySound = true;
    int VictoryPointFirstValue = 20;
    float remainingGameTime = 300;
    float playerTurnTime = 0;
    float victoryPanelScale = 0.5f;
    float achievementPanelScale = ACHIEVEMENT_PANEL_SMALL_SCALE;
    float timeFromStart = 0.0f;
    bool isPureGame = true;
    int maxActualTaskCards;
    //int maxActualPlayerCards;
    //int maxActualPowerUpCards;
    int actualTaskCardsCount = 0;
    //int actualPlayerCardsCount = 0;
    //int actualPowerUpCardsCount = 0;
    Color redColor = new Color32(SkinManager.RED_COLOR, 0, 0, 255);
    Color greenColor = new Color32( 0, SkinManager.GREEN_COLOR, 0, 255);
    Color blueColor = new Color32(0, 0, SkinManager.BLUE_COLOR, 255);
    bool isPVPCommandChange = false;
    bool isFirstTurn = false;

    [SyncVar(hook = nameof(_SetVictoryPointsP1))]
    float victoryPointsNumberP1=0;
    [SyncVar(hook = nameof(_SetVictoryPointsP2))]
    float victoryPointsNumberP2;
    [SyncVar(hook = nameof(_SetIsHostTurn))]
    bool isHostTurn = true;
    [SyncVar(hook = nameof(_SetPVPValue1))]
    int PVPValue1 = 0;
    [SyncVar(hook = nameof(_SetPVPValue2))]
    int PVPValue2 = 0;
    [SyncVar(hook = nameof(_SetPVPValue3))]
    int PVPValue3 = 0;
    [SyncVar(hook = nameof(_SetPVPCommand))]
    int PVPCommand = 0;
   

    void _SetPVPValue1(int oldValue, int newValue)
    {
        SetPVPValue1(newValue);
    }

    public void SetPVPValue1(int value)
    {
        if (isHost)
        {
            PVPValue1 = value;
        }
        else//assignAuthorityObj.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
        {
            CmdSetPVPValue1(value);
        }
    }

    [Command]
    void CmdSetPVPValue1(int value)
    {
        PVPValue1 = value;
    }

    public int GetPVPValue1()
    {
        return PVPValue1;
    }

    //pvpValue2
    void _SetPVPValue2(int oldValue, int newValue)
    {
        SetPVPValue2(newValue);
    }

    public void SetPVPValue2(int value)
    {
        if (isHost)
        {
            PVPValue2 = value;
        }
        else//assignAuthorityObj.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
        {
            CmdSetPVPValue2(value);
        }
    }

    [Command]
    void CmdSetPVPValue2(int value)
    {
        PVPValue2 = value;
    }

    public int GetPVPValue2()
    {
        return PVPValue2;
    }

    //pvpValue3
    void _SetPVPValue3(int oldValue, int newValue)
    {
        SetPVPValue3(newValue);
    }

    public void SetPVPValue3(int value)
    {
        if (isHost)
        {
            PVPValue3 = value;
        }
        else//assignAuthorityObj.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
        {
            CmdSetPVPValue3(value);
        }
    }

    [Command]
    void CmdSetPVPValue3(int value)
    {
        PVPValue3 = value;
    }

    public int GetPVPValue3()
    {
        return PVPValue3;
    }

    //pvpCommand
    void _SetPVPCommand(int oldValue, int newValue)
    {
        SetPVPCommand(newValue);
    }
    /*
   PVP_IDLE = 0;
   public const int PVP_SET_ACTIVE_CARD = 1;
   public const int PVP_SHOW_PLAYER_CARD = 2;
   public const int PVP_DRAW_TASK = 3;
   public const int PVP_DISCARD_TASK = 4;
   public const int PVP_SHOW_POWERUP_CARD = 5;
   public const int PVP_COLLECT_POINTS = 6;
   */
    public void SetPVPCommand(int value)
    {
        //Debug.Log("RunPVPCommand:"+isHost);
            if (isHost)
            {
                PVPCommand = value;
                isPVPCommandChange = true;
                if (((isHost) && (!GetIsHostTurn())) || ((!isHost) && (GetIsHostTurn())))
                {
                    if (value != PVP_IDLE)
                    {
                        //Debug.Log("RunPVPCommand:IN");
                        //Debug.Log("PVPCommand:before:" + GetPVPCommand());
                        RunPVPCommand();
                        //Debug.Log("PVPCommand:before:" + GetPVPCommand());
                        //CmdSetPVPCommand(value);
                    }
                }
            }
            else//assignAuthorityObj.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
            {
                //Debug.Log("Run not Host");
                //Debug.Log("PVPCommand:before:" + GetPVPCommand());
                CmdSetPVPCommand(value);
                isPVPCommandChange = true;
                //Debug.Log("PVPCommand:after:" + GetPVPCommand());
                //RunPVPCommand();
            }
        
    }

    [Command]
    void CmdSetPVPCommand(int value)
    {
       // PVPCommand = value;
        //Debug.Log("RunPVPCommand:CMD");
        PVPCommand = value;
        //if (((isHost) && (!GetIsHostTurn())) || ((!isHost) && (GetIsHostTurn())))
        if  ((!isHost) && (GetIsHostTurn()))
        {
            if (value != PVP_IDLE)
            {
                Debug.Log("RunPVPCommand:CMD_IN");
                RunPVPCommand();
            }
        }
    }

    public int GetPVPCommand()
    {
        return PVPCommand;
    }

    public void RunPVPCommand()
    {
        string kolor = RED_TEXT;
       // infoText.text = infoText.text + ";C" + GetPVPCommand();
       // Debug.Log("RunPVPCommand:IN");
        switch (GetPVPCommand())
        {
            case PVP_DRAW_TASK:
                if (GetPVPValue1() == PVP_RED)
                {
                    kolor = RED_TEXT;
                }
                else
                    if (GetPVPValue1() == PVP_GREEN)
                    {
                        kolor = GREEN_TEXT;
                    }
                    else
                        if (GetPVPValue1() == PVP_BLUE)
                        {
                            kolor = BLUE_TEXT;
                        }
                //infoText.text = infoText.text + ";" + kolor;
                //infoText.text = infoText.text + ";V" + GetPVPValue2();
                DrawTaskCard(kolor, GetPVPValue2());
                RerollTaskCardCheck();
                CheckCardNumbers(true);
                break;
            case PVP_DISCARD_TASK:
                //infoText.text = infoText.text + ";D" + GetPVPValue1();
                DiscardTaskCard(taskCards[GetPVPValue1()]);
                RerollTaskCardCheck();
                CheckCardNumbers(true);
                break;
            case PVP_SET_ACTIVE_CARD:

                break;
            case PVP_SHOW_PLAYER_CARD:

                break;
            case PVP_SHOW_POWERUP_CARD:

                break;
            case PVP_COLLECT_POINTS:

                break;
            // default:
            //    cout << "didn't get card \n";
        }
        SetPVPCommand(PVP_IDLE);
    }

    void SetMultiplayerGameModeClient()
    {
        //VictoryConditionsChange//synchronize
       /* SkinManager.instance.SetActiveVictoryConditions(victoryList.value);

        PlayerPrefs.SetInt("ActiveVictoryConditions", victoryList.value);
        if (victoryList.value == 0)
        {
            SkinManager.instance.SetIsVictoryTimePass(true);
            SkinManager.instance.SetIsVictoryPointFirst(false);
            SkinManager.instance.SetVictoryTimePassValue(5 * 60);
            SkinManager.instance.SetVictoryPointFirstValue(0);
            PlayerPrefs.SetInt("IsVictoryTimePass", true ? 1 : 0);
            PlayerPrefs.SetInt("IsVictoryPointFirst", false ? 1 : 0);
            PlayerPrefs.SetInt("VictoryTimePass", 5 * 60);
            PlayerPrefs.SetInt("VictoryPointFirst", 0);
        }
        else
        {
            if (victoryList.value == 1)
            {
                SkinManager.instance.SetIsVictoryTimePass(true);
                SkinManager.instance.SetIsVictoryPointFirst(false);
                SkinManager.instance.SetVictoryTimePassValue(15 * 60);
                SkinManager.instance.SetVictoryPointFirstValue(0);
                PlayerPrefs.SetInt("IsVictoryTimePass", true ? 1 : 0);
                PlayerPrefs.SetInt("IsVictoryPointFirst", false ? 1 : 0);
                PlayerPrefs.SetInt("VictoryTimePass", 15 * 60);
                PlayerPrefs.SetInt("VictoryPointFirst", 0);
            }
            else
            {
                if (victoryList.value == 2)
                {
                    SkinManager.instance.SetIsVictoryTimePass(true);
                    SkinManager.instance.SetIsVictoryPointFirst(false);
                    SkinManager.instance.SetVictoryTimePassValue(30 * 60);
                    SkinManager.instance.SetVictoryPointFirstValue(0);
                    PlayerPrefs.SetInt("IsVictoryTimePass", true ? 1 : 0);
                    PlayerPrefs.SetInt("IsVictoryPointFirst", false ? 1 : 0);
                    PlayerPrefs.SetInt("VictoryTimePass", 30 * 60);
                    PlayerPrefs.SetInt("VictoryPointFirst", 0);
                }
                else
                {
                    if (victoryList.value == 3)//points
                    {
                        SkinManager.instance.SetIsVictoryTimePass(false);
                        SkinManager.instance.SetIsVictoryPointFirst(true);
                        SkinManager.instance.SetVictoryTimePassValue(0);
                        SkinManager.instance.SetVictoryPointFirstValue(20);
                        PlayerPrefs.SetInt("IsVictoryTimePass", false ? 1 : 0);
                        PlayerPrefs.SetInt("IsVictoryPointFirst", true ? 1 : 0);
                        PlayerPrefs.SetInt("VictoryTimePass", 0);
                        PlayerPrefs.SetInt("VictoryPointFirst", 20);
                    }
                    else
                    {
                        SkinManager.instance.SetIsVictoryTimePass(false);
                        SkinManager.instance.SetIsVictoryPointFirst(true);
                        SkinManager.instance.SetVictoryTimePassValue(0);
                        SkinManager.instance.SetVictoryPointFirstValue(100);
                        PlayerPrefs.SetInt("IsVictoryTimePass", false ? 1 : 0);
                        PlayerPrefs.SetInt("IsVictoryPointFirst", true ? 1 : 0);
                        PlayerPrefs.SetInt("VictoryTimePass", 0);
                        PlayerPrefs.SetInt("VictoryPointFirst", 100);
                    }
                }
            }
        }//victoryList.value==0
        */
        //PlayerTurnConditionsChange//synchronize
        /*
        SkinManager.instance.SetActivePlayerTurnConditions(playerTurnList.value);
        PlayerPrefs.SetInt("ActivePlayerTurnConditions", playerTurnList.value);
        SkinManager.instance.SetActivePlayerEndTime(SkinManager.PLAYER_TURN[playerTurnList.value]);
        PlayerPrefs.SetInt("ActivePlayerEndTime", SkinManager.PLAYER_TURN[playerTurnList.value]);
         */
       // PlayerModeChange()
        SkinManager.instance.SetActivePlayerMode(GAME_CONDITION_PVP);
        PlayerPrefs.SetInt("ActivePlayerMode", GAME_CONDITION_PVP);
    }

    public bool GetIsHostTurn()
    {
        return isHostTurn;
    }

    void _SetIsHostTurn(bool oldValue, bool newValue)
    {
        if (((isHost) && (newValue)) || ((!isHost) && (!newValue)))
        {
            if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_SOLO)
            {

            }
            else
            {
                endTurnBtn.gameObject.SetActive(true);
                iaTurnImage.gameObject.SetActive(false);
                p2TurnImage.gameObject.SetActive(false);
                transparentAllPanel.gameObject.SetActive(false);
                RerollTaskCardCheck();
                CheckCardNumbers(true);
            }
        }
        else
        {
            transparentAllPanel.gameObject.SetActive(true);
        }
    }

    public void SetIsHostTurn(bool value)
    {
        if (isHost)
        {
            isHostTurn = value;
        }
        else//assignAuthorityObj.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
        {
            CmdSetIsHostTurn(value);
        }
        //print("HostTurn:" + value);
    }

    [Command]
    void CmdSetIsHostTurn(bool value)
    {
        isHostTurn = value;
    }

    public float GetVictoryPoints()
    {
        //print("Get");

        if (isHost)

            if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_AI)
            {
                if (GetIsHostTurn())
                {
                    return victoryPointsNumberP1;
                }
                else
                {
                    return victoryPointsNumberP2;
                }
            }
            else
            {
                return victoryPointsNumberP1;
            }
        else
            return victoryPointsNumberP2;
    }

    void _SetVictoryPointsP1(float oldValue, float newValue)
    {
        SetVictoryPointsTextP1(newValue);
    }

    void _SetVictoryPointsP2(float oldValue, float newValue)
    {
        SetVictoryPointsTextP2(newValue);
    }

    public void SetVictoryPoints(float value)
    {
        if (isHost)
        {
            if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_AI)
            {
                if (GetIsHostTurn())
                {
                    victoryPointsNumberP1 = value;
                }
                else
                {
                    victoryPointsNumberP2 = value;
                    SetVictoryPointsTextP2(value);
                }
            }
            else
            {
                victoryPointsNumberP1 = value;
            }
        }
        else
        {
            victoryPointsNumberP2 = value;
            SetVictoryPointsTextP2(value);
            CmdSetVictoryPoints(value);
        }
    }

    [Command]
    void CmdSetVictoryPoints(float value)
    {
        victoryPointsNumberP2 = value;
        print("CmdSet");
    }

    void SetVictoryPointsTextP1(float value)
    {
        victoryPoints.text = value.ToString("F2");
    }

    void SetVictoryPointsTextP2(float value)
    {
        victoryPointsP2.text = value.ToString("F2");
    }

    public void Back()
    {
        closeConfirmationPanel.gameObject.SetActive(true); 
    }

    public void BackNo()
    {
        closeConfirmationPanel.gameObject.SetActive(false); 
    }

    public void BackYes()
    {
        closeConfirmationPanel.gameObject.SetActive(false);
        if (GameObject.FindGameObjectWithTag("Room Manager") != null)
        {
            NetworkRoomManager netRoomManager = GameObject.FindGameObjectWithTag("Room Manager").GetComponent<NetworkRoomManager>();

            if (isHost)
                netRoomManager.StopHost();
            else
                netRoomManager.StopClient();
        }
        else
        {
            GameObject.FindGameObjectWithTag("Single Net Manager").GetComponent<NetworkManager>().StopHost();
        }
        SceneManager.LoadScene("Menu");
        //Debug.Log("YES");
    }
    
    
    void changeSound()
    {
        soundBackground.clip = (AudioClip)Resources.Load("Audio/" + SkinManager.instance.muzyki[SkinManager.instance.ActiveSound].Name); 
        soundBackground.Play();
    }

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

    void AIEasyPlay()
    {
        GameObject cardTask;
        TextMeshProUGUI valueText;
        int taskValue = 0;
        int suma = 0;
        bool isSuccess = false;
        int successIndex = 0;
        //collect first founded and nothing more, without multiply
        powerUpAICardsToRemove.Clear();
        playerAICardsRedToRemove.Clear();
        playerAICardsGreenToRemove.Clear();
        playerAICardsBlueToRemove.Clear();
        taskCardAIToRemove = 0;
        isSuccess = false;

         CheckAICards();

        for (int i = 0; i < taskCards.Count; ++i)
        {
            cardTask = taskCards[i];
            if ((cardTask.gameObject.activeSelf))//&&(!isSuccess))
            {
               suma = 0; 
               taskValue =  int.Parse(cardTask.transform.Find("Value Text").GetComponent<TextMeshProUGUI>().text);
               //Debug.Log("taskValue:" + taskValue);
               if (cardTask.transform.Find("Value Text").GetComponent<TextMeshProUGUI>().color == redColor)
               {
                   //Debug.Log("RED");
                    for (int j = 0; j < playerAICardsRed.Count; ++j)
                    {
                        card = playerAICardsRed[j];
                        valueText = card.transform.Find("Addition Text").GetComponent<TextMeshProUGUI>();
                        //Debug.Log("Card:" + valueText.text);
                        //Debug.Log("SumaBefore:" + suma);
                        suma += int.Parse(valueText.text);
                        //Debug.Log("SumaAfter:" + suma);
                        if (suma >= taskValue)
                        {
                            isSuccess = true;
                            //playerAICardsToRemove.Clear();
                            aiCommands.Add(AI_SET_ACTIVE_CARD);
                            aiCommands.Add(i);
                            successIndex = j;
                            
                            break;
                        }
                        if (isSuccess)
                            break;
                    }//cards loop
                    if (isSuccess)
                    {
                        //aiCommands.Add(AI_DISCARD_TASK);
                        //aiCommands.Add(i);
                        for (int ii = 0; ii <= successIndex; ++ii)
                        {
                            //Debug.Log("AddToRemove");
                            aiCommands.Add(AI_SHOW_POINTS_RED);
                            aiCommands.Add(ii);//add cards indexes
                            playerAICardsRedToRemove.Add(ii);
                        }
                        aiCommands.Add(AI_COLLECT_POINTS);
                        aiCommands.Add(suma);
                        aiCommands.Add(taskValue);
                        break;
                    }
               }//redColor
               if (isSuccess)
                   break;
                //now green
               if (cardTask.transform.Find("Value Text").GetComponent<TextMeshProUGUI>().color == greenColor)
               {
                   //Debug.Log("GREEN");
                   for (int j = 0; j < playerAICardsGreen.Count; ++j)
                   {
                       card = playerAICardsGreen[j];
                       valueText = card.transform.Find("Addition Text").GetComponent<TextMeshProUGUI>();
                       //Debug.Log("Card:" + valueText.text);
                       //Debug.Log("SumaBefore:" + suma);
                       suma += int.Parse(valueText.text);
                       //Debug.Log("SumaAfter:" + suma);
                       if (suma >= taskValue)
                       {
                           isSuccess = true;
                           //playerAICardsToRemove.Clear(); 
                           aiCommands.Add(AI_SET_ACTIVE_CARD);
                           aiCommands.Add(i);
                           successIndex = j;
                           
                           break;
                       }
                       if (isSuccess)
                           break;
                   }//cards loop
                   if (isSuccess)
                   {
                       //aiCommands.Add(AI_DISCARD_TASK);
                       //aiCommands.Add(i);
                       for (int ii = 0; ii <= successIndex; ++ii)
                       {
                           //Debug.Log("AddToRemove");
                           aiCommands.Add(AI_SHOW_POINTS_GREEN);
                           aiCommands.Add(ii);//add cards indexes
                           playerAICardsGreenToRemove.Add(ii);
                       }
                       aiCommands.Add(AI_COLLECT_POINTS);
                       aiCommands.Add(suma);
                       aiCommands.Add(taskValue);
                       break;
                   }
               }//greenColor
               if (isSuccess)
                   break;
                //now blue
               if (cardTask.transform.Find("Value Text").GetComponent<TextMeshProUGUI>().color == blueColor)
               {
                   //Debug.Log("BLUE");
                   for (int j = 0; j < playerAICardsBlue.Count; ++j)
                   {
                       card = playerAICardsBlue[j];
                       valueText = card.transform.Find("Addition Text").GetComponent<TextMeshProUGUI>();
                       //Debug.Log("Card:" + valueText.text);
                       //Debug.Log("SumaBefore:" + suma);
                       suma += int.Parse(valueText.text);
                       //Debug.Log("SumaAfter:" + suma);
                       if (suma >= taskValue)
                       {
                           isSuccess = true;
                           //playerAICardsToRemove.Clear(); 
                           aiCommands.Add(AI_SET_ACTIVE_CARD);
                           aiCommands.Add(i);
                           successIndex = j;
                           
                           break;
                       }
                       if (isSuccess)
                           break;
                   }//cards loop
                   if (isSuccess)
                   {
                       //aiCommands.Add(AI_DISCARD_TASK);
                       //aiCommands.Add(i);
                       for (int ii = 0; ii <= successIndex; ++ii)
                       {
                           //Debug.Log("AddToRemove");
                           aiCommands.Add(AI_SHOW_POINTS_BLUE);
                           aiCommands.Add(ii);//add cards indexes
                           playerAICardsBlueToRemove.Add(ii);
                       }
                       aiCommands.Add(AI_COLLECT_POINTS);
                       aiCommands.Add(suma);
                       aiCommands.Add(taskValue);
                       break;
                   }
               }//blueColor
               if (isSuccess)
                   break;
            }//active Task
            if (isSuccess)
                break;
        }

        aiCommands.Add(AI_END_TURN);
        
        /*
        AI_IDLE = 0;
        public const int AI_END_TURN = 1;
        public const int AI_SET_ACTIVE_CARD = 2;
        public const int AI_COLLECT_POINTS = 3;
        public const int AI_DISCARD_TASK = 4;
        public const int AI_DISCARD_AI_PLAYER_CARD = 5;
        public const int AI_DISCARD_AI_POWERUP_CARD = 6;
        public const int AI_PLAY_AI_PLAYER_CARD = 7;
        public const int AI_PLAY_AI_POWERUP_CARD = 8;
         */
    }

    void RunAICommand(int number)
    {
        Transform dropPanel;
        GameObject card;

        switch (number)
        {
            case AI_END_TURN:
                aiCommands.RemoveAt(0);
                EndTurn();
                CheckCardNumbers(false);
                break;
            case AI_COLLECT_POINTS:
                aiCommands.RemoveAt(0);
                CollectPoints(aiCommands[0],aiCommands[1]);
                aiCommands.RemoveAt(0);
                aiCommands.RemoveAt(0);
                for (int i = powerUpAICardsToRemove.Count - 1; i >= 0 ; --i)
                {
                    card = powerUpAICards[powerUpAICardsToRemove[i]];
                    powerUpAICards.Remove(card);
                    Destroy(card);
                }
                //powerUpAICardsToRemove[];
                //Debug.Log("playerAICardsRedToRemove:" + playerAICardsRedToRemove.Count);
                for (int i = playerAICardsRedToRemove.Count - 1; i >= 0 ; --i)
                {
                    //Debug.Log("RedIn:" + playerAICardsRedToRemove[i]);
                    card = playerAICardsRed[playerAICardsRedToRemove[i]];
                    playerAICardsRed.Remove(card);
                    Destroy(card);
                    
                }
                //Debug.Log("playerAICardsGreenToRemove:" + playerAICardsGreenToRemove.Count);
                for (int i = playerAICardsGreenToRemove.Count -1 ; i >= 0 ; --i)
                {
                    //Debug.Log("GreenIn:" + playerAICardsGreenToRemove[i]);
                    card = playerAICardsGreen[playerAICardsGreenToRemove[i]];
                    playerAICardsGreen.Remove(card);
                    Destroy(card);
                    
                }
                //Debug.Log("playerAICardsBlueToRemove:" + playerAICardsBlueToRemove.Count);
                for (int i = playerAICardsBlueToRemove.Count - 1; i >= 0 ; --i)
                {
                    //Debug.Log("BlueIn:" + playerAICardsBlueToRemove[i]);
                    card = playerAICardsBlue[playerAICardsBlueToRemove[i]];
                    playerAICardsBlue.Remove(card);
                    Destroy(card);
                    
                }
                SetActiveCard(taskCards[taskCardAIToRemove], true);
                DiscardTaskCard(taskCards[taskCardAIToRemove]);
                playerAICardsRedToRemove.Clear();
                playerAICardsGreenToRemove.Clear();
                playerAICardsBlueToRemove.Clear();
                taskCardAIToRemove = 0;
                //activeCard = null;
                //cout << "got Hearts \n";
                break;
            case AI_SHOW_POINTS_RED:
                aiCommands.RemoveAt(0);
                dropPanel = activeCard.gameObject.transform.Find("RawImage").GetComponent<RawImage>().transform.parent.gameObject.transform.Find("Drop Panel");
                playerAICardsRed[aiCommands[0]].gameObject.transform.SetParent(dropPanel);
                //playerAICardsRedToRemove.Add(aiCommands[0]);
                aiCommands.RemoveAt(0);
                //cout << "got Hearts \n";
                break;
            case AI_SHOW_POINTS_GREEN:
                aiCommands.RemoveAt(0);
                dropPanel = activeCard.gameObject.transform.Find("RawImage").GetComponent<RawImage>().transform.parent.gameObject.transform.Find("Drop Panel");
                playerAICardsGreen[aiCommands[0]].gameObject.transform.SetParent(dropPanel);
                //playerAICardsGreenToRemove.Add(aiCommands[0]);
                aiCommands.RemoveAt(0);
                //cout << "got Clubs \n";
                break;
            case AI_SHOW_POINTS_BLUE:
                aiCommands.RemoveAt(0);
                dropPanel = activeCard.gameObject.transform.Find("RawImage").GetComponent<RawImage>().transform.parent.gameObject.transform.Find("Drop Panel");
                playerAICardsBlue[aiCommands[0]].gameObject.transform.SetParent(dropPanel);
                //playerAICardsBlueToRemove.Add(aiCommands[0]);
                aiCommands.RemoveAt(0);
                //cout << "got Spades \n";
                break;
            case AI_SET_ACTIVE_CARD:
                aiCommands.RemoveAt(0);
                //activeCard = taskCards[aiCommands[0]];
                SetActiveCard(taskCards[aiCommands[0]],false);
                taskCardAIToRemove = aiCommands[0];
                aiCommands.RemoveAt(0);
                //cout << "got Spades \n";
                break;    
            case AI_SHOW_POWERUP:
                aiCommands.RemoveAt(0);
                //dodaj powerup do ostatnio dodanej playerAICard
                
                aiCommands.RemoveAt(0);
                //cout << "got Spades \n";
                break;                
            case AI_DISCARD_TASK:
                //cout << "got Spades \n";
                break;
            case AI_DISCARD_AI_POWERUP_CARD:
                //cout << "got Spades \n";
                break;
            case AI_DISCARD_AI_PLAYER_CARD_RED:
                //cout << "got Spades \n";
                break;
            case AI_DISCARD_AI_PLAYER_CARD_GREEN:
                //cout << "got Spades \n";
                break;
            case AI_DISCARD_AI_PLAYER_CARD_BLUE:
                //cout << "got Spades \n";
                break;
           
           // default:
            //    cout << "didn't get card \n";
        }
    }

    void CheckAICards()
    {
        int ileRazy = playerAICardsRed.Count + playerAICardsGreen.Count + playerAICardsBlue.Count;
        if (ileRazy > maxPlayerCards )
        {
            for (int i = 0; i < ileRazy - maxPlayerCards; ++i)
            {
                AIEasyDiscardAIPlayerCard();
            }
        }

        //Debug.Log("actualTaskCardsCount:" + actualTaskCardsCount);
        //Debug.Log("maxTaskCards:" + maxTaskCards);
        if (actualTaskCardsCount > maxTaskCards)
        {
            //Debug.Log("DiscardIn");
            AIEasyDiscardTaskCard();
        }

        if (powerUpAICards.Count > maxPowerUpCards)
        {
            AIEasyDiscardAIPowerUpCard();
        }
            
    }

    void AIEasyDiscardTaskCard()
    {
        List<int> activeTaskCards = new List<int>();
        //totally random

        for (int i = 0; i < taskCards.Count; ++i)
        {
            if (taskCards[i].gameObject.activeSelf)
            {
                activeTaskCards.Add(i);
            }
        }
        GameObject card = taskCards[activeTaskCards[Random.Range(0, activeTaskCards.Count)]];
        //Debug.Log(maxActualTaskCards);
        //Debug.Log(card);
        DiscardTaskCard(card);
    }

    void AIEasyDiscardAIPlayerCard()
    {
        bool isRed = false;
        bool isGreen = false;
        bool isBlue = false;
        int smallestIndex = 0;
       // int actualCardValue = 0;
        int minCardValue = 32000;
        TextMeshProUGUI valueText;
        GameObject card;
        //discard lower value

        for (int i = 0; i < playerAICardsRed.Count; ++i)
        {
            card = playerAICardsRed[i];
            //Debug.Log("playerAICardsRed:" + card);
            valueText = card.transform.Find("Addition Text").GetComponent<TextMeshProUGUI>();
            if (int.Parse(valueText.text) < minCardValue)
            {
                minCardValue = int.Parse(valueText.text);
                smallestIndex = i;
                isRed = true;
                isGreen = false;
                isBlue = false;
            }
        }
        for (int i = 0; i < playerAICardsGreen.Count; ++i)
        {
            card = playerAICardsGreen[i];
            valueText = card.transform.Find("Addition Text").GetComponent<TextMeshProUGUI>();
            if (int.Parse(valueText.text) < minCardValue)
            {
                minCardValue = int.Parse(valueText.text);
                smallestIndex = i;
                isRed = false;
                isGreen = true;
                isBlue = false;
            }
        }
        for (int i = 0; i < playerAICardsBlue.Count; ++i)
        {
            card = playerAICardsBlue[i];
            valueText = card.transform.Find("Addition Text").GetComponent<TextMeshProUGUI>();
            if (int.Parse(valueText.text) < minCardValue)
            {
                minCardValue = int.Parse(valueText.text);
                smallestIndex = i;
                isRed = false;
                isGreen = false;
                isBlue = true;
            }
        }

        if (isRed)
        {
            card = playerAICardsRed[smallestIndex];
            playerAICardsRed.Remove(card);
            Destroy(card);
        }
        if (isGreen)
        {
            card = playerAICardsGreen[smallestIndex];
            playerAICardsGreen.Remove(card);
            Destroy(card);
        }
        if (isBlue)
        {
            card = playerAICardsBlue[smallestIndex];
            playerAICardsBlue.Remove(card);
            Destroy(card);
        }
        

    }

    void AIEasyDiscardAIPowerUpCard()
    {
        //totally random
        GameObject card = powerUpAICards[Random.Range(0, powerUpAICards.Count)];
        powerUpAICards.Remove(card);
        Destroy(card);
       /* sumCardCount++;
                    valueText = card.transform.Find("Addition Text").GetComponent<TextMeshProUGUI>();
                    for (int j = 0; j < powerUpCards.Count; ++j)  
                    {
                        cardPowerUp = powerUpCards[j];
                        
                        if (cardPowerUp.transform.Find("PowerUp Parent Name").GetComponent<TextMeshProUGUI>().text != "")
                            if (cardPowerUp.transform.Find("PowerUp Parent Name").GetComponent<TextMeshProUGUI>().text == card.name)
                            {
                                multiplyCount++;
                                if (valueText.color == cardPowerUp.transform.Find("Red Text").GetComponent<TextMeshProUGUI>().color)
                                {
                                    kolorText = cardPowerUp.transform.Find("Red Text").GetComponent<TextMeshProUGUI>();
                                    Razy = int.Parse(kolorText.text);
                                    break;
                                }
                                else
                                    if (valueText.color == cardPowerUp.transform.Find("Green Text").GetComponent<TextMeshProUGUI>().color)
                                    {
                                        kolorText = cardPowerUp.transform.Find("Green Text").GetComponent<TextMeshProUGUI>();
                                        Razy = int.Parse(kolorText.text);
                                        break;
                                    }
                                    else
                                        if (valueText.color == cardPowerUp.transform.Find("Blue Text").GetComponent<TextMeshProUGUI>().color)
                                        {
                                            kolorText = cardPowerUp.transform.Find("Blue Text").GetComponent<TextMeshProUGUI>();
                                            Razy = int.Parse(kolorText.text);
                                            break;
                                        }                           
                            }
                    }
                    Suma += int.Parse(valueText.text)*Razy;
                    Razy = 1;
        */

    }

    void AIImpossibleDiscardTaskCard()
    {
        //change to bigger on color with smallest playerCardsAI value
        GameObject card = taskCards[Random.Range(0, maxActualTaskCards)];
        DiscardTaskCard(card);

    }

    void AIImpossibleDiscardAIPlayerCard()
    {
        //check all possibilities with powerUps, beginning from higer points and prefer exactly matches

    }

    void AIImpossibleDiscardAIPowerUpCard()
    {
        //check all possibilities with powerUps, beginning from higer points and prefer exactly matches

    }
    

    void changeBackground()
    {
        
        /*iOS uses Application.dataPath + "/Raw",
Android uses files inside a compressed APK
/JAR file, "jar:file://" + Application.dataPath + "!/assets".*/
        if (Application.platform == RuntimePlatform.Android)
        {
            string pom = SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name + ".jpg";//
            //string pom = SkinManager.instance.tla[LocalActiveBackground].Name + ".jpg";//
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
            //backgroundImage.sprite = Resources.Load<Sprite>(SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name);
            string pom2 = Application.streamingAssetsPath + "/" + SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name + ".jpg";
            StartCoroutine(GetWWWTexture(pom2));
        }
        //backgroundImage.sprite = Resources.Load<Sprite>("Background/" + SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name);//.Name
        //backgroundImage.sprite = Resources.Load<Sprite>(SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name);//.Name
    }

    public void SetActiveCard(GameObject card, bool isBack)
    {
        TextMeshProUGUI valueText;
        Color taskColor;

        if (!activeCardSpace.activeInHierarchy && !isBack)
        {
            tasks.SetActive(false);
            closeText.gameObject.SetActive(false);
            activeCard = card;
            transparentPlayerCardPanel.SetActive(false);
            transparentPowerUpCardPanel.SetActive(false);
            transparentButton.gameObject.SetActive(false);
            activeCardSpace.SetActive(true);
            activeCard.transform.SetParent(activeCardSpace.transform);
            card.transform.Find("Drop Panel").gameObject.SetActive(true);
            taskColor = activeCard.transform.Find("Value Text").GetComponent<TextMeshProUGUI>().color;
            for (int i = 0; i < playerCards.Count; ++i)
            {
                if (playerCards[i].gameObject.activeSelf)
                {
                    card = playerCards[i];
                    valueText = card.transform.Find("Addition Text").GetComponent<TextMeshProUGUI>();
                    card.transform.Find("Player Drop Panel").gameObject.SetActive(true);
                    //card.gameObject.enabled = false;
                    /*   var colliders = this.GetComponentsInChildren<Collider>();
                       foreach (var col in colliders)
                           //if (col.gameObject.name != _except) // for this to work, names should be unique. I just used the name as an example, use whatever unique identifier you want...
                               col.enabled = false;*/
                    if (valueText.color != taskColor)
                    {
                        card.SetActive(false);
                        //card.GetComponent<PlayerCard>().hideByColor = true;
                       // card.transform.SetParent(handsSorted.transform, false);
                    }
                }
            }
            //hands.SetActive(false);
            //handsSorted.SetActive(true);

            collectPointsBtn.SetActive(true);
            endTurnBtn.SetActive(false);
            transparentPlayerCardPanel.SetActive(false);
            transparentPowerUpCardPanel.SetActive(false);
            transparentButton.gameObject.SetActive(false);
            //Debug.Log("BEF"+transparentPlayerCardPanel.activeSelf);
            CheckCardNumbers(false);
            //Debug.Log("AFT"+transparentPlayerCardPanel.activeSelf);
        }
        else
        {
            taskResignSFX.Play();
            tasks.SetActive(true);
            closeText.gameObject.SetActive(true);
            
            activeCardSpace.SetActive(false);
            activeCard.transform.SetParent(tasks.transform);
            card.transform.Find("Drop Panel").gameObject.SetActive(false);
            activeCard = null;
            transparentPlayerCardPanel.SetActive(true);
            transparentPowerUpCardPanel.SetActive(true);
            transparentButton.gameObject.SetActive(false);
            for (int i = 0; i < powerUpCards.Count; ++i)
            {
                card = powerUpCards[i];
                card.transform.Find("PowerUp Parent Name").GetComponent<TextMeshProUGUI>().text = "";
                card.transform.SetParent(powerUps.transform);
            }
            for (int i = 0; i < playerCards.Count; ++i)
            {
               // if (playerCards[i].gameObject.activeSelf)
                {
                    card = playerCards[i];
                    card.SetActive(true);
                    //card.GetComponent<PlayerCard>().hideByColor = true;
                    card.GetComponent<PlayerCard>().hasMultiply = false;
                    card.transform.Find("Player Drop Panel").gameObject.SetActive(false);
                    card.transform.Find("Parent Name").GetComponent<TextMeshProUGUI>().text = "";
                    card.transform.SetParent(hands.transform);
                    card.transform.localScale = card.GetComponent<PlayerCard>().normalScale;
                }
            }

            hands.SetActive(true);
           // handsSorted.SetActive(false);
            collectPointsBtn.SetActive(false);
            if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_SOLO)
            {
                endTurnBtn.SetActive(true);
            }
            else
            {
                if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_AI)
                {
                    if (GetIsHostTurn())
                    {
                        endTurnBtn.SetActive(true);
                    }
                }
                else
                {
                    endTurnBtn.SetActive(true);
                }
            }

            CheckCardNumbers(true);
        }
 
    }

    void ShowAchievementPanel(int achievementNumber)
    {
       // if (SkinManager.instance.osiagniecia[achievementNumber].Reward > 0.0)
        {
            //achievementPanelScale += 0.01f;
            achievementPointsText.text = "+" + SkinManager.instance.osiagniecia[achievementNumber].Reward.ToString(); 
        }
        achievementNameText.text = SkinManager.instance.osiagniecia[achievementNumber].Name;
        achievementPanelScale = ACHIEVEMENT_PANEL_SMALL_SCALE;
        achievementPanel.SetActive(true);
    }

    public void RerollClick() //actualTaskCardsCount > maxTaskCards
    {
        GameObject cardTask;// = Instantiate(taskCardPrefab);
        int newCard = 0;
        float pom;
        int howManyCards = actualTaskCardsCount;
        //if (actualTaskCardsCount <= maxTaskCards)
        //    howManyCards--;

        if (!SkinManager.instance.Lucky)
        {
                PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.LUCKY].ID, true ? 1 : 0);
                SkinManager.instance.SetLucky(true);
                AddCash(SkinManager.instance.osiagniecia[SkinManager.LUCKY].Reward);
                ShowAchievementPanel(SkinManager.LUCKY);
        }

        //Debug.Log("actualTaskCardsCount BEFORE discard:" + actualTaskCardsCount);
        newCard = maxActualTaskCards;// actualTaskCardsCount;// taskCards.Count;//maxActualTaskCards
        for (int i = newCard -1 ; i >= 0; --i)//newCard - 1
        {
           // Debug.Log(i);
            cardTask = taskCards[i];
            //if (card.gameObject.activeSelf)
            DiscardTaskCard(cardTask);
        }
        actualTaskCardsCount = 0;
        //Debug.Log("actualTaskCardsCount BEFORE draw:" + actualTaskCardsCount);
        for (int i = 0; i < howManyCards; ++i) //newCard
        {
            DrawTaskCard();
            //Debug.Log("DrawTask Reroll++");
        }
        //Debug.Log("actualTaskCardsCount AFTER draw:" + actualTaskCardsCount);
        pom = GetVictoryPoints();
        if (pom < earlyGamePoint)
            {               
                pom -= SkinManager.REROLL_COST_EARLY;
            }
            else
            {
                if (pom < middleGamePoint)
                {
                    pom -= SkinManager.REROLL_COST_MIDDLE;
                }
                else
                {
                    pom -= SkinManager.REROLL_COST_LATE;
                }
            }
        SetVictoryPoints((float)pom);
        rerollPanel.SetActive(false);
        //CheckCardNumbers(false);
        //Debug.Log("actualTaskCardsCount END:" + actualTaskCardsCount);
        //Debug.Log("maxTaskCards END:" + maxTaskCards);
       // if (actualTaskCardsCount <= maxTaskCards)
       //     actualTaskCardsCount--;
    }

    public void RerollTaskCardCheck()
    {
        TextMeshProUGUI valueText;
        Color colorOne = new Color32(SkinManager.RED_COLOR, 0, 0, 255); ;
        Color colorTwo = new Color32(0, SkinManager.GREEN_COLOR, 0, 255); ;
        Color colorThree = new Color32(0, 0, SkinManager.BLUE_COLOR, 255); ;
        int oneCount=0;
        int twoCount=0;
        int threeCount=0;
        GameObject cardTask;// = Instantiate(taskCardPrefab);
        //float percent = 0.0f;

        for (int i = 0; i < maxActualTaskCards; ++i)//taskCards.Count; ++i)
        {
            cardTask = taskCards[i];
            if (cardTask.gameObject.activeSelf)
            {
                valueText = cardTask.transform.Find("Value Text").GetComponent<TextMeshProUGUI>();//color text
                /*  if (i==0){
                      colorOne = valueText.color;
                      oneCount++;
                  }
                  else*/
                {
                    if (valueText.color == colorOne)
                    {
                        oneCount++;
                    }
                    else
                    {
                        if (valueText.color == colorTwo)
                        {
                            twoCount++;
                        }
                        else
                        {
                            if (valueText.color == colorThree)
                            {
                                threeCount++;
                            }
                        }
                    }
                }
            }
        }

       // Debug.Log((float)threeCount / (float)maxTaskCards);
        //Debug.Log((float)twoCount / (float)maxTaskCards);
        //Debug.Log(percent);
        //percent = (float)oneCount / (float)maxTaskCards;
        if (((float)oneCount / (float)maxTaskCards >= SkinManager.REROLL_COLOR_RULE) ||
            ((float)twoCount / (float)maxTaskCards >= SkinManager.REROLL_COLOR_RULE) ||
            ((float)threeCount / (float)maxTaskCards >= SkinManager.REROLL_COLOR_RULE))
        {
            if (GetVictoryPoints() < earlyGamePoint)
            {
                rerollCostText.text = "-" + SkinManager.REROLL_COST_EARLY.ToString() + " pkt";
            }
            else
            {
                if (GetVictoryPoints() < middleGamePoint)
                {
                    rerollCostText.text = "-" + SkinManager.REROLL_COST_MIDDLE.ToString() + " pkt";
                }
                else
                {
                    rerollCostText.text = "-" + SkinManager.REROLL_COST_LATE.ToString() + " pkt";
                }
            }

            rerollPanel.SetActive(true);
        }
        else
        {
            rerollPanel.SetActive(false);
        }
    }

     
 
     IEnumerator LateStart(float waitTime)
     {
         
         yield return new WaitForSeconds(waitTime);
         //Your Function You Want to Call
         /*for (int i = 0; i < taskCardsOnStart; ++i)
         {
             DrawTaskCard();//player1
             //if pvp
             //DrawTaskCard(BLUE_TEXT, 11);//for player2(Client) to set it when need to show
         }*/
         for (int i = 0; i < maxTaskCardsAddLate + taskCardsToDraw; ++i)
         //for (int i = 0; i < maxActualTaskCards; ++i)
         {
             DiscardTaskCard(taskCards[i]);
         }
         for (int i = 0; i < taskCardsOnStart; ++i)
         {
            if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_PVP)
            {
                if (isHost)
                {
                    DrawTaskCard();
                }
                else
                {
                    isFirstTurn = true;
                }
            }
            else
            {
                DrawTaskCard();//player1
            }
             //if pvp
             //DrawTaskCard(BLUE_TEXT, 11);//for player2(Client) to set it when need to show
         }
         //tasks.SetActive(true);
         tasks.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
     }

    private void Start()
    {
        string SubStr = SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name;
        SubStr = SubStr.Substring(0, SubStr.Length - 1);

        wybranaRamka = Resources.Load<Texture2D>(SkinManager.instance.ramki[SkinManager.instance.ActiveFrame].Name);
        wybranyBlack = Resources.Load<Sprite>("Black");
        wybranyRed = Resources.Load<Sprite>(SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + RED_TEXT);
        wybranyGreen = Resources.Load<Sprite>(SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + GREEN_TEXT);
        wybranyBlue = Resources.Load<Sprite>(SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + BLUE_TEXT);
        wybranyRedStatic = Resources.Load<Sprite>(SubStr + RED_TEXT);
        wybranyGreenStatic = Resources.Load<Sprite>(SubStr + GREEN_TEXT);
        wybranyBlueStatic = Resources.Load<Sprite>(SubStr + BLUE_TEXT);
        wybranyClipRed = Resources.Load(SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + RED_TEXT) as VideoClip;
        wybranyClipGreen = Resources.Load(SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + GREEN_TEXT) as VideoClip;
        wybranyClipBlue = Resources.Load(SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + BLUE_TEXT) as VideoClip;
#if HTML5
        wybranyClipRed = Resources.Load("http://100plus.ieti.pl/public_html/StreamingAssets/Explodes" + RED_TEXT + ".mp4") as VideoClip;
        wybranyClipGreen = Resources.Load("http://100plus.ieti.pl/public_html/StreamingAssets/Explodes" +  GREEN_TEXT + ".mp4") as VideoClip;
        wybranyClipBlue = Resources.Load("http://100plus.ieti.pl/public_html/StreamingAssets/Explodes" + BLUE_TEXT + ".mp4") as VideoClip;
#endif
        

        maxActualTaskCards = maxTaskCardsAddLate + taskCardsToDraw;
        //maxActualPlayerCards = maxPlayerCardsAddLate + playerCardsToDraw;
       // maxActualPowerUpCards = maxPowerUpCardsAddLate + powerUpCardsToDraw;
        victoryPanel.SetActive(false);
        defeatVictoryPanel.SetActive(false);
        achievementPanel.SetActive(false);
        transparentPlayerCardPanel.SetActive(true);
        transparentPowerUpCardPanel.SetActive(true);
        transparentButton.gameObject.SetActive(false);
        closeConfirmationPanel.gameObject.SetActive(false);
        transparentAllPanel.gameObject.SetActive(false);
        if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_PVP)
        {
            infoText.gameObject.SetActive(true);
            if (!isHost)
            {
                transparentAllPanel.gameObject.SetActive(true);
            }
        }
        else
        {
            infoText.gameObject.SetActive(false);
        }
        if (SkinManager.instance.ActiveVictoryConditions < 3)
        {
            timerImage.gameObject.SetActive(true);
            scoreImage.gameObject.SetActive(false);
        }
        else
        {
            timerImage.gameObject.SetActive(false);
            scoreImage.gameObject.SetActive(true);
        }

        if (SkinManager.instance.ActivePlayerTurnConditions != 0)
        {
            timerPlayerTurnImage.gameObject.SetActive(true);
            timerPlayerTurnText.gameObject.SetActive(true);
        }
        else
        {
            timerPlayerTurnImage.gameObject.SetActive(false);
            timerPlayerTurnText.gameObject.SetActive(false);
        }
        
        //rerollPanel.SetActive(false);
        victoryPanel.gameObject.transform.localScale = new Vector3(victoryPanelScale, victoryPanelScale, victoryPanelScale);
        defeatVictoryPanel.gameObject.transform.localScale = new Vector3(victoryPanelScale, victoryPanelScale, victoryPanelScale);
        achievementPanel.gameObject.transform.localScale = new Vector3(achievementPanelScale, achievementPanelScale, achievementPanelScale);
        isVictory = false;
        isVictorySound = true;
        userActivityTime = SkinManager.MAX_USER_DISACTIVITY;
        AIActivityTime = SkinManager.AI_ACTIVITY_TIME;
        if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_PVP)
        {
            AIActivityTime = SkinManager.PVP_ACTIVITY_TIME;
        }
        /* var fill = slider.GetComponentsInChildren<UnityEngine.UI.Image>().FirstOrDefault(t => t.name == "Fill");
         if (fill != null)
         {
             fill.color = Color.green;// Color.Lerp(Color.red, Color.green, 0.5f);
         }*/

        GameObject card = Instantiate(playerCardPrefab);
        GameObject cardPowerUp = Instantiate(powerUpCardPrefab);
        coinGlobal = Instantiate(coinsPrefab);



        slider.maxValue = earlyGamePoint;
        slider.minValue = 0;
        slider.value = 0;
        Color color = new Color(0f / 255f, 255f / 255f, 0f / 255f);
        slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
        soundBackground.volume = SkinManager.instance.ActiveSoundValue / 100;
        //soundBackground.volume = 1;
        endTurnSFX.volume = SkinManager.instance.ActiveSFXValue / 100;
        exitSFX.volume = SkinManager.instance.ActiveSFXValue / 100;
        coinSingleSFX.volume = SkinManager.instance.ActiveSFXValue / 100;
        coinFewSFX.volume = SkinManager.instance.ActiveSFXValue / 100;
        coinMoreSFX.volume = SkinManager.instance.ActiveSFXValue / 100;
        coinManySFX.volume = SkinManager.instance.ActiveSFXValue / 100;
        coinMissSFX.volume = SkinManager.instance.ActiveSFXValue / 100;
        trashSFX.volume = SkinManager.instance.ActiveSFXValue / 100;
        taskResignSFX.volume = SkinManager.instance.ActiveSFXValue / 100;
        victorySFX.volume = SkinManager.instance.ActiveSFXValue / 100;
        isVictoryPointFirst = SkinManager.instance.isVictoryPointFirst;
        isVictoryTimePass = SkinManager.instance.isVictoryTimePass;
        VictoryPointFirstValue = SkinManager.instance.VictoryPointFirstValue;
        maxGameTimeInSeconds = SkinManager.instance.VictoryTimePassValue;
        maxPlayerTurnInSeconds = SkinManager.instance.ActivePlayerEndTime;
        remainingGameTime = maxGameTimeInSeconds;
        playerTurnTime = maxPlayerTurnInSeconds;
        iaTurnImage.gameObject.SetActive(false);
        p2TurnImage.gameObject.SetActive(false);
        if (SkinManager.instance.ActivePlayerMode != 0)//not single player mode
        {
            victoryPointsP2.gameObject.SetActive(true);
        }
        else
        {
            victoryPointsP2.gameObject.SetActive(false); //nie pokazuje w multiplayer?
        }
        changeBackground();
        changeSound();

        CreateTaskCards();
        //CreatePlayerCards();

        for (int i = 0; i < powerUpCardsOnStart; i++)
        {
            DrawPowerUpCard();//player1
            //DrawPowerUpCard(1,2,3);//for player2(Client) to set it when need to show
        }
        if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_AI)
        {
            for (int i = 0; i < powerUpCardsOnStart; i++)
            {
                DrawPowerUpAICard();//AI
            }
        }

        for (int i = 0; i < playerCardsOnStart; i++)
        {
            DrawPlayerCard();//player1
            //DrawPlayerCard(BLUE_TEXT, 11);//for player2(Client) to set it when need to show
        }
        if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_AI)
        {
            for (int i = 0; i < playerCardsOnStart; ++i)
            {
                DrawPlayerAICard();//AI
            }
        }

        /*for (int i = 0; i < taskCardsOnStart; ++i)
        {
            DrawTaskCard();//player1
            //if pvp
            //DrawTaskCard(BLUE_TEXT, 11);//for player2(Client) to set it when need to show
        }
        
        for (int i = taskCardsOnStart - 1; i >= 0; --i) //maxTaskCardsAddLate
        {
            DiscardTaskCard(taskCards[i]);
        }

        for (int i = 0; i < taskCardsOnStart; ++i)
        {
            DrawTaskCard();//player1
            //if pvp
            //DrawTaskCard(BLUE_TEXT, 11);//for player2(Client) to set it when need to show
        }*/

        if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_PVP)
        {
            if (isHost)
            {
                endTurnBtn.gameObject.SetActive(GetIsHostTurn());
                p2TurnImage.gameObject.SetActive(false);
            }
            else
            {
                endTurnBtn.gameObject.SetActive(!GetIsHostTurn());
                p2TurnImage.gameObject.SetActive(true);
            }
            iaTurnImage.gameObject.SetActive(false);

        }
        /*else
        {
            if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_AI)
            {

            }
        
        }*/
               

        //helpTask.transform.position = tasks.transform.position;//(taskCards[0].transform.position);//transform.TransformPoint
        //helpTask.transform.SetParent(tasks.transform);
        SetVictoryPoints(0.0f);
        StartCoroutine(LateStart(0.5f));
    }

    public void AchievementPanelHide()
    {
        achievementPanel.SetActive(false);
    }

    public void AddCash(int value)
    {
        int newCash;
        newCash = SkinManager.instance.CurrentCash + value;
        SkinManager.instance.SetCurrentCash(newCash);
        PlayerPrefs.SetInt("CurrentCash", newCash);
    }

    private void Update()
    {
        float tmpFloat = 0.0f;
        //print("VP1: " + victoryPointsNumberP1);
        //print("VP2: " + victoryPointsNumberP2);
       /* if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_PVP)
        {
            infoText.text = "";
            if (isHost)
                infoText.text = infoText.text + "isHost: True; ";
            else
                infoText.text = infoText.text + "isHost: False; ";
            if (isHostTurn)
                infoText.text = infoText.text + "isHostTurn: True;";
            else
                infoText.text = infoText.text + "isHostTurn: False;";
        }*/

        //infoText.text = GetPVPValue1().ToString() + ",Comm:" + GetPVPCommand().ToString();
        timeFromStart += Time.deltaTime;
        if (achievementPanel.activeSelf)
        {
            if (achievementPanelScale < 1.0)
            {
                achievementPanelScale += 0.0025f;
            }
            else
            {
                //achievementPanel.SetActive(false);
                //achievementPanelScale = ACHIEVEMENT_PANEL_SMALL_SCALE;
            }
            achievementPanel.gameObject.transform.localScale = new Vector3(achievementPanelScale, achievementPanelScale, achievementPanelScale);
           
        }
        
        if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_AI)
        {
            if ((isHost) && (!GetIsHostTurn()))
            {
                if (AIActivityTime > 0)
                {
                    AIActivityTime -= Time.deltaTime;
                }
                else
                {
                    if (aiCommands.Count > 0)
                        RunAICommand(aiCommands[0]);
                    AIActivityTime = SkinManager.AI_ACTIVITY_TIME;
                }
            }
        }

        if (userActivityTime > 0)
        {
            userActivityTime -= Time.deltaTime;
            helpTask.gameObject.SetActive(false);
            helpTurn.gameObject.SetActive(false);
            helpCard.gameObject.SetActive(false);
            helpEndTask.gameObject.SetActive(false);
            helpTrash.gameObject.SetActive(false);
        }//= SkinManager.MAX_USER_DISACTIVITY;
        else
        {
            if (trashArea.activeSelf)
            {
                helpTrash.gameObject.SetActive(true);
                helpTask.gameObject.SetActive(false);
                helpTurn.gameObject.SetActive(false);
                helpCard.gameObject.SetActive(false);
                helpEndTask.gameObject.SetActive(false);
            }
            else
            {
                if (tasks.activeSelf)
                {
                    if (actualTaskCardsCount > 0)//taskCards.Count
                        helpTask.gameObject.SetActive(true);
                    helpTurn.gameObject.SetActive(true);
                    helpCard.gameObject.SetActive(false);
                    helpEndTask.gameObject.SetActive(false);
                    helpTrash.gameObject.SetActive(false);
                }
                else
                {
                    helpTask.gameObject.SetActive(false);
                    helpTurn.gameObject.SetActive(false);
                    helpCard.gameObject.SetActive(true);
                    helpEndTask.gameObject.SetActive(true);
                    helpTrash.gameObject.SetActive(false);
                }
            }
        }

        //tura gracza
        if (SkinManager.instance.ActivePlayerTurnConditions != 0)//jest ograniczenie tury gracza
        {
            if ((SkinManager.instance.ActivePlayerMode != GAME_CONDITION_PVP) ||
                ((SkinManager.instance.ActivePlayerMode == GAME_CONDITION_PVP)&&(isHost)&&(isHostTurn))||
                ((SkinManager.instance.ActivePlayerMode == GAME_CONDITION_PVP) && (!isHost) && (!isHostTurn)))
            {
                if (playerTurnTime > 0)
                {
                    playerTurnTime = Mathf.FloorToInt((maxPlayerTurnInSeconds) -= Time.deltaTime);
                    timerPlayerTurnText.text = playerTurnTime.ToString();
                }
                else
                {
                    //simply check cards
                    for (int i = 0; i < playerCardsToDraw; i++)
                    {
                        if (playerCards.Count > maxPlayerCards)
                        {
                            DiscardPlayerCard(playerCards[0]);
                        }
                    }

                    for (int i = 0; i < taskCardsToDraw; i++)
                    {
                        if (actualTaskCardsCount > maxTaskCards)
                        {
                            DiscardTaskCard(taskCards[0]);
                        }
                    }

                    for (int i = 0; i < playerCardsToDraw; i++)
                    {
                        if (powerUpCards.Count > maxPowerUpCards)
                        {
                            DiscardPowerUpCard(powerUpCards[0]);
                        }
                    }

                    if (activeCard != null)
                    {
                        SetActiveCard(activeCard, false);
                    }
                    EndTurn();
                    //tutaj czekanie na drugiego gracza
                    maxPlayerTurnInSeconds = SkinManager.instance.ActivePlayerEndTime;
                    playerTurnTime = maxPlayerTurnInSeconds;
                }
            }
        }
        //koniec tury gracza
        if (remainingGameTime > 0)
        {
            remainingGameTime = Mathf.FloorToInt((maxGameTimeInSeconds) -= Time.deltaTime);
            timerText.text = remainingGameTime.ToString();
        }
        else
        {
            if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_PVP)
            {
                if (isHost)
                {
                    tmpFloat = VictoryPointFirstValue - float.Parse(victoryPoints.text);
                    //timerText.text = (VictoryPointFirstValue - float.Parse(victoryPoints.text)).ToString("F2");
                }
                else
                {
                    tmpFloat = VictoryPointFirstValue - float.Parse(victoryPointsP2.text);
                    //timerText.text = ((float)VictoryPointFirstValue - float.Parse(victoryPointsP2.text)).ToString("F2");
                }
                timerText.text = tmpFloat.ToString("F2");
            }
            else
            {
               // timerText.text = (VictoryPointFirstValue - float.Parse(victoryPoints.text)).ToString("F2");
                // tmpFloat = VictoryPointFirstValue - float.Parse(victoryPoints.text);
                tmpFloat = VictoryPointFirstValue - float.Parse(victoryPoints.text);
                timerText.text = tmpFloat.ToString("F2");
            }
            //Victory panel
            if ((isVictoryTimePass)||(isVictory))
            {
                //Debug.Log("Victory Time Pass");
                if (isVictorySound)
                {
                    victorySFX.Play();
                    isVictorySound = false;
                }
                if (isVictoryResult)
                {
                    victoryPanel.SetActive(true);
                }
                else
                {
                    defeatVictoryPanel.SetActive(true);
                }
                if (SkinManager.instance.ActiveVictoryConditions > 2) //must collect 20 or 100 points
                {
                    if ((!SkinManager.instance.PureGame) && (isPureGame))
                    {
                        if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_SOLO)
                        {
                            PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.PUREGAME].ID, true ? 1 : 0);
                            SkinManager.instance.SetPureGame(true);
                            AddCash(SkinManager.instance.osiagniecia[SkinManager.PUREGAME].Reward);
                            ShowAchievementPanel(SkinManager.PUREGAME);
                        }
                    }
                }
                closeText.gameObject.SetActive(true);
                if (victoryPanelScale < 1.0)
                {
                    victoryPanelScale += 0.01f;
                }

                if (isVictoryResult)
                {
                    victoryText.text = VICTORY;
                    victoryScoreText.text = ActualVictoryPointsText.text;
                    //defeatVictoryText.text = VICTORY;
                }
                else
                {
                    //victoryText.text = DEFEAT;
                    defeatVictoryText.text = DEFEAT;
                    defeatVictoryScoreText.text = ActualVictoryPointsText.text;
                }
                
                
                //best result
                if (SkinManager.instance.BestResult < float.Parse(ActualVictoryPointsText.text))
                {
                    SkinManager.instance.SetBestResult(float.Parse(ActualVictoryPointsText.text));
                    PlayerPrefs.SetFloat("BestResult", float.Parse(ActualVictoryPointsText.text));
                }
                //victory achievement
                if (!SkinManager.instance.WinSolo)
                {
                    if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_SOLO)
                    {
                        PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.WINSOLO].ID, true ? 1 : 0); 
                        SkinManager.instance.SetWinSolo(true);
                        AddCash(SkinManager.instance.osiagniecia[SkinManager.WINSOLO].Reward);                      
                        ShowAchievementPanel(SkinManager.WINSOLO);
                    }
                }
                if (!SkinManager.instance.WinSI)
                {
                    if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_AI)
                    {
                        if (SkinManager.instance.AIDifficulty != SkinManager.AI_EASY)
                        {
                            if (float.Parse(victoryPoints.text) > float.Parse(victoryPointsP2.text))
                            {
                                PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.WINSI].ID, true ? 1 : 0);
                                SkinManager.instance.SetWinSI(true);
                                AddCash(SkinManager.instance.osiagniecia[SkinManager.WINSI].Reward);
                                ShowAchievementPanel(SkinManager.WINSI);
                            }
                        }
                    }
                }
                if (!SkinManager.instance.WinPVP)
                {
                    if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_PVP)
                    {
                        PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.WINPVP].ID, true ? 1 : 0);
                        SkinManager.instance.SetWinPVP(true);
                        AddCash(SkinManager.instance.osiagniecia[SkinManager.WINPVP].Reward);
                        ShowAchievementPanel(SkinManager.WINPVP);
                    }
                }
                

                if (SkinManager.instance.ActiveVictoryConditions == 0)
                {
                    if (isVictoryResult)
                    {
                        victoryConditionsText.text = "5 min";
                    }
                    else
                    {
                        defeatVictoryConditionsText.text = "5 min";
                    }
                }
                else
                {
                    if (SkinManager.instance.ActiveVictoryConditions == 1)
                    {
                        if (isVictoryResult)
                        {
                            victoryConditionsText.text = "15 min";
                        }
                        else
                        {
                            defeatVictoryConditionsText.text = "15 min";
                        }
                    }
                    else
                    {
                        if (SkinManager.instance.ActiveVictoryConditions == 2)
                        {
                            if (isVictoryResult)
                            {
                                victoryConditionsText.text = "30 min";
                            }
                            else
                            {
                                defeatVictoryConditionsText.text = "30 min";
                            }
                        }
                        else
                        {
                            if (SkinManager.instance.ActiveVictoryConditions == 3)
                            {
                                if (isVictoryResult)
                                {   
                                    victoryConditionsText.text = "20 points";
                                }
                                else
                                {
                                    defeatVictoryConditionsText.text = "20 points";
                                }
                            }
                            else
                                if (SkinManager.instance.ActiveVictoryConditions == 4)
                                {
                                    if (isVictoryResult)
                                    {
                                        victoryConditionsText.text = "100 points";
                                    }
                                    else
                                    {
                                        defeatVictoryConditionsText.text = "100 points";
                                    }
                                }
                        }
                    }
                }
                //Victory Panel
                if (isVictoryResult)
                {
                    victoryPanel.gameObject.transform.localScale = new Vector3(victoryPanelScale, victoryPanelScale, victoryPanelScale);
                }
                else
                {
                    defeatVictoryPanel.gameObject.transform.localScale = new Vector3(victoryPanelScale, victoryPanelScale, victoryPanelScale);
                }
            }
        }

        //infoText.text = GetPVPCommand().ToString() + ";" + GetPVPValue1().ToString() + ";" + GetPVPValue2().ToString() + ";" + GetPVPValue3().ToString();

        if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_AI)
        {
            if ((isHost) && (!GetIsHostTurn()))
            {
                if (AIActivityTime > 0)
                {
                    AIActivityTime -= Time.deltaTime;
                }
                else
                {
                    if (aiCommands.Count > 0)
                        RunAICommand(aiCommands[0]);
                    AIActivityTime = SkinManager.AI_ACTIVITY_TIME;
                }
            }
        }
        if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_PVP)
        {
            if ((GetPVPCommand() != PVP_IDLE) &&
                //(((isHostTurn) && (!isHost)) || ((!isHostTurn) && (isHost))))
                (((!isHostTurn) && (!isHost)) || ((isFirstTurn)&&(!isHost))||(!isHost))
                )
               
            {
                //if (isPVPCommandChange)
                if (AIActivityTime > 0)
                {
                    AIActivityTime -= Time.deltaTime;
                }
                else
                
                {
                    Debug.Log("RunPVPCommand:UPDATE");
                    RunPVPCommand();
                    //isPVPCommandChange = false;
                    SetPVPCommand(PVP_IDLE);
                    AIActivityTime = SkinManager.PVP_ACTIVITY_TIME;
                }
            }
        }
    }


    void DrawCoin()
    {        
       // GameObject coin = Instantiate(coinsPrefab);
       // coins.Add(coin);
        coinGlobal.gameObject.SetActive(true);
        coinGlobal.transform.SetParent(backgroundImage.transform, false);
        //Debug.Log(activeCard.transform.position);
        coinGlobal.transform.position = activeCard.transform.position;//new Vector3(0,10,0);// activeCard.transform.position;//.SetParent(hands.transform, false);
        //Debug.Log("DRAWCOIN");*/
    }
    
    void DrawPlayerCard()
    {
        if (playerCards.Count >= maxPlayerCards + playerCardsToDraw) return;
        GameObject card = Instantiate(playerCardPrefab);
        playerCards.Add(card);
        card.transform.SetParent(hands.transform, false);
        card.name = "PlayerCard" + playerCards.Count.ToString();
        RandomizePlayerCard(card);
//        RandomizePlayerCard2(card, RED_TEXT, 10);
    }

    void DrawPlayerCard(GameObject playerCardList, GameObject gridLayoutGroup)
    {

    }

    void DrawPlayerAICard()
    {
        Color cardColor;
        int AICardCount = playerAICardsRed.Count + playerAICardsGreen.Count + playerAICardsBlue.Count;
        if (AICardCount >= maxPlayerCards + playerCardsToDraw) return;
        GameObject card = Instantiate(playerCardPrefab);
       // playerAICards.Add(card);
        AICardCount++;
        card.transform.SetParent(handsP2.transform, false);
        card.name = "PlayerCard" + AICardCount.ToString();
        cardColor = RandomizePlayerCard(card);
        if (cardColor == redColor)
            playerAICardsRed.Add(card);
        else
            if (cardColor == greenColor)
                playerAICardsGreen.Add(card);
            else
                if (cardColor == blueColor)
                    playerAICardsBlue.Add(card);
        //        RandomizePlayerCard2(card, RED_TEXT, 10);
        card.transform.SetParent(handsP2.transform, false);
    }

    void DrawPlayerCard(string kolor, int colorValue)
    {
        if (playerCards.Count >= maxPlayerCards + playerCardsToDraw) return;
        GameObject card = Instantiate(playerCardPrefab);
        playerCards.Add(card);
        card.transform.SetParent(hands.transform, false);
        card.name = "PlayerCard" + playerCards.Count.ToString();
        RandomizePlayerCard(card, kolor, colorValue);
        //        RandomizePlayerCard2(card, RED_TEXT, 10);
    }

   /* void DrawPlayerCardNew()
    {
        if (actualPlayerCardsCount >= maxPlayerCards + playerCardsToDraw) return;
        for (int i = 0; i < maxActualPlayerCards; ++i)
        {
            card = playerCards[i];
            if (!card.gameObject.activeSelf)
            {
                card.transform.SetParent(hands.transform, false);
                card.transform.localScale = card.GetComponent<PlayerCard>().normalScale;
                card.name = "PlayerCard" + playerCards.Count.ToString();
                card.GetComponent<PlayerCard>().hideByColor = false;
                card.GetComponent<PlayerCard>().hasMultiply = false;
                card.gameObject.SetActive(true);
                RandomizePlayerCard(card);
                actualPlayerCardsCount++;
                break;
            }
        }
    }*/

    /*void CreatePlayerCards()
    {
        for (int i = 0; i < maxActualPlayerCards; ++i)
        {
            GameObject card = Instantiate(playerCardPrefab);
            playerCards.Add(card);
            card.transform.SetParent(hands.transform, false);
            card.transform.localScale = card.GetComponent<PlayerCard>().normalScale;
            card.name = "PlayerCard" + playerCards.Count.ToString();
            card.gameObject.SetActive(false);
        }
    }*/

    void CreateTaskCards()
    {
        //Debug.Log("maxTaskCardsAddLate:"+maxTaskCardsAddLate);
        //tasks.SetActive(false);
        tasks.gameObject.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        for (int i = 0; i < maxTaskCardsAddLate + taskCardsToDraw; ++i)
        //for (int i = 0; i < maxActualTaskCards; ++i)
        {
            GameObject card = Instantiate(taskCardPrefab);
            //card.gameObject.SetActive(true);
            taskCards.Add(card);
            card.transform.SetParent(tasks.transform, false);
            card.name = "TaskCard" + taskCards.Count.ToString();
            card.gameObject.SetActive(false);
                if (!card.gameObject.activeSelf)//dla wyswietlania STATIC cards
                {
                    card.gameObject.SetActive(true);
                    card.transform.SetParent(tasks.transform, false);
                    RandomizeTaskCard(card);
                    actualTaskCardsCount++;
                }
            
            //DrawTaskCard();
            //DiscardTaskCard(card);
        }
        
    }

    void DrawTaskCardOld()
    {
        if (taskCards.Count >= maxTaskCards + taskCardsToDraw) return;
        GameObject card = Instantiate(taskCardPrefab);
        taskCards.Add(card);
        card.transform.SetParent(tasks.transform, false);
        card.name = "TaskCard"+ taskCards.Count.ToString();
    }

    void DrawTaskCard(string color, int colorValue) //, GameObject whichTasks)
    {
        //actualTaskCardsCount
       
        if (actualTaskCardsCount >= maxTaskCards + taskCardsToDraw) return;
        // GameObject card = Instantiate(taskCardPrefab);
        // taskCards.Add(card);
        for (int i = 0; i < maxActualTaskCards; ++i)
        {
            card = taskCards[i];
            if (!card.gameObject.activeSelf)
            {
                card.transform.SetParent(tasks.transform, false);
                //               card.name = "TaskCard" + taskCards.Count.ToString();
                //RandomizeTaskCard(card);

                card.gameObject.SetActive(true);
                //card.GetComponent<TaskCard>().Randomize();
                SetValueTaskCard(card, color, colorValue);
                actualTaskCardsCount++;
                break;
            }
        }
        //Debug.Log("Zmieniona TaskCard");
    }

    void DrawTaskCard()
    {
        //Debug.Log("actualTaskCardsCount:" + actualTaskCardsCount);
        //Debug.Log("maxTaskCards:" + maxTaskCards);
        //Debug.Log("taskCardsToDraw:" + taskCardsToDraw);
        //actualTaskCardsCount
        if (actualTaskCardsCount >= maxTaskCards + taskCardsToDraw) return;
       // GameObject card = Instantiate(taskCardPrefab);
       // taskCards.Add(card);
        for (int i = 0; i < maxActualTaskCards; ++i)
        {
            card = taskCards[i];
            if (!card.gameObject.activeSelf)
            {
                card.gameObject.SetActive(true);
                card.transform.SetParent(tasks.transform, false);
 //               card.name = "TaskCard" + taskCards.Count.ToString();
                //RandomizeTaskCard(card);
                
                card.gameObject.SetActive(true);
                //card.GetComponent<TaskCard>().Randomize();
                RandomizeTaskCard(card);
                actualTaskCardsCount++;
                Debug.Log("SetPVPCommands, HostTurn:" + GetIsHostTurn());
                if (((isHost) && (GetIsHostTurn())) || ((!isHost) && (!GetIsHostTurn())))
                {
                    if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_PVP)
                    {
                        //sendColor
                        
                        if (card.transform.Find("Value Text").GetComponent<TextMeshProUGUI>().color == redColor)
                        {
                            SetPVPValue1(PVP_RED);
                        }
                        else
                        {
                            if (card.transform.Find("Value Text").GetComponent<TextMeshProUGUI>().color == greenColor)
                            {
                                SetPVPValue1(PVP_GREEN);
                            }
                            else
                            {
                                SetPVPValue1(PVP_BLUE);
                            }
                        }
                        //sendValue                        
                        SetPVPValue2(int.Parse(card.transform.Find("Value Text").GetComponent<TextMeshProUGUI>().text));
                        //sendCommand
                        SetPVPCommand(PVP_DRAW_TASK);
                    }
                }
                break;
            }
        }
    }

    public void DiscardFirst()
    {
        DiscardTaskCard(taskCards[0]);
    }

    void SetValueTaskCard(GameObject card, string color, int colorValue)
    {
        //float rand = Random.Range(1, COLOR_NUMBER + 1);//to number of colors
        TaskCard localCard = card.GetComponent<TaskCard>();
        localCard.activeVideoPlayer.GetComponent<VideoPlayer>().targetTexture = localCard.ActiveTexture;
        localCard.tex = localCard.activeRawImage.GetComponent<RawImage>();
        localCard.tex.texture = localCard.ActiveTexture;

        localCard.valueText.text = colorValue.ToString();

        //Debug.Log("kolor:" + color + "," + RED_TEXT);
        if (color == RED_TEXT)
        {
            localCard.valueText.color = new Color32(SkinManager.RED_COLOR, 0, 0, 255);
            applyTaskCardSkin(localCard, RED_TEXT);
            localCard.colorText.text = GameManager.RED_TEXT;
        }
        else
        {
            if (color == GREEN_TEXT)
            {
                localCard.valueText.color = new Color32(0, SkinManager.GREEN_COLOR, 0, 255);
                localCard.colorText.text = GameManager.GREEN_TEXT;
                applyTaskCardSkin(localCard, GREEN_TEXT);
            }
            else
            {
                localCard.valueText.color = new Color32(0, 0, SkinManager.BLUE_COLOR, 255);
                localCard.colorText.text = GameManager.BLUE_TEXT;
                applyTaskCardSkin(localCard, BLUE_TEXT);
            }
        }
        localCard.victoryPointsText.text = (int.Parse(localCard.valueText.text) / 10).ToString();
    }

    void RandomizeTaskCard(GameObject card)
    {
        float rand = Random.Range(1, COLOR_NUMBER + 1);//to number of colors
        TaskCard localCard = card.GetComponent<TaskCard>();
        /* if (isPreset)
             rand = presetColor;*/
        localCard.activeVideoPlayer.GetComponent<VideoPlayer>().targetTexture = localCard.ActiveTexture;

    /*    Transform[] children;
         children = card.GetComponentsInChildren<Transform>();
         for (int i = 0; i < children.Length; ++i)
         {
             GameObject child = card.transform.GetChild(i).gameObject;

             Debug.Log(child);
             //Debug.Log(card.GetComponentInChildren<RawImage>());//OK!
         }*/
        //cardPowerUp.transform.Find("Red Text").GetComponent<TextMeshProUGUI>().color
        localCard.tex = localCard.activeRawImage.GetComponent<RawImage>();//transform.Find("RawImage").GetComponent<RawImage>();
        localCard.tex.texture = localCard.ActiveTexture;


        if (GetVictoryPoints() < earlyGamePoint)
        {
            localCard.valueText.text = Random.Range(10, earlyGameTaskCardMax).ToString();
            /* if (isPreset)
                 valueText.text = presetTask.ToString();*/
        }
        else
        {
            if (GetVictoryPoints() < middleGamePoint)
            {
                localCard.valueText.text = Random.Range(earlyGameTaskCardMax, middleGameTaskCardMax).ToString();
                /*if (isPreset)
                    valueText.text = presetTask.ToString();*/
            }
            else //lateGamePoint
            {
                localCard.valueText.text = Random.Range(middleGameTaskCardMax, lateGameTaskCardMax).ToString();
                /* if (isPreset)
                     valueText.text = presetTask.ToString();*/
            }
        }


        if (rand <= 1)
        {
            localCard.valueText.color = new Color32(SkinManager.RED_COLOR, 0, 0, 255);
           // applySkin(GameManager.RED_TEXT, false);
            applyTaskCardSkin(localCard, RED_TEXT);
            localCard.colorText.text = GameManager.RED_TEXT;
        }
        else
        {
            if (rand <= 2)
            {
                localCard.valueText.color = new Color32(0, SkinManager.GREEN_COLOR, 0, 255);
                localCard.colorText.text = GameManager.GREEN_TEXT;
               // applySkin(GameManager.GREEN_TEXT, false);
                applyTaskCardSkin(localCard, GREEN_TEXT);
                //localCard.applySkin(GREEN_TEXT, true);
            }
            else
            {
                localCard.valueText.color = new Color32(0, 0, SkinManager.BLUE_COLOR, 255);
                localCard.colorText.text = GameManager.BLUE_TEXT;
                //applySkin(GameManager.BLUE_TEXT, false);
                applyTaskCardSkin(localCard,BLUE_TEXT);
                //localCard.applySkin(BLUE_TEXT, false);
            }
        }
        localCard.victoryPointsText.text = (int.Parse(localCard.valueText.text) / 10).ToString();
    }

    Color RandomizePlayerCard(GameObject card, string kolor, int colorValue)
    {
        PlayerCard localCard = card.GetComponent<PlayerCard>();
        string SubStr;
        localCard.hasMultiply = false;
        //float rand = Random.Range(1, COLOR_NUMBER + 1);//to number of colors
        localCard.additionText.text = colorValue.ToString();
        
        localCard.frameImage.sprite = Resources.Load<Sprite>(SkinManager.instance.ramki[SkinManager.instance.ActiveFrame].Name);
        if (kolor == RED_TEXT)
        {
            localCard.additionText.color = new Color32(SkinManager.RED_COLOR, 0, 0, 255);

            if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == KARTA_DYNAMICZNA)
            {
                SubStr = SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name;
                SubStr = SubStr.Substring(0, SubStr.Length - 1);
                localCard.activeImage.GetComponent<Image>().sprite = wybranyRedStatic;// Resources.Load(SubStr + RED_TEXT, typeof(Sprite)) as Sprite;
            }
            else
                if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == KARTA_STATYCZNA)
                {
                    SubStr = SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name;
                    localCard.activeImage.GetComponent<Image>().sprite = wybranyRedStatic;// Resources.Load(SubStr + RED_TEXT, typeof(Sprite)) as Sprite;
                    
                }
        }
        else
        {
            if (kolor == GREEN_TEXT)
            {
                localCard.additionText.color = new Color32(0, SkinManager.GREEN_COLOR, 0, 255);
                if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == KARTA_DYNAMICZNA)
                {
                    SubStr = SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name;
                    SubStr = SubStr.Substring(0, SubStr.Length - 1);
                    localCard.activeImage.GetComponent<Image>().sprite = wybranyGreenStatic;// Resources.Load(SubStr + GREEN_TEXT, typeof(Sprite)) as Sprite;
                }
                else
                    if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == KARTA_STATYCZNA)
                    {
                        SubStr = SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name;
                        localCard.activeImage.GetComponent<Image>().sprite = wybranyGreenStatic;// Resources.Load(SubStr + GREEN_TEXT, typeof(Sprite)) as Sprite;
                        //Debug.Log(SubStr);
                    }
            }
            else
            {
                localCard.additionText.color = new Color32(0, 0, SkinManager.BLUE_COLOR, 255);
                if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == KARTA_DYNAMICZNA)
                {
                    SubStr = SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name;
                    SubStr = SubStr.Substring(0, SubStr.Length - 1);

                    localCard.activeImage.GetComponent<Image>().sprite = wybranyBlueStatic;// Resources.Load(SubStr + BLUE_TEXT, typeof(Sprite)) as Sprite;
                }
                else
                    if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == KARTA_STATYCZNA)
                    {
                        SubStr = SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name;
                        localCard.activeImage.GetComponent<Image>().sprite = wybranyBlueStatic;// Resources.Load(SubStr + BLUE_TEXT, typeof(Sprite)) as Sprite;
                       // Debug.Log(SubStr);
                    }
                
            }
            
        }
        return localCard.additionText.color;
    }

    Color RandomizePlayerCard(GameObject card)
    {
        PlayerCard localCard = card.GetComponent<PlayerCard>();
        string SubStr;
        localCard.hasMultiply = false;
        float rand = Random.Range(1, COLOR_NUMBER + 1);//to number of colors
        if (GetVictoryPoints() < earlyGamePoint)
        {
            localCard.additionText.text = Random.Range(1, earlyGamePlayerCardMax).ToString();
        }
        else
        {
            if (GetVictoryPoints() < middleGamePoint)
            {
                if (Random.Range(1, 100) <= earlyChanceOnMiddle)
                {
                    localCard.additionText.text = Random.Range(1, earlyGamePlayerCardMax).ToString();
                }
                else
                {
                    localCard.additionText.text = Random.Range(earlyGamePlayerCardMax, middleGamePlayerCardMax).ToString();
                }
            }
            else //lateGamePoint
            {

                if (Random.Range(1, 100) <= earlyChanceOnLate)
                {
                    localCard.additionText.text = Random.Range(1, earlyGamePlayerCardMax).ToString();
                }
                else
                {
                    if (Random.Range(1, 100) <= middleChanceOnLate)
                    {
                        localCard.additionText.text = Random.Range(earlyGamePlayerCardMax, middleGamePlayerCardMax).ToString();
                    }
                    else
                    {
                        localCard.additionText.text = Random.Range(middleGamePlayerCardMax, lateGamePlayerCardMax).ToString();
                    }
                }

            }
        }

        localCard.frameImage.sprite = Resources.Load<Sprite>(SkinManager.instance.ramki[SkinManager.instance.ActiveFrame].Name);

        if (rand <= 1)
        {
            localCard.additionText.color = new Color32(SkinManager.RED_COLOR, 0, 0, 255);

            if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == KARTA_DYNAMICZNA)
            {
                SubStr = SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name;
                SubStr = SubStr.Substring(0, SubStr.Length - 1);
                localCard.activeImage.GetComponent<Image>().sprite = wybranyRedStatic;// Resources.Load(SubStr + RED_TEXT, typeof(Sprite)) as Sprite;
                //Debug.Log(SubStr);
            }
            else
                if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == KARTA_STATYCZNA)
                {
                    SubStr = SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name;
                    localCard.activeImage.GetComponent<Image>().sprite = wybranyRed;// Resources.Load(SubStr + RED_TEXT, typeof(Sprite)) as Sprite;
                    //Debug.Log(SubStr);
                }
        }
        else
        {
            if (rand <= 2)
            {
                localCard.additionText.color = new Color32(0, SkinManager.GREEN_COLOR, 0, 255);
                if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == KARTA_DYNAMICZNA)
                {
                    SubStr = SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name;
                    SubStr = SubStr.Substring(0, SubStr.Length - 1);
                    localCard.activeImage.GetComponent<Image>().sprite = wybranyGreenStatic;// Resources.Load(SubStr + GREEN_TEXT, typeof(Sprite)) as Sprite;
                    //Debug.Log(SubStr);
                }
                else
                    if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == KARTA_STATYCZNA)
                    {
                        SubStr = SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name;
                        localCard.activeImage.GetComponent<Image>().sprite = wybranyGreen;// Resources.Load(SubStr + GREEN_TEXT, typeof(Sprite)) as Sprite;
                        //Debug.Log(SubStr);
                    }
            }
            else
            {
                localCard.additionText.color = new Color32(0, 0, SkinManager.BLUE_COLOR, 255);
                if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == KARTA_DYNAMICZNA)
                {
                    SubStr = SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name;
                    SubStr = SubStr.Substring(0, SubStr.Length - 1);

                    localCard.activeImage.GetComponent<Image>().sprite = wybranyBlueStatic;// Resources.Load(SubStr + BLUE_TEXT, typeof(Sprite)) as Sprite;
                    //Debug.Log(SubStr);
                }
                else
                    if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == KARTA_STATYCZNA)
                    {
                        SubStr = SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name;
                        localCard.activeImage.GetComponent<Image>().sprite = wybranyBlue;// Resources.Load(SubStr + BLUE_TEXT, typeof(Sprite)) as Sprite;
                        //Debug.Log(SubStr);
                    }
            }
        }
        return localCard.additionText.color;
    }

    void applyTaskCardSkin(TaskCard taskCard, string Kolor )
    {
        int pm;
        //SpriteRenderer spriteR;
        //spriteR = taskCard.GetComponent<Image>();

        taskCard.activeRawImage.GetComponent<RawImage>().material.SetTexture("_SecondaryTex", wybranaRamka);//Resources.Load<Texture2D>(SkinManager.instance.ramki[SkinManager.instance.ActiveFrame].Name));//do shadera
        taskCard.activeImage.GetComponent<Image>().material.SetTexture("_SecondaryTex", wybranaRamka);//Resources.Load<Texture2D>(SkinManager.instance.ramki[SkinManager.instance.ActiveFrame].Name));
        //Debug.Log("In");
        if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == KARTA_DYNAMICZNA)
        {
            taskCard.activeRawImage.GetComponent<RawImage>().gameObject.SetActive(true);
            taskCard.activeImage.GetComponent<Image>().gameObject.GetComponent<Image>().sprite = wybranyBlack;// Resources.Load<Sprite>("Black");
            taskCard.activeVideoPlayer.GetComponent<VideoPlayer>().gameObject.SetActive(true);
#if HTML5
//#if UNITY_WEBGL 
            taskCard.activeVideoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,SkinManager.instance.skorki[0].Name + Kolor + ".mp4");
            taskCard.activeVideoPlayer.url = "http://100plus.ieti.pl/public_html/StreamingAssets/Explodes" + Kolor + ".mp4";
//#endif
#endif
            //taskCard.activeVideoPlayer.GetComponent<VideoPlayer>().clip = Resources.Load(SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + Kolor) as VideoClip;
            if (Kolor == RED_TEXT)
            {
                taskCard.activeVideoPlayer.GetComponent<VideoPlayer>().clip = wybranyClipRed;
            }
            else
                if (Kolor == GREEN_TEXT)
                {
                    taskCard.activeVideoPlayer.GetComponent<VideoPlayer>().clip = wybranyClipGreen;
                }
                else
                {
                    taskCard.activeVideoPlayer.GetComponent<VideoPlayer>().clip = wybranyClipBlue;
                }

            pm = (int)Mathf.Round(Random.Range(0.0f, (float)taskCard.activeVideoPlayer.GetComponent<VideoPlayer>().length));
            taskCard.activeVideoPlayer.GetComponent<VideoPlayer>().frame = pm;
            taskCard.activeVideoPlayer.GetComponent<VideoPlayer>().Play();
        }

        if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == GameManager.KARTA_STATYCZNA)
        {
            //taskCard.activeImage.SetAllDirty();
            taskCard.activeRawImage.GetComponent<RawImage>().gameObject.SetActive(false);
            taskCard.activeImage.GetComponent<Image>().gameObject.SetActive(true);
            //taskCard.activeImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + Kolor);
            
            if (Kolor == RED_TEXT)
            {
                taskCard.activeImage.GetComponent<Image>().sprite = wybranyRed;
                //Debug.Log("Red");
            }
            else
                if (Kolor == GREEN_TEXT)
                {
                    taskCard.activeImage.GetComponent<Image>().sprite = wybranyGreen;
                    //spriteR.sprite = wybranyGreen;
                    //Debug.Log("Green");
                }
                else
                {
                    taskCard.activeImage.GetComponent<Image>().sprite = wybranyBlue;
                    //spriteR.sprite = wybranyBlue;
                    //Debug.Log("Blue");
                }
            //taskCard.activeImage.SetAllDirty();
            //Debug.Log(taskCard.activeImage.GetComponent<Image>().sprite);
            //gameObject.GetComponent<Image>()
           // taskCard.activeImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + Kolor);
           // Debug.Log(wybranyBlue);
        }
    }

    /*void DrawTaskCardPreset()
    {
        if (taskCards.Count >= maxTaskCards + taskCardsToDraw) return;
        GameObject card = Instantiate(taskCardPrefab);
        taskCards.Add(card);
        card.transform.SetParent(tasks.transform, false);
        card.name = "TaskCard" + taskCards.Count.ToString();
    }*/

    void DrawPowerUpCard()
    {
        if (powerUpCards.Count >= maxPowerUpCards + powerUpCardsToDraw) return;
        GameObject card = Instantiate(powerUpCardPrefab);
        powerUpCards.Add(card);
        card.transform.SetParent(powerUps.transform, false);
        card.name = "PowerUpCard" + powerUpCards.Count.ToString();
        RandomizePowerUpCard(card);
    }

    void DrawPowerUpCard(int redValue, int greenValue, int blueValue)
    {
        if (powerUpCards.Count >= maxPowerUpCards + powerUpCardsToDraw) return;
        GameObject card = Instantiate(powerUpCardPrefab);
        powerUpCards.Add(card);
        card.transform.SetParent(powerUps.transform, false);
        card.name = "PowerUpCard" + powerUpCards.Count.ToString();
        RandomizePowerUpCard(card, redValue, greenValue, blueValue);
    }

    void DrawPowerUpAICard()
    {
        if (powerUpAICards.Count >= maxPowerUpCards + powerUpCardsToDraw) return;
        GameObject card = Instantiate(powerUpCardPrefab);
        powerUpAICards.Add(card);
        card.transform.SetParent(handsP2.transform, false);
        //card.transform.SetParent(powerUps.transform, false);
        card.name = "PowerUpCard" + powerUpCards.Count.ToString();
        RandomizePowerUpCard(card);
    }

    void RandomizePowerUpCard(GameObject card)
    {
        int rand;
        PowerUpCard localCard = card.GetComponent<PowerUpCard>();
        if (float.Parse(victoryPoints.text) < middleGamePoint)
        {
            rand = Random.Range(2, 4);
            localCard.redText.text = rand.ToString();
            localCard.greenText.text = rand.ToString();
            localCard.blueText.text = rand.ToString();
        }
        else //lateGamePoint
        {
            localCard.redText.text = Random.Range(1, 6).ToString();
            localCard.greenText.text = Random.Range(1, 6).ToString();
            localCard.blueText.text = Random.Range(1, 6).ToString();
        }
    }
   

    void RandomizePowerUpCard(GameObject card, int redValue, int greenValue, int blueValue)
    {
        PowerUpCard localCard = card.GetComponent<PowerUpCard>();
        localCard.redText.text = redValue.ToString();
        localCard.greenText.text = greenValue.ToString();
        localCard.blueText.text = blueValue.ToString();
    }

    public void DiscardCoin(GameObject coin)
    {
        if (coin.transform.GetComponent<Coins>() != null)
        {
            
            coins.Remove(coin);
            coins.Clear();
            Destroy(coin);
        }
    }

    public void DiscardTaskCardOLD(GameObject card)
    {
        if (card.transform.GetComponent<TaskCard>() != null)
        {
            trashSFX.Play();
            
            taskCards.Remove(card);
            Destroy(card);
        }
    }

    public void DiscardTaskCard(GameObject cardToRemove)
    {
        trashSFX.Play();
        //Debug.Log("Running DiscardTaskCard");
        for (int i = 0; i < maxActualTaskCards; ++i)
        {
            card = taskCards[i];
            if (card == cardToRemove)
            {
                Debug.Log("Discard:isHostTurn" + GetIsHostTurn());
                Debug.Log("Discard:isHost" + isHost);
                if (((isHost) && (GetIsHostTurn())) || ((!isHost) && (!GetIsHostTurn())))
                {
                    if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_PVP)
                    {
                        SetPVPValue1(i);
                        SetPVPCommand(PVP_DISCARD_TASK);
                        
                    }
                }

                card.GetComponent<TaskCard>().activeVideoPlayer.GetComponent<VideoPlayer>().Stop();
                //card.transform.SetParent(null, false);
                card.gameObject.SetActive(false);
                actualTaskCardsCount--;
                break;
            }
        }
    }

    public void DiscardPlayerCard(GameObject card)
    {
        if (card.transform.GetComponent<PlayerCard>() != null)
        {
            trashSFX.Play();
            
            playerCards.Remove(card);
            Destroy(card);
        }
    }

/*    public void DiscardPlayerCardNew(GameObject cardToRemove)
    {
        trashSFX.Play();
        for (int i = 0; i < maxActualPlayerCards; ++i)
        {
            card = playerCards[i];
            if (card == cardToRemove)
            {
                card.transform.SetParent(null, false);
                card.gameObject.SetActive(false);
                actualPlayerCardsCount--;
                break;
            }
        }
    }*/

    public void DiscardPowerUpCard(GameObject card)
    {
        if (card.transform.GetComponent<PowerUpCard>() != null)
        {
            trashSFX.Play();
            
            powerUpCards.Remove(card);
            Destroy(card);
        }
    }

    public void CheckCardNumbers(bool taskEnable)
    {
        
        //Debug.Log(actualTaskCardsCount + ">" + maxTaskCards);
        //if (playerCards.Count > maxPlayerCards || taskCards.Count > maxTaskCards || powerUpCards.Count > maxPowerUpCards)
        if (playerCards.Count > maxPlayerCards || actualTaskCardsCount > maxTaskCards || powerUpCards.Count > maxPowerUpCards)            
        {
            if (playerCards.Count > maxPlayerCards)
            {
                hands.SetActive(true);//tutaj
                //tasks.SetActive(false);
                tasks.SetActive(true);
                //powerUps.SetActive(false);
                transparentPlayerCardPanel.SetActive(false);
                transparentPowerUpCardPanel.SetActive(true);
                transparentButton.gameObject.SetActive(false);
            }
            else
            {
                if (powerUpCards.Count > maxPowerUpCards)
                {
                    powerUps.SetActive(true);
                    //hands.SetActive(false);
                    //tasks.SetActive(false);
                    transparentPlayerCardPanel.SetActive(true);
                    transparentPowerUpCardPanel.SetActive(false);
                    transparentButton.gameObject.SetActive(false);
                }
                else //trash tasks
                {
                    
                    tasks.SetActive(true);
                    //hands.SetActive(false);
                    //powerUps.SetActive(false);
                    transparentPlayerCardPanel.SetActive(true);
                    transparentPowerUpCardPanel.SetActive(true);
                    transparentButton.gameObject.SetActive(false);

                    for (int i = 0; i < maxActualTaskCards; ++i)//taskCards.Count; ++i)
                    {
                        card = taskCards[i];
                        card.transform.Find("Kosz").gameObject.SetActive(true);
                    }
                }
            }
            trashArea.SetActive(true);
            endTurnBtn.SetActive(false);
        }
        else
        {
            if (trashArea.activeSelf)
                endTurnBtn.SetActive(true);
            trashArea.SetActive(false);
            if (!collectPointsBtn.gameObject.activeSelf)
            {
                transparentPlayerCardPanel.SetActive(true);
                transparentPowerUpCardPanel.SetActive(true);
                transparentButton.gameObject.SetActive(false);
            }
            else
            {
                transparentPlayerCardPanel.SetActive(false);
                transparentPowerUpCardPanel.SetActive(false);
                transparentButton.gameObject.SetActive(true);
            }
            if (!hands.activeSelf)
            {
                hands.SetActive(true);
            }
            powerUps.SetActive(true);
            if (taskEnable)
            {
                tasks.SetActive(true);
                for (int i = 0; i < maxActualTaskCards;++i)//taskCards.Count; ++i)
                {
                    card = taskCards[i];
                    card.transform.Find("Kosz").gameObject.SetActive(false);
                }
            }
            
        }
    }

    public void EndTurn()
    {
        playerTurnTime = maxPlayerTurnInSeconds;
        // if (((isHost) && (isHostTurn)) || ((!isHost) && (!isHostTurn)))
        if (((isHost) && (GetIsHostTurn())) || ((!isHost) && (!GetIsHostTurn())))
        {
            /*if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_SOLO)
            {
                SetIsHostTurn(true);
                endTurnBtn.gameObject.SetActive(true);
                //isHostTurn = true;
            }
            else
            {
                SetIsHostTurn(!GetIsHostTurn());
                endTurnBtn.gameObject.SetActive(GetIsHostTurn());
                //isHostTurn = !isHostTurn;
                if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_AI)
                {
                    iaTurnImage.gameObject.SetActive(true);
                    p2TurnImage.gameObject.SetActive(false);                  
                }
                else //different PVP modes
                {
                    iaTurnImage.gameObject.SetActive(false);
                    p2TurnImage.gameObject.SetActive(true);
                }
            }*/

            userActivityTime = SkinManager.MAX_USER_DISACTIVITY;
            endTurnSFX.Play();
            for (int i = 0; i < playerCardsToDraw; i++)
            {
                if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_AI)
                {
                    DrawPlayerAICard();//player1                    
                }
                else
                {
                    DrawPlayerCard();//player1
                }
            }

            for (int i = 0; i < taskCardsToDraw; i++)
            {
                //if (SkinManager.instance.ActivePlayerMode != GAME_CONDITION_AI)
                {
                    DrawTaskCard();//player1
                    //Debug.Log("DrawTask IsHost++");
                }
                //DrawTaskCard(BLUE_TEXT, 11);//for player2(Client) to set it 
            }

            if (GetVictoryPoints() >= earlyGamePoint)
                for (int i = 0; i < powerUpCardsToDraw; i++)
                {
                    if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_AI)
                    {
                        DrawPowerUpAICard();                  
                    }
                    else
                    {
                        DrawPowerUpCard();//player1
                    }
                    //DrawPowerUpCard(3, 4, 5);//for player2(Client) to set it 
                }
            if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_AI)
            {
                if (SkinManager.instance.AIDifficulty == SkinManager.AI_EASY)
                {
                    AIEasyPlay();
                    AIActivityTime = SkinManager.AI_ACTIVITY_TIME;
                }
                else
                {
                    if (SkinManager.instance.AIDifficulty == SkinManager.AI_IMPOSSIBLE)
                    {
                        //
                    }
                }
            }

            //zmiana isHostTurn
            if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_SOLO)
            {
                SetIsHostTurn(true);
                endTurnBtn.gameObject.SetActive(true);
                //isHostTurn = true;
            }
            else
            {
                SetIsHostTurn(!GetIsHostTurn());
                endTurnBtn.gameObject.SetActive(GetIsHostTurn());
                //isHostTurn = !isHostTurn;
                if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_AI)
                {
                    iaTurnImage.gameObject.SetActive(true);
                    p2TurnImage.gameObject.SetActive(false);
                }
                else //different PVP modes
                {
                    iaTurnImage.gameObject.SetActive(false);
                    p2TurnImage.gameObject.SetActive(true);
                }
            }
            //zmiana isHostTurn
          //  Debug.Log("IsHost:" + isHost);
           // Debug.Log("IsHostTurn:" + isHostTurn);
            if ((SkinManager.instance.ActivePlayerMode != GAME_CONDITION_PVP)  //||
           // (((isHost)&&(!isHostTurn))||((!isHost)&&(isHostTurn)))
                )
            {
              //  Debug.Log("CheckCardNumbers");
                RerollTaskCardCheck();
                CheckCardNumbers(true);
            }
        }
        else//not my turn, opposite turn
        {
            //AI mode
            if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_AI)
            {
                if ((isHost) && (!GetIsHostTurn()))
                {
                    SetIsHostTurn(!GetIsHostTurn());
                    endTurnBtn.gameObject.SetActive(GetIsHostTurn());
                    iaTurnImage.gameObject.SetActive(false);
                    p2TurnImage.gameObject.SetActive(false);

                    for (int i = 0; i < playerCardsToDraw; i++)
                    {
                        DrawPlayerCard();                       
                    }

                    for (int i = 0; i < taskCardsToDraw; i++)
                    {
                        DrawTaskCard();//player1   
                        //Debug.Log("DrawTask Isn't HostTurn");
                    }

                    if (GetVictoryPoints() >= earlyGamePoint)
                        for (int i = 0; i < powerUpCardsToDraw; i++)
                        {
                            DrawPowerUpCard();                            
                        }
                    RerollTaskCardCheck();
                    CheckCardNumbers(true);
                }
            }
            else//PVP modes
            {

            }
            
        }
        AIActivityTime = SkinManager.AI_ACTIVITY_TIME;

        if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_PVP)
        {
            AIActivityTime = SkinManager.PVP_ACTIVITY_TIME;
        }

        isFirstTurn = false;
    }

    void AddAchievementPurePoint(float Value)
    {
        float pomoc = SkinManager.instance.PureSolo + Value;
        if (SkinManager.instance.ActivePlayerMode == 0)
        {
            PlayerPrefs.SetFloat("PureSolo", pomoc);
            SkinManager.instance.SetPureSolo(pomoc);
            if (!SkinManager.instance.isPureSolo1)
            {
                if (SkinManager.instance.PureSolo >= SkinManager.ACHIEVEMENT_PURE_1ST)
                {
                    PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.PURE1KSOLO].ID, true ? 1 : 0);
                    SkinManager.instance.SetIsPureSolo1(true);
                    AddCash(SkinManager.instance.osiagniecia[SkinManager.PURE1KSOLO].Reward);
                    ShowAchievementPanel(SkinManager.PURE1KSOLO);
                }
            }
            else
            {
                if (!SkinManager.instance.isPureSolo2)
                {
                    if (SkinManager.instance.PureSolo >= SkinManager.ACHIEVEMENT_PURE_2ND)
                    {
                        PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.PURE2KSOLO].ID, true ? 1 : 0);
                        SkinManager.instance.SetIsPureSolo2(true);
                        AddCash(SkinManager.instance.osiagniecia[SkinManager.PURE2KSOLO].Reward);
                        ShowAchievementPanel(SkinManager.PURE2KSOLO);
                    }
                }
                else
                {
                    if (!SkinManager.instance.isPureSolo3)
                    {
                        if (SkinManager.instance.PureSolo >= SkinManager.ACHIEVEMENT_PURE_3RD)
                        {
                            PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.PURE4KSOLO].ID, true ? 1 : 0);
                            SkinManager.instance.SetIsPureSolo3(true);
                            AddCash(SkinManager.instance.osiagniecia[SkinManager.PURE4KSOLO].Reward);
                            ShowAchievementPanel(SkinManager.PURE4KSOLO);
                            // PlayerPrefs.SetInt("IsVictoryTimePass", true ? 1 : 0);
                        }
                    }
                }
            }
        }
        else
        {
            if (SkinManager.instance.ActivePlayerMode == 1)
            {
                PlayerPrefs.SetFloat("PureSI", SkinManager.instance.PureSI + Value);
                SkinManager.instance.SetPureSI(SkinManager.instance.PureSI + Value);
                if (!SkinManager.instance.isPureSI1)
                {
                    if (SkinManager.instance.PureSI >= SkinManager.ACHIEVEMENT_PURE_1ST)
                    {
                        PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.PURE1KSI].ID, true ? 1 : 0);
                        SkinManager.instance.SetIsPureSI1(true);
                        AddCash(SkinManager.instance.osiagniecia[SkinManager.PURE1KSI].Reward);
                        ShowAchievementPanel(SkinManager.PURE1KSI);
                    }
                }
                else
                {
                    if (!SkinManager.instance.isPureSI2)
                    {
                        if (SkinManager.instance.PureSI >= SkinManager.ACHIEVEMENT_PURE_2ND)
                        {
                            PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.PURE2KSI].ID, true ? 1 : 0);
                            SkinManager.instance.SetIsPureSI2(true);
                            AddCash(SkinManager.instance.osiagniecia[SkinManager.PURE2KSI].Reward);
                            ShowAchievementPanel(SkinManager.PURE2KSI);
                        }
                    }
                    else
                    {
                        if (!SkinManager.instance.isPureSI3)
                        {
                            if (SkinManager.instance.PureSI >= SkinManager.ACHIEVEMENT_PURE_3RD)
                            {
                                PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.PURE4KSI].ID, true ? 1 : 0);
                                SkinManager.instance.SetIsPureSI3(true);
                                AddCash(SkinManager.instance.osiagniecia[SkinManager.PURE4KSI].Reward);
                                ShowAchievementPanel(SkinManager.PURE4KSI);
                            }
                        }
                    }
                }
            }
            else
            {
                if (SkinManager.instance.ActivePlayerMode == 2)
                {
                    PlayerPrefs.SetFloat("PurePVP", SkinManager.instance.PurePVP + Value);
                    SkinManager.instance.SetPurePVP(SkinManager.instance.PurePVP + Value);
                    if (!SkinManager.instance.isPurePVP1)
                    {
                        if (SkinManager.instance.PurePVP >= SkinManager.ACHIEVEMENT_PURE_1ST)
                        {
                            PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.PURE1KPVP].ID, true ? 1 : 0);
                            SkinManager.instance.SetIsPurePVP1(true);
                            AddCash(SkinManager.instance.osiagniecia[SkinManager.PURE1KPVP].Reward);
                            ShowAchievementPanel(SkinManager.PURE1KPVP);
                        }
                    }
                    else
                    {
                        if (!SkinManager.instance.isPurePVP2)
                        {
                            if (SkinManager.instance.PurePVP >= SkinManager.ACHIEVEMENT_PURE_2ND)
                            {
                                PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.PURE2KPVP].ID, true ? 1 : 0);
                                SkinManager.instance.SetIsPurePVP2(true);
                                AddCash(SkinManager.instance.osiagniecia[SkinManager.PURE2KPVP].Reward);
                                ShowAchievementPanel(SkinManager.PURE2KPVP);
                            }
                        }
                        else
                        {
                            if (!SkinManager.instance.isPurePVP3)
                            {
                                if (SkinManager.instance.PurePVP >= SkinManager.ACHIEVEMENT_PURE_3RD)
                                {
                                    PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.PURE4KPVP].ID, true ? 1 : 0);
                                    SkinManager.instance.SetIsPurePVP3(true);
                                    AddCash(SkinManager.instance.osiagniecia[SkinManager.PURE4KPVP].Reward);
                                    ShowAchievementPanel(SkinManager.PURE4KPVP);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void AddAchievementNotPurePoint(float Value)
    {
        float pomoc = SkinManager.instance.NotPureSolo + Value;
        //Debug.Log("player mode:" + SkinManager.instance.ActivePlayerMode);
        if (SkinManager.instance.ActivePlayerMode == 0)
        {
           // Debug.Log("BEFORE notPureSolo:" + SkinManager.instance.NotPureSolo);
            //Debug.Log("pom:" + pomoc);
            SkinManager.instance.SetNotPureSolo(pomoc);
            PlayerPrefs.SetFloat("NotPureSolo", pomoc);
            
           // Debug.Log("AFTER notPureSolo:" + SkinManager.instance.NotPureSolo);
           // Debug.Log("pom:" + pomoc);
            if (!SkinManager.instance.isNotPureSolo1)
            {
                if (SkinManager.instance.NotPureSolo >= SkinManager.ACHIEVEMENT_NOT_PURE_1ST)
                {
                    PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.NOTPURE1KSOLO].ID, true ? 1 : 0);
                    SkinManager.instance.SetIsNotPureSolo1(true);
                    AddCash(SkinManager.instance.osiagniecia[SkinManager.NOTPURE1KSOLO].Reward);
                    ShowAchievementPanel(SkinManager.NOTPURE1KSOLO);
                }
            }
            else
            {
                if (!SkinManager.instance.isNotPureSolo2)
                {
                    if (SkinManager.instance.NotPureSolo >= SkinManager.ACHIEVEMENT_NOT_PURE_2ND)
                    {
                        PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.NOTPURE5KSOLO].ID, true ? 1 : 0);
                        SkinManager.instance.SetIsNotPureSolo2(true);
                        AddCash(SkinManager.instance.osiagniecia[SkinManager.NOTPURE5KSOLO].Reward);
                        ShowAchievementPanel(SkinManager.NOTPURE5KSOLO);
                    }
                }
                else
                {
                    if (!SkinManager.instance.isNotPureSolo3)
                    {
                        if (SkinManager.instance.NotPureSolo >= SkinManager.ACHIEVEMENT_NOT_PURE_3RD)
                        {
                            PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.NOTPURE10KSOLO].ID, true ? 1 : 0);
                            SkinManager.instance.SetIsNotPureSolo3(true);
                            AddCash(SkinManager.instance.osiagniecia[SkinManager.NOTPURE10KSOLO].Reward);
                            ShowAchievementPanel(SkinManager.NOTPURE10KSOLO);
                        }
                    }
                }
            }

        }
        else
        {
            if (SkinManager.instance.ActivePlayerMode == 1)
            {
                PlayerPrefs.SetFloat("NotPureSI", SkinManager.instance.NotPureSI + Value);
                SkinManager.instance.SetNotPureSI(SkinManager.instance.NotPureSI + Value);
                if (!SkinManager.instance.isNotPureSI1)
                {
                    if (SkinManager.instance.NotPureSI >= SkinManager.ACHIEVEMENT_NOT_PURE_1ST)
                    {
                        PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.NOTPURE1KSI].ID, true ? 1 : 0);
                        SkinManager.instance.SetIsNotPureSI1(true);
                        AddCash(SkinManager.instance.osiagniecia[SkinManager.NOTPURE1KSI].Reward);
                        ShowAchievementPanel(SkinManager.NOTPURE1KSI);
                    }
                }
                else
                {
                    if (!SkinManager.instance.isNotPureSI2)
                    {
                        if (SkinManager.instance.NotPureSI >= SkinManager.ACHIEVEMENT_NOT_PURE_2ND)
                        {
                            PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.NOTPURE5KSI].ID, true ? 1 : 0);
                            SkinManager.instance.SetIsNotPureSI2(true);
                            AddCash(SkinManager.instance.osiagniecia[SkinManager.NOTPURE5KSI].Reward);
                            ShowAchievementPanel(SkinManager.NOTPURE5KSI);
                        }
                    }
                    else
                    {
                        if (!SkinManager.instance.isNotPureSI3)
                        {
                            if (SkinManager.instance.NotPureSI >= SkinManager.ACHIEVEMENT_NOT_PURE_3RD)
                            {
                                PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.NOTPURE10KSI].ID, true ? 1 : 0);
                                SkinManager.instance.SetIsNotPureSI3(true);
                                AddCash(SkinManager.instance.osiagniecia[SkinManager.NOTPURE10KSI].Reward);
                                ShowAchievementPanel(SkinManager.NOTPURE10KSI);
                            }
                        }
                    }
                }
            }
            else
            {
                if (SkinManager.instance.ActivePlayerMode == 2)
                {
                    PlayerPrefs.SetFloat("NotPurePVP", SkinManager.instance.NotPurePVP + Value);
                    SkinManager.instance.SetNotPurePVP(SkinManager.instance.NotPurePVP + Value);
                    if (!SkinManager.instance.isNotPurePVP1)
                    {
                        if (SkinManager.instance.NotPurePVP >= SkinManager.ACHIEVEMENT_NOT_PURE_1ST)
                        {
                            PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.NOTPURE1KPVP].ID, true ? 1 : 0);
                            SkinManager.instance.SetIsNotPurePVP1(true);
                            AddCash(SkinManager.instance.osiagniecia[SkinManager.NOTPURE1KPVP].Reward);
                            ShowAchievementPanel(SkinManager.NOTPURE1KPVP);
                        }
                    }
                    else
                    {
                        if (!SkinManager.instance.isNotPurePVP2)
                        {
                            if (SkinManager.instance.NotPurePVP >= SkinManager.ACHIEVEMENT_NOT_PURE_2ND)
                            {
                                PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.NOTPURE5KPVP].ID, true ? 1 : 0);
                                SkinManager.instance.SetIsNotPurePVP2(true);
                                AddCash(SkinManager.instance.osiagniecia[SkinManager.NOTPURE5KPVP].Reward);
                                ShowAchievementPanel(SkinManager.NOTPURE5KPVP);
                            }
                        }
                        else
                        {
                            if (!SkinManager.instance.isNotPurePVP3)
                            {
                                if (SkinManager.instance.NotPurePVP >= SkinManager.ACHIEVEMENT_NOT_PURE_3RD)
                                {
                                    PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.NOTPURE10KPVP].ID, true ? 1 : 0);
                                    SkinManager.instance.SetIsNotPurePVP3(true);
                                    AddCash(SkinManager.instance.osiagniecia[SkinManager.NOTPURE10KPVP].Reward);
                                    ShowAchievementPanel(SkinManager.NOTPURE10KPVP);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void CollectPoints(int aiPointsGet, int aiTaskNeed)
    {
        float Victory;
        double Pom;
        double aiResult, needed;

        if (aiPointsGet > aiTaskNeed)
        {
            aiResult = aiPointsGet;
            needed = aiTaskNeed;
            Pom = (double)((aiResult - needed) / (needed));
            //Debug.Log("Przekroczenie:aiPointsGet" + aiPointsGet + "," + aiTaskNeed + "=" + Pom);
            Victory = Mathf.Pow(0.5f, ((float)Pom));
            
            //Debug.Log("mnoznik:" + Victory);
            Pom = float.Parse(activeCard.transform.Find("Victory Points Text").GetComponent<TextMeshProUGUI>().text) * RESULT_PENALTY;
            Pom = Pom * Victory;

            Pom += GetVictoryPoints();
            Pom *= 100;
            Pom = Mathf.Round((float)Pom);
            Pom /= 100;
            SetVictoryPoints((float)Pom);
        }
        else
        {
            if (aiPointsGet == aiTaskNeed)
            {
                Pom = double.Parse(activeCard.transform.Find("Victory Points Text").GetComponent<TextMeshProUGUI>().text);
                Victory = GetVictoryPoints() + float.Parse(activeCard.transform.Find("Victory Points Text").GetComponent<TextMeshProUGUI>().text);
                SetVictoryPoints((float)Victory);
            }
        }
        if (isVictoryPointFirst)
        {
            //Debug.Log("Victory Point");
            if (SkinManager.instance.ActivePlayerMode != GAME_CONDITION_PVP)
            {
                if (GetVictoryPoints() >= VictoryPointFirstValue)
                {

                    isVictory = true;
                    if (float.Parse(victoryPoints.text) > float.Parse(victoryPointsP2.text))
                    {
                        isVictoryResult = true;
                        victorySFX.Play();
                    }
                    else
                    {
                        isVictoryResult = false;
                    }
                }
            }
            
            {
                if (SkinManager.instance.ActivePlayerMode == GAME_CONDITION_PVP)
                {
                    if ((float.Parse(victoryPoints.text) >= VictoryPointFirstValue) || (float.Parse(victoryPointsP2.text) >= VictoryPointFirstValue))
                    {
                        isVictory = true;
                        if (float.Parse(victoryPoints.text) == float.Parse(victoryPointsP2.text))
                        {
                            isVictoryResult = true;
                            victorySFX.Play();
                        }
                        else
                        {
                            if (((float.Parse(victoryPoints.text) > float.Parse(victoryPointsP2.text))&&(isHost))||
                            ((float.Parse(victoryPoints.text) < float.Parse(victoryPointsP2.text))&&(!isHost)))
                            {
                                isVictoryResult = true;
                                victorySFX.Play();
                            }
                            else
                            {
                                isVictoryResult = false;
                            }
                        }
                    }
                }
            }
        }
    }

    public void CollectPoints()
    {
        GameObject  cardToDiscard;
        TextMeshProUGUI valueText;
        TextMeshProUGUI kolorText;
        float Victory = 0;
        float Suma = 0;
        double Pom = 1.0;
        int Razy = 1;
        bool isScored = false;
        int multiplyCount = 0;
        int sumCardCount = 0;

        userActivityTime = SkinManager.MAX_USER_DISACTIVITY;
        //multiplyCount = 0;
        for (int i = 0; i < playerCards.Count; ++i)
        {
            card = playerCards[i];
            valueText = card.transform.Find("Parent Name").GetComponent<TextMeshProUGUI>();
            
            if (activeCard != null)
            {
                if (valueText.text == activeCard.name)
                {
                    sumCardCount++;
                    valueText = card.transform.Find("Addition Text").GetComponent<TextMeshProUGUI>();
                    for (int j = 0; j < powerUpCards.Count; ++j)  
                    {
                        cardPowerUp = powerUpCards[j];
                        
                        if (cardPowerUp.transform.Find("PowerUp Parent Name").GetComponent<TextMeshProUGUI>().text != "")
                            if (cardPowerUp.transform.Find("PowerUp Parent Name").GetComponent<TextMeshProUGUI>().text == card.name)
                            {
                                multiplyCount++;
                                if (valueText.color == cardPowerUp.transform.Find("Red Text").GetComponent<TextMeshProUGUI>().color)
                                {
                                    kolorText = cardPowerUp.transform.Find("Red Text").GetComponent<TextMeshProUGUI>();
                                    Razy = int.Parse(kolorText.text);
                                    break;
                                }
                                else
                                    if (valueText.color == cardPowerUp.transform.Find("Green Text").GetComponent<TextMeshProUGUI>().color)
                                    {
                                        kolorText = cardPowerUp.transform.Find("Green Text").GetComponent<TextMeshProUGUI>();
                                        Razy = int.Parse(kolorText.text);
                                        break;
                                    }
                                    else
                                        if (valueText.color == cardPowerUp.transform.Find("Blue Text").GetComponent<TextMeshProUGUI>().color)
                                        {
                                            kolorText = cardPowerUp.transform.Find("Blue Text").GetComponent<TextMeshProUGUI>();
                                            Razy = int.Parse(kolorText.text);
                                            break;
                                        }                           
                            }
                    }
                    Suma += int.Parse(valueText.text)*Razy;
                    Razy = 1;
                }
            }          
        }
        Victory = float.Parse(activeCard.transform.Find("Value Text").GetComponent<TextMeshProUGUI>().text);
        //Multiply Achievement
        if (!SkinManager.instance.MultiplyTwice)
        {
            if (multiplyCount == 2)
            {
                PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.MULTIPLYTWICE].ID, true ? 1 : 0);
                SkinManager.instance.SetMultiplyTwice(true);
                AddCash(SkinManager.instance.osiagniecia[SkinManager.MULTIPLYTWICE].Reward);
                ShowAchievementPanel(SkinManager.MULTIPLYTWICE);
            }
        }
        if (!SkinManager.instance.MultiplyThree)
        {
            if (multiplyCount == 3)
            {
                PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.MULTIPLYTHREE].ID, true ? 1 : 0);
                SkinManager.instance.SetMultiplyThree(true);
                AddCash(SkinManager.instance.osiagniecia[SkinManager.MULTIPLYTHREE].Reward);
                ShowAchievementPanel(SkinManager.MULTIPLYTHREE);
            }
        }
        if (!SkinManager.instance.MultiplyFour)
        {
            if (multiplyCount == 4)
            {
                PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.MULTIPLYFOUR].ID, true ? 1 : 0);
                SkinManager.instance.SetMultiplyFour(true);
                AddCash(SkinManager.instance.osiagniecia[SkinManager.MULTIPLYFOUR].Reward);
                ShowAchievementPanel(SkinManager.MULTIPLYFOUR);
            }
        }
        if (Victory == Suma)
        {
            isScored = true;
            Pom = double.Parse(activeCard.transform.Find("Victory Points Text").GetComponent<TextMeshProUGUI>().text);
            AddAchievementPurePoint((float)Pom);
            Victory = GetVictoryPoints() + float.Parse(activeCard.transform.Find("Victory Points Text").GetComponent<TextMeshProUGUI>().text);
            SetVictoryPoints((float)Victory);
            DrawCoin();
            if (Pom <= 1.0)
            {
                coinSingleSFX.Play();
                //DrawCoin();
            }
            else
            {
                /*for (int i = 0; i < Mathf.Round((float)Pom); ++i)
                {
                    DrawCoin();
                }*/
                if (Pom <= 3.0)
                {
                    coinFewSFX.Play();
                }
                else
                {
                    if (Pom <= 6.0)
                    {
                        coinMoreSFX.Play();
                    }
                    else
                    {
                        coinManySFX.Play();
                    }
                }
            }
            //discard cards -> do osobnej funkcji        
            for (int i = 0; i < playerCards.Count; ++i)
            {
                card = playerCards[i];
                valueText = card.transform.Find("Parent Name").GetComponent<TextMeshProUGUI>();
                if (valueText.text == activeCard.name)
                {
                    for (int j = 0; j < powerUpCards.Count; ++j)
                    {
                        cardPowerUp = powerUpCards[j];
                        if (cardPowerUp.transform.Find("PowerUp Parent Name").GetComponent<TextMeshProUGUI>().text != "")
                            if (cardPowerUp.transform.Find("PowerUp Parent Name").GetComponent<TextMeshProUGUI>().text == card.name)
                            {
                                DiscardPowerUpCard(cardPowerUp);
                            }
                    }
                }
            }
            for (int i = 0; i < playerCards.Count; ++i)
            {
                card = playerCards[i];
                valueText = card.transform.Find("Parent Name").GetComponent<TextMeshProUGUI>();
            
                if (activeCard != null)
                {
                    if (valueText.text == activeCard.name)
                    {
                        DiscardPlayerCard(card);
                    }
                }             
             }
            cardToDiscard = activeCard;
            SetActiveCard(activeCard, true);
            DiscardTaskCard(cardToDiscard);
            //bonus card
            if (GetVictoryPoints() < middleGamePoint)
            {
                DrawPlayerCard();
            }
            //DrawPlayerCard();
            CheckCardNumbers(true);
            //achievement LongWay
            if (sumCardCount >= 5)
                if (!SkinManager.instance.LongWay)
                {
                    PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.LONGWAY].ID, true ? 1 : 0);
                    SkinManager.instance.SetLongWay(true);
                    AddCash(SkinManager.instance.osiagniecia[SkinManager.LONGWAY].Reward);
                    ShowAchievementPanel(SkinManager.LONGWAY);
                }
        }
        else
        {
            if (Suma > Victory) 
            {
                isPureGame = false;
                isScored = true;
                Pom = (Suma - Victory) / (Victory);
                Victory = Mathf.Pow(0.5f, ((float)Pom));
                Pom = float.Parse(activeCard.transform.Find("Victory Points Text").GetComponent<TextMeshProUGUI>().text) * RESULT_PENALTY;
                Pom = Pom * Victory;
                AddAchievementNotPurePoint((float)Pom);
                DrawCoin();
                if (Pom <= 1.0)
                {
                    coinSingleSFX.Play();
                    //DrawCoin();
                }
                else
                {
                  /*  for (int i = 0; i < Mathf.Round((float)Pom); ++i)
                    {
                        DrawCoin();
                    }*/
                    if (Pom <= 3.0)
                    {
                        coinFewSFX.Play();
                        
                    }
                    else
                    {
                        if (Pom <= 6.0)
                        {
                            coinMoreSFX.Play();
                        }
                        else
                        {
                            coinManySFX.Play();
                        }
                    }
                }
                Pom += GetVictoryPoints();
                Pom *= 100;
                Pom = Mathf.Round((float)Pom);
                Pom /= 100;
                SetVictoryPoints((float)Pom);
               // Discard cards -> do osobnej funkcji
                
                for (int i = 0; i < playerCards.Count; ++i)
                {
                    card = playerCards[i];
                    valueText = card.transform.Find("Parent Name").GetComponent<TextMeshProUGUI>();
                    if (valueText.text == activeCard.name)
                    {
                        for (int j = 0; j < powerUpCards.Count; ++j)
                        {
                            cardPowerUp = powerUpCards[j];
                            if (cardPowerUp.transform.Find("PowerUp Parent Name").GetComponent<TextMeshProUGUI>().text != "")
                                if (cardPowerUp.transform.Find("PowerUp Parent Name").GetComponent<TextMeshProUGUI>().text == card.name)
                                {
                                    DiscardPowerUpCard(cardPowerUp);
                                }
                        }
                    }
                }
                for (int i = 0; i < playerCards.Count; ++i)
                {
                    card = playerCards[i];
                    valueText = card.transform.Find("Parent Name").GetComponent<TextMeshProUGUI>();

                    if (activeCard != null)
                    {
                        if (valueText.text == activeCard.name)
                        {
                            DiscardPlayerCard(card);
                        }
                    }
                }
                cardToDiscard = activeCard;
                SetActiveCard(activeCard, true);
                DiscardTaskCard(cardToDiscard);
                //bonuscard
                if (GetVictoryPoints() < earlyGamePoint)
                {
                    DrawPlayerCard();
                }
                CheckCardNumbers(true);
            }

        }

    //set Slidebar
         if (GetVictoryPoints() < earlyGamePoint)
        {
            
        }
        else
        {
            if (GetVictoryPoints() < middleGamePoint)
            {
                //achievement
                if (!SkinManager.instance.MiddlePass)
                {
                        PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.MIDDLEPASS].ID, true ? 1 : 0);
                        SkinManager.instance.SetMiddlePass(true);
                        AddCash(SkinManager.instance.osiagniecia[SkinManager.MIDDLEPASS].Reward);
                        ShowAchievementPanel(SkinManager.MIDDLEPASS);
                }
                if (!SkinManager.instance.FasterThanLightMiddle)
                {
                    if (timeFromStart < SkinManager.FASTER_THAN_LIGHT_MIDDLE)
                    {
                        PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.FASTERTHANLIGHTMIDDLE].ID, true ? 1 : 0);
                        SkinManager.instance.SetFasterThanLightMiddle(true);
                        AddCash(SkinManager.instance.osiagniecia[SkinManager.FASTERTHANLIGHTMIDDLE].Reward);
                        ShowAchievementPanel(SkinManager.FASTERTHANLIGHTMIDDLE);
                    }
                }
                slider.minValue = earlyGamePoint;
                slider.maxValue = middleGamePoint;
                maxPlayerCards = maxPlayerCardsAddMidle;
                maxTaskCards = maxTaskCardsAddMiddle;
            }
            else//lateGame
            {
                if (!SkinManager.instance.LatePass)
                {
                    PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.LATEPASS].ID, true ? 1 : 0);
                    SkinManager.instance.SetLatePass(true);
                    AddCash(SkinManager.instance.osiagniecia[SkinManager.LATEPASS].Reward);
                    ShowAchievementPanel(SkinManager.LATEPASS);
                }
                if (!SkinManager.instance.FasterThanLightLate)
                {
                    if (timeFromStart < SkinManager.FASTER_THAN_LIGHT_LATE)
                    {
                        PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.FASTERTHANLIGHTLATE].ID, true ? 1 : 0);
                        SkinManager.instance.SetFasterThanLightLate(true);
                        AddCash(SkinManager.instance.osiagniecia[SkinManager.FASTERTHANLIGHTLATE].Reward);
                        ShowAchievementPanel(SkinManager.FASTERTHANLIGHTLATE);
                    }
                }
                slider.minValue = middleGamePoint;
                slider.maxValue = lateGamePoint;
                maxPlayerCards = maxPlayerCardsAddLate;
                maxPowerUpCards = maxPowerUpCardsAddLate;
                maxTaskCards = maxTaskCardsAddLate;
            }
        }
        slider.value = GetVictoryPoints();
        //miss Sound
        if (!isScored)
        {
            coinMissSFX.Play();
        }
        //endGame
        if (isVictoryPointFirst)
        {
            //Debug.Log("Victory Point");
            if (GetVictoryPoints() >= VictoryPointFirstValue)
            {
                victorySFX.Play();
                isVictory = true;
                if (float.Parse(victoryPoints.text) > float.Parse(victoryPointsP2.text))
                {
                    isVictoryResult = true;
                }
                else
                {
                    isVictoryResult = false;
                }
            }
        }
    }
   
}
