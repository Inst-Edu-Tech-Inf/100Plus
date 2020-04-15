using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    /*private static SkinManager _instance;

    public static SkinManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }*/

    public const int CARD_IMAGE_WIDTH = 150;
    public const int CARD_IMAGE_HEIGHT = 300;
    public const string RED_TEXT = "Red";
    public const string GREEN_TEXT = "Green";
    public const string BLUE_TEXT = "Blue";
    public const int COLOR_NUMBER = 3;
    public string[] COLORS_ARRAY = new string[] { RED_TEXT, GREEN_TEXT, BLUE_TEXT };
    public const float MAX_USER_DISACTIVITY = 10.0f; //sec
    public const int ANIMATED_CARD_PRICE = 200;
    public const int STATIC_CARD_PRICE = 90;
    public const int FRAME_PRICE = 15;
    public const int BACKGROUND_PRICE = 45;
    public const int SOUND_PRICE = 175;
    public struct SkinsInfo
    {
        //Variable declaration
        //Note: I'm explicitly declaring them as public, but they are public by default. You can use private if you choose.
        public string Name; //also part of path
        public int Type; //rodzaj
        public int Price;
        public string Title;

        //Constructor (not necessary, but helpful)
        public SkinsInfo(string name, int type, string title)
        {
            this.Type = type;
            this.Name = name;
            this.Price = 0;
            this.Title = title;
        }
        public SkinsInfo(string name, int type, int price, string title)
        {
            this.Type = type;
            this.Name = name;
            this.Price = price;
            this.Title = title;
        }
    }
    public SkinsInfo skorka = new SkinsInfo("Rings", GameManager.KARTA_DYNAMICZNA, "Lord of the Rings");
    public List<SkinsInfo> skorki = new List<SkinsInfo>();
    public string ramka = "RamkaGold";
    public string tlo = "NGC_5477_Hubble";
    public string muzyka = "Island Puzzle Acoustic";
    public List<SkinsInfo> ramki = new List<SkinsInfo>();
    public List<SkinsInfo> tla = new List<SkinsInfo>();
    public List<SkinsInfo> muzyki = new List<SkinsInfo>();
    public int ActiveSkin = 0;
    public int ActiveFrame = 0;
    public int ActiveBackground = 0;
    public int ActiveSound = 0;
    public float ActiveSoundValue = 100;
    public float ActiveSFXValue = 100;
    public bool isVictoryPointFirst = false;
    public bool isVictoryTimePass = true;
    public int VictoryPointFirstValue = 50;
    public int VictoryTimePassValue = 300; //sec 300==5min
    public int ActiveVictoryConditions = 0;
    public int CurrentCash = 0;

    public static SkinManager instance;
    public int CurrentScore;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        skorki.Clear();
        ramki.Clear();
        tla.Clear();
        muzyki.Clear();

        skorki.Add(new SkinsInfo("Explodes", GameManager.KARTA_DYNAMICZNA, "I see fire"));
        skorki.Add(new SkinsInfo("Rings",GameManager.KARTA_DYNAMICZNA,ANIMATED_CARD_PRICE, "Lord of the Rings"));
        skorki.Add(new SkinsInfo("Fireworks", GameManager.KARTA_DYNAMICZNA, ANIMATED_CARD_PRICE, "Ziiiiip..."));
        skorki.Add(new SkinsInfo("Explode", GameManager.KARTA_STATYCZNA, "I saw fire"));
        skorki.Add(new SkinsInfo("Ring", GameManager.KARTA_STATYCZNA, STATIC_CARD_PRICE, "lord of the rings"));
        skorki.Add(new SkinsInfo("Firework", GameManager.KARTA_STATYCZNA, STATIC_CARD_PRICE, "ziiip..."));
        skorki.Add(new SkinsInfo("DiamondRing", GameManager.KARTA_STATYCZNA, STATIC_CARD_PRICE, "eye ring"));
        //
        ramki.Add(new SkinsInfo("RamkaGold", GameManager.KARTA_RAMKA, "The golden rectangle"));
        ramki.Add(new SkinsInfo("CatFrame", GameManager.KARTA_RAMKA, FRAME_PRICE, "The white kitty"));
        ramki.Add(new SkinsInfo("CatFramePink", GameManager.KARTA_RAMKA, FRAME_PRICE, "Hello kitty"));
        ramki.Add(new SkinsInfo("CatFrameYellow", GameManager.KARTA_RAMKA, FRAME_PRICE, "The yellow kitty"));//yellow submarine
        //
        tla.Add(new SkinsInfo("SplashScreen", GameManager.BACKGROUND_STATIC, "Lets Summ On"));
        tla.Add(new SkinsInfo("NGC_5477_Hubble", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, "Starry night"));
        tla.Add(new SkinsInfo("STSCI-H-p2003c-m", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, "Deep space"));
        //
        muzyki.Add(new SkinsInfo("Island Puzzle Acoustic", GameManager.SOUND_BACKGROUND, "Island Puzzle Acoustic"));
        muzyki.Add(new SkinsInfo("Crazy Puzzle Electronic", GameManager.SOUND_BACKGROUND, SOUND_PRICE, "Crazy Puzzle Electronic"));
        muzyki.Add(new SkinsInfo("Epic Puzzle Orchestral", GameManager.SOUND_BACKGROUND, SOUND_PRICE, "Epic Puzzle Orchestral"));
       // ResetAllSkins();
        LoadUserData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetAllSkins()
    {
        for (int i = 1; i < SkinManager.instance.skorki.Count; ++i)
        {
            PlayerPrefs.SetInt(SkinManager.instance.skorki[i].Name, 0);
        }
        for (int i = 1; i < SkinManager.instance.ramki.Count; ++i)
        {
            PlayerPrefs.SetInt(SkinManager.instance.ramki[i].Name, 0);
        }
        for (int i = 1; i < SkinManager.instance.tla.Count; ++i)
        {
            PlayerPrefs.SetInt(SkinManager.instance.tla[i].Name, 0);
        }
        for (int i = 1; i < SkinManager.instance.muzyki.Count; ++i)
        {
            PlayerPrefs.SetInt(SkinManager.instance.muzyki[i].Name, 0);
        }
        SetCurrentCash(0);
        PlayerPrefs.SetInt("CurrentCash", 0);
    }

    void LoadUserData()
    {
        ActiveSkin = PlayerPrefs.GetInt("ActiveSkin");
        ActiveFrame = PlayerPrefs.GetInt("ActiveFrame");
        ActiveBackground = PlayerPrefs.GetInt("ActiveBackground");
        ActiveSound = PlayerPrefs.GetInt("ActiveSound");
        ActiveSFXValue = PlayerPrefs.GetFloat("ActiveSFXValue");
        ActiveSoundValue = PlayerPrefs.GetFloat("ActiveSoundValue");
        isVictoryPointFirst = (PlayerPrefs.GetInt("IsVictoryPointFirst") != 0);
        isVictoryTimePass = (PlayerPrefs.GetInt("IsVictoryTimePass") != 0);
        VictoryPointFirstValue = PlayerPrefs.GetInt("VictoryPointFirst");
        VictoryTimePassValue = PlayerPrefs.GetInt("VictoryTimePass");
        ActiveVictoryConditions = PlayerPrefs.GetInt("ActiveVictoryConditions");
        CurrentCash = PlayerPrefs.GetInt("CurrentCash");
    }

    public void SetActiveSkin(int Value)
    {
        ActiveSkin = Value;
    }

    public void SetActiveFrame(int Value)
    {
        ActiveFrame = Value;
    }

    public void SetActiveBackground(int Value)
    {
        ActiveBackground = Value;
    }

    public void SetActiveSound(int Value)
    {
        ActiveSound = Value;
    }

    public void SetActiveSoundValue(float Value)
    {
        ActiveSoundValue = Value;
    }

    public void SetActiveSFXValue(float Value)
    {
        ActiveSFXValue = Value;
    }

    public void SetVictoryPointFirstValue(int Value)
    {
        VictoryPointFirstValue = Value;
    }

    public void SetVictoryTimePassValue(int Value)
    {
        VictoryTimePassValue = Value;
    }

    public void SetIsVictoryPointFirst(bool Value)
    {
        isVictoryPointFirst = Value;
    }

    public void SetIsVictoryTimePass(bool Value)
    {
        isVictoryTimePass = Value;
    }

    public void SetActiveVictoryConditions(int Value)
    {
        ActiveVictoryConditions = Value;
    }

    public void SetCurrentCash(int Value)
    {
        CurrentCash = Value;
    }
    


    void Awake()
    {
        //!!root game object only
        //DontDestroyOnLoad(transform.gameObject);
        LoadUserData();
    }

    void OnEnable()
    {
        LoadUserData();
    }
}
