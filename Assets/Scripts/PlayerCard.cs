﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PlayerCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    GameObject hands;
    GameManager gm;
    public GameObject ActualParent = null;
    public Image image;
    public Image frameImage;
    public AudioSource cardMissSFX;
    public AudioSource cardCorrectSFX;
    Vector3 originalPosition;
    Vector2 normalScale = new Vector2(1.9f, 1.9f); 
    Vector2 biggerScale = new Vector2(2.2f, 2.2f); 
    TextMeshProUGUI additionText;
    public bool hasMultiply;               

    void Start()
    {
        string SubStr;
        hands = GameObject.Find("Hands");
        hasMultiply = false;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        image = GameObject.Find("PlayerCardImage").GetComponent<Image>();
        additionText = transform.Find("Addition Text").GetComponent<TextMeshProUGUI>();
        float rand = Random.Range(1, GameManager.COLOR_NUMBER+1 );//to number of colors
        if (float.Parse(gm.victoryPoints.text) < gm.earlyGamePoint)
        {
            additionText.text = Random.Range(1, gm.earlyGamePlayerCardMax).ToString();
        }
        else 
        {
            if  (float.Parse(gm.victoryPoints.text) < gm.middleGamePoint)
            {
                if (Random.Range(1, 100) <= gm.earlyChanceOnMiddle)
                {
                    additionText.text = Random.Range(1, gm.earlyGamePlayerCardMax).ToString();
                }
                else{
                    additionText.text = Random.Range(gm.earlyGamePlayerCardMax, gm.middleGamePlayerCardMax).ToString();
                }             
            }
            else //lateGamePoint
            {

                if (Random.Range(1, 100) <= gm.earlyChanceOnLate)
                {
                    additionText.text = Random.Range(1, gm.earlyGamePlayerCardMax).ToString();
                }
                else
                {
                    if (Random.Range(1, 100) <= gm.middleChanceOnLate)
                    {
                        additionText.text = Random.Range(gm.earlyGamePlayerCardMax, gm.middleGamePlayerCardMax).ToString();
                    }
                    else
                    {
                        additionText.text = Random.Range(gm.middleGamePlayerCardMax, gm.lateGamePlayerCardMax).ToString();
                    }
                }
                
            }
        }

        //image.material.SetTexture("_SecondaryTex", Resources.Load<Texture2D>(SkinManager.instance.ramki[SkinManager.instance.ActiveFrame].Name)); //to shader
        frameImage.sprite = Resources.Load<Sprite>(SkinManager.instance.ramki[SkinManager.instance.ActiveFrame].Name);
      
        /*string pom2 = SkinManager.instance.ramki[SkinManager.instance.ActiveFrame].Name + ".png";
        pom2 = System.IO.Path.Combine(Application.streamingAssetsPath, pom2);

        byte[] pngBytes2 = System.IO.File.ReadAllBytes(pom2);
        //Creates texture and loads byte array data to create image
        Texture2D tex2 = new Texture2D(2, 2);
        tex2.LoadImage(pngBytes2);

        //Creates a new Sprite based on the Texture2D
        //Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

        //Assigns the UI sprite
        image.material.SetTexture("_SecondaryTex", tex2);*/
        if (rand <= 1)
        {
            additionText.color = new Color32(SkinManager.RED_COLOR, 0, 0, 255);

            if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == GameManager.KARTA_DYNAMICZNA)
            {
                SubStr = SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name;
                SubStr = SubStr.Substring(0, SubStr.Length - 1);
                gameObject.GetComponent<Image>().sprite = Resources.Load(SubStr + GameManager.RED_TEXT, typeof(Sprite)) as Sprite;
               /* //gameObject.GetComponent<Image>().sprite = Resources.Load(SubStr + GameManager.RED_TEXT, typeof(Sprite)) as Sprite;
                //Substr = Application.streamingAssetsPath + "/"  + GameManager.RED_TEX;
                string pom = SubStr + GameManager.RED_TEXT + ".png";
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
            else
                if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == GameManager.KARTA_STATYCZNA)
                {
                    SubStr = SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name;
                    gameObject.GetComponent<Image>().sprite = Resources.Load(SubStr + GameManager.RED_TEXT, typeof(Sprite)) as Sprite;
                    /*string pom = SubStr + GameManager.RED_TEXT + ".png";
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
        else
        {
            if (rand <= 2)
            {
                additionText.color = new Color32(0, SkinManager.GREEN_COLOR, 0, 255);
                if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == GameManager.KARTA_DYNAMICZNA)
                {
                    SubStr = SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name;
                    SubStr = SubStr.Substring(0, SubStr.Length - 1);
                    gameObject.GetComponent<Image>().sprite = Resources.Load(SubStr + GameManager.GREEN_TEXT, typeof(Sprite)) as Sprite;
                   /* string pom =  SubStr + GameManager.GREEN_TEXT + ".png";
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
                else
                    if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == GameManager.KARTA_STATYCZNA)
                    {
                        SubStr = SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name;
                        gameObject.GetComponent<Image>().sprite = Resources.Load(SubStr + GameManager.GREEN_TEXT, typeof(Sprite)) as Sprite;
                       /* string pom =  SubStr + GameManager.GREEN_TEXT + ".png";
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
            else
            {
                additionText.color = new Color32(0, 0, SkinManager.BLUE_COLOR, 255);
                if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == GameManager.KARTA_DYNAMICZNA)
                {
                    SubStr = SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name;
                    SubStr = SubStr.Substring(0, SubStr.Length - 1);
                    gameObject.GetComponent<Image>().sprite = Resources.Load(SubStr + GameManager.BLUE_TEXT, typeof(Sprite)) as Sprite;
                  /*  string pom = SubStr + GameManager.BLUE_TEXT + ".png";
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
                else
                    if (SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Type == GameManager.KARTA_STATYCZNA)
                    {
                        SubStr = SkinManager.instance.skorki[SkinManager.instance.ActiveSkin].Name;
                        gameObject.GetComponent<Image>().sprite = Resources.Load(SubStr + GameManager.BLUE_TEXT, typeof(Sprite)) as Sprite;
                       /* string pom =  SubStr + GameManager.BLUE_TEXT + ".png";
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
        }
        cardMissSFX.volume = SkinManager.instance.ActiveSFXValue / 100;
        cardCorrectSFX.volume = SkinManager.instance.ActiveSFXValue / 100;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
       // Debug.Log("playerCardClick");
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        transform.position = pointerEventData.position;
        gameObject.layer = 1;
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        originalPosition = transform.position;
        if (gameObject.transform.parent.name == "Hands")
        gameObject.transform.localScale = normalScale;
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        TextMeshProUGUI parentNameText;
        gameObject.layer = 0;
        Transform dropPanel;
        bool czyRawImageHit = false;

        //Debug.Log(pointerEventData.pointerCurrentRaycast.gameObject.name);

        if (pointerEventData.pointerCurrentRaycast.gameObject.name == "Trash")
        {
            gm.DiscardPlayerCard(this.gameObject);
            gm.CheckCardNumbers(true);
        }



        if ((pointerEventData.pointerCurrentRaycast.gameObject.GetComponent<RawImage>() != null))//||
        {
            dropPanel = pointerEventData.pointerCurrentRaycast.gameObject.GetComponent<RawImage>().transform.parent.gameObject.transform.Find("Drop Panel");
            if (gameObject.transform.parent.name != "Drop Panel")
                czyRawImageHit = true;
        }
        else
        {
             if ((pointerEventData.pointerCurrentRaycast.gameObject.GetComponent<Image>() != null))//||
                 {
                    
                     if (pointerEventData.pointerCurrentRaycast.gameObject.GetComponent<Image>().transform.parent.gameObject.transform.parent.gameObject.name == "Drop Panel")
                     {
                          dropPanel = gm.activeCard.gameObject.transform.Find("RawImage").GetComponent<RawImage>().transform.parent.gameObject.transform.Find("Drop Panel");
                          if (gameObject.transform.parent.name != "Drop Panel")
                              czyRawImageHit = true;
                     }
                     else
                     {
                         dropPanel = pointerEventData.pointerCurrentRaycast.gameObject.transform.Find("Drop Panel");
                     }
                 }
             else
            {
                dropPanel = pointerEventData.pointerCurrentRaycast.gameObject.transform.Find("Drop Panel");
            }
        }

        if (pointerEventData.pointerCurrentRaycast.gameObject.name == "TaskArea")
        {
            dropPanel = gm.activeCard.gameObject.transform.Find("RawImage").GetComponent<RawImage>().transform.parent.gameObject.transform.Find("Drop Panel");
            if (gameObject.transform.parent.name != "Drop Panel")
                czyRawImageHit = true;
           // Debug.Log();
        }

        if (pointerEventData.pointerCurrentRaycast.gameObject != null && 
            (pointerEventData.pointerCurrentRaycast.gameObject.GetComponent<TaskCard>() != null || czyRawImageHit)
            && (!gm.trashArea.activeSelf))
        {
            cardCorrectSFX.Play();
            gameObject.transform.SetParent(dropPanel);
            ActualParent = gm.activeCard;
            parentNameText = gameObject.transform.Find("Parent Name").GetComponent<TextMeshProUGUI>();
            parentNameText.text = ActualParent.name.ToString();
        }
        else
        {
            if (!gm.trashArea.activeSelf)
            {
                cardMissSFX.Play();
                GameObject child;
                for (int i = 0; i < gm.powerUpCards.Count; ++i)
                {                    
                    child = gm.powerUpCards[i];
                    if (child.transform.parent.transform.parent.name == this.name)
                    {
                        child.transform.Find("PowerUp Parent Name").GetComponent<TextMeshProUGUI>().text = "";
                        child.transform.SetParent(gm.powerUps.transform);
                        this.GetComponent<PlayerCard>().hasMultiply = false;
                    }
                }
            }
            if (gameObject.transform.parent.name != "Hands")
            {
                gameObject.transform.SetParent(hands.transform);
                ActualParent = null;
                parentNameText = gameObject.transform.Find("Parent Name").GetComponent<TextMeshProUGUI>();
                parentNameText.text = "";
            }
            else
            {
                transform.position = originalPosition;
                ActualParent = null;
                parentNameText = gameObject.transform.Find("Parent Name").GetComponent<TextMeshProUGUI>();
                parentNameText.text = "";
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        gm.userActivityTime = SkinManager.MAX_USER_DISACTIVITY;
        if (gameObject.transform.parent.name == "Hands")
        {
            gameObject.transform.localScale = biggerScale;
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (gameObject.transform.parent.name == "Hands")
        {
            gameObject.transform.localScale = normalScale;
        }
    }
}
