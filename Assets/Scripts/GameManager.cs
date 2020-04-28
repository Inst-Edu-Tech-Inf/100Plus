﻿//#define HTML5

using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
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
    public const float COINS_RANGE_SQUARED = 5.0f * 5.0f;
    public string[] COLORS_ARRAY = new string[] { RED_TEXT , GREEN_TEXT, BLUE_TEXT };
    const float ACHIEVEMENT_PANEL_SMALL_SCALE = 0.5f;
    
    public GameObject activeCardSpace;
    public GameObject collectPointsBtn;
    public GameObject trashArea;
    public GameObject endTurnBtn;
    public GameObject tasks;
    public GameObject hands;
    public GameObject powerUps;
    public GameObject playerCardPrefab;
    public GameObject taskCardPrefab;
    public GameObject powerUpCardPrefab;
    public GameObject coinsPrefab;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI victoryPoints;
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
    public Text victoryScoreText;
    public Text victoryConditionsText;
    public GameObject victoryPanel;
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
    public Image scoreImage;
    public GameObject transparentPlayerCardPanel;
    public GameObject transparentPowerUpCardPanel;
    public GameObject closeConfirmationPanel;
    public Button transparentButton;


    List<GameObject> playerCards = new List<GameObject>();
    List<GameObject> taskCards = new List<GameObject>();
    public List<GameObject> powerUpCards = new List<GameObject>();
    List<GameObject> coins = new List<GameObject>();

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
    

    bool isVictoryPointFirst = false;
    bool isVictoryTimePass = true;
    bool isVictory = false;
    bool isVictorySound = true;
    int VictoryPointFirstValue = 20;
    float remainingGameTime = 300;
    float victoryPanelScale = 0.5f;
    float achievementPanelScale = ACHIEVEMENT_PANEL_SMALL_SCALE;
    float timeFromStart = 0.0f;
    bool isPureGame = true;

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

    

    void changeBackground()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            //backgroundImage.sprite = Resources.Load<Sprite>(SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name);
            string pom2 = Application.streamingAssetsPath + "/" + SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name + ".jpg";
            StartCoroutine(GetWWWTexture(pom2));
        }
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
                }       
            }
            collectPointsBtn.SetActive(true);
            endTurnBtn.SetActive(false);
            CheckCardNumbers(false);
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
                card = playerCards[i];
                card.SetActive(true);
               card.GetComponent<PlayerCard>().hasMultiply = false;
                card.transform.Find("Player Drop Panel").gameObject.SetActive(false);
                card.transform.Find("Parent Name").GetComponent<TextMeshProUGUI>().text = "";
                card.transform.SetParent(hands.transform);
            }
            collectPointsBtn.SetActive(false);
            endTurnBtn.SetActive(true);
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

    public void RerollClick()
    {
        GameObject cardTask = Instantiate(taskCardPrefab);
        int newCard = 0;
        float pom;

        if (!SkinManager.instance.Lucky)
        {
                PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.LUCKY].ID, true ? 1 : 0);
                SkinManager.instance.SetLucky(true);
                AddCash(SkinManager.instance.osiagniecia[SkinManager.LUCKY].Reward);
                ShowAchievementPanel(SkinManager.LUCKY);
        }
        
        newCard = taskCards.Count;
        for (int i = newCard - 1; i >= 0; --i)
        {
           // Debug.Log(i);
            cardTask = taskCards[i];
            DiscardTaskCard(cardTask);
        }
        for (int i = 0; i < newCard; ++i)
        {
            DrawTaskCard();
        }
        pom = float.Parse(victoryPoints.text);
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
        victoryPoints.text = pom.ToString("F2");
        rerollPanel.SetActive(false);
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
        GameObject cardTask = Instantiate(taskCardPrefab);
        //float percent = 0.0f;

        for (int i = 0; i < taskCards.Count; ++i)
        {
            cardTask = taskCards[i];
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

       // Debug.Log((float)threeCount / (float)maxTaskCards);
        //Debug.Log((float)twoCount / (float)maxTaskCards);
        //Debug.Log(percent);
        //percent = (float)oneCount / (float)maxTaskCards;
        if (((float)oneCount / (float)maxTaskCards >= SkinManager.REROLL_COLOR_RULE) ||
            ((float)twoCount / (float)maxTaskCards >= SkinManager.REROLL_COLOR_RULE) ||
            ((float)threeCount / (float)maxTaskCards >= SkinManager.REROLL_COLOR_RULE))
        {
            if (float.Parse(victoryPoints.text) < earlyGamePoint)
            {
                rerollCostText.text = "-" + SkinManager.REROLL_COST_EARLY.ToString() + " pkt";
            }
            else
            {
                if (float.Parse(victoryPoints.text) < middleGamePoint)
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

    private void Start()
    {
        victoryPanel.SetActive(false);
        achievementPanel.SetActive(false);
        transparentPlayerCardPanel.SetActive(true);
        transparentPowerUpCardPanel.SetActive(true);
        transparentButton.gameObject.SetActive(false);
        closeConfirmationPanel.gameObject.SetActive(false);
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
        //rerollPanel.SetActive(false);
        victoryPanel.gameObject.transform.localScale = new Vector3(victoryPanelScale, victoryPanelScale, victoryPanelScale);
        achievementPanel.gameObject.transform.localScale = new Vector3(achievementPanelScale, achievementPanelScale, achievementPanelScale);      
        isVictory = false;
        isVictorySound = true;
        userActivityTime = SkinManager.MAX_USER_DISACTIVITY;
       /* var fill = slider.GetComponentsInChildren<UnityEngine.UI.Image>().FirstOrDefault(t => t.name == "Fill");
        if (fill != null)
        {
            fill.color = Color.green;// Color.Lerp(Color.red, Color.green, 0.5f);
        }*/

        GameObject card = Instantiate(playerCardPrefab);
        GameObject cardPowerUp = Instantiate(powerUpCardPrefab);
        GameObject coin = Instantiate(coinsPrefab);

        for (int i = 0; i < powerUpCardsOnStart; i++)
        {
            DrawPowerUpCard();
        }

        for (int i = 0; i < playerCardsOnStart; i++)
        {
            DrawPlayerCard();
        }

        for (int i = 0; i < taskCardsOnStart; i++)
        {
            DrawTaskCard();
        }

        slider.maxValue = earlyGamePoint;
        slider.minValue = 0;
        slider.value = 0;
        Color color = new Color(0f / 255f, 255f / 255f, 0f / 255f);
        slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
        soundBackground.volume = SkinManager.instance.ActiveSoundValue / 100;
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
        remainingGameTime = maxGameTimeInSeconds;
        changeBackground();
        changeSound();
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
        if (remainingGameTime > 0)
        {
            remainingGameTime = Mathf.FloorToInt((maxGameTimeInSeconds) -= Time.deltaTime);
            timerText.text = remainingGameTime.ToString();
        }
        else
        {
            timerText.text = (VictoryPointFirstValue - float.Parse(victoryPoints.text)).ToString("F2");
            //Victory panel
            if ((isVictoryTimePass)||(isVictory))
            {
                //Debug.Log("Victory Time Pass");
                if (isVictorySound)
                {
                    victorySFX.Play();
                    isVictorySound = false;
                }
                victoryPanel.SetActive(true);
                if (SkinManager.instance.ActiveVictoryConditions > 2) //must collect 20 or 100 points
                {
                    if ((!SkinManager.instance.PureGame) && (isPureGame))
                    {
                            PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.PUREGAME].ID, true ? 1 : 0);
                            SkinManager.instance.SetPureGame(true);
                            AddCash(SkinManager.instance.osiagniecia[SkinManager.PUREGAME].Reward);
                            ShowAchievementPanel(SkinManager.PUREGAME);
                    }
                }
                closeText.gameObject.SetActive(true);
                if (victoryPanelScale < 1.0)
                {
                    victoryPanelScale += 0.01f;
                }
                
                    
                victoryText.text = VICTORY;
                victoryScoreText.text = ActualVictoryPointsText.text;
                //best result
                if (SkinManager.instance.BestResult < float.Parse(ActualVictoryPointsText.text))
                {
                    SkinManager.instance.SetBestResult(float.Parse(ActualVictoryPointsText.text));
                    PlayerPrefs.SetFloat("BestResult", float.Parse(ActualVictoryPointsText.text));
                }
                //victory achievement
                if (!SkinManager.instance.WinSolo)
                {
                    if (SkinManager.instance.ActivePlayerMode == 0)
                    {
                        PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.WINSOLO].ID, true ? 1 : 0); 
                        SkinManager.instance.SetWinSolo(true);
                        AddCash(SkinManager.instance.osiagniecia[SkinManager.WINSOLO].Reward);                      
                        ShowAchievementPanel(SkinManager.WINSOLO);
                    }
                }
                if (!SkinManager.instance.WinSI)
                {
                    if (SkinManager.instance.ActivePlayerMode == 1)
                    {
                        PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.WINSI].ID, true ? 1 : 0);
                        SkinManager.instance.SetWinSI(true);
                        AddCash(SkinManager.instance.osiagniecia[SkinManager.WINSI].Reward);
                        ShowAchievementPanel(SkinManager.WINSI);
                    }
                }
                if (!SkinManager.instance.WinPVP)
                {
                    if (SkinManager.instance.ActivePlayerMode == 2)
                    {
                        PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.WINPVP].ID, true ? 1 : 0);
                        SkinManager.instance.SetWinPVP(true);
                        AddCash(SkinManager.instance.osiagniecia[SkinManager.WINPVP].Reward);
                        ShowAchievementPanel(SkinManager.WINPVP);
                    }
                }
                

                if (SkinManager.instance.ActiveVictoryConditions == 0)
                {
                    victoryConditionsText.text = "5 min";
                }
                else
                {
                    if (SkinManager.instance.ActiveVictoryConditions == 1)
                    {
                        victoryConditionsText.text = "15 min";
                    }
                    else
                    {
                        if (SkinManager.instance.ActiveVictoryConditions == 2)
                        {
                            victoryConditionsText.text = "30 min";
                        }
                        else
                        {
                            if (SkinManager.instance.ActiveVictoryConditions == 3)
                            {
                                victoryConditionsText.text = "20 points";
                            }
                            else
                                if (SkinManager.instance.ActiveVictoryConditions == 4)
                                {
                                    victoryConditionsText.text = "100 points";
                                }
                        }
                    }
                }
                //Victory Panel
                victoryPanel.gameObject.transform.localScale = new Vector3(victoryPanelScale, victoryPanelScale, victoryPanelScale);
            }
        }
    }


    void DrawCoin()
    {        
       /* GameObject coin = Instantiate(coinsPrefab);
        coins.Add(coin);
        coin.transform.SetParent(backgroundImage.transform, false);
        //Debug.Log(activeCard.transform.position);
        coin.transform.position = activeCard.transform.position;//new Vector3(0,10,0);// activeCard.transform.position;//.SetParent(hands.transform, false);
        //Debug.Log("DRAWCOIN");*/
    }
    
    void DrawPlayerCard()
    {
        if (playerCards.Count >= maxPlayerCards + playerCardsToDraw) return;
        GameObject card = Instantiate(playerCardPrefab);
        playerCards.Add(card);
        card.transform.SetParent(hands.transform, false);
        card.name = "PlayerCard" + playerCards.Count.ToString();
    }

    void DrawTaskCard()
    {
        if (taskCards.Count >= maxTaskCards + taskCardsToDraw) return;
        GameObject card = Instantiate(taskCardPrefab);
        taskCards.Add(card);
        card.transform.SetParent(tasks.transform, false);
        card.name = "TaskCard"+ taskCards.Count.ToString();
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

    public void DiscardTaskCard(GameObject card)
    {
        if (card.transform.GetComponent<TaskCard>() != null)
        {
            trashSFX.Play();
            
            taskCards.Remove(card);
            Destroy(card);
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
        if (playerCards.Count > maxPlayerCards || taskCards.Count > maxTaskCards || powerUpCards.Count > maxPowerUpCards)
        {
            if (playerCards.Count > maxPlayerCards )
            {
                hands.SetActive(true);
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

                    for (int i = 0; i < taskCards.Count; ++i)
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
            //transparentPlayerCardPanel.SetActive(true);
            //transparentPowerUpCardPanel.SetActive(true);
            transparentButton.gameObject.SetActive(true);
            hands.SetActive(true);
            powerUps.SetActive(true);
            if (taskEnable)
            {
                tasks.SetActive(true);
                for (int i = 0; i < taskCards.Count; ++i)
                {
                    card = taskCards[i];
                    card.transform.Find("Kosz").gameObject.SetActive(false);
                }
            }
            
        }
    }

    public void EndTurn()
    {
        userActivityTime = SkinManager.MAX_USER_DISACTIVITY;
        endTurnSFX.Play();
       for (int i = 0; i < playerCardsToDraw; i++)
            DrawPlayerCard();

       for (int i = 0; i < taskCardsToDraw; i++)
           DrawTaskCard();

       if (float.Parse(victoryPoints.text) >= earlyGamePoint)
           for (int i = 0; i < powerUpCardsToDraw; i++)
               DrawPowerUpCard();

       RerollTaskCardCheck();
       CheckCardNumbers(true);
       
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
            Victory = (float.Parse(victoryPoints.text) + float.Parse(activeCard.transform.Find("Victory Points Text").GetComponent<TextMeshProUGUI>().text));
            victoryPoints.text = Victory.ToString("F2");
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
            if (float.Parse(victoryPoints.text) < middleGamePoint)
            {
                DrawPlayerCard();
            }
            //DrawPlayerCard();
            CheckCardNumbers(true);
            
        }
        else
        {
            if (Suma > Victory) 
            {
                isPureGame = false;
                isScored = true;
                Pom = (Suma - Victory) / (Victory);
                Victory = Mathf.Pow(0.5f, ((float)Pom));
                Pom = float.Parse(activeCard.transform.Find("Victory Points Text").GetComponent<TextMeshProUGUI>().text) * 0.75f;
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
                Pom += double.Parse(victoryPoints.text);
                Pom *= 100;
                Pom = Mathf.Round((float)Pom);
                Pom /= 100;
                victoryPoints.text = Pom.ToString("F2");
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
                if (float.Parse(victoryPoints.text) < middleGamePoint)
                {
                    DrawPlayerCard();
                }
                CheckCardNumbers(true);
            }

        }

    //set Slidebar
         if (float.Parse(victoryPoints.text) < earlyGamePoint)
        {
            
        }
        else
        {
            if (float.Parse(victoryPoints.text) < middleGamePoint)
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
        slider.value = float.Parse(victoryPoints.text);
        //miss Sound
        if (!isScored)
        {
            coinMissSFX.Play();
        }
        //endGame
        if (isVictoryPointFirst)
        {
            //Debug.Log("Victory Point");
            if (float.Parse(victoryPoints.text) >= VictoryPointFirstValue)
            {
                victorySFX.Play();
                isVictory = true;
            }
        }
    }
   
}
