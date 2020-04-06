using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;


public class Skins : MonoBehaviour
{
    
    int ActiveColor = 1; 
    const float maxColorTime = 10;
    float remainingColorTime = maxColorTime;
    

    [Header("Obrazki"), SerializeField]
    public GameObject rawImageA;

    RenderTexture ActiveTexture;
    RawImage tex;
    //public SkinManager skinManager;
    public Button chooseButton;
    //public GameObject chooseButton;
    int ActiveSkin ;
    
//    public Image[] images;


    void changeSkin(string Kolor)
    {
        int pm;
        //Debug.Log(skinManager.ActiveSkin);
        if (SkinManager.instance.skorki[ActiveSkin].Type == GameManager.KARTA_DYNAMICZNA)
        {
            //delete
            ActiveTexture = new RenderTexture(SkinManager.CARD_IMAGE_WIDTH, SkinManager.CARD_IMAGE_HEIGHT, 16);
            rawImageA.GetComponent<RawImage>().texture = ActiveTexture;
            rawImageA.GetComponent<VideoPlayer>().targetTexture = ActiveTexture;
            rawImageA.GetComponent<VideoPlayer>().clip = Resources.Load(SkinManager.instance.skorki[ActiveSkin].Name + Kolor) as VideoClip;

            pm = (int)Mathf.Round(Random.Range(0.0f, (float)rawImageA.GetComponent<VideoPlayer>().length));
            rawImageA.GetComponent<VideoPlayer>().frame = pm;
            rawImageA.GetComponent<VideoPlayer>().Play();
        }
        else
            if (SkinManager.instance.skorki[ActiveSkin].Type == GameManager.KARTA_STATYCZNA)
        {
            rawImageA.GetComponent<RawImage>().texture = Resources.Load<Texture2D>(SkinManager.instance.skorki[ActiveSkin].Name + Kolor);
            // imageA.GetComponent<Image>().sprite = Resources.Load(SkinManager.instance.skorki[ActiveSkin].Name + Kolor, typeof(Sprite)) as Sprite;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "RawImage")
        {
            //skinManager = gameObject.GetComponent<SkinManager>();
        }

   /*     images = GetComponents<Image>();

        foreach (Image img in images)
        {
            // Do stuff
            //Debug.Log(img);
            img.enabled = true;
            image = img;
        }*/
       // if (skinManager != null)
        {
            ActiveSkin = SkinManager.instance.ActiveSkin;
            changeSkin("Red");
        }
        //ActiveSkin = SkinManager.instance.ActiveSkin;
        //chooseButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        //change colors
        if (remainingColorTime > 0)
        {
           remainingColorTime  -= Time.deltaTime;
        }
        else{
            remainingColorTime = maxColorTime;
            if (ActiveColor < SkinManager.COLOR_NUMBER - 1)
            {
                ActiveColor++;
            }
            else
            {
                ActiveColor = 0;
            }
            changeSkin(SkinManager.instance.COLORS_ARRAY[ActiveColor]);
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Next()
    {
        //if (skinManager != null)
        {
            if (ActiveSkin < SkinManager.instance.skorki.Count - 1)
            {
                ActiveSkin++;
                if (SkinManager.instance.ActiveSkin != ActiveSkin)
                {
                    chooseButton.interactable = true;
                }
                else
                {
                    chooseButton.interactable = false;
                }
                changeSkin(SkinManager.instance.COLORS_ARRAY[ActiveColor]);
            }
        }
    }

    public void Prev()
    {
        //if (skinManager != null)
        {
            if (ActiveSkin > 0)
            {
                ActiveSkin--;
                if (SkinManager.instance.ActiveSkin != ActiveSkin)
                {
                    chooseButton.interactable =true;
                }
                else
                {
                    chooseButton.interactable=false;
                }
                changeSkin(SkinManager.instance.COLORS_ARRAY[ActiveColor]);
            }
        }
    }

    public void Choose()
    {
        //skinManager.ActiveSkin = ActiveSkin;
        SkinManager.instance.SetActiveSkin(ActiveSkin);
        chooseButton.interactable = false;
       // SkinManager.instance.CurrentScore = ActiveSkin;
        PlayerPrefs.SetInt("ActiveSkin", ActiveSkin);
        Debug.Log(SkinManager.instance.ActiveSkin);
    }

    void OnEnable()
    {
        ActiveSkin = PlayerPrefs.GetInt("ActiveSkin");
    }
}
