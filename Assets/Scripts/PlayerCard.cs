using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    GameObject hands;

    Vector3 originalPosition;
    Vector2 normalScale = new Vector2(1.8f, 1.8f); 
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
        if (gameObject.transform.parent == hands.transform)
        gameObject.transform.localScale = normalScale;
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        gameObject.layer = 0;

        GameObject hitObject = pointerEventData.pointerCurrentRaycast.gameObject;

        print(hitObject.name);

        if (hitObject != null && hitObject.tag == "Player Card Drop")
        {
            if (hitObject.transform.GetComponent<TaskCard>() != null)
            {
                gameObject.transform.SetParent(hitObject.transform.Find("Dropped Cards Area"));
            }
            else
            {
                gameObject.transform.SetParent(hitObject.transform.parent.Find("Dropped Cards Area"));
            }
        }
        else
        {
            if (gameObject.transform.parent != hands.transform)
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
        if (gameObject.transform.parent == hands.transform)
        {
            gameObject.transform.localScale = biggerScale;
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (gameObject.transform.parent == hands.transform)
        {
            gameObject.transform.localScale = normalScale;
        }
    }
}
