//#define HTML5
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;


public class Skins : MonoBehaviour
{
    
    int ActiveColor = 1; 
    const float maxColorTime = 4;
    const float BUTTON_SCALE = 1.9f;
    float remainingColorTime = maxColorTime;
    

    [Header("Obrazki"), SerializeField]
    public GameObject rawImageA;

    RenderTexture ActiveTexture;
    RawImage tex;
    public Image backgroundImage;
    public Button chooseButton;
    public AudioSource soundBackground;
    public AudioSource cashBuyDone;
    public GameObject purchasePanel;
    public int LocalActiveSkin ;
    public int LocalActiveFrame;
    public int LocalActiveBackground;
    public int LocalActiveSound;
    public TextMeshProUGUI cash;
    public TextMeshProUGUI price;
    public TextMeshProUGUI skinName;
    public Image chooseButtonImage;
    bool isSkins = true;
    bool isFrames = false;
    bool isBackgrounds = false;
    bool isSounds = false;
    Vector2 normalSize;
    Vector2 biggerSize;
//    public Image[] images;


    void changeSkin(string Kolor)
    {
        int pm;
        //rawImageA.GetComponent<RawImage>().material.SetTexture("_SecondaryTex", Resources.Load<Texture2D>("Ramki/" + SkinManager.instance.ramki[SkinManager.instance.ActiveFrame]));//
        rawImageA.GetComponent<RawImage>().material.SetTexture("_SecondaryTex", Resources.Load<Texture2D>("Ramki/" + SkinManager.instance.ramki[LocalActiveFrame].Name));//
       // if (isSkins)
        {
            if (SkinManager.instance.skorki[LocalActiveSkin].Type == GameManager.KARTA_DYNAMICZNA)
            {
                ActiveTexture = new RenderTexture(SkinManager.CARD_IMAGE_WIDTH, SkinManager.CARD_IMAGE_HEIGHT, 16);
                rawImageA.GetComponent<RawImage>().texture = ActiveTexture;
#if HTML5
                //#if UNITY_WEBGL
                // rawImageA.GetComponent<VideoPlayer>().url = "http://100plus.ieti.pl/" + SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + Kolor + ".mp4"; 
                rawImageA.GetComponent<VideoPlayer>().url = System.IO.Path.Combine (Application.streamingAssetsPath,SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + Kolor + ".mp4");

                //#endif
#endif
                rawImageA.GetComponent<VideoPlayer>().targetTexture = ActiveTexture;
                rawImageA.GetComponent<VideoPlayer>().clip = Resources.Load(SkinManager.instance.skorki[LocalActiveSkin].Name + Kolor) as VideoClip;

                pm = (int)Mathf.Round(Random.Range(0.0f, (float)rawImageA.GetComponent<VideoPlayer>().length));
                rawImageA.GetComponent<VideoPlayer>().frame = pm;
                rawImageA.GetComponent<VideoPlayer>().Play();
            }
            else
                if (SkinManager.instance.skorki[LocalActiveSkin].Type == GameManager.KARTA_STATYCZNA)
                {
                    rawImageA.GetComponent<VideoPlayer>().targetTexture = null;
                    rawImageA.GetComponent<RawImage>().texture = Resources.Load<Texture2D>(SkinManager.instance.skorki[LocalActiveSkin].Name + Kolor);
                }
        }
        //else
        {
            if (isFrames)
            {
            
            }
        }
    }

    void changeBackground()
    {
        backgroundImage.sprite = Resources.Load<Sprite>("Background/" + SkinManager.instance.tla[LocalActiveBackground].Name);//.Name
    }

