using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public  string RED_TEXT = "Red";
    public  string GREEN_TEXT = "Green";
    public  string BLUE_TEXT = "Blue";
    public  int COLOR_NUMBER = 3;
    public string[] COLORS_ARRAY = new string[] { "Red", "Green", "Blue" };
    //string[] names = { "Matt", "Joanne", "Robert" };
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

    List<GameObject> playerCards = new List<GameObject>();
    List<GameObject> taskCards = new List<GameObject>();
    List<GameObject> powerUpCards = new List<GameObject>();

    [Header("Game Settings"), SerializeField]
    int maxPlayerCards;
    [SerializeField]
    int maxTaskCards;
    [SerializeField]
    int maxPowerUpCards;
    [SerializeField]
    int playerCardsOnStart;
    [SerializeField]
    int taskCardsOnStart;
    [SerializeField]
    int powerUpCardsOnStart;
    [SerializeField]
    int playerCardsToDraw;
    [SerializeField]
    int taskCardsToDraw;
    [SerializeField]
    int powerUpCardsToDraw;
    [SerializeField]
    float maxGameTimeInMinutes;
    [SerializeField]
    public int earlyGamePoint;
    [SerializeField]
    public int middleGamePoint;
    [SerializeField]
    public int lateGamePoint;
    [SerializeField]
    public int earlyGamePlayerCardMax;
    [SerializeField]
    public int middleGamePlayerCardMax;
    [SerializeField]
    public int lateGamePlayerCardMax;
    [SerializeField]
    public int earlyGameTaskCardMax;
    [SerializeField]
    public int middleGameTaskCardMax;
    [SerializeField]
    public int lateGameTaskCardMax;

    public GameObject activeCard;

    float remainingGameTime = 3000;

  
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
        GameObject card = Instantiate(playerCardPrefab);
        GameObject cardPowerUp = Instantiate(powerUpCardPrefab);
       
        remainingGameTime = maxGameTimeInMinutes;

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
    }

    private void Update()
    {
        if (remainingGameTime > 0)
        {
           remainingGameTime = Mathf.FloorToInt((maxGameTimeInMinutes) -= Time.deltaTime);
           timerText.text = remainingGameTime.ToString();
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
            Destroy(card);
            taskCards.Remove(card);
        }
    }

    public void DiscardPlayerCard(GameObject card)
    {
        if (card.transform.GetComponent<PlayerCard>() != null)
        {
            Destroy(card);
            playerCards.Remove(card);
        }
    }

    public void DiscardPowerUpCard(GameObject card)
    {
        if (card.transform.GetComponent<PowerUpCard>() != null)
        {
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
            if (trashArea.active)
                endTurnBtn.SetActive(true);
            trashArea.SetActive(false);
            hands.SetActive(true);
            powerUps.SetActive(true);
            if (taskEnable)
                tasks.SetActive(true);
            
        }
    }

    public void EndTurn()
    {
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
            Victory = (float.Parse(victoryPoints.text) + float.Parse(activeCard.transform.Find("Victory Points Text").GetComponent<TextMeshProUGUI>().text));
            victoryPoints.text = Victory.ToString();
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
                Pom = (Suma - Victory) / (Victory);
                Victory = Mathf.Pow(0.5f, ((float)Pom));
                Pom = float.Parse(activeCard.transform.Find("Victory Points Text").GetComponent<TextMeshProUGUI>().text) * 0.75f;
                Pom = Pom * Victory;
                Debug.Log(Pom.ToString());
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

    }
}
