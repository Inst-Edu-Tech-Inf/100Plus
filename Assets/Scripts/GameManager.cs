using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject hands;
    
    [SerializeField]
    int victoryPoints = 0;

    [SerializeField]
    GameObject activeCardSpace;
    [SerializeField]
    GameObject tasks;
    [SerializeField]
    GameObject playerCardPrefab;
    [SerializeField]
    GameObject taskCardPrefab;
    [SerializeField] 
    TextMeshProUGUI timerText;
    [SerializeField]
    TextMeshProUGUI victoryPointsText;

    List<GameObject> playerCards = new List<GameObject>();
    List<GameObject> taskCards = new List<GameObject>();

    [Header("Game Settings"), SerializeField]
    int maxPlayerCards;
    [SerializeField]
    int maxTaskCards;
    [SerializeField]
    int playerCardsOnStart;
    [SerializeField]
    int taskCardsOnStart;
    [SerializeField]
    int playerCardsToDraw;
    [SerializeField]
    int taskCardsToDraw;
    [SerializeField]
    float maxGameTimeInMinutes;

    GameObject activeCard;

    float remainingGameTime;

    private void Awake()
    {
        Instance = this;
    }

    public void SetActiveCard(GameObject card)
    {
        if (!activeCardSpace.activeInHierarchy)
        {
            tasks.SetActive(false);
            activeCard = card;
            activeCardSpace.SetActive(true);
            activeCard.transform.SetParent(activeCardSpace.transform);
            card.transform.Find("Dropped Cards Area").gameObject.SetActive(true);
        }
        else
        {
            tasks.SetActive(true);
            activeCardSpace.SetActive(false);
            activeCard.transform.SetParent(tasks.transform);
            card.transform.Find("Dropped Cards Area").gameObject.SetActive(false);
            activeCard = null;
        }
    }

    private void Start()
    {
        remainingGameTime = maxGameTimeInMinutes;

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
        //if (remainingGameTime > 0)
        //{
        //    remainingGameTime = Mathf.FloorToInt((maxGameTimeInMinutes) -= Time.deltaTime);
        //    timerText.text = remainingGameTime.ToString();
        //}
    }

    void DrawPlayerCard()
    {
        if (playerCards.Count >= maxPlayerCards) return;
        GameObject card = Instantiate(playerCardPrefab);
        playerCards.Add(card);
        card.transform.SetParent(hands.transform, false);
    }

    void DrawTaskCard()
    {
        if (taskCards.Count >= maxTaskCards) return;
        GameObject card = Instantiate(taskCardPrefab);
        taskCards.Add(card);
        card.transform.SetParent(tasks.transform, false);
    }
   
    void DiscardTaskCard(GameObject card)
    {
        if (card.transform.GetComponent<TaskCard>() != null)
        {
            Destroy(card);
            taskCards.Remove(card);
        }
    }

    public void EndTurn()
    {
       for (int i = 0; i < playerCardsToDraw; i++)
            DrawPlayerCard();

       for (int i = 0; i < taskCardsToDraw; i++)
            DrawTaskCard();

        CalculateVictoryPoints();
    }

    void AddVictoryPoints(int value)
    {
        victoryPoints = victoryPoints + value;
        victoryPointsText.text = "VP: " + victoryPoints.ToString();
    }

    public void CalculateVictoryPoints()
    {
        int playerCardsValue = 0;
        Transform taskCardsContainer;

        if (activeCardSpace.gameObject.activeSelf)
            taskCardsContainer = activeCardSpace.transform;
        else
            taskCardsContainer = tasks.transform;

        foreach (Transform taskCardTransform in taskCardsContainer)
        {
            TaskCard taskCard = taskCardTransform.GetComponent<TaskCard>();
            PlayerCard playerCard;
            List<Transform> cardsToRemove = new List<Transform>();

            foreach (Transform playerCardTransform in taskCard.droppedCardsArea.transform)
            {
                playerCard = playerCardTransform.GetComponent<PlayerCard>();
                playerCardsValue += playerCard.addition;
                cardsToRemove.Add(playerCardTransform);
            }
            
            if (playerCardsValue >= taskCard.value)
            {
                AddVictoryPoints(taskCard.victoryPoints);
                foreach (Transform card in cardsToRemove)
                {
                    Destroy(card.gameObject);
                }
            }
        }
    }
}
