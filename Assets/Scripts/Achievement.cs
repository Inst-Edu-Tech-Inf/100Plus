using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Linq;

public class Achievement : MonoBehaviour
{
    [Header("Background"), SerializeField]
    public Image backgroundImage;
    public Slider progres;
    public Text description;
    public Text reward;
    public Image middlePass;
    public Image latePass;
    public Image winSolo;
    public Image winSI;
    public Image winPVP;
    public Image multiplyTwice;
    public Image multiplyThree;
    public Image multiplyFour;
    public Image pureGame;
    public Image unlockAllSkins;
    public Image fasterThanLightMiddle;
    public Image fasterThanLightLate;
    public Image lucky;
    public Image pure1KSolo;
    public Image notPure1KSolo;
    public Image pure2KSolo;
    public Image notPure5KSolo;
    public Image pure4KSolo;
    public Image notPure10KSolo;
    public Image pure1KSI;
    public Image notPure1KSI;
    public Image pure2KSI;
    public Image notPure5KSI;
    public Image pure4KSI;
    public Image notPure10KSI;
    public Image pure1KPVP;
    public Image notPure1KPVP;
    public Image pure2KPVP;
    public Image notPure5KPVP;
    public Image pure4KPVP;
    public Image notPure10KPVP;
    public Image fillSlider;
    public Image longWay;
    public Text bestResult;
    Color colorGray = new Color(50f / 255f, 50f / 255f, 50f / 255f);
    Color colorWhite = new Color(255f / 255f, 255f / 255f, 255f / 255f);
    GameObject manager;
    float sliderMax;
    float sliderStep;
    Image background;
    RectTransform slider;
    Gradient g;


    IEnumerator GetWWWTexture(string pathWithPrefix)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(pathWithPrefix);
        yield return www.SendWebRequest();

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

