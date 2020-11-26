//#define HTML5
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using UnityEngine.Networking;
using CompleteProject;
using UnityEngine.EventSystems;
//using UnityEngine.Store;


namespace CompleteProject
{
    public class Skins : MonoBehaviour, IPointerEnterHandler
    {

        int ActiveColor = 1;
        const float maxColorTime = 4;
        const float BUTTON_SCALE = 1.9f;
        float remainingColorTime = maxColorTime;
        int activeTutorialStep = 0;


        [Header("Obrazki"), SerializeField]
        public GameObject rawImageA;
        public Image helpNext;
        public Image helpPrev;
        public Image helpRamka;
        public Image helpCoin;
        public Text numberText;

        RenderTexture ActiveTexture;// = new RenderTexture(SkinManager.CARD_IMAGE_WIDTH, SkinManager.CARD_IMAGE_HEIGHT, 16);
        RawImage tex;
        public Image backgroundImage;
        public Button chooseButton;
        public AudioSource soundBackground;
        public AudioSource cashBuyDone;
        public GameObject purchasePanel;
        public int LocalActiveSkin;
        public int LocalActiveFrame;
        public int LocalActiveBackground;
        public int LocalActiveSound;
        public TextMeshProUGUI cash;
        public TextMeshProUGUI price;
        public TextMeshProUGUI skinName;
        public TextMeshProUGUI infoText;
        public Image chooseButtonImage;
        bool isSkins = true;
        bool isFrames = false;
        bool isBackgrounds = false;
        bool isSounds = false;
        Vector2 normalSize;
        Vector2 biggerSize;
        float userActivityTime = 0.0f;
        //    public Image[] images;


        IEnumerator GetWWWTexture(string pathWithPrefix)
        {
            //
            byte[] imgData;


            //if ((pathWithPrefix.Contains("://")||pathWithPrefix.Contains(":///")))
            if ((Application.platform == RuntimePlatform.IPhonePlayer) ||
                (Application.platform == RuntimePlatform.OSXEditor))
            {
                imgData = System.IO.File.ReadAllBytes(pathWithPrefix);
                Debug.Log(imgData.Length);
                Texture2D texture2D = new Texture2D(2, 2);
                texture2D.LoadImage(imgData);
                Sprite fromTex = Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
                backgroundImage.sprite = fromTex;
            }
            else
            {
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(pathWithPrefix);
                yield return www.SendWebRequest();
                //imgData = www.downloadHandler.data;
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    //Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                    Texture2D texture2D = ((DownloadHandlerTexture)www.downloadHandler).texture as Texture2D;
                    Sprite fromTex = Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
                    backgroundImage.sprite = fromTex;
                }
            }
        }



