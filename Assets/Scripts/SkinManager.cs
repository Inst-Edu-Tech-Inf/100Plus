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
    public struct SkinsInfo
    {
        //Variable declaration
        //Note: I'm explicitly declaring them as public, but they are public by default. You can use private if you choose.
        public string Name; //also part of path
        public int Type; //rodzaj

        //Constructor (not necessary, but helpful)
        public SkinsInfo(string name, int type)
        {
            this.Type = type;
            this.Name = name;
        }
    }
    public SkinsInfo skorka = new SkinsInfo("Rings", GameManager.KARTA_DYNAMICZNA);
    public List<SkinsInfo> skorki = new List<SkinsInfo>();
    public string ramka = "RamkaGold";
    public List<string> ramki = new List<string>();
    public int ActiveSkin = 0;
    public int ActiveRamka = 0;

    public static SkinManager instance;
    public int CurrentScore;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        skorki.Clear();
        skorki.Add(skorka);
        skorka.Name = "Explodes";
        skorka.Type = GameManager.KARTA_DYNAMICZNA;
        skorki.Add(skorka);
        skorka.Name = "Ring";
        skorka.Type = GameManager.KARTA_STATYCZNA;
        skorki.Add(skorka);
        //skorka.Name = "Ramka";
        //skorka.Type = GameManager.KARTA_STATYCZNA;
        //skorki.Add(skorka);
        ramka = "RamkaGold";
        ramki.Add(ramka);
        ramka = "RamkaRed";
        ramki.Add(ramka);
        ramka = "RamkaBlue";
        ramki.Add(ramka);
        ramka = "RamkaGreen";
        ramki.Add(ramka);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActiveSkin(int Value)
    {
        ActiveSkin = Value;
        Debug.Log("Working");
    }

    void Awake()
    {
        //!!root game object only
        //DontDestroyOnLoad(transform.gameObject);
        ActiveSkin = PlayerPrefs.GetInt("ActiveSkin");
       // Debug.Log("Awake " + ActiveSkin);
    }

    void OnEnable()
    {
        ActiveSkin = PlayerPrefs.GetInt("ActiveSkin");
        //Debug.Log("Enable " + ActiveSkin);
    }
}
