using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Card : MonoBehaviour, IDragHandler
{
    int value;
    int victoryPoints;
    CardColor color;

    TextMeshProUGUI valueText;
    TextMeshProUGUI victoryPointsText;
    TextMeshProUGUI colorText;

    void Start()
    {
        valueText = transform.Find("Value Text").GetComponent<TextMeshProUGUI>();
        victoryPointsText = transform.Find("Victory Points Text").GetComponent<TextMeshProUGUI>();
        colorText = transform.Find("Color Text").GetComponent<TextMeshProUGUI>();
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        transform.position = pointerEventData.position;
    }

    enum CardColor
    {
        red,
        blue,
        green,
    }
}
