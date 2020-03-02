using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PlayerCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    GameObject hands;
    GameManager gm;
    GameObject ActualParent = null;
    Image image;
    Vector3 originalPosition;
    Vector2 normalScale = new Vector2(1.9f, 1.9f); 
    Vector2 biggerScale = new Vector2(2.2f, 2.2f); 

    TextMeshProUGUI additionText;
        
        

    void Start()
    {
        hands = GameObject.Find("Hands");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        additionText = transform.Find("Addition Text").GetComponent<TextMeshProUGUI>();
        image = GetComponent<Image>();
        float rand = Random.Range(1, 4);//to number of colors
        additionText.text = Random.Range(1, 30).ToString();
        if (rand <= 1)
        {
            additionText.color = new Color32(255, 0, 0, 255);
            gameObject.GetComponent<Image>().sprite = Resources.Load("Red", typeof(Sprite)) as Sprite;
        }
        else
        {
            if (rand <= 2)
            {
                additionText.color = new Color32(0, 255, 0, 255);
                gameObject.GetComponent<Image>().sprite = Resources.Load(gm.GREEN_TEXT, typeof(Sprite)) as Sprite;
            }
            else
            {
                additionText.color = new Color32(0, 0, 255, 255);
                gameObject.GetComponent<Image>().sprite = Resources.Load("Blue", typeof(Sprite)) as Sprite;
            }
        }
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

        if (pointerEventData.pointerCurrentRaycast.gameObject != null && pointerEventData.pointerCurrentRaycast.gameObject.GetComponent<TaskCard>() != null)
        {
            Transform dropPanel = pointerEventData.pointerCurrentRaycast.gameObject.transform.Find("Drop Panel");
            gameObject.transform.SetParent(dropPanel);
            ActualParent = gm.activeCard;
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