    // Start is called before the first frame update
    void Start()
    {

        //var fill = (slider as UnityEngine.UI.Slider).GetComponentsInChildren<UnityEngine.UI.Image>()
        var fill = progres.GetComponentsInChildren<UnityEngine.UI.Image>().FirstOrDefault(t => t.name == "Fill");
        if (fill != null)
        {
            fill.color = Color.green;// Color.Lerp(Color.red, Color.green, 0.5f);
        }
       /* {
            //manager = GameObject.Find(Names.Game).GetComponentInChildren<LevelManager>();
 
            background = progres.GetComponent<Image>();
            slider = progres.GetComponent<RectTransform>();
            sliderMax = slider.rect.width;
            sliderStep = sliderMax / 100;// manager.totalTime;
 
            g = new Gradient();
            var gck = new GradientColorKey[2];
            gck[0].color = Color.red;
            gck[0].time = 0.0f;
 
            gck[1].color = Color.green;
            gck[1].time = 1.0f;
 
            // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
            var gak = new GradientAlphaKey[0];
 
            g.SetKeys(gck, gak);
        }*/
 /*
    // Update is called once per frame
    void Update()
    {
        background.color = g.Evaluate(manager.currentTime / manager.totalTime);
        slider.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, manager.currentTime * sliderStep);
    }
 * */
        //changeBackground();
        bestResult.text = SkinManager.instance.BestResult.ToString();

        if (SkinManager.instance.MiddlePass)
        {
            middlePass.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            middlePass.GetComponent<Image>().color = colorGray;
        }
        

        if (SkinManager.instance.LatePass)
        {
            latePass.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            latePass.GetComponent<Image>().color = colorGray;
        }
        

        if (SkinManager.instance.WinSolo)
        {
            winSolo.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            winSolo.GetComponent<Image>().color = colorGray;
        }

        if (SkinManager.instance.WinSI)
        {
            winSI.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            winSI.GetComponent<Image>().color = colorGray;
        }

        if (SkinManager.instance.WinPVP)
        {
            winPVP.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            winPVP.GetComponent<Image>().color = colorGray;
        }

        if (SkinManager.instance.MultiplyTwice)
        {
            multiplyTwice.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            multiplyTwice.GetComponent<Image>().color = colorGray;
        }

        if (SkinManager.instance.MultiplyThree)
        {
            multiplyThree.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            multiplyThree.GetComponent<Image>().color = colorGray;
        }

        if (SkinManager.instance.MultiplyFour)
        {
            multiplyFour.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            multiplyFour.GetComponent<Image>().color = colorGray;
        }

        if (SkinManager.instance.PureGame)
        {
            pureGame.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            pureGame.GetComponent<Image>().color = colorGray;
        }

        if (SkinManager.instance.UnlockAllSkins)
        {
            unlockAllSkins.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            unlockAllSkins.GetComponent<Image>().color = colorGray;
        }

        if (SkinManager.instance.FasterThanLightMiddle)
        {
            fasterThanLightMiddle.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            fasterThanLightMiddle.GetComponent<Image>().color = colorGray;
        }

        if (SkinManager.instance.FasterThanLightLate)
        {
            fasterThanLightLate.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            fasterThanLightLate.GetComponent<Image>().color = colorGray;
        }

        if (SkinManager.instance.Lucky)
        {
            lucky.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            lucky.GetComponent<Image>().color = colorGray;
        }

        if (SkinManager.instance.LongWay)
        {
            longWay.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            longWay.GetComponent<Image>().color = colorGray;
        }
        
        //solo game
        if (SkinManager.instance.isPureSolo1)//2,3
        {
            pure1KSolo.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            pure1KSolo.GetComponent<Image>().color = colorGray;
        }
        if (SkinManager.instance.isPureSolo2)//2,3
        {
            pure2KSolo.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            pure2KSolo.GetComponent<Image>().color = colorGray;
        }
        if (SkinManager.instance.isPureSolo3)//2,3
        {
            pure4KSolo.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            pure4KSolo.GetComponent<Image>().color = colorGray;
        }

        if (SkinManager.instance.isNotPureSolo1)//2,3
        {
            notPure1KSolo.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            notPure1KSolo.GetComponent<Image>().color = colorGray;
        }
        if (SkinManager.instance.isNotPureSolo2)//2,3
        {
            notPure5KSolo.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            notPure5KSolo.GetComponent<Image>().color = colorGray;
        }
        if (SkinManager.instance.isNotPureSolo3)//2,3
        {
            notPure10KSolo.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            notPure10KSolo.GetComponent<Image>().color = colorGray;
        }
        
        //SI game
        if (SkinManager.instance.isPureSI1)//2,3
        {
            pure1KSI.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            pure1KSI.GetComponent<Image>().color = colorGray;
        }
        if (SkinManager.instance.isPureSI2)//2,3
        {
            pure2KSI.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            pure2KSI.GetComponent<Image>().color = colorGray;
        }
        if (SkinManager.instance.isPureSI3)//2,3
        {
            pure4KSI.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            pure4KSI.GetComponent<Image>().color = colorGray;
        }

        if (SkinManager.instance.isNotPureSI1)//2,3
        {
            notPure1KSI.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            notPure1KSI.GetComponent<Image>().color = colorGray;
        }
        if (SkinManager.instance.isNotPureSI2)//2,3
        {
            notPure5KSI.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            notPure5KSI.GetComponent<Image>().color = colorGray;
        }
        if (SkinManager.instance.isNotPureSI3)//2,3
        {
            notPure10KSI.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            notPure10KSI.GetComponent<Image>().color = colorGray;
        }

        //PVP game
        if (SkinManager.instance.isPurePVP1)//2,3
        {
            pure1KPVP.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            pure1KPVP.GetComponent<Image>().color = colorGray;
        }
        if (SkinManager.instance.isPurePVP2)//2,3
        {
            pure2KPVP.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            pure2KPVP.GetComponent<Image>().color = colorGray;
        }
        if (SkinManager.instance.isPurePVP3)//2,3
        {
            pure4KPVP.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            pure4KPVP.GetComponent<Image>().color = colorGray;
        }

        if (SkinManager.instance.isNotPurePVP1)//2,3
        {
            notPure1KPVP.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            notPure1KPVP.GetComponent<Image>().color = colorGray;
        }
        if (SkinManager.instance.isNotPurePVP2)//2,3
        {
            notPure5KPVP.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            notPure5KPVP.GetComponent<Image>().color = colorGray;
        }
        if (SkinManager.instance.isNotPurePVP3)//2,3
        {
            notPure10KPVP.GetComponent<Image>().color = colorWhite;
        }
        else
        {
            notPure10KPVP.GetComponent<Image>().color = colorGray;
        }

        MiddlePassClick();
    }

