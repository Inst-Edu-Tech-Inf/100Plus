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
    //SkinManager skinManager;
    public RawImage activeRawImage;
    public VideoPlayer activeVideoPlayer;
    int activeSkin;
    int activeRamka;
    public Texture m_MainTexture, m_Normal, m_Metal;
    Renderer m_Renderer;

    void applySkin(string Kolor, bool isAwake)
    {
        int pm;
        //Debug.Log(skorki[ActiveSkin].Name);
        if (!isAwake)
        {
            if (activeRawImage != null)
            activeRawImage = gameObject.GetComponent<RawImage>();
            if (activeVideoPlayer != null)
            activeVideoPlayer = gameObject.GetComponent<VideoPlayer>();
        }

        if (SkinManager.instance.skorki[activeSkin].Type == GameManager.KARTA_DYNAMICZNA)
        //if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == GameManager.KARTA_DYNAMICZNA)
        {
            activeVideoPlayer.clip = Resources.Load(SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + Kolor) as VideoClip;
            pm = (int)Mathf.Round(Random.Range(0.0f, (float)activeVideoPlayer.length));
            activeVideoPlayer.frame = pm;
            activeVideoPlayer.Play();
        }
        else
            if (SkinManager.instance.skorki[activeSkin].Type == GameManager.KARTA_STATYCZNA)
            {
                if (activeRawImage != null)
                activeRawImage.texture = Resources.Load<Texture2D>(SkinManager.instance.skorki[activeSkin].Name + Kolor);
                //gameObject.GetComponent<Image>().sprite = Resources.Load(GameManager.RED_TEXT, typeof(Sprite)) as Sprite;
            }
        //teraz ramka

    }

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //Debug.Log(this.GetComponent<SkinManager>());
        //skinManager = this.GetComponent<SkinManager>();
        
        //Instantiate 
        activeSkin = SkinManager.instance.ActiveSkin;
        activeRamka = SkinManager.instance.ActiveRamka;
        ActiveTexture = new RenderTexture(SkinManager.CARD_IMAGE_WIDTH, SkinManager.CARD_IMAGE_HEIGHT, 16);//OnDestroy free??
        
        valueText = transform.Find("Value Text").GetComponent<TextMeshProUGUI>();
        victoryPointsText = transform.Find("Victory Points Text").GetComponent<TextMeshProUGUI>();
        victoryPointsText.color = new Color32(255, 255, 0, 255);
        colorText = transform.Find("Color Text").GetComponent<TextMeshProUGUI>();

      //  image = GetComponent<Image>();
        

        Randomize();
    }

   /* void Awake()
    {
       /* if (activeRawImage !=null)
        applySkin(GameManager.RED_TEXT, true);*/

       /* for (int i = 0; i < playerCards.Count; ++i)
        {
            card = playerCards[i];
            valueText = card.transform.Find("Addition Text").GetComponent<TextMeshProUGUI>();
            card.transform.Find("Player Drop Panel").gameObject.SetActive(true);
            if (valueText.color != taskColor)
            {
                card.SetActive(false);
            }
        }
        
    }*/

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

        //Resources.Load<Texture2D>(SkinManager.instance.skorki[activeSkin].Name + GameManager.RED_TEXT);
        //Debug.Log(SkinManager.instance.ramki[activeRamka]);
        activeRawImage.material.SetTexture("_SecondaryTex", Resources.Load<Texture2D>(SkinManager.instance.ramki[activeRamka]));//"RamkaGold")); 
        float rand = Random.Range(1, GameManager.COLOR_NUMBER+1);//to number of colors
        //int pm;
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
            /*gameObject.GetComponent<Image>().sprite = Resources.Load(GameManager.RED_TEXT, typeof(Sprite)) as Sprite;

            gameObject.GetComponent<VideoPlayer>().clip = Resources.Load(GameManager.RED_TEXT) as VideoClip;
            pm = (int)Mathf.Round(Random.Range(0.0f, (float)gameObject.GetComponent<VideoPlayer>().length));
            gameObject.GetComponent<VideoPlayer>().frame = pm;
            gameObject.GetComponent<VideoPlayer>().Play();*/
            if (SkinManager.instance.skorki[activeSkin].Type == GameManager.KARTA_STATYCZNA)
            activeRawImage.texture = Resources.Load<Texture2D>(SkinManager.instance.skorki[activeSkin].Name + GameManager.RED_TEXT);
            applySkin(GameManager.RED_TEXT, false);

            colorText.text = "Red";
        }
        else
        {
            if (rand <= 2)
            {
                valueText.color = new Color32(0, 255, 0, 255);
                //gameObject.GetComponent<Image>().sprite = Resources.Load(GameManager.GREEN_TEXT, typeof(Sprite)) as Sprite;
                if (SkinManager.instance.skorki[activeSkin].Type == GameManager.KARTA_STATYCZNA)
                activeRawImage.texture = Resources.Load<Texture2D>(SkinManager.instance.skorki[activeSkin].Name + GameManager.GREEN_TEXT);
            
                colorText.text = "Green";
                applySkin(GameManager.GREEN_TEXT, false);
                /*gameObject.GetComponent<VideoPlayer>().clip = Resources.Load(GameManager.GREEN_TEXT, typeof(VideoClip)) as VideoClip;     
                pm = (int)Mathf.Round(Random.Range(0.0f, (float)gameObject.GetComponent<VideoPlayer>().length));
                gameObject.GetComponent<VideoPlayer>().frame = pm;

                gameObject.GetComponent<VideoPlayer>().Play();*/
            }
            else 
            {
                valueText.color = new Color32(0, 0, 255, 255);
                //gameObject.GetComponent<Image>().sprite = Resources.Load(GameManager.BLUE_TEXT, typeof(Sprite)) as Sprite;
                if (SkinManager.instance.skorki[activeSkin].Type == GameManager.KARTA_STATYCZNA)
                activeRawImage.texture = Resources.Load<Texture2D>(SkinManager.instance.skorki[activeSkin].Name + GameManager.BLUE_TEXT);
            
                colorText.text = "Blue";
                //gameObject.GetComponent<VideoPlayer>().targetTexture.Release();
                /*gameObject.GetComponent<VideoPlayer>().clip = Resources.Load(GameManager.BLUE_TEXT, typeof(VideoClip)) as VideoClip;
                pm = (int)Mathf.Round(Random.Range(0.0f, (float)gameObject.GetComponent<VideoPlayer>().length));
                gameObject.GetComponent<VideoPlayer>().frame = pm;
                gameObject.GetComponent<VideoPlayer>().Play();*/
                applySkin(GameManager.BLUE_TEXT, false);
            }
        }
        victoryPointsText.text = (int.Parse(valueText.text)/10).ToString();
    }

}
