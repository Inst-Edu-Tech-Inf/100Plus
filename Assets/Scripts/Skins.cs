using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;


public class Skins : MonoBehaviour
{
    public const int KARTA_STATYCZNA = 1;
    public const int KARTA_DYNAMICZNA = 2;
    public const int KARTA_RAMKA = 3;
    public const int BACKGROUND_STATIC = 4;
    public const int BACKGROUND_DYNAMIC = 5;
    public const int SOUND_BACKGROUND = 6;
    public const int SFX = 7;
    int ActiveColor = 1; 
    const float maxColorTime = 10;
    float remainingColorTime = maxColorTime;
    GameManager gm;

    [Header("Obrazki"), SerializeField]
    public GameObject rawImageA;
    [SerializeField]
    public GameObject imageA;

    public struct SkinsInfo {
    //Variable declaration
    //Note: I'm explicitly declaring them as public, but they are public by default. You can use private if you choose.
        public string Name; //also part of path
        public int Type; //rodzaj
   
        //Constructor (not necessary, but helpful)
        public SkinsInfo(string name, int type ) {
            this.Type = type;
            this.Name = name;
        }
    }

    List<SkinsInfo> skorki = new List<SkinsInfo>();
    SkinsInfo skorka = new SkinsInfo("Rings", KARTA_DYNAMICZNA);
    RenderTexture ActiveTexture;
    RawImage tex;
    int ActiveSkin = 0;
//    public Image[] images;


    void changeSkin(string Kolor)
    {
        //Debug.Log(skorki[ActiveSkin].Name);
        if (skorki[ActiveSkin].Type == KARTA_DYNAMICZNA)
        {
            imageA.SetActive(false);
            rawImageA.SetActive(true);
            ActiveTexture = new RenderTexture(300, 600, 16);
                rawImageA.GetComponent<RawImage>().texture = ActiveTexture;
                rawImageA.GetComponent<VideoPlayer>().targetTexture = ActiveTexture;
                rawImageA.GetComponent<VideoPlayer>().clip = Resources.Load(skorki[ActiveSkin].Name + Kolor) as VideoClip;

                int pm = (int)Mathf.Round(Random.Range(0.0f, (float)rawImageA.GetComponent<VideoPlayer>().length));
                rawImageA.GetComponent<VideoPlayer>().frame = pm;
                rawImageA.GetComponent<VideoPlayer>().Play();
        }

        if (skorki[ActiveSkin].Type == KARTA_STATYCZNA)
        {
            rawImageA.SetActive(false);
            imageA.SetActive(true);
            imageA.GetComponent<Image>().sprite = Resources.Load(skorki[ActiveSkin].Name + Kolor, typeof(Sprite)) as Sprite;
            if (gameObject.GetComponent<RawImage>() != null)
            {
                rawImageA.SetActive(false);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //remainingColorTime = maxColorTime;
        skorki.Clear();
        skorki.Add(skorka);
        skorka.Name = "Explode";
        skorka.Type = KARTA_DYNAMICZNA;
        skorki.Add(skorka);
        skorka.Name = "Ring";
        skorka.Type = KARTA_STATYCZNA;
        skorki.Add(skorka);
        skorka.Name = "Ramka";
        skorka.Type = KARTA_STATYCZNA;
        skorki.Add(skorka);
       
   /*     images = GetComponents<Image>();

        foreach (Image img in images)
        {
            // Do stuff
            //Debug.Log(img);
            img.enabled = true;
            image = img;
        }*/

 

        changeSkin("Red");
        
    }

    // Update is called once per frame
    void Update()
    {
        //change colors
       /* if (remainingColorTime > 0)
        {
           remainingColorTime  -= Time.deltaTime;
           if (ActiveColor < gm.COLOR_NUMBER){
               ActiveColor++;
           }
           else{
               ActiveColor=1;
           }
        //changeSkin(gm.COLORS_ARRAY[ActiveColor]);
        }
        else{
            remainingColorTime = maxColorTime;
        }*/
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Next()
    {
        if (ActiveSkin < skorki.Count - 1)
        {
            ActiveSkin++;
            changeSkin("Red");
        }
    }

    public void Prev()
    {
        if (ActiveSkin > 0)
        {
            ActiveSkin--;
            changeSkin("Red");
        }
    }
}
