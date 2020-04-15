//#define HTML5

using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//using System.Collections;

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
    public string[] COLORS_ARRAY = new string[] { RED_TEXT , GREEN_TEXT, BLUE_TEXT };
    
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
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI victoryPoints;
    public GameObject card;
    public GameObject cardPowerUp;
    public Slider slider;
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
    public TextMeshProUGUI ActualVictoryPointsText;
    public Image helpTask;
    public Image helpTurn;
    public Image helpCard;
    public Image helpEndTask;


    List<GameObject> playerCards = new List<GameObject>();
    List<GameObject> taskCards = new List<GameObject>();
    List<GameObject> powerUpCards = new List<GameObject>();

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
    
    void changeSound()
    {
        soundBackground.clip = (AudioClip)Resources.Load("Audio/" + SkinManager.instance.muzyki[SkinManager.instance.ActiveSound].Name); ;
        soundBackground.Play();
    }

    void changeBackground()
    {
        backgroundImage.sprite = Resources.Load<Sprite>("Background/" + SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name);//.Name
    }

    public void SetActiveCard(GameObject card, bool isBack)
    {
        TextMeshProUGUI valueText;
        Color taskColor;

        if (!activeCardSpace.activeInHierarchy && !isBack)
        {
            tasks.SetActive(false);
            activeCard = card;
            activeCardSpace.SetActive(true);
            activeCard.transform.SetParent(activeCardSpace.transform);
            card.transform.Find("Drop Panel").gameObject.SetActive(true);
            taskColor = activeCard.transform.Find("Value Text").GetComponent<TextMeshProUGUI>().color;
            for (int i = 0; i < playerCards.Count; ++i)
            {
                card = playerCards[i];
                valueText = card.transform.Find("Addition Text").GetComponent<TextMeshProUGUI>();
                card.transform.Find("Player Drop Panel").gameObject.SetActive(true);
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
            activeCardSpace.SetActive(false);
            activeCard.transform.SetParent(tasks.transform);
            card.transform.Find("Drop Panel").gameObject.SetActive(false);
            activeCard = null;
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

    private void Start()
    {
        victoryPanel.SetActive(false);
        victoryPanel.gameObject.transform.localScale = new Vector3(victoryPanelScale, victoryPanelScale, victoryPanelScale);
        isVictory = false;
        isVictorySound = true;
        userActivityTime = SkinManager.MAX_USER_DISACTIVITY;

        GameObject card = Instantiate(playerCardPrefab);
        GameObject cardPowerUp = Instantiate(powerUpCardPrefab);

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
        Color color = new Color(255f / 255f, 255f / 255f, 0f / 255f);
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

    private void Update()
    {
        if (userActivityTime > 0)
        {
            userActivityTime -= Time.deltaTime;
            helpTask.gameObject.SetActive(false);
            helpTurn.gameObject.SetActive(false);
            helpCard.gameObject.SetActive(false);
            helpEndTask.gameObject.SetActive(false);
        }//= SkinManager.MAX_USER_DISACTIVITY;
        else
        {
            if (tasks.activeSelf)
            {
                helpTask.gameObject.SetActive(true);
                helpTurn.gameObject.SetActive(true);
            }
            else
            {
                helpCard.gameObject.SetActive(true);
                helpEndTask.gameObject.SetActive(true);
            }
        }
        if (remainingGameTime > 0)
        {
            remainingGameTime = Mathf.FloorToInt((maxGameTimeInSeconds) -= Time.deltaTime);
            timerText.text = remainingGameTime.ToString();
        }
        else
        {
            if ((isVictoryTimePass)||(isVictory))
            {
                //Debug.Log("Victory Time Pass");
                if (isVictorySound)
                {
                    victorySFX.Play();
                    isVictorySound = false;
                }
                victoryPanel.SetActive(true);
                if (victoryPanelScale < 1.0)
                {
                    victoryPanelScale += 0.01f;
                }
                victoryText.text = VICTORY;
                victoryScoreText.text = ActualVictoryPointsText.text;
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

    void DrawPowerUpCard()
    {
        if (powerUpCards.Count >= maxPowerUpCards + powerUpCardsToDraw) return;
        GameObject card = Instantiate(powerUpCardPrefab);
        powerUpCards.Add(card);
        card.transform.SetParent(powerUps.transform, false);
        card.name = "PowerUpCard" + powerUpCards.Count.ToString();
    }

    public void DiscardTaskCard(GameObject card)
    {
        if (card.transform.GetComponent<TaskCard>() != null)
        {
            trashSFX.Play();
            Destroy(card);
            taskCards.Remove(card);
        }
    }

    public void DiscardPlayerCard(GameObject card)
    {
        if (card.transform.GetComponent<PlayerCard>() != null)
        {
            trashSFX.Play();
            Destroy(card);
            playerCards.Remove(card);
        }
    }

    public void DiscardPowerUpCard(GameObject card)
    {
        if (card.transform.GetComponent<PowerUpCard>() != null)
        {
            trashSFX.Play();
            Destroy(card);
            powerUpCards.Remove(card);
        }
    }

    public void CheckCardNumbers(bool taskEnable)
    {
        if (playerCards.Count > maxPlayerCards || taskCards.Count > maxTaskCards || powerUpCards.Count > maxPowerUpCards)
        {
            if (playerCards.Count > maxPlayerCards )
            {
                hands.SetActive(true);
                tasks.SetActive(false);
                powerUps.SetActive(false);
            }
            else
            {
                if ( taskCards.Count > maxTaskCards )
                {
                    tasks.SetActive(true);
                    hands.SetActive(false);
                    powerUps.SetActive(false);

                    for (int i = 0; i < taskCards.Count; ++i)
                    {
                        card = taskCards[i];
                        card.transform.Find("Kosz").gameObject.SetActive(true);                 
                    }
                }
                else
                {
                    powerUps.SetActive(true);
                    hands.SetActive(false);
                    tasks.SetActive(false);
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
        endTurnSFX.Play();
       for (int i = 0; i < playerCardsToDraw; i++)
            DrawPlayerCard();

       for (int i = 0; i < taskCardsToDraw; i++)
           DrawTaskCard();

       if (float.Parse(victoryPoints.text) >= earlyGamePoint)
           for (int i = 0; i < powerUpCardsToDraw; i++)
               DrawPowerUpCard();

       CheckCardNumbers(true);
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
        if (Victory == Suma)
        {
            isScored = true;
            Pom = double.Parse(activeCard.transform.Find("Victory Points Text").GetComponent<TextMeshProUGUI>().text);
            Victory = (float.Parse(victoryPoints.text) + float.Parse(activeCard.transform.Find("Victory Points Text").GetComponent<TextMeshProUGUI>().text));
            victoryPoints.text = Victory.ToString();
            if (Pom <= 1.0)
            {
                coinSingleSFX.Play();
            }
            else
            {
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
            CheckCardNumbers(true);
            
        }
        else
        {
            if (Suma > Victory) 
            {
                isScored = true;
                Pom = (Suma - Victory) / (Victory);
                Victory = Mathf.Pow(0.5f, ((float)Pom));
                Pom = float.Parse(activeCard.transform.Find("Victory Points Text").GetComponent<TextMeshProUGUI>().text) * 0.75f;
                Pom = Pom * Victory;
                if (Pom <= 1.0)
                {
                    coinSingleSFX.Play();
                }
                else
                {
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
                victoryPoints.text = Pom.ToString();
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
                slider.minValue = earlyGamePoint;
                slider.maxValue = middleGamePoint;
                maxPlayerCards = maxPlayerCardsAddMidle;
                maxTaskCards = maxTaskCardsAddMiddle;
            }
            else//lateGame
            {
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
