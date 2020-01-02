using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TaskCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    int value;
    int victoryPoints;

    TextMeshProUGUI valueText;
    TextMeshProUGUI victoryPointsText;
    TextMeshProUGUI colorText;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerEnter)
        {
            GetComponent<Outline>().enabled = true;
            transform.Find("Drop Panel").gameObject.SetActive(true);
        }
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerEnter)
        {
            GetComponent<Outline>().enabled = false;
            transform.Find("Drop Panel").gameObject.SetActive(false);
        }
    }


}
