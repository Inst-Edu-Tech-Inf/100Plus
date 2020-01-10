using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public GameObject activeCardSpace;
    public GameObject tasks;

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
}
