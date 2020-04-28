//#define HTML5
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
    public Image activeImage;
    Vector3 originalPosition;
    RenderTexture ActiveTexture;
    //RenderTexture miniActiveTexture;
    RawImage tex;
   // RawImage miniTex;
    public RawImage activeRawImage;
   // public RawImage miniRawImage;
    public VideoPlayer activeVideoPlayer;
    //public VideoPlayer miniVideoPlayer;
    //public Image frameImage;
    int activeSkin;
    int activeRamka;
    Vector2 normalScale = new Vector2(1.9f, 1.9f);
    Vector2 biggerScale = new Vector2(2.2f, 2.2f);
    bool isPreset = false;
    int presetColor = 0;
    int presetTask = 0;

    public TaskCard(string kolor, int zadanie)
    {
        isPreset = true;
        if (kolor == GameManager.RED_TEXT)
        {
            presetColor = 1;
        }
        else
        {
            if (kolor == GameManager.GREEN_TEXT)
            {
                presetColor = 2;
            }
            else
                presetColor = 3;
        }
        presetTask = zadanie;
    }

    public TaskCard()
    {
        isPreset = false;
    }

    void applySkin(string Kolor, bool isAwake)
    {     
        int pm;
       
        activeRawImage.material.SetTexture("_SecondaryTex", Resources.Load<Texture2D>(SkinManager.instance.ramki[SkinManager.instance.ActiveFrame].Name));//do shadera
        activeImage.material.SetTexture("_SecondaryTex", Resources.Load<Texture2D>(SkinManager.instance.ramki[SkinManager.instance.ActiveFrame].Name));
        //frameImage.sprite = Resources.Load<Sprite>(SkinManager.instance.ramki[SkinManager.instance.ActiveFrame].Name);
       /* string pom2 = SkinManager.instance.ramki[SkinManager.instance.ActiveFrame].Name + ".png";
        pom2 = System.IO.Path.Combine(Application.streamingAssetsPath, pom2);
        
        byte[] pngBytes2 = System.IO.File.ReadAllBytes(pom2);
        //Creates texture and loads byte array data to create image
        Texture2D tex2 = new Texture2D(2, 2);
        tex2.LoadImage(pngBytes2);

        //Creates a new Sprite based on the Texture2D
        //Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

        //Assigns the UI sprite
        activeRawImage.material.SetTexture("_SecondaryTex", tex2);*/
       
        if (SkinManager.instance.skorki[activeSkin].Type == GameManager.KARTA_DYNAMICZNA)
        {
            activeRawImage.gameObject.SetActive(true);
           // gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("przezroczysty");
            activeImage.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Black");
            //activeImage.gameObject.SetActive(false);
            activeVideoPlayer.gameObject.SetActive(true);
            //miniVideoPlayer.gameObject.SetActive(true);
#if HTML5
//#if UNITY_WEBGL 
            activeVideoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,SkinManager.instance.skorki[0].Name + Kolor + ".mp4");
//#endif
#endif
            //miniVideoPlayer.clip = Resources.Load(SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + Kolor) as VideoClip;
            activeVideoPlayer.clip = Resources.Load(SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + Kolor) as VideoClip;
            //activeVideoPlayer.clip = Resources.Load(System.IO.Path.Combine(Application.streamingAssetsPath, SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + Kolor + ".mp4")) as VideoClip;
            //Debug.Log(System.IO.Path.Combine(Application.streamingAssetsPath));//, SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + Kolor + ".mp4"));
            //activeVideoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + Kolor + ".mp4");

            pm = (int)Mathf.Round(Random.Range(0.0f, (float)activeVideoPlayer.length));
            activeVideoPlayer.frame = pm; 
            activeVideoPlayer.Play();
            //miniVideoPlayer.frame = pm;
            //miniVideoPlayer.Play();
        }  
       // else
            if (SkinManager.instance.skorki[activeSkin].Type == GameManager.KARTA_STATYCZNA)
            {
                activeRawImage.gameObject.SetActive(false);
                activeImage.gameObject.SetActive(true);
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + Kolor);
               /* string pom = SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + Kolor + ".png";
                pom = System.IO.Path.Combine(Application.streamingAssetsPath, pom);
                //backgroundImage.sprite = Resources.Load<Sprite>(System.IO.Path.Combine(Application.streamingAssetsPath,"Background/" + SkinManager.instance.tla[LocalActiveBackground].Name) + ".jpg");//.Name

                byte[] pngBytes = System.IO.File.ReadAllBytes(pom);

                //Creates texture and loads byte array data to create image
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(pngBytes);

                //Creates a new Sprite based on the Texture2D
                Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

                //Assigns the UI sprite

                gameObject.GetComponent<Image>().sprite = fromTex;*/
            }
    }

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        activeRawImage = GameObject.Find("RawImage").GetComponent<RawImage>();
        activeImage.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Przezroczysty");//.gameObject.SetActive(false);
        //Instantiate 
        activeSkin = SkinManager.instance.ActiveSkin;
        activeRamka = SkinManager.instance.ActiveFrame;
        ActiveTexture = new RenderTexture(SkinManager.CARD_IMAGE_WIDTH, SkinManager.CARD_IMAGE_HEIGHT, 16);//OnDestroy free??
        //miniActiveTexture = new RenderTexture(SkinManager.CARD_IMAGE_WIDTH, SkinManager.CARD_IMAGE_HEIGHT, 16);//OnDestroy free??
        
        valueText = transform.Find("Value Text").GetComponent<TextMeshProUGUI>();
        victoryPointsText = transform.Find("Victory Points Text").GetComponent<TextMeshProUGUI>();
        victoryPointsText.color = new Color32(255, 255, 0, 255);
        colorText = transform.Find("Color Text").GetComponent<TextMeshProUGUI>();

        Randomize();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerEnter)
        {
            //GetComponent<Outline>().enabled = true;
        }
        gm.userActivityTime =  SkinManager.MAX_USER_DISACTIVITY;
        if (gm.tasks.gameObject.activeSelf)
            gameObject.transform.localScale = biggerScale;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerEnter)
        {
           // GetComponent<Outline>().enabled = false;
        }
        if (gm.tasks.gameObject.activeSelf)
            gameObject.transform.localScale = normalScale;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Debug.Log(pointerEventData);
        if (gm.trashArea.activeSelf)
        {
            if ((gm.transparentPlayerCardPanel.activeSelf)&&(gm.transparentPowerUpCardPanel.activeSelf))
            {
                gm.DiscardTaskCard(this.gameObject);
                gm.CheckCardNumbers(true);
            }
            else
            {
                gm.userActivityTime = 0;
            }
        }
        else
        {
            gm.SetActiveCard(gameObject, false);
           // gm.transparentPlayerCardPanel.SetActive(false);
           // gm.transparentPowerUpCardPanel.SetActive(false);
        }
    }

    public void Randomize()
    {
        float rand = Random.Range(1, GameManager.COLOR_NUMBER + 1);//to number of colors
        if (isPreset)
            rand = presetColor;
            gameObject.GetComponent<VideoPlayer>().targetTexture = ActiveTexture;
            //miniRawImage.GetComponent<VideoPlayer>().targetTexture = miniActiveTexture;
            tex = transform.Find("RawImage").GetComponent<RawImage>();
            tex.texture = ActiveTexture;
           // miniTex = transform.Find("RawImage").GetComponent<RawImage>();
           // miniTex.texture = miniActiveTexture;

        if (float.Parse(gm.victoryPoints.text) < gm.earlyGamePoint)
        {
            valueText.text = Random.Range(10, gm.earlyGameTaskCardMax).ToString();
            if (isPreset)
                valueText.text = presetTask.ToString();
        }
        else
        {
            if (float.Parse(gm.victoryPoints.text) < gm.middleGamePoint)
            {
                valueText.text = Random.Range(gm.earlyGameTaskCardMax, gm.middleGameTaskCardMax).ToString();
                if (isPreset)
                    valueText.text = presetTask.ToString();
            }
            else //lateGamePoint
            {
                valueText.text = Random.Range(gm.middleGameTaskCardMax, gm.lateGameTaskCardMax).ToString();
                if (isPreset)
                    valueText.text = presetTask.ToString();
            }
        }
       
        if (rand <= 1)
        {
            valueText.color = new Color32(SkinManager.RED_COLOR, 0, 0, 255);
            applySkin(GameManager.RED_TEXT, false);
            colorText.text = GameManager.RED_TEXT;
        }
        else
        {
            if (rand <= 2)
            {
                valueText.color = new Color32(0, SkinManager.GREEN_COLOR, 0, 255);
                colorText.text = GameManager.GREEN_TEXT;
                applySkin(GameManager.GREEN_TEXT, false);
            }
            else 
            {
                valueText.color = new Color32(0, 0, SkinManager.BLUE_COLOR, 255);
                colorText.text = GameManager.BLUE_TEXT;
                applySkin(GameManager.BLUE_TEXT, false);
            }
        }
        victoryPointsText.text = (int.Parse(valueText.text)/10).ToString();
    }

}
