using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PowerUpCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    GameObject powerUp;
    GameManager gm;
    public GameObject ActualParent = null;
    public AudioSource cardMissSFX;
    public AudioSource cardCorrectSFX;
    Image image;
    Vector3 originalPosition;
    Vector2 normalScale = new Vector2(1.9f, 1.9f);
    Vector2 biggerScale = new Vector2(2.2f, 2.2f);
    public TextMeshProUGUI redText;
    public TextMeshProUGUI greenText;
    public TextMeshProUGUI blueText;

    // Start is called before the first frame update
    void Start()
    {
        int rand;

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        powerUp = GameObject.Find("PowerUp");
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
        cardMissSFX.volume = SkinManager.instance.ActiveSFXValue / 100;
        cardCorrectSFX.volume = SkinManager.instance.ActiveSFXValue / 100;
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
        gameObject.layer = 0;
        gameObject.transform.localScale = normalScale;

        if (pointerEventData.pointerCurrentRaycast.gameObject.name == "Trash")
        {
            gm.DiscardPowerUpCard(this.gameObject);
            gm.CheckCardNumbers(true);
        }
        //Debug.Log(pointerEventData.pointerCurrentRaycast.gameObject.transform.parent.gameObject);
        if (pointerEventData.pointerCurrentRaycast.gameObject.transform.parent.gameObject != null && pointerEventData.pointerCurrentRaycast.gameObject.transform.parent.gameObject.GetComponent<PlayerCard>() != null
            && !pointerEventData.pointerCurrentRaycast.gameObject.transform.parent.gameObject.GetComponent<PlayerCard>().hasMultiply
            && pointerEventData.pointerCurrentRaycast.gameObject.transform.parent.gameObject.transform.Find("Parent Name").GetComponent<TextMeshProUGUI>().text != "")
        {
            cardCorrectSFX.Play();
            Transform dropPanel = pointerEventData.pointerCurrentRaycast.gameObject.transform.parent.gameObject.transform.Find("Player Drop Panel");
            gameObject.transform.SetParent(dropPanel);
            parentNameText = gameObject.transform.Find("PowerUp Parent Name").GetComponent<TextMeshProUGUI>();
            parentNameText.text = pointerEventData.pointerCurrentRaycast.gameObject.transform.parent.gameObject.name.ToString();
            ActualParent = pointerEventData.pointerCurrentRaycast.gameObject.transform.parent.gameObject;
            pointerEventData.pointerCurrentRaycast.gameObject.transform.parent.gameObject.GetComponent<PlayerCard>().hasMultiply = true;
        }
        else
        {
            if (!gm.trashArea.activeSelf)
                cardMissSFX.Play();
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
                if (ActualParent != null)
                    ActualParent.GetComponent<PlayerCard>().hasMultiply = false;
                ActualParent = null;
                transform.position = originalPosition;
                parentNameText = gameObject.transform.Find("PowerUp Parent Name").GetComponent<TextMeshProUGUI>();
                parentNameText.text = "";
            }
        }
        
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        gm.userActivityTime = SkinManager.MAX_USER_DISACTIVITY;
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

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // Debug.Log("powerupCardClick");
    }
}