    public void LongWayClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.LONGWAY].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.LONGWAY].Reward.ToString();
        progres.value = 0;
    }

    public void LuckyClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.LUCKY].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.LUCKY].Reward.ToString();
        progres.value = 0;
    }

    public void MiddlePassClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.MIDDLEPASS].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.MIDDLEPASS].Reward.ToString();
        progres.value = 0;
    }

    public void LatePassClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.LATEPASS].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.LATEPASS].Reward.ToString();
        progres.value = 0;
    }

    public void WinSoloClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.WINSOLO].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.WINSOLO].Reward.ToString();
        progres.value = 0;
    }

    public void WinSIClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.WINSI].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.WINSI].Reward.ToString();
        progres.value = 0;
    }

    public void WinPVPClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.WINPVP].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.WINPVP].Reward.ToString();
        progres.value = 0;
    }

    public void MultiplyTwiceClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.MULTIPLYTWICE].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.MULTIPLYTWICE].Reward.ToString();
        progres.value = 0;
    }

    public void MultiplyThreeClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.MULTIPLYTHREE].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.MULTIPLYTHREE].Reward.ToString();
        progres.value = 0;
    }

    public void MultiplyFourClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.MULTIPLYFOUR].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.MULTIPLYFOUR].Reward.ToString();
        progres.value = 0;
    }

    public void PureGameClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.PUREGAME].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.PUREGAME].Reward.ToString();
        progres.value = 0;
    }

    public void UnlockAllSkinsClick()
    {
        if (SkinManager.instance.UnlockAllSkins)
        {
            description.text = SkinManager.instance.osiagniecia[SkinManager.UNLOCKALLSKINS].Description;
        }
        else
        {
            description.text = "Hidden yet";
        }
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.UNLOCKALLSKINS].Reward.ToString();
        progres.value = 0;
    }



    public void FasterThanLightMiddleClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.FASTERTHANLIGHTMIDDLE].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.FASTERTHANLIGHTMIDDLE].Reward.ToString();
        progres.value = 0;
    }

    public void FasterThanLightLateClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.FASTERTHANLIGHTLATE].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.FASTERTHANLIGHTLATE].Reward.ToString();
        progres.value = 0;
    }

    //SOLO game PURE and NOT
    public void Pure1KSoloClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.PURE1KSOLO].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.PURE1KSOLO].Reward.ToString();
        progres.maxValue = SkinManager.ACHIEVEMENT_PURE_1ST;
        progres.value = SkinManager.instance.PureSolo;
        //Debug.Log("Pure:"+SkinManager.instance.PureSolo);
    }

    public void Pure2KSoloClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.PURE2KSOLO].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.PURE2KSOLO].Reward.ToString();
        progres.maxValue = SkinManager.ACHIEVEMENT_PURE_2ND;
        progres.value = SkinManager.instance.PureSolo;
        //Debug.Log("Pure:" + SkinManager.instance.PureSolo);
    }

    public void Pure4KSoloClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.PURE4KSOLO].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.PURE4KSOLO].Reward.ToString();
        progres.maxValue = SkinManager.ACHIEVEMENT_PURE_3RD;
        progres.value = SkinManager.instance.PureSolo;
        //Debug.Log("Pure:" + SkinManager.instance.PureSolo);
    }

    public void NotPure1KSoloClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.NOTPURE1KSOLO].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.NOTPURE1KSOLO].Reward.ToString();
        progres.maxValue = SkinManager.ACHIEVEMENT_NOT_PURE_1ST;
        progres.value = SkinManager.instance.NotPureSolo;
        //Debug.Log("NotPure:"+SkinManager.instance.NotPureSolo);
    }

    public void NotPure5KSoloClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.NOTPURE5KSOLO].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.NOTPURE5KSOLO].Reward.ToString();
        progres.maxValue = SkinManager.ACHIEVEMENT_NOT_PURE_2ND;
        progres.value = SkinManager.instance.NotPureSolo;
        //Debug.Log("NotPure:"+SkinManager.instance.NotPureSolo);
    }

    public void NotPure10KSoloClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.NOTPURE10KSOLO].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.NOTPURE10KSOLO].Reward.ToString();
        progres.maxValue = SkinManager.ACHIEVEMENT_NOT_PURE_3RD;
        progres.value = SkinManager.instance.NotPureSolo;
        //Debug.Log("NotPure:"+SkinManager.instance.NotPureSolo);
    }

    //SI game PURE and NOT
    public void Pure1KSIClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.PURE1KSI].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.PURE1KSI].Reward.ToString();
        progres.maxValue = SkinManager.ACHIEVEMENT_PURE_1ST;
        progres.value = SkinManager.instance.PureSI;
        //Debug.Log("Pure:"+SkinManager.instance.PureSI);
    }

    public void Pure2KSIClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.PURE2KSI].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.PURE2KSI].Reward.ToString();
        progres.maxValue = SkinManager.ACHIEVEMENT_PURE_2ND;
        progres.value = SkinManager.instance.PureSI;
        //Debug.Log("Pure:" + SkinManager.instance.PureSI);
    }

    public void Pure4KSIClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.PURE4KSI].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.PURE4KSI].Reward.ToString();
        progres.maxValue = SkinManager.ACHIEVEMENT_PURE_3RD;
        progres.value = SkinManager.instance.PureSI;
        //Debug.Log("Pure:" + SkinManager.instance.PureSI);
    }

    public void NotPure1KSIClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.NOTPURE1KSI].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.NOTPURE1KSI].Reward.ToString();
        progres.maxValue = SkinManager.ACHIEVEMENT_NOT_PURE_1ST;
        progres.value = SkinManager.instance.NotPureSI;
        //Debug.Log("NotPure:"+SkinManager.instance.NotPureSI);
    }

    public void NotPure5KSIClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.NOTPURE5KSI].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.NOTPURE5KSI].Reward.ToString();
        progres.maxValue = SkinManager.ACHIEVEMENT_NOT_PURE_2ND;
        progres.value = SkinManager.instance.NotPureSI;
        //Debug.Log("NotPure:"+SkinManager.instance.NotPureSI);
    }

    public void NotPure10KSIClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.NOTPURE10KSI].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.NOTPURE10KSI].Reward.ToString();
        progres.maxValue = SkinManager.ACHIEVEMENT_NOT_PURE_3RD;
        progres.value = SkinManager.instance.NotPureSI;
        //Debug.Log("NotPure:"+SkinManager.instance.NotPureSI);
    }

    //PVP game PURE and NOT
    public void Pure1KPVPClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.PURE1KPVP].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.PURE1KPVP].Reward.ToString();
        progres.maxValue = SkinManager.ACHIEVEMENT_PURE_1ST;
        progres.value = SkinManager.instance.PurePVP;
        //Debug.Log("Pure:"+SkinManager.instance.PurePVP);
    }

    public void Pure2KPVPClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.PURE2KPVP].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.PURE2KPVP].Reward.ToString();
        progres.maxValue = SkinManager.ACHIEVEMENT_PURE_2ND;
        progres.value = SkinManager.instance.PurePVP;
        //Debug.Log("Pure:" + SkinManager.instance.PurePVP);
    }

    public void Pure4KPVPClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.PURE4KPVP].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.PURE4KPVP].Reward.ToString();
        progres.maxValue = SkinManager.ACHIEVEMENT_PURE_3RD;
        progres.value = SkinManager.instance.PurePVP;
        //Debug.Log("Pure:" + SkinManager.instance.PurePVP);
    }

    public void NotPure1KPVPClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.NOTPURE1KPVP].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.NOTPURE1KPVP].Reward.ToString();
        progres.maxValue = SkinManager.ACHIEVEMENT_NOT_PURE_1ST;
        progres.value = SkinManager.instance.NotPurePVP;
        //Debug.Log("NotPure:"+SkinManager.instance.NotPurePVP);
    }

    public void NotPure5KPVPClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.NOTPURE5KPVP].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.NOTPURE5KPVP].Reward.ToString();
        progres.maxValue = SkinManager.ACHIEVEMENT_NOT_PURE_2ND;
        progres.value = SkinManager.instance.NotPurePVP;
        //Debug.Log("NotPure:"+SkinManager.instance.NotPurePVP);
    }

    public void NotPure10KPVPClick()
    {
        description.text = SkinManager.instance.osiagniecia[SkinManager.NOTPURE10KPVP].Description;
        reward.text = "+" + SkinManager.instance.osiagniecia[SkinManager.NOTPURE10KPVP].Reward.ToString();
        progres.maxValue = SkinManager.ACHIEVEMENT_NOT_PURE_3RD;
        progres.value = SkinManager.instance.NotPurePVP;
        //Debug.Log("NotPure:"+SkinManager.instance.NotPurePVP);
    }

    // Update is called once per frame
    void Update()
    {
      /*  g = new Gradient();
        var gck = new GradientColorKey[2];
        gck[0].color = Color.red;
        gck[0].time = 0.0f;

        gck[1].color = Color.green;
        gck[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        var gak = new GradientAlphaKey[0];

        g.SetKeys(gck, gak);

        sliderMax = progres.GetComponent<RectTransform>().rect.width;

        var fill = progres.GetComponentsInChildren<UnityEngine.UI.Image>().FirstOrDefault(t => t.name == "Fill");
        if (fill != null)
            for (int i=0; i<10; ++i)
            {
                fill.color = g.Evaluate(i/10);// Color.Lerp(Color.red, Color.green, 0.1f);
                progres.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, i/10*sliderMax);//manager.currentTime * sliderStep);

            }*/
/*
        //background = progres.GetComponent<Image>();
        //slider = progres.GetComponent<RectTransform>();
        sliderMax = progres.GetComponent<RectTransform>().rect.width;
        sliderStep = sliderMax / 100;// manager.totalTime;

        g = new Gradient();
        var gck = new GradientColorKey[2];
        gck[0].color = Color.red;
        gck[0].time = 0.0f;

        gck[1].color = Color.green;
        gck[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        var gak = new GradientAlphaKey[0];

        g.SetKeys(gck, gak);

        fillSlider.color = Color.red;//g.Evaluate(0.5f);//manager.currentTime / manager.totalTime);
        //progres.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1.0f);//manager.currentTime * sliderStep);
   */
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    void changeBackground()
    {
        //        backgroundImage.sprite = Resources.Load<Sprite>(SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name);
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            string pom2 = Application.streamingAssetsPath + "/" + SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name + ".jpg";
            StartCoroutine(GetWWWTexture(pom2));
        }
        /*iOS uses Application.dataPath + "/Raw",
Android uses files inside a compressed APK
/JAR file, "jar:file://" + Application.dataPath + "!/assets".*/
        if (Application.platform == RuntimePlatform.Android)
        {
            string pom = SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name + ".jpg";//
            pom = System.IO.Path.Combine("jar:file://" + Application.dataPath + "!/assets", pom);
            StartCoroutine(GetWWWTexture(pom));
        }
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string pom3 = SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name + ".jpg";//
            //string pom = SkinManager.instance.tla[LocalActiveBackground].Name + ".jpg";//
            pom3 = System.IO.Path.Combine(Application.dataPath + "/Raw", pom3);
            StartCoroutine(GetWWWTexture(pom3));
        }
    }
}