    void changeSound()
    {

        soundBackground.clip = (AudioClip)Resources.Load("Audio/" + SkinManager.instance.muzyki[LocalActiveSound].Name); ;
        soundBackground.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
   /*     images = GetComponents<Image>();

        foreach (Image img in images)
        {
            // Do stuff
            //Debug.Log(img);
            img.enabled = true;
            image = img;
        }*/
        purchasePanel.gameObject.SetActive(false);
        cash.color = new Color32(255, 255, 0, 255);
        price.color = new Color32(0, 255, 0, 255);
        chooseButtonImage.sprite = Resources.Load<Sprite>("ChoiceOK");

            
        normalSize = GameObject.Find("SkinsButton").GetComponent<Button>().image.rectTransform.sizeDelta;
        biggerSize = normalSize * BUTTON_SCALE;
        GameObject.Find("SkinsButton").GetComponent<Button>().image.rectTransform.sizeDelta = biggerSize;
        {
            LocalActiveSkin = SkinManager.instance.ActiveSkin;
            LocalActiveFrame = SkinManager.instance.ActiveFrame;
            changeSkin(SkinManager.RED_TEXT);
        }
        backgroundImage = GameObject.Find("BackgroundImage").GetComponent<Image>();
        LocalActiveBackground = SkinManager.instance.ActiveBackground;
        changeBackground();
        LocalActiveSound = SkinManager.instance.ActiveSound;
        changeSound();
        skinName.text = SkinManager.instance.skorki[LocalActiveSkin].Title;
        cash.text = SkinManager.instance.CurrentCash.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //change colors
        if (remainingColorTime >= 0)
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

    public void Buy20Coins()
    {
        int newCash;
        cashBuyDone.Play();
        newCash = SkinManager.instance.CurrentCash + 20;
        SkinManager.instance.SetCurrentCash(newCash);
        PlayerPrefs.SetInt("CurrentCash", newCash);
        cash.text = SkinManager.instance.CurrentCash.ToString();
      //  purchasePanel.gameObject.SetActive(false);//throw runtime error!
    }

    public void Buy100Coins()
    {
        int newCash;
        cashBuyDone.Play();
        newCash = SkinManager.instance.CurrentCash + 100;
        SkinManager.instance.SetCurrentCash(newCash);
        PlayerPrefs.SetInt("CurrentCash", newCash);
        cash.text = SkinManager.instance.CurrentCash.ToString();
        // purchasePanel.gameObject.SetActive(false);//throw runtime error!
    }

    public void Buy350Coins()
    {
        int newCash;
        cashBuyDone.Play();
        newCash = SkinManager.instance.CurrentCash + 350;
        SkinManager.instance.SetCurrentCash(newCash);
        PlayerPrefs.SetInt("CurrentCash", newCash);
        cash.text = SkinManager.instance.CurrentCash.ToString();
        //  purchasePanel.gameObject.SetActive(false);//throw runtime error!
    }

    public void BuyFailed()
    {
        // purchasePanel.gameObject.SetActive(false); ////throw runtime error!
        skinName.text = "Purchase FAILED";
    }

    public void BuySucced()
    {
        //nothing yet
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void BackPurchase()
    {
        purchasePanel.gameObject.SetActive(false);
    }

    public void CheckSkinsPossible(SkinManager.SkinsInfo skin)
    {
        if (skin.Price > 0)
        {
            //check is already bought?
            if (PlayerPrefs.GetInt(skin.Name) == 0)
            {
                cash.color = new Color32(255, 0, 0, 255);
                chooseButtonImage.sprite = Resources.Load<Sprite>("Coins");
                price.text = skin.Price.ToString();

            }
            else
            {
                cash.color = new Color32(255, 255, 0, 255);
                chooseButtonImage.sprite = Resources.Load<Sprite>("ChoiceOK");
                price.text = "0";
            }
        }
        else
        {
            cash.color = new Color32(255, 255, 0, 255);
            chooseButtonImage.sprite = Resources.Load<Sprite>("ChoiceOK");
            price.text = "0";
        }
    }

    public void ChangeLocalActive(bool conditions, bool increase)
    {
     
    }

    public void Next()
    {
        
        if (isSkins)
        {
            if (LocalActiveSkin < SkinManager.instance.skorki.Count - 1)
            {
                LocalActiveSkin++;
                chooseButtonImage.sprite = Resources.Load<Sprite>("ChoiceOK");
                if (SkinManager.instance.ActiveSkin != LocalActiveSkin)
                {
                    chooseButton.interactable = true;
                }
                else
                {
                    chooseButton.interactable = false;
                }
                //
                CheckSkinsPossible(SkinManager.instance.skorki[LocalActiveSkin]);               
                //
                skinName.text = SkinManager.instance.skorki[LocalActiveSkin].Title;
                changeSkin(SkinManager.instance.COLORS_ARRAY[ActiveColor]);
            }
        }
        else
        {
            if (isFrames)
            {
                if (LocalActiveFrame < SkinManager.instance.ramki.Count - 1)
                {
                    LocalActiveFrame++;
                    if (SkinManager.instance.ActiveFrame != LocalActiveFrame)
                    {
                        chooseButton.interactable = true;
                    }
                    else
                    {
                        chooseButton.interactable = false;
                    }
                    CheckSkinsPossible(SkinManager.instance.ramki[LocalActiveFrame]);
                    skinName.text = SkinManager.instance.ramki[LocalActiveFrame].Title;
                    changeSkin(SkinManager.instance.COLORS_ARRAY[ActiveColor]);
                }
            }
            else
            {
                if (isBackgrounds)
                {
                    if (LocalActiveBackground < SkinManager.instance.tla.Count - 1)
                    {
                        LocalActiveBackground++;
                        if (SkinManager.instance.ActiveBackground != LocalActiveBackground)
                        {
                            chooseButton.interactable = true;
                        }
                        else
                        {
                            chooseButton.interactable = false;
                        }
                        CheckSkinsPossible(SkinManager.instance.tla[LocalActiveBackground]);
                        skinName.text = SkinManager.instance.tla[LocalActiveBackground].Title;
                        changeBackground();
                    }
                    
                }
                else
                {
                    if (isSounds)
                    {
                        if (LocalActiveSound < SkinManager.instance.muzyki.Count - 1)
                        {
                            LocalActiveSound++;
                            if (SkinManager.instance.ActiveSound != LocalActiveSound)
                            {
                                chooseButton.interactable = true;
                            }
                            else
                            {
                                chooseButton.interactable = false;
                            }
                            CheckSkinsPossible(SkinManager.instance.muzyki[LocalActiveSound]);
                            skinName.text = SkinManager.instance.muzyki[LocalActiveSound].Title;
                            changeSound();
                        }
                        
                    }
                }
            }
        }

        if (int.Parse(cash.text) < int.Parse(price.text))
        {
            price.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            price.color = new Color32(0, 255, 0, 255);
        }
    }

    public void Prev()
    {
        if (isSkins)
        {
            if (LocalActiveSkin > 0)
            {
                LocalActiveSkin--;
                if (SkinManager.instance.ActiveSkin != LocalActiveSkin)
                {
                    chooseButton.interactable = true;
                }
                else
                {
                    chooseButton.interactable = false;
                }
                //
                CheckSkinsPossible(SkinManager.instance.skorki[LocalActiveSkin]);
                skinName.text = SkinManager.instance.skorki[LocalActiveSkin].Title;
                changeSkin(SkinManager.instance.COLORS_ARRAY[ActiveColor]);
            }
        }
        else
        {
            if (isFrames)
            {
                if (LocalActiveFrame > 0)
                {
                    LocalActiveFrame--;
                    if (SkinManager.instance.ActiveFrame != LocalActiveFrame)
                    {
                        chooseButton.interactable = true;
                    }
                    else
                    {
                        chooseButton.interactable = false;
                    }
                    CheckSkinsPossible(SkinManager.instance.ramki[LocalActiveFrame]);
                    skinName.text = SkinManager.instance.ramki[LocalActiveFrame].Title;
                    changeSkin(SkinManager.instance.COLORS_ARRAY[ActiveColor]);
                }
            }
            else
            {
                if (isBackgrounds)
                {
                    if (LocalActiveBackground > 0)
                    {
                        LocalActiveBackground--;
                        if (SkinManager.instance.ActiveBackground != LocalActiveBackground)
                        {
                            chooseButton.interactable = true;
                        }
                        else
                        {
                            chooseButton.interactable = false;
                        }
                        CheckSkinsPossible(SkinManager.instance.tla[LocalActiveBackground]);
                        skinName.text = SkinManager.instance.tla[LocalActiveBackground].Title;
                        changeBackground();
                    }
                    
                }
                else
                {
                    if (isSounds)
                    {
                        if (LocalActiveSound > 0)
                        {
                            LocalActiveSound--;
                            if (SkinManager.instance.ActiveSound != LocalActiveSound)
                            {
                                chooseButton.interactable = true;
                            }
                            else
                            {
                                chooseButton.interactable = false;
                            }
                            CheckSkinsPossible(SkinManager.instance.muzyki[LocalActiveSound]);
                            skinName.text = SkinManager.instance.muzyki[LocalActiveSound].Title;
                            changeSound();
                        }
                    }
                }
            }
        }

        if (int.Parse(cash.text) < int.Parse(price.text))
        {
            price.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            price.color = new Color32(0, 255, 0, 255);
        }
    }

    public void SwitchSkin()
    {
        isSkins = true;
        isFrames = false;
        isBackgrounds = false;
        isSounds = false;
        skinName.text = SkinManager.instance.skorki[LocalActiveSkin].Title;
        chooseButtonImage.sprite = Resources.Load<Sprite>("ChoiceOK");
        
        GameObject.Find("SkinsButton").GetComponent<Button>().image.rectTransform.sizeDelta = biggerSize;
        GameObject.Find("Frames").GetComponent<Button>().image.rectTransform.sizeDelta = normalSize;
        GameObject.Find("Backgrounds").GetComponent<Button>().image.rectTransform.sizeDelta = normalSize;
        GameObject.Find("Sounds").GetComponent<Button>().image.rectTransform.sizeDelta = normalSize;
        LocalActiveSkin = SkinManager.instance.ActiveSkin;
        LocalActiveFrame = SkinManager.instance.ActiveFrame;
        LocalActiveBackground = SkinManager.instance.ActiveBackground;
        if (LocalActiveSound != SkinManager.instance.ActiveSound)
        {
            LocalActiveSound = SkinManager.instance.ActiveSound;
            changeSound();
        }
        changeBackground();
        changeSkin(SkinManager.instance.COLORS_ARRAY[ActiveColor]);
        cash.color = new Color32(255, 255, 0, 255);
        price.color = new Color32(0, 255, 0, 255);
        price.text = "0";
    }

    public void SwitchFrame()
    {
        isSkins = false;
        isFrames = true;
        isBackgrounds = false;
        isSounds = false;
        skinName.text = SkinManager.instance.ramki[LocalActiveFrame].Title;
        chooseButtonImage.sprite = Resources.Load<Sprite>("ChoiceOK");
        
        GameObject.Find("SkinsButton").GetComponent<Button>().image.rectTransform.sizeDelta = normalSize;
        GameObject.Find("Frames").GetComponent<Button>().image.rectTransform.sizeDelta = biggerSize;
        GameObject.Find("Backgrounds").GetComponent<Button>().image.rectTransform.sizeDelta = normalSize;
        GameObject.Find("Sounds").GetComponent<Button>().image.rectTransform.sizeDelta = normalSize;
        LocalActiveSkin = SkinManager.instance.ActiveSkin;
        LocalActiveFrame = SkinManager.instance.ActiveFrame;
        LocalActiveBackground = SkinManager.instance.ActiveBackground;
        if (LocalActiveSound != SkinManager.instance.ActiveSound)
        {
            LocalActiveSound = SkinManager.instance.ActiveSound;
            changeSound();
        }
        changeBackground();
        changeSkin(SkinManager.instance.COLORS_ARRAY[ActiveColor]);
        cash.color = new Color32(255, 255, 0, 255);
        price.color = new Color32(0, 255, 0, 255);
        price.text = "0";
    }

    public void SwitchBackground()
    {
        isSkins = false;
        isFrames = false;
        isBackgrounds = true;
        isSounds = false;
        skinName.text = SkinManager.instance.tla[LocalActiveBackground].Title;
        chooseButtonImage.sprite = Resources.Load<Sprite>("ChoiceOK");
        GameObject.Find("SkinsButton").GetComponent<Button>().image.rectTransform.sizeDelta = normalSize;
        GameObject.Find("Frames").GetComponent<Button>().image.rectTransform.sizeDelta = normalSize;
        GameObject.Find("Backgrounds").GetComponent<Button>().image.rectTransform.sizeDelta = biggerSize;
        GameObject.Find("Sounds").GetComponent<Button>().image.rectTransform.sizeDelta = normalSize;
        LocalActiveSkin = SkinManager.instance.ActiveSkin;
        LocalActiveFrame = SkinManager.instance.ActiveFrame;
        LocalActiveBackground = SkinManager.instance.ActiveBackground;
        if (LocalActiveSound != SkinManager.instance.ActiveSound)
        {
            LocalActiveSound = SkinManager.instance.ActiveSound;
            changeSound();
        }
        changeSkin(SkinManager.instance.COLORS_ARRAY[ActiveColor]);
        cash.color = new Color32(255, 255, 0, 255);
        price.color = new Color32(0, 255, 0, 255);
        price.text = "0";
    }

    public void SwitchSound()
    {
        isSkins = false;
        isFrames = false;
        isBackgrounds = false;
        isSounds = true;
        skinName.text = SkinManager.instance.muzyki[LocalActiveSound].Title;
        chooseButtonImage.sprite = Resources.Load<Sprite>("ChoiceOK");
        GameObject.Find("SkinsButton").GetComponent<Button>().image.rectTransform.sizeDelta = normalSize;
        GameObject.Find("Frames").GetComponent<Button>().image.rectTransform.sizeDelta = normalSize;
        GameObject.Find("Backgrounds").GetComponent<Button>().image.rectTransform.sizeDelta = normalSize;
        GameObject.Find("Sounds").GetComponent<Button>().image.rectTransform.sizeDelta = biggerSize;
        LocalActiveSkin = SkinManager.instance.ActiveSkin;
        LocalActiveFrame = SkinManager.instance.ActiveFrame;
        LocalActiveBackground = SkinManager.instance.ActiveBackground;
        LocalActiveSound = SkinManager.instance.ActiveSound;
        changeBackground();
        changeSkin(SkinManager.instance.COLORS_ARRAY[ActiveColor]);
        cash.color = new Color32(255, 255, 0, 255);
        price.color = new Color32(0, 255, 0, 255);
        price.text = "0";
    }

    public bool CheckPaymentPossible(SkinManager.SkinsInfo skin)
    {
        if ((skin.Price > 0) && (PlayerPrefs.GetInt(skin.Name) == 0))
        {
            if (int.Parse(cash.text) < skin.Price)
            {
                purchasePanel.gameObject.SetActive(true);
                return false;
            }
            else
            {
                SkinManager.instance.SetCurrentCash(int.Parse(cash.text) - skin.Price);
                cash.text = SkinManager.instance.CurrentCash.ToString();
                PlayerPrefs.SetInt("CurrentCash", SkinManager.instance.CurrentCash);
                PlayerPrefs.SetInt(skin.Name, 1);
                chooseButtonImage.sprite = Resources.Load<Sprite>("ChoiceOK");
                return true;
            }
        }
        else
        {
            return true;
        }
    }

    public void Choose()
    {
        if (isSkins)
        {   
            //check available cash and buy
            if (CheckPaymentPossible(SkinManager.instance.skorki[LocalActiveSkin]))
            {
                SkinManager.instance.SetActiveSkin(LocalActiveSkin);
                PlayerPrefs.SetInt("ActiveSkin", LocalActiveSkin);
                chooseButton.interactable = false;
            }
        }
        else
        {
            if (isFrames)
            {
                if (CheckPaymentPossible(SkinManager.instance.ramki[LocalActiveFrame]))
                {
                    SkinManager.instance.SetActiveFrame(LocalActiveFrame);
                    PlayerPrefs.SetInt("ActiveFrame", LocalActiveFrame);
                    chooseButton.interactable = false;
                }
            }
            else
            {
                if (isBackgrounds)
                {
                    if (CheckPaymentPossible(SkinManager.instance.tla[LocalActiveBackground]))
                    {
                        SkinManager.instance.SetActiveBackground(LocalActiveBackground);
                        PlayerPrefs.SetInt("ActiveBackground", LocalActiveBackground);
                        chooseButton.interactable = false;
                    }
                }
                else
                {
                    if (isSounds)
                    {
                        if (CheckPaymentPossible(SkinManager.instance.muzyki[LocalActiveSound]))
                        {
                            SkinManager.instance.SetActiveSound(LocalActiveSound);
                            PlayerPrefs.SetInt("ActiveSound", LocalActiveSound);
                            chooseButton.interactable = false;
                        }
                    }
                }
            }
        }
        
    }

    void OnEnable()
    {
        //
    }
}