        void changeSkin(string Kolor)
        {
            int pm;
            //rawImageA.GetComponent<RawImage>().material.SetTexture("_SecondaryTex", Resources.Load<Texture2D>("Ramki/" + SkinManager.instance.ramki[SkinManager.instance.ActiveFrame]));//
            // activeVideoPlayer.clip = Resources.Load(System.IO.Path.Combine(Application.streamingAssetsPath, SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + Kolor + ".mp4")) as VideoClip;

            rawImageA.GetComponent<RawImage>().material.SetTexture("_SecondaryTex", Resources.Load<Texture2D>(SkinManager.instance.ramki[LocalActiveFrame].Name));//
            //  rawImageA.GetComponent<RawImage>().material.SetTexture("_SecondaryTex", Resources.Load<Texture2D>(System.IO.Path.Combine(Application.streamingAssetsPath,
            //      "Ramki/" + SkinManager.instance.ramki[LocalActiveFrame].Name)));//


            /*   string pom2 = SkinManager.instance.ramki[LocalActiveFrame].Name + ".png";
               pom2 = System.IO.Path.Combine(Application.streamingAssetsPath, pom2);// SkinManager.instance.tla[LocalActiveBackground].Name + ".jpg";//"/Background/"
        
               //backgroundImage.sprite = Resources.Load<Sprite>(System.IO.Path.Combine(Application.streamingAssetsPath,"Background/" + SkinManager.instance.tla[LocalActiveBackground].Name) + ".jpg");//.Name

               byte[] pngBytes2 = System.IO.File.ReadAllBytes(pom2);

               //Creates texture and loads byte array data to create image
               Texture2D tex2 = new Texture2D(2, 2);
               tex2.LoadImage(pngBytes2);

               //Creates a new Sprite based on the Texture2D
               //Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

               //Assigns the UI sprite
               rawImageA.GetComponent<RawImage>().material.SetTexture("_SecondaryTex", tex2);*/

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
                    //rawImageA.GetComponent<VideoPlayer>().url = System.IO.Path.Combine(Application.streamingAssetsPath, SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name + Kolor + ".mp4");
                    //              rawImageA.GetComponent<VideoPlayer>().url = System.IO.Path.Combine(Application.streamingAssetsPath, SkinManager.instance.skorki[LocalActiveSkin].Name + Kolor + ".mp4");

                    //rawImageA.GetComponent<VideoPlayer>().clip = Resources.Load(System.IO.Path.Combine(Application.streamingAssetsPath,SkinManager.instance.skorki[LocalActiveSkin].Name + Kolor)) as VideoClip;
                    rawImageA.GetComponent<VideoPlayer>().clip = Resources.Load(SkinManager.instance.skorki[LocalActiveSkin].Name + Kolor) as VideoClip;

                    pm = (int)Mathf.Round(Random.Range(0.0f, (float)rawImageA.GetComponent<VideoPlayer>().length));
                    rawImageA.GetComponent<VideoPlayer>().frame = pm;
                    rawImageA.GetComponent<VideoPlayer>().Play();
                }
                else
                    if (SkinManager.instance.skorki[LocalActiveSkin].Type == GameManager.KARTA_STATYCZNA)
                    {
                        rawImageA.GetComponent<VideoPlayer>().targetTexture = null;
                        //rawImageA.GetComponent<RawImage>().texture = Resources.Load<Texture2D>(System.IO.Path.Combine(Application.streamingAssetsPath,SkinManager.instance.skorki[LocalActiveSkin].Name + Kolor));
                        rawImageA.GetComponent<RawImage>().texture = Resources.Load<Texture2D>(SkinManager.instance.skorki[LocalActiveSkin].Name + Kolor);
                        /* string pom = SkinManager.instance.skorki[LocalActiveSkin].Name + Kolor + ".png";
                         pom = System.IO.Path.Combine(Application.streamingAssetsPath, pom);
                         //backgroundImage.sprite = Resources.Load<Sprite>(System.IO.Path.Combine(Application.streamingAssetsPath,"Background/" + SkinManager.instance.tla[LocalActiveBackground].Name) + ".jpg");//.Name

                         byte[] pngBytes = System.IO.File.ReadAllBytes(pom);

                         //Creates texture and loads byte array data to create image
                         Texture2D tex = new Texture2D(2, 2);
                         tex.LoadImage(pngBytes);

                         //Creates a new Sprite based on the Texture2D
                         //Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

                         //Assigns the UI sprite

                         rawImageA.GetComponent<RawImage>().texture = tex;*/
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


            /*iOS uses Application.dataPath + "/Raw",
    Android uses files inside a compressed APK
    /JAR file, "jar:file://" + Application.dataPath + "!/assets".*/
            if (Application.platform == RuntimePlatform.Android)
            {
                //string pom = SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name + ".jpg";//
                string pom = SkinManager.instance.tla[LocalActiveBackground].Name + ".jpg";//
                pom = System.IO.Path.Combine("jar:file://" + Application.dataPath + "!/assets", pom);
                StartCoroutine(GetWWWTexture(pom));
            }
            //////
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                string pom3 = SkinManager.instance.tla[LocalActiveBackground].Name + ".jpg";//
                //string pom = SkinManager.instance.tla[LocalActiveBackground].Name + ".jpg";//
                //pom3 = System.IO.Path.Combine(Application.dataPath + "/Raw", pom3);
                pom3 = System.IO.Path.Combine(Application.streamingAssetsPath, pom3);
                //skinName.text = pom3;
                StartCoroutine(GetWWWTexture(pom3));
            }
            if (Application.platform == RuntimePlatform.OSXEditor)
            {
                string pom4 = SkinManager.instance.tla[LocalActiveBackground].Name + ".jpg";//

                //pom4 = System.IO.Path.Combine(Application.dataPath + "/Raw", pom4);
                //pom4 = System.IO.Path.Combine(Application.streamingAssetsPath + "/Raw", pom4);
                //pom4 = Application.streamingAssetsPath + pom4;
                pom4 = System.IO.Path.Combine(Application.streamingAssetsPath, pom4);
                StartCoroutine(GetWWWTexture(pom4));
            }
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                //string pom2 = System.IO.Path.Combine(Application.streamingAssetsPath, SkinManager.instance.tla[LocalActiveBackground].Name + ".jpg");
                string pom2 = Application.streamingAssetsPath + "/" + SkinManager.instance.tla[LocalActiveBackground].Name + ".jpg";
                //backgroundImage.sprite = Resources.Load<Sprite>(SkinManager.instance.tla[LocalActiveBackground].Name);
                //backgroundImage.sprite = Resources.Load<Sprite>(System.IO.Path.Combine(Application.streamingAssetsPath, SkinManager.instance.tla[LocalActiveBackground].Name) + ".jpg");
                StartCoroutine(GetWWWTexture(pom2));
                //Debug.Log(pom2);
            }
            //#if UNITY_ANDROID

            //#endif

            //#if UNITY_EDITOR

            //#endif

        }

        /*   IEnumerator changeAsyncSound()
           {
               string pom = Application.streamingAssetsPath + "/Audio/" + SkinManager.instance.muzyki[LocalActiveSound].Name + ".ogg";
               UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(pom, AudioType.OGGVORBIS);

               soundBackground.clip = (AudioClip)Resources.Load("Audio/" + SkinManager.instance.muzyki[LocalActiveSound].Name); 
               soundBackground.Play();

               yield return request.SendWebRequest();

               if (request.isNetworkError)
               {
                   Debug.LogWarning(request.error + "\n" + pom);
               }
               else
               {

               }
        
           }*/

        void changeSound()
        {
            soundBackground.clip = (AudioClip)Resources.Load("Audio/" + SkinManager.instance.muzyki[LocalActiveSound].Name); ;
            soundBackground.Play();
            //Debug.Log(request);
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
            //CompleteProject.Purchaser.Start();
            //UnityEngine.UDP.IInitListener listener = new UnityEngine.UDP.IInitListener();
          //  UnityEngine.UDP.AppInfo appInfo;
           // StoreService.Initialize( listener);
           // UnityEngine.UDP.StoreService.Initialize(listener, appInfo);
            //StoreService.Initialize( listener);
            //var myLoginListener = new IInitListener();
            //InitListener listener;
            //StoreService.Initialize( listener);


            //StoreService.Initialize(IInitListener, listener);

            userActivityTime = SkinManager.MAX_USER_DISACTIVITY;
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
            soundBackground.volume = SkinManager.instance.ActiveSoundValue / 100;
            //changeAsyncSound();
            skinName.text = SkinManager.instance.skorki[LocalActiveSkin].Title;
            cash.text = SkinManager.instance.CurrentCash.ToString();
            isSkins = true;
            numberText.text = (LocalActiveSkin + 1).ToString() + " / " + SkinManager.instance.skorki.Count.ToString();

            if (!SkinManager.instance.isSkinTutorialPass)
            {
                infoText.gameObject.SetActive(true);
                activeTutorialStep = SkinManager.SAMOUCZEK_SKLEP_POCZATEK;
                infoText.text = SkinManager.instance.SkinTutorialLang[activeTutorialStep];
                Debug.Log("Tutorial");
            }
            else
            {
                infoText.gameObject.SetActive(false);
                Debug.Log("notTutorial");
            }
        }

        // Update is called once per frame
        void Update()
        {
            //change colors
            if (remainingColorTime >= 0)
            {
                remainingColorTime -= Time.deltaTime;
            }
            else
            {
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

            if (userActivityTime > 0)
            {
                userActivityTime -= Time.deltaTime;
                helpNext.gameObject.SetActive(false);
                helpPrev.gameObject.SetActive(false);
                helpCoin.gameObject.SetActive(false);
                helpRamka.gameObject.SetActive(false);
            }
            else
            {
                if (!SkinManager.instance.isSkinTutorialPass)
                {
                    if (activeTutorialStep == SkinManager.SAMOUCZEK_SKLEP_WLACZ_RAMKI)
                        helpRamka.gameObject.SetActive(true);
                    else
                        if (activeTutorialStep == SkinManager.SAMOUCZEK_SKLEP_WYBIERZ_RAMKE)
                            helpCoin.gameObject.SetActive(true);
                        else
                        {
                            helpNext.gameObject.SetActive(true);
                            helpPrev.gameObject.SetActive(true);
                        }
                }
                else
                {
                    helpNext.gameObject.SetActive(true);
                    helpPrev.gameObject.SetActive(true);
                }
            }
            /* string pom = SkinManager.instance.tla[LocalActiveBackground].Name + ".jpg";//"/Background/"
             pom = System.IO.Path.Combine("jar:file://" + Application.dataPath + "!/assets", pom);
             skinName.text = pom;*/

            /*string pom4 = SkinManager.instance.tla[LocalActiveBackground].Name + ".jpg";//

            //pom4 = System.IO.Path.Combine(Application.dataPath + "/Raw", pom4);
            pom4 = System.IO.Path.Combine(Application.streamingAssetsPath , pom4);
            //pom4 = Path.Combine(Application.streamingAssetsPath,pom4);
            skinName.text = pom4;*/
            //skinName.text = SkinManager.instance.GetAIPToShow();

        }

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            userActivityTime = SkinManager.MAX_USER_DISACTIVITY;
        }

        public void Buy20Coins()
        {
            /*if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                int newCash;

                newCash = SkinManager.instance.CurrentCash + 20;
                SkinManager.instance.SetCurrentCash(newCash);
                PlayerPrefs.SetInt("CurrentCash", newCash);
            }*/
            cashBuyDone.Play();
            cash.text = SkinManager.instance.CurrentCash.ToString();
            BackPurchase();
            //  purchasePanel.gameObject.SetActive(false);//throw runtime error!
        }

        public void Buy100Coins()
        {
            /* if (Application.platform == RuntimePlatform.IPhonePlayer)
             {
                 int newCash;

                 newCash = SkinManager.instance.CurrentCash + 100;
                 SkinManager.instance.SetCurrentCash(newCash);
                 PlayerPrefs.SetInt("CurrentCash", newCash);
             }*/
            cashBuyDone.Play();
            cash.text = SkinManager.instance.CurrentCash.ToString();
            BackPurchase();
            // purchasePanel.gameObject.SetActive(false);//throw runtime error!
        }

        public void Buy350Coins()
        {
            /*if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                int newCash;

                newCash = SkinManager.instance.CurrentCash + 350;
                SkinManager.instance.SetCurrentCash(newCash);
                PlayerPrefs.SetInt("CurrentCash", newCash);
            }*/
            cashBuyDone.Play();
            cash.text = SkinManager.instance.CurrentCash.ToString();
            BackPurchase();
            //  purchasePanel.gameObject.SetActive(false);//throw runtime error!
        }

        public void BuyFailed()
        {
            // purchasePanel.gameObject.SetActive(false); ////throw runtime error!
            //skinName.text = "Purchase FAILED";
            cash.text = SkinManager.instance.CurrentCash.ToString();
            purchasePanel.gameObject.SetActive(false);
            //BackPurchase();
        }

        public void BuySucced()
        {
            //nothing yet
            cash.text = SkinManager.instance.CurrentCash.ToString();
            purchasePanel.gameObject.SetActive(false);
            //BackPurchase();
        }

        public void Back()
        {
            SceneManager.LoadScene("Menu");
        }

        public void BackPurchase()
        {
            cash.text = SkinManager.instance.CurrentCash.ToString();
            purchasePanel.gameObject.SetActive(false);
            checkPriceColor();
        }

        public void EnablePurchase()
        {
            if (!SkinManager.instance.isSkinTutorialPass)
            {
                if (activeTutorialStep == -1)
                {
                    activeTutorialStep = SkinManager.SAMOUCZEK_SKLEP_WLACZ_RAMKI;
                    infoText.text = SkinManager.instance.SkinTutorialLang[activeTutorialStep];
                }
                else
                {
                    return;
                }
            }

            purchasePanel.gameObject.SetActive(true);
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


        public void checkPriceColor()
        {
            if (int.Parse(cash.text) < int.Parse(price.text))
            {
                price.color = new Color32(255, 0, 0, 255);
            }
            else
            {
                price.color = new Color32(0, 255, 0, 255);
            }
        }

        public void Next()
        {
            if (!SkinManager.instance.isSkinTutorialPass)
            {
                Debug.Log("Step:" + activeTutorialStep);
                if (activeTutorialStep == SkinManager.SAMOUCZEK_SKLEP_POCZATEK)
                {
                    activeTutorialStep = SkinManager.SAMOUCZEK_SKLEP_WLACZ_RAMKI;
                    infoText.text = SkinManager.instance.SkinTutorialLang[activeTutorialStep];
                }
                else
                    if (activeTutorialStep == SkinManager.SAMOUCZEK_SKLEP_ZMIEN_RAMKI)
                    {
                        activeTutorialStep = SkinManager.SAMOUCZEK_SKLEP_WYBIERZ_RAMKE;
                        infoText.text = SkinManager.instance.SkinTutorialLang[activeTutorialStep];
                    }
                if (activeTutorialStep == SkinManager.SAMOUCZEK_SKLEP_WYBIERZ_RAMKE)
                {

                }
                else
                {
                    return;
                }
            }

            userActivityTime = SkinManager.MAX_USER_DISACTIVITY;
            if (isSkins)
            {

                if (LocalActiveSkin < SkinManager.instance.skorki.Count - 1)
                {
                    LocalActiveSkin++;
                }
                else
                {
                    LocalActiveSkin = 0;
                }
                {
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
                numberText.text = (LocalActiveSkin + 1).ToString() + " / " + SkinManager.instance.skorki.Count.ToString();
            }
            else
            {
                if (isFrames)
                {

                    if (LocalActiveFrame < SkinManager.instance.ramki.Count - 1)
                    {
                        LocalActiveFrame++;
                    }
                    else
                    {
                        LocalActiveFrame = 0;
                    }
                    {
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
                    numberText.text = (LocalActiveFrame + 1).ToString() + " / " + SkinManager.instance.ramki.Count.ToString();
                }
                else
                {
                    if (isBackgrounds)
                    {

                        if (LocalActiveBackground < SkinManager.instance.tla.Count - 1)
                        {
                            LocalActiveBackground++;
                        }
                        else
                        {
                            LocalActiveBackground = 0;
                        }
                        {
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
                        numberText.text = (LocalActiveBackground + 1).ToString() + " / " + SkinManager.instance.tla.Count.ToString();
                    }
                    else
                    {
                        if (isSounds)
                        {

                            if (LocalActiveSound < SkinManager.instance.muzyki.Count - 1)
                            {
                                LocalActiveSound++;
                            }
                            else
                            {
                                LocalActiveSound = 0;
                            }
                            {
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
                                //changeAsyncSound();
                            }
                            numberText.text = (LocalActiveSound + 1).ToString() + " / " + SkinManager.instance.muzyki.Count.ToString();
                        }
                    }
                }
            }

            /* if (int.Parse(cash.text) < int.Parse(price.text))
             {
                 price.color = new Color32(255, 0, 0, 255);
             }
             else
             {
                 price.color = new Color32(0, 255, 0, 255);
             }*/
            checkPriceColor();
        }

        public void Prev()
        {
            if (!SkinManager.instance.isSkinTutorialPass)
            {
                if (activeTutorialStep == SkinManager.SAMOUCZEK_SKLEP_POCZATEK)
                {
                    activeTutorialStep = SkinManager.SAMOUCZEK_SKLEP_WLACZ_RAMKI;
                    infoText.text = SkinManager.instance.SkinTutorialLang[activeTutorialStep];
                }
                if (activeTutorialStep == SkinManager.SAMOUCZEK_SKLEP_ZMIEN_RAMKI)
                {
                    activeTutorialStep = SkinManager.SAMOUCZEK_SKLEP_WYBIERZ_RAMKE;
                    infoText.text = SkinManager.instance.SkinTutorialLang[activeTutorialStep];
                }
                if (activeTutorialStep == SkinManager.SAMOUCZEK_SKLEP_WYBIERZ_RAMKE)
                {

                }
                else
                {
                    return;
                }
            }

            userActivityTime = SkinManager.MAX_USER_DISACTIVITY;
            if (isSkins)
            {

                if (LocalActiveSkin > 0)
                {
                    LocalActiveSkin--;
                }
                else
                {
                    LocalActiveSkin = SkinManager.instance.skorki.Count - 1;
                }

                {
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
                numberText.text = (LocalActiveSkin + 1).ToString() + " / " + SkinManager.instance.skorki.Count.ToString();
            }
            else
            {
                if (isFrames)
                {

                    if (LocalActiveFrame > 0)
                    {
                        LocalActiveFrame--;
                    }
                    else
                    {
                        LocalActiveFrame = SkinManager.instance.ramki.Count - 1;
                    }

                    {
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
                    numberText.text = (LocalActiveFrame + 1).ToString() + " / " + SkinManager.instance.ramki.Count.ToString();
                }
                else
                {
                    if (isBackgrounds)
                    {

                        if (LocalActiveBackground > 0)
                        {
                            LocalActiveBackground--;
                        }
                        else
                        {
                            LocalActiveBackground = SkinManager.instance.tla.Count - 1;
                        }

                        {
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
                        numberText.text = (LocalActiveBackground + 1).ToString() + " / " + SkinManager.instance.tla.Count.ToString();
                    }
                    else
                    {
                        if (isSounds)
                        {

                            if (LocalActiveSound > 0)
                            {
                                LocalActiveSound--;
                            }
                            else
                            {
                                LocalActiveSound = SkinManager.instance.muzyki.Count - 1;
                            }

                            {
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
                                //changeAsyncSound();
                            }
                            numberText.text = (LocalActiveSound + 1).ToString() + " / " + SkinManager.instance.muzyki.Count.ToString();
                        }
                    }
                }
            }

            /* if (int.Parse(cash.text) < int.Parse(price.text))
             {
                 price.color = new Color32(255, 0, 0, 255);
             }
             else
             {
                 price.color = new Color32(0, 255, 0, 255);
             }*/
            checkPriceColor();
        }

        public void SwitchSkin()
        {
            if (!SkinManager.instance.isSkinTutorialPass)
            {
                if (activeTutorialStep == -1)
                {
                    activeTutorialStep = SkinManager.SAMOUCZEK_SKLEP_WLACZ_RAMKI;
                    infoText.text = SkinManager.instance.SkinTutorialLang[activeTutorialStep];
                }
                else
                {
                    return;
                }
            }

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
                //changeAsyncSound();
                changeSound();
            }
            changeBackground();
            changeSkin(SkinManager.instance.COLORS_ARRAY[ActiveColor]);
            cash.color = new Color32(255, 255, 0, 255);
            price.color = new Color32(0, 255, 0, 255);
            price.text = "0";
            userActivityTime = 0;
            numberText.text = (LocalActiveSkin + 1).ToString() + " / " + SkinManager.instance.skorki.Count.ToString();
        }

        public void SwitchFrame()
        {

            if (!SkinManager.instance.isSkinTutorialPass)
            {
                if (activeTutorialStep == SkinManager.SAMOUCZEK_SKLEP_WLACZ_RAMKI)
                {
                    activeTutorialStep = SkinManager.SAMOUCZEK_SKLEP_ZMIEN_RAMKI;
                    infoText.text = SkinManager.instance.SkinTutorialLang[activeTutorialStep];
                }
                else
                {
                    return;
                }
            }

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
                //changeAsyncSound();
                changeSound();
            }
            changeBackground();
            changeSkin(SkinManager.instance.COLORS_ARRAY[ActiveColor]);
            cash.color = new Color32(255, 255, 0, 255);
            price.color = new Color32(0, 255, 0, 255);
            price.text = "0";
            userActivityTime = 0;
            numberText.text = (LocalActiveFrame + 1).ToString() + " / " + SkinManager.instance.ramki.Count.ToString();
        }

        public void SwitchBackground()
        {

            if (!SkinManager.instance.isSkinTutorialPass)
            {
                if (activeTutorialStep == -1)
                {
                    activeTutorialStep = SkinManager.SAMOUCZEK_SKLEP_WLACZ_RAMKI;
                    infoText.text = SkinManager.instance.SkinTutorialLang[activeTutorialStep];
                }
                else
                {
                    return;
                }
            }

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
                //changeAsyncSound();
                changeSound();
            }
            changeSkin(SkinManager.instance.COLORS_ARRAY[ActiveColor]);
            cash.color = new Color32(255, 255, 0, 255);
            price.color = new Color32(0, 255, 0, 255);
            price.text = "0";
            userActivityTime = 0;
            numberText.text = (LocalActiveBackground + 1).ToString() + " / " + SkinManager.instance.tla.Count.ToString();
        }

        public void SwitchSound()
        {
            if (!SkinManager.instance.isSkinTutorialPass)
            {
                if (activeTutorialStep == -1)
                {
                    activeTutorialStep = SkinManager.SAMOUCZEK_SKLEP_WLACZ_RAMKI;
                    infoText.text = SkinManager.instance.SkinTutorialLang[activeTutorialStep];
                }
                else
                {
                    return;
                }
            }

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
            userActivityTime = 0;
            numberText.text = (LocalActiveSound + 1).ToString() + " / " + SkinManager.instance.muzyki.Count.ToString();
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
            if (!SkinManager.instance.isSkinTutorialPass)
            {
                if (activeTutorialStep == SkinManager.SAMOUCZEK_SKLEP_WYBIERZ_RAMKE)
                {
                    activeTutorialStep = SkinManager.SAMOUCZEK_SKLEP_UKONCZ;
                    infoText.text = SkinManager.instance.SkinTutorialLang[activeTutorialStep];
                    PlayerPrefs.SetInt("isSkinTutorialPass", true ? 1 : 0);
                    SkinManager.instance.SetIsSkinTutorialPass(true);
                    //infoText.gameObject.SetActive(false);
                }
                else
                {
                    return;
                }
            }

            userActivityTime = SkinManager.MAX_USER_DISACTIVITY;
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
}
