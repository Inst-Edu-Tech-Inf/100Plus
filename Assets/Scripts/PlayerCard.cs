using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    int addition;

    GameObject hands;

    void Start()
    {
        hands = GameObject.Find("Hands");
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        transform.position = pointerEventData.position;
        gameObject.layer = 2;
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {

    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        gameObject.layer = 0;

        if (pointerEventData.pointerCurrentRaycast.gameObject.GetComponent<TaskCard>() != null)
        {
            Transform dropPanel = pointerEventData.pointerCurrentRaycast.gameObject.transform.Find("Drop Panel");
            gameObject.transform.SetParent(dropPanel);
        }
        else
        {
            gameObject.transform.SetParent(hands.transform);
        }
    }
}
