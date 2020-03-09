﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PlayerCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    GameObject hands;
    GameManager gm;
    public GameObject ActualParent = null;
    Image image;
    Vector3 originalPosition;
    Vector2 normalScale = new Vector2(1.9f, 1.9f); 
    Vector2 biggerScale = new Vector2(2.2f, 2.2f); 
    TextMeshProUGUI additionText;
    public bool hasMultiply;
        
        

    void Start()
    {
        hands = GameObject.Find("Hands");
        hasMultiply = false;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        additionText = transform.Find("Addition Text").GetComponent<TextMeshProUGUI>();
        //image = GetComponent<Image>();
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
        gameObject.layer = 1;
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        originalPosition = transform.position;
        if (gameObject.transform.parent.name == "Hands")
        gameObject.transform.localScale = normalScale;
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        TextMeshProUGUI parentNameText;
        gameObject.layer = 0;

        if (pointerEventData.pointerCurrentRaycast.gameObject.name == "Trash")
        {
            gm.DiscardPlayerCard(this.gameObject);
            gm.CheckCardNumbers(true);
        }

        if (pointerEventData.pointerCurrentRaycast.gameObject != null && pointerEventData.pointerCurrentRaycast.gameObject.GetComponent<TaskCard>() != null)
        {
            Transform dropPanel = pointerEventData.pointerCurrentRaycast.gameObject.transform.Find("Drop Panel");
            gameObject.transform.SetParent(dropPanel);
            ActualParent = gm.activeCard;
            parentNameText = gameObject.transform.Find("Parent Name").GetComponent<TextMeshProUGUI>();
            parentNameText.text = ActualParent.name.ToString();
        }
        else
        {
            if (gameObject.transform.parent.name != "Hands")
            {
                gameObject.transform.SetParent(hands.transform);
                ActualParent = null;
                parentNameText = gameObject.transform.Find("Parent Name").GetComponent<TextMeshProUGUI>();
                parentNameText.text = "";
            }
            else
            {
                transform.position = originalPosition;
                ActualParent = null;
                parentNameText = gameObject.transform.Find("Parent Name").GetComponent<TextMeshProUGUI>();
                parentNameText.text = "";
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
