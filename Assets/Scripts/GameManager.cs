using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{   
    public GameObject activeCardSpace;
    public GameObject tasks;
    public GameObject hands;
    public GameObject playerCardPrefab;
    public GameObject taskCardPrefab;
    public TextMeshProUGUI timerText;

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
    }
}
