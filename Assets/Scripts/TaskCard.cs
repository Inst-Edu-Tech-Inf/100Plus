using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TaskCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    TextMeshProUGUI valueText;
    TextMeshProUGUI victoryPointsText;
    TextMeshProUGUI colorText;
    GameManager gm;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

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
        gm.SetActiveCard(gameObject);
    }

    void Randomize()
    {
        valueText.text = Random.Range(1, 101).ToString();
        victoryPointsText.text = Random.Range(1, 10).ToString();
    }

}
