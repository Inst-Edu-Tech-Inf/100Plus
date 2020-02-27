using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TaskCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Transform droppedCardsArea;

    public int value;
    public int victoryPoints;

    [HideInInspector]
    public TextMeshProUGUI valueText;
    [HideInInspector]
    public TextMeshProUGUI victoryPointsText;
    [HideInInspector]
    public TextMeshProUGUI colorText;


    void Start()
    {
        valueText = transform.Find("Value Text").GetComponent<TextMeshProUGUI>();
        victoryPointsText = transform.Find("Victory Points Text").GetComponent<TextMeshProUGUI>();
        colorText = transform.Find("Color Text").GetComponent<TextMeshProUGUI>();

        Randomize();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerEnter)
        {
            GetComponent<Outline>().enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerEnter)
        {
            GetComponent<Outline>().enabled = false;
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GameManager.Instance.SetActiveCard(gameObject);
    }

    void Randomize()
    {
        value = Random.Range(1, 101);
        valueText.text = value.ToString();
        victoryPoints = Random.Range(1, 10);
        victoryPointsText.text = victoryPoints.ToString();
    }

}
