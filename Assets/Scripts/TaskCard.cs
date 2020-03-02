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
    Image image;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        valueText = transform.Find("Value Text").GetComponent<TextMeshProUGUI>();
        victoryPointsText = transform.Find("Victory Points Text").GetComponent<TextMeshProUGUI>();
        victoryPointsText.color = new Color32(255, 255, 0, 255);
        colorText = transform.Find("Color Text").GetComponent<TextMeshProUGUI>();

        image = GetComponent<Image>();

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
        float rand = Random.Range(1, 4);//to number of colors
        //gameObject.GetComponent<Image>().sprite = Resources.Load("Blue", typeof(Sprite)) as Sprite;
        valueText.text = Random.Range(1, 100).ToString();
        if (rand <= 1)
        {
            valueText.color = new Color32(255, 0, 0, 255);
            gameObject.GetComponent<Image>().sprite = Resources.Load("Red", typeof(Sprite)) as Sprite;
            colorText.text = "Red";
        }
        else
        {
            if (rand <= 2)
            {
                valueText.color = new Color32(0, 255, 0, 255);        
                gameObject.GetComponent<Image>().sprite = Resources.Load(gm.GREEN_TEXT, typeof(Sprite)) as Sprite;
                colorText.text = "Green";
            }
            else 
            {
                valueText.color = new Color32(0, 0, 255, 255);
                gameObject.GetComponent<Image>().sprite = Resources.Load("Blue", typeof(Sprite)) as Sprite;
                colorText.text = "Blue";
            }
        }
        victoryPointsText.text = (int.Parse(valueText.text)/10).ToString();

        /*
         *  public Color imageColorToBeUsed = Color.green;
     public float currentImageColorAlpha = 0.5f;
 
     // Use this for initialization
     void Start () {
         image = GetComponent<Image> ();
         //Get the alpha value of Color
         currentImageColorAlpha = image.color.a;
         //Assign Color
         image.color = imageColorToBeUsed;*/
    }

}
