using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Video;

public class TaskCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    TextMeshProUGUI valueText;
    TextMeshProUGUI victoryPointsText;
    TextMeshProUGUI colorText;
    GameManager gm;
    Image image;
    Vector3 originalPosition;
    RenderTexture ActiveTexture;
    RawImage tex; 

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //Instantiate 
        ActiveTexture = new RenderTexture(300, 600, 16);//OnDestroy free??
        
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
        if (gm.trashArea.activeSelf)
        {
            gm.DiscardTaskCard(this.gameObject);
            gm.CheckCardNumbers(true);
        }
        else
        {
            gm.SetActiveCard(gameObject, false);
        }
    }

    void Randomize()
    {
        float rand = Random.Range(1, 4);//to number of colors
        int pm;
        //ActiveTexture = gameObject.GetComponent<VideoPlayer>().targetTexture;
        gameObject.GetComponent<VideoPlayer>().targetTexture = ActiveTexture;
        tex = transform.Find("RawImage").GetComponent<RawImage>();
        tex.texture = ActiveTexture;
      //  valueText.text = Random.Range(11, 100).ToString();
        if (float.Parse(gm.victoryPoints.text) < gm.earlyGamePoint)
        {
            valueText.text = Random.Range(10, gm.earlyGameTaskCardMax).ToString();
        }
        else
        {
            if (float.Parse(gm.victoryPoints.text) < gm.middleGamePoint)
            {
                valueText.text = Random.Range(gm.earlyGameTaskCardMax, gm.middleGameTaskCardMax).ToString();
            }
            else //lateGamePoint
            {
                valueText.text = Random.Range(gm.middleGameTaskCardMax, gm.lateGameTaskCardMax).ToString();
            }
        }
        
        if (rand <= 1)
        {
            valueText.color = new Color32(255, 0, 0, 255);
            gameObject.GetComponent<Image>().sprite = Resources.Load("Red", typeof(Sprite)) as Sprite;


            //gameObject.GetComponent<VideoPlayer>().targetTexture.Release();
            gameObject.GetComponent<VideoPlayer>().clip = Resources.Load("Red") as VideoClip;
            //Resources.Load<VideoClip>(videoName) as VideoClip;

            pm = (int)Mathf.Round(Random.Range(0.0f, (float)gameObject.GetComponent<VideoPlayer>().length));
            gameObject.GetComponent<VideoPlayer>().frame = pm;// 30;// Random.Range(0.0f, 30.0f);//gameObject.GetComponent<VideoPlayer>().length));

            gameObject.GetComponent<VideoPlayer>().Play();

            colorText.text = "Red";
        }
        else
        {
            if (rand <= 2)
            {
                valueText.color = new Color32(0, 255, 0, 255);        
                gameObject.GetComponent<Image>().sprite = Resources.Load(gm.GREEN_TEXT, typeof(Sprite)) as Sprite;
                colorText.text = "Green";

                //gameObject.GetComponent<VideoPlayer>().targetTexture.Release();
                gameObject.GetComponent<VideoPlayer>().clip = Resources.Load("Green", typeof(VideoClip)) as VideoClip;

               //  gameObject.GetComponent<VideoPlayer>().frame = Mathf.Round(Random.Range(0,(float)(gameObject.GetComponent<VideoPlayer>().length)));

                pm = (int)Mathf.Round(Random.Range(0.0f, (float)gameObject.GetComponent<VideoPlayer>().length));
                gameObject.GetComponent<VideoPlayer>().frame = pm;

                gameObject.GetComponent<VideoPlayer>().Play();
            }
            else 
            {
                valueText.color = new Color32(0, 0, 255, 255);
                gameObject.GetComponent<Image>().sprite = Resources.Load("Blue", typeof(Sprite)) as Sprite;
                colorText.text = "Blue";
                //gameObject.GetComponent<VideoPlayer>().targetTexture.Release();
                gameObject.GetComponent<VideoPlayer>().clip = Resources.Load("Blue", typeof(VideoClip)) as VideoClip;

              //  gameObject.GetComponent<VideoPlayer>().frame = Mathf.Round(Random.Range(0, (float)(gameObject.GetComponent<VideoPlayer>().length)));
                pm = (int)Mathf.Round(Random.Range(0.0f, (float)gameObject.GetComponent<VideoPlayer>().length));
                gameObject.GetComponent<VideoPlayer>().frame = pm;
                gameObject.GetComponent<VideoPlayer>().Play();
            }
        }
        victoryPointsText.text = (int.Parse(valueText.text)/10).ToString();
    }

}
