using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public GameObject activeCardSpace;
    public GameObject tasks;
    public GameObject hands;
    public GameObject playerCardPrefab;
    public GameObject taskCardPrefab;

    [Header("Game Settings"), SerializeField]
    int playerCardsOnStart;
    [SerializeField]
    int taskCardsOnStart;

    GameObject activeCard;

    public void SetActiveCard(GameObject card)
    {
        if (!activeCardSpace.activeInHierarchy)
        {
            tasks.SetActive(false);
            activeCard = card;
            activeCardSpace.SetActive(true);
            activeCard.transform.SetParent(activeCardSpace.transform);
            card.transform.Find("Drop Panel").gameObject.SetActive(true);
        }
        else
        {
            tasks.SetActive(true);
            activeCardSpace.SetActive(false);
            activeCard.transform.SetParent(tasks.transform);
            card.transform.Find("Drop Panel").gameObject.SetActive(false);
            activeCard = null;
        }
    }

    private void Start()
    {
        for (int i = 0; i < playerCardsOnStart; i++)
        {
            DrawPlayerCard();
        }

        for (int i = 0; i < taskCardsOnStart; i++)
        {
            DrawTaskCard();
        }
    }

    void DrawPlayerCard()
    {
        GameObject card = Instantiate(playerCardPrefab);
        card.transform.SetParent(hands.transform);
    }

    void DrawTaskCard()
    {
        GameObject card = Instantiate(taskCardPrefab);
        card.transform.SetParent(tasks.transform);
    }
}
