using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PowerUpCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    GameObject powerUp;
    GameManager gm;
    public GameObject ActualParent = null;
    Image image;
    Vector3 originalPosition;
    Vector2 normalScale = new Vector2(1.9f, 1.9f);
    Vector2 biggerScale = new Vector2(2.2f, 2.2f);
    TextMeshProUGUI redText;
    TextMeshProUGUI greenText;
    TextMeshProUGUI blueText;

    // Start is called before the first frame update
    void Start()
    {
        int rand;

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        powerUp = GameObject.Find("PowerUp");
        redText = transform.Find("Red Text").GetComponent<TextMeshProUGUI>();
        greenText = transform.Find("Green Text").GetComponent<TextMeshProUGUI>();
        blueText = transform.Find("Blue Text").GetComponent<TextMeshProUGUI>();
       // redText.text = Random.Range(1, 4).ToString();
        //greenText.text = Random.Range(1, 4).ToString();
        //blueText.text = Random.Range(1, 4).ToString();
        if (float.Parse(gm.victoryPoints.text) < gm.earlyGamePoint)
        {
            
        }
        else
        {
            if (float.Parse(gm.victoryPoints.text) < gm.middleGamePoint)
            {
                rand = Random.Range(2, 4);
                redText.text = rand.ToString();
                greenText.text = rand.ToString();
                blueText.text = rand.ToString();
            }
            else //lateGamePoint
            {
                redText.text = Random.Range(1, 5).ToString();
                greenText.text = Random.Range(1, 5).ToString();
                blueText.text = Random.Range(1, 5).ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void OnDrag(PointerEventData pointerEventData)
    {
        transform.position = pointerEventData.position;
        gameObject.layer = 1;
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        originalPosition = transform.position;
        if (gameObject.transform.parent.name == "PowerUp")
            gameObject.transform.localScale = normalScale;
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        TextMeshProUGUI parentNameText;
        TextMeshProUGUI playerParentNameText;
        bool czyUstawic = false;
        gameObject.layer = 0;

        if (pointerEventData.pointerCurrentRaycast.gameObject.name == "Trash")
        {
            gm.DiscardPowerUpCard(this.gameObject);
            gm.CheckCardNumbers(true);
        }
        if (pointerEventData.pointerCurrentRaycast.gameObject != null && pointerEventData.pointerCurrentRaycast.gameObject.GetComponent<PlayerCard>() != null
            && !pointerEventData.pointerCurrentRaycast.gameObject.GetComponent<PlayerCard>().hasMultiply
            && pointerEventData.pointerCurrentRaycast.gameObject.transform.Find("Parent Name").GetComponent<TextMeshProUGUI>().text != "")
        {
            Transform dropPanel = pointerEventData.pointerCurrentRaycast.gameObject.transform.Find("Player Drop Panel");
            gameObject.transform.SetParent(dropPanel);
            parentNameText = gameObject.transform.Find("PowerUp Parent Name").GetComponent<TextMeshProUGUI>();
            parentNameText.text = pointerEventData.pointerCurrentRaycast.gameObject.name.ToString();
            ActualParent = pointerEventData.pointerCurrentRaycast.gameObject;
            pointerEventData.pointerCurrentRaycast.gameObject.GetComponent<PlayerCard>().hasMultiply = true;
        }
        else
        
           if (gameObject.transform.parent.name != "PowerUp")//
            {
                ActualParent.GetComponent<PlayerCard>().hasMultiply = false;
                ActualParent = null;
                gameObject.transform.SetParent(powerUp.transform);
                parentNameText = gameObject.transform.Find("PowerUp Parent Name").GetComponent<TextMeshProUGUI>();
                parentNameText.text = "";
            }
            else
            {
                ActualParent = null;
                transform.position = originalPosition;
                parentNameText = gameObject.transform.Find("PowerUp Parent Name").GetComponent<TextMeshProUGUI>();
                parentNameText.text = "";
            }
        
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (gameObject.transform.parent.name == "PowerUp")
        {
            gameObject.transform.localScale = biggerScale;
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (gameObject.transform.parent.name == "PowerUp")
        {
            gameObject.transform.localScale = normalScale;
        }
    }
}
