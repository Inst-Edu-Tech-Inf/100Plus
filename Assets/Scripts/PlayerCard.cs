using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    GameObject hands;

    Vector3 originalPosition;
    Vector2 normalScale = new Vector2(1.9f, 1.9f); 
    Vector2 biggerScale = new Vector2(2.2f, 2.2f); 

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
        originalPosition = transform.position;
        if (gameObject.transform.parent.name == "Hands")
        gameObject.transform.localScale = normalScale;
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
            if (gameObject.transform.parent.name != "Hands")
            {
                gameObject.transform.SetParent(hands.transform);
            }
            else
            {
                transform.position = originalPosition;
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (gameObject.transform.parent.name == "Hands")
        {
            gameObject.transform.localScale = biggerScale;
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (gameObject.transform.parent.name == "Hands")
        {
            gameObject.transform.localScale = normalScale;
        }
    }
}
