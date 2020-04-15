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
    RawImage tex;
    public RawImage activeRawImage;
    public VideoPlayer activeVideoPlayer;
    int activeSkin;
    int activeRamka;
    Vector2 normalScale = new Vector2(1.9f, 1.9f);
    Vector2 biggerScale = new Vector2(2.2f, 2.2f); 

    void applySkin(string Kolor, bool isAwake)
    {     
        int pm;
        activeRawImage.material.SetTexture("_SecondaryTex", Resources.Load<Texture2D>("Ramki/" + SkinManager.instance.ramki[SkinManager.instance.ActiveFrame].Name));
        if (SkinManager.instance.skorki[activeSkin].Type == GameManager.KARTA_DYNAMICZNA)
        {
            activeRawImage.gameObject.SetActive(true);
            activeImage.gameObject.SetActive(false);
            activeVideoPlayer.gameObject.SetActive(true);
#if HTML5
//#if UNITY_WEBGL 
            activeVideoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,SkinManager.instance.skorki[0].Name + Kolor + ".mp4");
//#endif
#endif
            activeVideoPlayer.clip = Resources.Load(SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + Kolor) as VideoClip;
            pm = (int)Mathf.Round(Random.Range(0.0f, (float)activeVideoPlayer.length));
            activeVideoPlayer.frame = pm; 
            activeVideoPlayer.Play();
        }  
       // else
            if (SkinManager.instance.skorki[activeSkin].Type == GameManager.KARTA_STATYCZNA)
            {
                activeRawImage.gameObject.SetActive(false);
                activeImage.gameObject.SetActive(true);
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + Kolor);
            }
    }

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        activeRawImage = GameObject.Find("RawImage").GetComponent<RawImage>();     
        //Instantiate 
        activeSkin = SkinManager.instance.ActiveSkin;
        activeRamka = SkinManager.instance.ActiveFrame;
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
        float rand = Random.Range(1, GameManager.COLOR_NUMBER + 1);//to number of colors
            gameObject.GetComponent<VideoPlayer>().targetTexture = ActiveTexture;
            tex = transform.Find("RawImage").GetComponent<RawImage>();
            tex.texture = ActiveTexture;
            
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
            applySkin(GameManager.RED_TEXT, false);
            colorText.text = GameManager.RED_TEXT;
        }
        else
        {
            if (rand <= 2)
            {
                valueText.color = new Color32(0, 255, 0, 255);
                colorText.text = GameManager.GREEN_TEXT;
                applySkin(GameManager.GREEN_TEXT, false);
            }
            else 
            {
                valueText.color = new Color32(0, 0, 255, 255);
                colorText.text = GameManager.BLUE_TEXT;
                applySkin(GameManager.BLUE_TEXT, false);
            }
        }
        victoryPointsText.text = (int.Parse(valueText.text)/10).ToString();
    }

}
