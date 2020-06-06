//#define HTML5
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Video;

public class TaskCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshProUGUI valueText;
    public TextMeshProUGUI victoryPointsText;
    public TextMeshProUGUI colorText;
    GameManager gm;
    public Image activeImage;
    Vector3 originalPosition;
    public RenderTexture ActiveTexture;
    public RawImage tex;
    public RawImage activeRawImage;
    public VideoPlayer activeVideoPlayer;
    Vector2 normalScale = new Vector2(1.9f, 1.9f);
    Vector2 biggerScale = new Vector2(2.2f, 2.2f);

   /* public TaskCard(string kolor, int zadanie)
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
    }*/

    void applySkin()
    {
        //int pm;

        activeRawImage.material.SetTexture("_SecondaryTex", gm.wybranaRamka);//Resources.Load<Texture2D>(SkinManager.instance.ramki[SkinManager.instance.ActiveFrame].Name));//do shadera
        activeImage.material.SetTexture("_SecondaryTex", gm.wybranaRamka);//Resources.Load<Texture2D>(SkinManager.instance.ramki[SkinManager.instance.ActiveFrame].Name));
        if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == GameManager.KARTA_DYNAMICZNA)
        {
            activeRawImage.gameObject.SetActive(true);
            activeImage.gameObject.GetComponent<Image>().sprite = gm.wybranyBlack;// Resources.Load<Sprite>("Black");
            activeVideoPlayer.gameObject.SetActive(true);
#if HTML5
//#if UNITY_WEBGL 
            //activeVideoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,SkinManager.instance.skorki[0].Name + Kolor + ".mp4");
            activeVideoPlayer.url = "http://100plus.ieti.pl/public_html/StreamingAssets/Explodes" + Kolor + ".mp4";
//#endif
#endif
         /*   if (Kolor == GameManager.RED_TEXT)
            {
                activeVideoPlayer.GetComponent<VideoPlayer>().clip = gm.wybranyClipRed;
            }
            else
                if (Kolor == GameManager.GREEN_TEXT)
                {
                    activeVideoPlayer.GetComponent<VideoPlayer>().clip = gm.wybranyClipGreen;
                }
                else
                {
                    activeVideoPlayer.GetComponent<VideoPlayer>().clip = gm.wybranyClipBlue;
                }

            pm = (int)Mathf.Round(Random.Range(0.0f, (float)activeVideoPlayer.length));
            activeVideoPlayer.frame = pm;
            activeVideoPlayer.Play();*/
        }
        // else
        if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == GameManager.KARTA_STATYCZNA)
        {
            activeRawImage.gameObject.SetActive(false);
            activeImage.gameObject.SetActive(true);
          /*  if (Kolor == GameManager.RED_TEXT)
            {
                activeImage.GetComponent<Image>().sprite = gm.wybranyRed;
            }
            else
                if (Kolor == GameManager.GREEN_TEXT)
                {
                    activeImage.GetComponent<Image>().sprite = gm.wybranyGreen;
                }
                else
                {
                    activeImage.GetComponent<Image>().sprite = gm.wybranyBlue;
                }*/
            activeImage.GetComponent<Image>().sprite = gm.wybranyRed;
        }
    }

     void applySkin(string Kolor, bool isAwake)
    {     
        int pm;
       
        activeRawImage.material.SetTexture("_SecondaryTex", gm.wybranaRamka);//Resources.Load<Texture2D>(SkinManager.instance.ramki[SkinManager.instance.ActiveFrame].Name));//do shadera
        activeImage.material.SetTexture("_SecondaryTex", gm.wybranaRamka);//Resources.Load<Texture2D>(SkinManager.instance.ramki[SkinManager.instance.ActiveFrame].Name));
        if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == GameManager.KARTA_DYNAMICZNA)
        {
            activeRawImage.gameObject.SetActive(true);
            activeImage.gameObject.GetComponent<Image>().sprite = gm.wybranyBlack;// Resources.Load<Sprite>("Black");
            activeVideoPlayer.gameObject.SetActive(true);
#if HTML5
//#if UNITY_WEBGL 
            //activeVideoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,SkinManager.instance.skorki[0].Name + Kolor + ".mp4");
            activeVideoPlayer.url = "http://100plus.ieti.pl/public_html/StreamingAssets/Explodes" + Kolor + ".mp4";
//#endif
#endif
            if (Kolor == GameManager.RED_TEXT)
            {
                activeVideoPlayer.GetComponent<VideoPlayer>().clip = gm.wybranyClipRed;
            }
            else
                if (Kolor == GameManager.GREEN_TEXT)
                {
                    activeVideoPlayer.GetComponent<VideoPlayer>().clip = gm.wybranyClipGreen;
                }
                else
                {
                    activeVideoPlayer.GetComponent<VideoPlayer>().clip = gm.wybranyClipBlue;
                }

            pm = (int)Mathf.Round(Random.Range(0.0f, (float)activeVideoPlayer.length));
            activeVideoPlayer.frame = pm; 
            activeVideoPlayer.Play();
        }  
       // else
        if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == GameManager.KARTA_STATYCZNA)
            {
                activeRawImage.gameObject.SetActive(false);
                activeImage.gameObject.SetActive(true);
                if (Kolor == GameManager.RED_TEXT)
                {
                    activeImage.GetComponent<Image>().sprite = gm.wybranyRed;
                }
                else
                    if (Kolor == GameManager.GREEN_TEXT)
                    {
                        activeImage.GetComponent<Image>().sprite = gm.wybranyGreen;
                    }
                    else
                    {
                        activeImage.GetComponent<Image>().sprite = gm.wybranyBlue;
                    }
            }
    }

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        activeImage.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Przezroczysty");//.gameObject.SetActive(false);
        ActiveTexture = new RenderTexture(SkinManager.CARD_IMAGE_WIDTH, SkinManager.CARD_IMAGE_HEIGHT, 16);//OnDestroy free??
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

     void Randomize()
    {
        float rand = Random.Range(1, GameManager.COLOR_NUMBER + 1);//to number of colors
       /* if (isPreset)
            rand = presetColor;*/
//            gameObject.GetComponent<VideoPlayer>().targetTexture = ActiveTexture;

           // gameObject.GetComponent<RawImage>().GetComponent<VideoPlayer>().targetTexture = ActiveTexture;
        activeVideoPlayer.targetTexture = ActiveTexture;
           /* Transform[] children;
            children = transform.Find("RawImage").GetComponent<RawImage>().GetComponentsInChildren<Transform>();
            for (int i = 0; i < children.Length; ++i)
            {
                GameObject child = gameObject.GetComponent<RawImage>().transform.GetChild(i).gameObject;

                Debug.Log(child);
                //Debug.Log(card.GetComponentInChildren<RawImage>());//OK!
            }*/
            //miniRawImage.GetComponent<VideoPlayer>().targetTexture = miniActiveTexture;
            tex = transform.Find("RawImage").GetComponent<RawImage>();
            tex.texture = ActiveTexture;
            //Debug.Log(tex.transform.GetChild(0).gameObject);
           // miniTex = transform.Find("RawImage").GetComponent<RawImage>();
           // miniTex.texture = miniActiveTexture;
/*
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
       */
        /*if (rand <= 1)
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
        }*/
            applySkin();
        //gm.SetVictoryPoints(int.Parse(valueText.text)/10);
    }

}
