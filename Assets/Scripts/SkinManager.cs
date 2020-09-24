//#define HTML5

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;

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

    public const int VICTORY_CONDITIONS_20 = 20;
    public const int VICTORY_CONDITIONS_100 = 100;
    public const int CARD_IMAGE_WIDTH = 150;
    public const int CARD_IMAGE_HEIGHT = 300;
    public const string RED_TEXT = "Red";
    public const string GREEN_TEXT = "Green";
    public const string BLUE_TEXT = "Blue";
    public const int GREEN_COLOR = 120;
    public const int RED_COLOR = 255;
    public const int BLUE_COLOR = 255;
    public const int COLOR_NUMBER = 3;
    public string[] COLORS_ARRAY = new string[] { RED_TEXT, GREEN_TEXT, BLUE_TEXT };
    public static readonly int[] PLAYER_TURN = new int[] { 0, 30, 45, 60, 120 };
    public const float MAX_USER_DISACTIVITY = 10.0f; //sec
    public const float AI_ACTIVITY_TIME = 1.35f;//sec
    public const float PVP_ACTIVITY_TIME = 0.0f;//sec
    public const float REROLL_COLOR_RULE = 0.625f;
    public const int REROLL_COST_EARLY = 1;
    public const int REROLL_COST_MIDDLE = 2;
    public const int REROLL_COST_LATE = 4;
    public const int ANIMATED_CARD_PRICE = 200;
    public const int STATIC_CARD_PRICE = 90;
    public const int FRAME_PRICE = 15;
    public const int BACKGROUND_PRICE = 45;
    public const int SOUND_PRICE = 175;
    public const int INCREMENTAL_ACHIEVEMENT = 1;
    public const int NORMAL_ACHIEVEMENT = 0;
    public const int HIDDEN_ACHIEVEMENT = 2;
    public const int FASTER_THAN_LIGHT_MIDDLE = 150;
    public const int FASTER_THAN_LIGHT_LATE = 600;
    public const int ACHIEVEMENT_PURE_1ST = 1000;
    public const int ACHIEVEMENT_PURE_2ND = 2000;
    public const int ACHIEVEMENT_PURE_3RD = 4000;
    public const int ACHIEVEMENT_NOT_PURE_1ST = 1000;
    public const int ACHIEVEMENT_NOT_PURE_2ND = 5000;
    public const int ACHIEVEMENT_NOT_PURE_3RD = 10000;
    public const int MIDDLEPASS = 0;
    public const int LATEPASS = 1;
    public const int FASTERTHANLIGHTMIDDLE = 2;
    public const int FASTERTHANLIGHTLATE = 3;
    public const int MULTIPLYTWICE = 4;
    public const int MULTIPLYTHREE = 5;
    public const int MULTIPLYFOUR = 6;
    public const int WINSOLO = 7;
    public const int WINSI = 8;
    public const int WINPVP = 9;
    public const int PURE1KSOLO = 10;
    public const int NOTPURE1KSOLO = 11;
    public const int PURE1KSI = 12;
    public const int NOTPURE1KSI = 13;
    public const int PURE1KPVP = 14;
    public const int NOTPURE1KPVP = 15;
    public const int PURE2KSOLO = 16;
    public const int NOTPURE5KSOLO = 17;
    public const int PURE2KSI = 18;
    public const int NOTPURE5KSI = 19;
    public const int PURE2KPVP = 20;
    public const int NOTPURE5KPVP = 21;
    public const int PURE4KSOLO = 22;
    public const int NOTPURE10KSOLO = 23;
    public const int PURE4KSI = 24;
    public const int NOTPURE10KSI = 25;
    public const int PURE4KPVP = 26;
    public const int NOTPURE10KPVP = 27;
    public const int UNLOCKALLSKINS = 28;
    public const int PUREGAME = 29;
    public const int LUCKY = 30;
    public const int LONGWAY = 31;
    public const int AI_EASY = 0;
    public const int AI_IMPOSSIBLE = 1;

    public const int PROGRAMISCI = 0;
    public const int GRAFICY = 1;
    public const int TESTERZY = 2;
    public const int KONCEPCJA_GRY = 3;
    public const int WARUNKI_ZWYCIESTWA = 4;
    public const int KONIEC_TURY_GRACZA = 5;
    public const int DZWIEK = 6;
    public const int TRYB_GRY = 7;
    public const int GOTOWY = 8;
    public static string[] SKORKI_PL = new string[] { "Widzê ogieñ", "W³adca Pierœcieni", "Ziuuuu...", "Widzia³em ogieñ", "W³adca pierœcieni", "ziuuuu...", "Pierœcionek",
        "Aaaaaaa! Troll!","Jednoro¿ec","Zagrajmy","Wszyscy razem", "Zimorodek"};
    public static string[] RAMKI_PL = new string[] { "Z³oty prostok¹t", "Bia³y kociak", "Hello kitty","Jak w albumie","Jak na dawnej fotografii", "Krok po kroku",
        "Razem","Pokój","Pianino","Fale","Z³ote fale","Podniebne fale","Trawiaste fale","Ró¿owe fale","Ogniste fale"};
    public static string[] TLA_PL = new string[] { "W sumie...", "GwieŸdzista noc", "G³êboka przestrzeñ", "Jaœniejszy Summ On",  "Buuum","Galaktyka spiralna","Têcza","MroŸnie",
        "Gotów, weŸ, rysuj","Palma","Storczyk","Na Ksiê¿yc!","Ziemia","Jezioro w lesie","Tak ró¿owo...","Kwiat","Jesieñ","Kasztany","Ró¿a", "¯ó³w",
        "Saturn","System s³oneczny","Dziki i groŸny","Fajerwerki","Gdzie jest skarb?"};
    public static string[] MUZYKI_PL = new string[] { "Jak s³odko", "Szalona", "Jak mi³o" };
    public static string[] OSIAGNIECIA_PL = new string[] { "Trudne pocz¹tki", "Zmiana kart", "Szybki","Szybszy","Podwójnie",
        "Potrójnie","Poczwórnie","Zwyciêzca","Bystry","Bystrzejszy","Idealny Uczeñ",
        "Niechlujny Uczeñ","Idealny Pracownik",
        "Niechlujny Pracownik", "Idealny Akolita",
        "Niechlujny Akolita","Idealny Czeladnik",
        "Niechlujny Czeladnk","Idealny Rzemieœlnik",
        "Niechlujny Rzemieœlnik","Idealny Magik",
        "Niechlujny Magik","Idealny Master",
        "Niechlujny Master", "Idealny Artysta",
        "Niechlujny Artysta","Idealny Czarodziej",
        "Niechlujny Czarodziej","Bogactwo","Doskonale","Szczêœciarz", 
        "Daleka droga"}; 
    public static string[] OSIAGNIECIA_OPIS_PL = new string[] { "Ukoñczony samouczek", "Odblokowane zaawansowane karty mno¿ników", "Szybszy ni¿ b³yskawica","Szybszy ni¿ œwiat³o","Pomnó¿ dwukrotnie",
        "Pomnó¿ trzykrotnie","Pomnó¿ czterokrotnie","Wygraj grê solo","Wygraj grê z komputerem (poziom hard)", "Wygraj grê z innym graczem","Uzbieraj sam " + ACHIEVEMENT_PURE_1ST.ToString() + " punktów - tylko idealne wyniki",
        "Uzbieraj sam " + ACHIEVEMENT_NOT_PURE_1ST.ToString() + " punktów - tylko za du¿e wyniki","Uzbieraj przeciw SI " + ACHIEVEMENT_PURE_1ST.ToString() + " punktów - tylko idealne wyniki",
        "Uzbieraj przeciw SI " + ACHIEVEMENT_NOT_PURE_1ST.ToString() + " punktów - tylko za du¿e wyniki","Uzbieraj przeciw innym " + ACHIEVEMENT_PURE_1ST.ToString() + " punktów - tylko idealne wyniki",
        "Uzbieraj przeciw innym " + ACHIEVEMENT_NOT_PURE_1ST.ToString() + " punktów - tylko za du¿e wyniki","Uzbieraj sam " + ACHIEVEMENT_PURE_2ND.ToString() + " punktów - tylko idealne wyniki",
        "Uzbieraj sam " + ACHIEVEMENT_NOT_PURE_2ND.ToString() + " punktów - tylko za du¿e wyniki","Uzbieraj przeciw SI " + ACHIEVEMENT_PURE_2ND.ToString() + " punktów - tylko idealne wyniki",
        "Uzbieraj przeciw SI " + ACHIEVEMENT_NOT_PURE_2ND.ToString() + " punktów - tylko za du¿e wyniki","Uzbieraj przeciw innym " + ACHIEVEMENT_PURE_2ND.ToString() + " punktów - tylko idealne wyniki",
        "Uzbieraj przeciw innyms " + ACHIEVEMENT_NOT_PURE_2ND.ToString() + " punktów - tylko za du¿e wyniki","Uzbieraj sam " + ACHIEVEMENT_PURE_3RD.ToString() + " punktów - tylko idealne wyniki",
        "Uzbieraj sam " + ACHIEVEMENT_NOT_PURE_3RD.ToString() + " punktów - tylko za du¿e wyniki","Uzbieraj przeciw SI " + ACHIEVEMENT_PURE_3RD.ToString() + " punktów - tylko idealne wyniki",
        "Uzbieraj przeciw SI " + ACHIEVEMENT_NOT_PURE_3RD.ToString() + " punktów - tylko za du¿e wyniki","Uzbieraj przeciw innym " + ACHIEVEMENT_PURE_3RD.ToString() + " punktów - tylko idealne wyniki",
        "Uzbieraj przeciw innym " + ACHIEVEMENT_NOT_PURE_3RD.ToString() + " punktów - tylko za du¿e wyniki","Jestem bogaty...","Po prostu perfekcyjna gra", "Ryzyko czasem pop³aca",
        "U¿yj 5 kart na raz, ¿eby zebraæ idealny wynik"};
    public static string[] MENU_PL = new string[] { "Programisci", "Graficy", "Testerzy", "Koncepcja gry", "Warunki zwyciêstwa", "Koniec tury gracza", "DŸwiêk", "Tryb gry", "Gotowy" };
   // public sstring[] MENU_PL = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i" };
    public static string[] SAMOUCZEK_PL = new string[] { RED_TEXT, GREEN_TEXT, BLUE_TEXT };

    public static string[] SKORKI_EN = new string[] { "I see fire", "Lord of the Rings", "Ziiiiip...", "I saw fire", "lord of the rings", "ziiip...", "Eye ring",
        "Aaaaaaa! A troll!","The unicorn","Lets play","All together", "The kingfisher"};
    public static string[] RAMKI_EN = new string[] { "The golden rectangle", "The white kitty", "Hello kitty","Like photo album","Like colored photo album", "Step by step",
        "Together","Peace","The piano","The waves","The gold waves","The sky waves","The grass waves","The pink waves","The fire waves"};
    public static string[] TLA_EN = new string[] { "Lets Summ On", "Starry night", "Deep space", "Summ On Lighter",  "Baaam","Spiral galaxy","Rainbow","Cold look",
        "Ready, set, colour","The palm","The orchidea","Go to the Moon","Earth","Lake in the forest","So pinky...","The flower","Autumn","Chestnuts","Rose", "The turtle",
        "Saturn","Solar system","The wild and dangerous","Fireworks","Where is the treasure?"};
    public static string[] MUZYKI_EN = new string[] { "So sweet", "I'm crazy", "How nice" };
    public static string[] OSIAGNIECIA_EN = new string[] { "Hard begining", "Cards changed", "Fast","Faster","Twice",
        "Triple","Quadruple","Winner","Smart","Smarter","Pure Pupil",
        "Messy Pupil","Pure Worker",
        "Messy Worker", "Pure Acolyte",
        "Messy Acolyte","Pure Peon",
        "Messy Peon","Pure Craftsman",
        "Messy Craftsman","Pure Magican",
        "Messy Magican","Pure Master",
        "Messy Master", "Pure Artist",
        "Messy Artist","Pure Mage",
        "Messy Mage","Richness","Excelent","Lucky", 
        "Long way"};   
    public static string[] OSIAGNIECIA_OPIS_EN = new string[] { "Pass the tutorial", "The first mixed multiply available", "Faster than bolt","Faster than light","Multiply twice",
        "Multiply triple","Multiply quad","Win solo game","Win game against computer (hard level)", "Win game against human","Gain solo " + ACHIEVEMENT_PURE_1ST.ToString() + " score - only the exact task value",
        "Gain solo " + ACHIEVEMENT_NOT_PURE_1ST.ToString() + " score - only bigger than task value","Gain AI games " + ACHIEVEMENT_PURE_1ST.ToString() + " score - only the exact task value",
        "Gain AI games " + ACHIEVEMENT_NOT_PURE_1ST.ToString() + " score - only bigger than task value","Gain PVP games " + ACHIEVEMENT_PURE_1ST.ToString() + " score - only the exact task value",
        "Gain PVP games " + ACHIEVEMENT_NOT_PURE_1ST.ToString() + " score - only bigger than task value","Gain solo " + ACHIEVEMENT_PURE_2ND.ToString() + " score - only the exact task value",
        "Gain solo " + ACHIEVEMENT_NOT_PURE_2ND.ToString() + " score - only bigger than task value","Gain AI games " + ACHIEVEMENT_PURE_2ND.ToString() + " score - only the exact task value",
        "Gain AI games " + ACHIEVEMENT_NOT_PURE_2ND.ToString() + " score - only bigger than task value","Gain PVP games " + ACHIEVEMENT_PURE_2ND.ToString() + " score - only the exact task value",
        "Gain PVP games " + ACHIEVEMENT_NOT_PURE_2ND.ToString() + " score - only bigger than task value","Gain solo " + ACHIEVEMENT_PURE_3RD.ToString() + " score - only the exact task value",
        "Gain solo " + ACHIEVEMENT_NOT_PURE_3RD.ToString() + " score - only bigger than task value","Gain AI games " + ACHIEVEMENT_PURE_3RD.ToString() + " score - only the exact task value",
        "Gain AI games " + ACHIEVEMENT_NOT_PURE_3RD.ToString() + " score - only bigger than task value","Gain PVP games " + ACHIEVEMENT_PURE_3RD.ToString() + " score - only the exact task value",
        "Gain PVP games " + ACHIEVEMENT_NOT_PURE_3RD.ToString() + " score - only bigger than task value","I'm rich...","Just excelent game", "The risk is sometimes better",
        "Use 5 card at row to collect pure result"};  
    public static string[] MENU_EN = new string[] { "Programmers", "Graphics", "Testers","Game concept", "Victory settings", "Player end turn", "Sound settings", "Game mode", "Ready" };
    public static string[] SAMOUCZEK_EN = new string[] { RED_TEXT, GREEN_TEXT, BLUE_TEXT };
             




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
    public struct AchievementInfo
    {
        //Variable declaration
        //Note: I'm explicitly declaring them as public, but they are public by default. You can use private if you choose.
        public string ID; //also part of path
        public int Type; //rodzaj
        public float Progress;
        public int Reward;
        public string Description;
        public string Name;

        //Constructor (not necessary, but helpful)
        public AchievementInfo(string id, int type, float progress, int reward, string name, string description)
        {
            this.Type = type;
            this.ID = id;
            this.Progress = progress;
            this.Reward = reward;
            this.Name = name;
            this.Description = description;
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
    public List<AchievementInfo> osiagniecia = new List<AchievementInfo>();
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
    public int ActivePlayerTurnConditions = 0;
    public int ActivePlayerEndTime = 0;//sec
    public int CurrentCash = 0;
    public float BestResult = 0.0f;
    public int AIDifficulty = AI_EASY;

    public int ActivePlayerMode = 0;//solo, SI, PVP
    public float PureSolo = 0;
    public float NotPureSolo = 0;
    public float PureSI = 0;
    public float NotPureSI = 0;
    public float PurePVP = 0;
    public float NotPurePVP = 0;

    public bool isPureSolo1 = false;
    public bool isNotPureSolo1 = false;
    public bool isPureSI1 = false;
    public bool isNotPureSI1 = false;
    public bool isPurePVP1 = false;
    public bool isNotPurePVP1 = false;
    public bool isPureSolo2 = false;
    public bool isNotPureSolo2 = false;
    public bool isPureSI2 = false;
    public bool isNotPureSI2 = false;
    public bool isPurePVP2 = false;
    public bool isNotPurePVP2 = false;
    public bool isPureSolo3 = false;
    public bool isNotPureSolo3 = false;
    public bool isPureSI3 = false;
    public bool isNotPureSI3 = false;
    public bool isPurePVP3 = false;
    public bool isNotPurePVP3 = false;
    public bool AllSkins = false;
    public bool MiddlePass = false;
    public bool LatePass = false;
    public bool FasterThanLightMiddle = false;
    public bool FasterThanLightLate = false;
    public bool MultiplyTwice = false;
    public bool MultiplyThree = false;
    public bool MultiplyFour = false;
    public bool WinSolo = false;
    public bool WinSI = false;
    public bool WinPVP = false;
    public bool UnlockAllSkins = false;
    public bool PureGame = false;
    public bool Lucky = false;
    public bool LongWay = false;

    public static SkinManager instance;
    public int CurrentScore;
    public string DebugToShow;
    public string AIPToShow;
    public string UserID;
    

    // Start is called before the first frame update
    void Start()
    {
        string[] skorkiLang, ramkiLang, tlaLang, muzykiLang, osiagnieciaLang, osiagnieciaOpisLang;
        SystemLanguage iLang = Application.systemLanguage;
        instance = this;
        skorki.Clear();
        ramki.Clear();
        tla.Clear();
        muzyki.Clear();
        osiagniecia.Clear();
        SetUserID();

        switch (iLang)
        {
            case SystemLanguage.English:
                skorkiLang = SKORKI_EN;
                ramkiLang = RAMKI_EN;
                tlaLang = TLA_EN;
                muzykiLang = MUZYKI_EN;
                osiagnieciaLang = OSIAGNIECIA_EN;
                osiagnieciaOpisLang = OSIAGNIECIA_OPIS_EN;
                break;
            case SystemLanguage.Polish:
                skorkiLang = SKORKI_PL;
                ramkiLang = RAMKI_PL;
                tlaLang = TLA_PL;
                muzykiLang = MUZYKI_PL;
                osiagnieciaLang = OSIAGNIECIA_PL;
                osiagnieciaOpisLang = OSIAGNIECIA_OPIS_PL;
                break;
            default:
                skorkiLang = SKORKI_EN;
                ramkiLang = RAMKI_EN;
                tlaLang = TLA_EN;
                muzykiLang = MUZYKI_EN;
                osiagnieciaLang = OSIAGNIECIA_EN;
                osiagnieciaOpisLang = OSIAGNIECIA_OPIS_EN;
                break;
        }
        //.Log(Application.systemLanguage);

        skorki.Add(new SkinsInfo("Explodes", GameManager.KARTA_DYNAMICZNA, skorkiLang[0]));
        skorki.Add(new SkinsInfo("Rings", GameManager.KARTA_DYNAMICZNA, ANIMATED_CARD_PRICE, skorkiLang[1]));
        skorki.Add(new SkinsInfo("Fireworks", GameManager.KARTA_DYNAMICZNA, ANIMATED_CARD_PRICE, skorkiLang[2]));
        skorki.Add(new SkinsInfo("Explode", GameManager.KARTA_STATYCZNA, skorkiLang[3]));
        skorki.Add(new SkinsInfo("Ring", GameManager.KARTA_STATYCZNA, STATIC_CARD_PRICE, skorkiLang[4]));
        skorki.Add(new SkinsInfo("Firework", GameManager.KARTA_STATYCZNA, STATIC_CARD_PRICE, skorkiLang[5]));
        skorki.Add(new SkinsInfo("DiamondRing", GameManager.KARTA_STATYCZNA, STATIC_CARD_PRICE, skorkiLang[6]));
        skorki.Add(new SkinsInfo("Troll", GameManager.KARTA_STATYCZNA, STATIC_CARD_PRICE, skorkiLang[7]));
        skorki.Add(new SkinsInfo("Unicorn", GameManager.KARTA_STATYCZNA, STATIC_CARD_PRICE, skorkiLang[8]));
        skorki.Add(new SkinsInfo("Ball", GameManager.KARTA_STATYCZNA, STATIC_CARD_PRICE, skorkiLang[9]));
        skorki.Add(new SkinsInfo("Peace", GameManager.KARTA_STATYCZNA, STATIC_CARD_PRICE, skorkiLang[10]));
        skorki.Add(new SkinsInfo("Zimorodek", GameManager.KARTA_STATYCZNA, STATIC_CARD_PRICE, skorkiLang[11]));
        //skorki.Add(new SkinsInfo("gifDoGry", GameManager.KARTA_DYNAMICZNA, "Zuza"));
        //skorki.Add(new SkinsInfo("summOnDots", GameManager.KARTA_DYNAMICZNA, "Wiktor"));
        //
        ramki.Add(new SkinsInfo("RamkaGold", GameManager.KARTA_RAMKA, ramkiLang[0]));
        ramki.Add(new SkinsInfo("CatFrame", GameManager.KARTA_RAMKA, FRAME_PRICE, ramkiLang[1]));
        ramki.Add(new SkinsInfo("CatFramePink", GameManager.KARTA_RAMKA, FRAME_PRICE, ramkiLang[2]));
        //ramki.Add(new SkinsInfo("CatFrameYellow", GameManager.KARTA_RAMKA, FRAME_PRICE, "The yellow kitty"));//yellow submarine
        ramki.Add(new SkinsInfo("Flower", GameManager.KARTA_RAMKA, FRAME_PRICE, ramkiLang[3]));
        ramki.Add(new SkinsInfo("FlowerLightBlue", GameManager.KARTA_RAMKA, FRAME_PRICE, ramkiLang[4]));
        ramki.Add(new SkinsInfo("PawsWhite", GameManager.KARTA_RAMKA, FRAME_PRICE, ramkiLang[5]));
        ramki.Add(new SkinsInfo("PeopleWhite", GameManager.KARTA_RAMKA, FRAME_PRICE, ramkiLang[6]));
        ramki.Add(new SkinsInfo("CelticPeaceWhite", GameManager.KARTA_RAMKA, FRAME_PRICE, ramkiLang[7]));
        ramki.Add(new SkinsInfo("Piano", GameManager.KARTA_RAMKA, FRAME_PRICE, ramkiLang[8]));
        ramki.Add(new SkinsInfo("WaveWhite", GameManager.KARTA_RAMKA, FRAME_PRICE, ramkiLang[9]));
        ramki.Add(new SkinsInfo("WaveYellow", GameManager.KARTA_RAMKA, FRAME_PRICE, ramkiLang[10]));
        ramki.Add(new SkinsInfo("WaveLightBlue", GameManager.KARTA_RAMKA, FRAME_PRICE, ramkiLang[11]));
        ramki.Add(new SkinsInfo("WaveGreen", GameManager.KARTA_RAMKA, FRAME_PRICE, ramkiLang[12]));
        ramki.Add(new SkinsInfo("WavePink", GameManager.KARTA_RAMKA, FRAME_PRICE, ramkiLang[13]));
        ramki.Add(new SkinsInfo("WaveRed", GameManager.KARTA_RAMKA, FRAME_PRICE, ramkiLang[14]));
        //
        tla.Add(new SkinsInfo("SplashScreen", GameManager.BACKGROUND_STATIC, tlaLang[0]));
        tla.Add(new SkinsInfo("NGC_5477_Hubble", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE,tlaLang[1]));
        tla.Add(new SkinsInfo("STSCI-H-p2003c-m", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[2]));
        tla.Add(new SkinsInfo("SummOnLight", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[3]));
        tla.Add(new SkinsInfo("STSCI-H-p1918a-f", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[4]));
        //tla.Add(new SkinsInfo("BubbleNebula", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, "Blue space"));
        tla.Add(new SkinsInfo("STSCI-H-p2001a-m", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[5]));
        tla.Add(new SkinsInfo("Rainbow", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[6]));
        tla.Add(new SkinsInfo("PolarBear", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[7]));
        tla.Add(new SkinsInfo("Pencils", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[8]));
        tla.Add(new SkinsInfo("Palm", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[9]));
        tla.Add(new SkinsInfo("Orchidea", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[10]));
        tla.Add(new SkinsInfo("Moon", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[11]));
        tla.Add(new SkinsInfo("Earth", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[12]));
        tla.Add(new SkinsInfo("Lake", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[13]));
        tla.Add(new SkinsInfo("Flowers", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[14]));
        tla.Add(new SkinsInfo("Flower", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[15]));
        tla.Add(new SkinsInfo("Autumn", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[16]));
        tla.Add(new SkinsInfo("Kasztany", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[17]));
        tla.Add(new SkinsInfo("Rose", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[18]));
        tla.Add(new SkinsInfo("Turtle", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[19]));
        tla.Add(new SkinsInfo("Saturn", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[20]));
        tla.Add(new SkinsInfo("SolarSystem", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[21]));
        tla.Add(new SkinsInfo("Lions", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[22]));
        tla.Add(new SkinsInfo("Fireworks", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[23]));
        tla.Add(new SkinsInfo("OldMap", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[24]));
            
                
                    
        //
        muzyki.Add(new SkinsInfo("Island Puzzle Acoustic", GameManager.SOUND_BACKGROUND, muzykiLang[0]));
        muzyki.Add(new SkinsInfo("Crazy Puzzle Electronic", GameManager.SOUND_BACKGROUND, SOUND_PRICE, muzykiLang[1]));
        muzyki.Add(new SkinsInfo("Epic Puzzle Orchestral", GameManager.SOUND_BACKGROUND, SOUND_PRICE, muzykiLang[2]));
       // ResetAllSkins();


        osiagniecia.Add(new AchievementInfo("MiddlePass", NORMAL_ACHIEVEMENT, 0, 5, osiagnieciaLang[0], osiagnieciaOpisLang[0]));//ID, type, progress, reward, descripton 
        osiagniecia.Add(new AchievementInfo("LatePass", NORMAL_ACHIEVEMENT, 0, 5, osiagnieciaLang[1], osiagnieciaOpisLang[1]));
        osiagniecia.Add(new AchievementInfo("FasterThanLightMiddle", NORMAL_ACHIEVEMENT, 0, 5, osiagnieciaLang[2], osiagnieciaOpisLang[2]));
        osiagniecia.Add(new AchievementInfo("FasterThanLightLate", NORMAL_ACHIEVEMENT, 0, 15, osiagnieciaLang[3], osiagnieciaOpisLang[3]));
        osiagniecia.Add(new AchievementInfo("MultiplyTwice", NORMAL_ACHIEVEMENT, 0, 5, osiagnieciaLang[4], osiagnieciaOpisLang[4]));
        osiagniecia.Add(new AchievementInfo("MultiplyThree", NORMAL_ACHIEVEMENT, 0, 5, osiagnieciaLang[5], osiagnieciaOpisLang[5]));
        osiagniecia.Add(new AchievementInfo("MultiplyFour", NORMAL_ACHIEVEMENT, 0, 5, osiagnieciaLang[6], osiagnieciaOpisLang[6]));
        osiagniecia.Add(new AchievementInfo("WinSolo", NORMAL_ACHIEVEMENT, 0, 5, osiagnieciaLang[7], osiagnieciaOpisLang[7]));
        osiagniecia.Add(new AchievementInfo("WinSI", NORMAL_ACHIEVEMENT, 0, 10, osiagnieciaLang[8], osiagnieciaOpisLang[8]));
        osiagniecia.Add(new AchievementInfo("WinPVP", NORMAL_ACHIEVEMENT, 0, 15, osiagnieciaLang[9], osiagnieciaOpisLang[9]));

        osiagniecia.Add(new AchievementInfo("Pure1kSolo", INCREMENTAL_ACHIEVEMENT, 0, 5, osiagnieciaLang[10], osiagnieciaOpisLang[10]));
        osiagniecia.Add(new AchievementInfo("NotPure1kSolo", INCREMENTAL_ACHIEVEMENT, 0, 5, osiagnieciaLang[11], osiagnieciaOpisLang[11]));
        osiagniecia.Add(new AchievementInfo("Pure1kSI", INCREMENTAL_ACHIEVEMENT, 0, 5, osiagnieciaLang[12], osiagnieciaOpisLang[12]));
        osiagniecia.Add(new AchievementInfo("NotPure1kSI", INCREMENTAL_ACHIEVEMENT, 0, 5, osiagnieciaLang[13], osiagnieciaOpisLang[13]));
        osiagniecia.Add(new AchievementInfo("Pure1kPVP", INCREMENTAL_ACHIEVEMENT, 0, 5, osiagnieciaLang[14], osiagnieciaOpisLang[14]));
        osiagniecia.Add(new AchievementInfo("NotPure1kPVP", INCREMENTAL_ACHIEVEMENT, 0, 5, osiagnieciaLang[15], osiagnieciaOpisLang[15]));

        osiagniecia.Add(new AchievementInfo("Pure2kSolo", INCREMENTAL_ACHIEVEMENT, 0, 10, osiagnieciaLang[16], osiagnieciaOpisLang[16]));
        osiagniecia.Add(new AchievementInfo("NotPure5kSolo", INCREMENTAL_ACHIEVEMENT, 0, 5, osiagnieciaLang[17], osiagnieciaOpisLang[17]));
        osiagniecia.Add(new AchievementInfo("Pure2kSI", INCREMENTAL_ACHIEVEMENT, 0, 10, osiagnieciaLang[18], osiagnieciaOpisLang[18]));
        osiagniecia.Add(new AchievementInfo("NotPure5kSI", INCREMENTAL_ACHIEVEMENT, 0, 5, osiagnieciaLang[19], osiagnieciaOpisLang[19]));
        osiagniecia.Add(new AchievementInfo("Pure2kPVP", INCREMENTAL_ACHIEVEMENT, 0, 10, osiagnieciaLang[20], osiagnieciaOpisLang[20]));
        osiagniecia.Add(new AchievementInfo("NotPure5kPVP", INCREMENTAL_ACHIEVEMENT, 0, 5, osiagnieciaLang[21], osiagnieciaOpisLang[21]));

        osiagniecia.Add(new AchievementInfo("Pure4kSolo", INCREMENTAL_ACHIEVEMENT, 0, 10, osiagnieciaLang[22], osiagnieciaOpisLang[22]));
        osiagniecia.Add(new AchievementInfo("NotPure10kSolo", INCREMENTAL_ACHIEVEMENT, 0, 5, osiagnieciaLang[23], osiagnieciaOpisLang[23]));
        osiagniecia.Add(new AchievementInfo("Pure4kSI", INCREMENTAL_ACHIEVEMENT, 0, 15, osiagnieciaLang[24], osiagnieciaOpisLang[24]));
        osiagniecia.Add(new AchievementInfo("NotPure10kSI", INCREMENTAL_ACHIEVEMENT, 0, 10, osiagnieciaLang[25], osiagnieciaOpisLang[25]));
        osiagniecia.Add(new AchievementInfo("Pure4kPVP", INCREMENTAL_ACHIEVEMENT, 0, 30, osiagnieciaLang[26], osiagnieciaOpisLang[26]));
        osiagniecia.Add(new AchievementInfo("NotPure10kPVP", INCREMENTAL_ACHIEVEMENT, 0, 25, osiagnieciaLang[27], osiagnieciaOpisLang[27]));

        osiagniecia.Add(new AchievementInfo("UnlockAllSkins", HIDDEN_ACHIEVEMENT, 0, 0, osiagnieciaLang[28], osiagnieciaOpisLang[28]));
        osiagniecia.Add(new AchievementInfo("PureGame", NORMAL_ACHIEVEMENT, 0, 30, osiagnieciaLang[29], osiagnieciaOpisLang[29]));
        osiagniecia.Add(new AchievementInfo("Lucky", NORMAL_ACHIEVEMENT, 0, 10, osiagnieciaLang[30], osiagnieciaOpisLang[30]));
        osiagniecia.Add(new AchievementInfo("LongWay", NORMAL_ACHIEVEMENT, 0, 5, osiagnieciaLang[31],osiagnieciaOpisLang[31]));
       // ResetAllAchievements();
        LoadUserData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);
        // encrypt bytes
        System.Security.Cryptography.SHA1CryptoServiceProvider md5 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);
        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";
        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }
        return hashString.PadLeft(32, '0');
    }

    public void SetUserID()
    {
        string pom = PlayerPrefs.GetString("UserID");
        
        //Debug.Log(pom.Length);
        if (pom.Length <= 0)
        {
            pom = System.DateTime.Now.ToString() + Random.Range(0.0f, 1000.0f);
            //Debug.Log(pom);
            pom = Md5Sum(pom);
            //Debug.Log("Po:" + pom);
            PlayerPrefs.SetString("UserID", pom); 
            SkinManager.instance.UserID = pom;
        }
        else
        {
            //Debug.Log("ID existed:"+pom);
            SkinManager.instance.UserID = pom;
        }
    }

    void ResetBestResult()
    {
        PlayerPrefs.SetInt("BestResult", 0);
        SkinManager.instance.BestResult = 0;
    }

    public void ResetAllAchievements()
    {
        for (int i = 0; i < SkinManager.instance.osiagniecia.Count; ++i)
        {
            PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[i].ID, 0);
        }
        PlayerPrefs.SetFloat("PureSolo", 0);
        PlayerPrefs.SetFloat("NotPureSolo", 0);
        PlayerPrefs.SetFloat("PureSI", 0);
        PlayerPrefs.SetFloat("NotPureSI", 0);
        PlayerPrefs.SetFloat("PurePVP", 0);
        PlayerPrefs.SetFloat("NotPurePVP", 0);
        SetPureSolo(0);
        SetNotPureSolo(0);
        SetPureSI(0);
        SetNotPureSI(0);
        SetPurePVP(0);
        SetNotPurePVP(0);
        ResetBestResult();
        LoadUserData();
        
    }

    public void ResetCash()
    {
        SetCurrentCash(0);
        PlayerPrefs.SetInt("CurrentCash", 0);
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
        bool isUnlockAllSkins = true;
        AchievementInfo tmpAchievement;
        
#if HTML5
        ActiveSkin = 3;
#else
        ActiveSkin = PlayerPrefs.GetInt("ActiveSkin");
#endif
        ActiveFrame = PlayerPrefs.GetInt("ActiveFrame");
        ActiveBackground = PlayerPrefs.GetInt("ActiveBackground");
        ActiveSound = PlayerPrefs.GetInt("ActiveSound");
        ActiveSFXValue = PlayerPrefs.GetFloat("ActiveSFXValue");
        if (ActiveSFXValue == 0)
        {
            PlayerPrefs.SetFloat("ActiveSFXValue", 100.0f);
            ActiveSFXValue = 100.0f;
        }
        ActiveSoundValue = PlayerPrefs.GetFloat("ActiveSoundValue");
        if (ActiveSoundValue == 0)
        {
            PlayerPrefs.SetFloat("ActiveSoundValue", 100.0f);
            ActiveSoundValue = 100.0f;
        }
        isVictoryPointFirst = (PlayerPrefs.GetInt("IsVictoryPointFirst") != 0);
        isVictoryTimePass = (PlayerPrefs.GetInt("IsVictoryTimePass") != 0);
        VictoryPointFirstValue = PlayerPrefs.GetInt("VictoryPointFirst");
        VictoryTimePassValue = PlayerPrefs.GetInt("VictoryTimePass");
        ActiveVictoryConditions = PlayerPrefs.GetInt("ActiveVictoryConditions");
        Debug.Log("ActiveVictoryConditions:"+ActiveVictoryConditions);
        if (ActiveVictoryConditions == 0)
        {
            SkinManager.instance.SetIsVictoryTimePass(true);
            SkinManager.instance.SetIsVictoryPointFirst(false);
            SkinManager.instance.SetVictoryTimePassValue(5 * 60);
            SkinManager.instance.SetVictoryPointFirstValue(0);
            PlayerPrefs.SetInt("IsVictoryTimePass", true ? 1 : 0);
            PlayerPrefs.SetInt("IsVictoryPointFirst", false ? 1 : 0);
            PlayerPrefs.SetInt("VictoryTimePass", 5 * 60);
            PlayerPrefs.SetInt("VictoryPointFirst", 0);
        }
        ActivePlayerTurnConditions = PlayerPrefs.GetInt("ActivePlayerTurnConditions");
        ActivePlayerEndTime = PlayerPrefs.GetInt("ActivePlayerEndTime");
        CurrentCash = PlayerPrefs.GetInt("CurrentCash");
        BestResult = PlayerPrefs.GetInt("BestResult");
        ActivePlayerMode = PlayerPrefs.GetInt("ActivePlayerMode");
        PureSolo = PlayerPrefs.GetFloat("PureSolo");
        NotPureSolo = PlayerPrefs.GetFloat("NotPureSolo");
        PureSI = PlayerPrefs.GetFloat("PureSI");
        NotPureSI = PlayerPrefs.GetFloat("NotPureSI");
        PurePVP = PlayerPrefs.GetFloat("PurePVP");
        NotPurePVP = PlayerPrefs.GetFloat("NotPurePVP");
        BestResult = PlayerPrefs.GetFloat("BestResult");
        AllSkins = (PlayerPrefs.GetInt("AllSkins") != 0);
        MiddlePass = (PlayerPrefs.GetInt("MiddlePass") != 0);
        LatePass = (PlayerPrefs.GetInt("LatePass") != 0);
        FasterThanLightMiddle = (PlayerPrefs.GetInt("FasterThanLightMiddle") != 0);
        FasterThanLightLate = (PlayerPrefs.GetInt("FasterThanLightLate") != 0);
        MultiplyTwice = (PlayerPrefs.GetInt("MultiplyTwice") != 0);
        MultiplyThree = (PlayerPrefs.GetInt("MultiplyThree") != 0);
        MultiplyFour = (PlayerPrefs.GetInt("MultiplyFour") != 0);
        WinSolo = (PlayerPrefs.GetInt("WinSolo") != 0);
        WinSI = (PlayerPrefs.GetInt("WinSI") != 0);
        WinPVP = (PlayerPrefs.GetInt("WinPVP") != 0);
        Lucky = (PlayerPrefs.GetInt("Lucky") != 0);
        LongWay = (PlayerPrefs.GetInt("LongWay") != 0);
        AIDifficulty = PlayerPrefs.GetInt("AIDifficulty");
        //TODO progres
       // Debug.Log(osiagniecia.Count);
       // Debug.Log(PURE1KSOLO);
       /* if (PlayerPrefs.GetInt(SkinManager.instance.osiagniecia[SkinManager.PURE1KSOLO].ID) == null)
        {
            PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.PURE1KSOLO].ID, 0);
        }*/
        isPureSolo1 = (PlayerPrefs.GetInt(osiagniecia[PURE1KSOLO].ID) != 0);
        tmpAchievement = SkinManager.instance.osiagniecia[SkinManager.PURE1KSOLO];
        tmpAchievement.Progress = PureSolo;
        SkinManager.instance.osiagniecia[SkinManager.PURE1KSOLO] = tmpAchievement;

        isNotPureSolo1 = (PlayerPrefs.GetInt(SkinManager.instance.osiagniecia[SkinManager.NOTPURE1KSOLO].ID) != 0);
        tmpAchievement = SkinManager.instance.osiagniecia[SkinManager.NOTPURE1KSOLO];
        tmpAchievement.Progress = PureSolo;
        SkinManager.instance.osiagniecia[SkinManager.NOTPURE1KSOLO] = tmpAchievement;

        isPureSI1 = (PlayerPrefs.GetInt(SkinManager.instance.osiagniecia[SkinManager.PURE1KSI].ID) != 0);
        tmpAchievement = SkinManager.instance.osiagniecia[SkinManager.PURE1KSI];
        tmpAchievement.Progress = PureSolo;
        SkinManager.instance.osiagniecia[SkinManager.PURE1KSI] = tmpAchievement;

        isNotPureSI1 = (PlayerPrefs.GetInt(SkinManager.instance.osiagniecia[SkinManager.NOTPURE1KSI].ID) != 0);
        tmpAchievement = SkinManager.instance.osiagniecia[SkinManager.NOTPURE1KSI];
        tmpAchievement.Progress = PureSolo;
        SkinManager.instance.osiagniecia[SkinManager.NOTPURE1KSI] = tmpAchievement;

        isPurePVP1 = (PlayerPrefs.GetInt(SkinManager.instance.osiagniecia[SkinManager.PURE1KPVP].ID) != 0);
        tmpAchievement = SkinManager.instance.osiagniecia[SkinManager.PURE1KPVP];
        tmpAchievement.Progress = PureSolo;
        SkinManager.instance.osiagniecia[SkinManager.PURE1KPVP] = tmpAchievement;

        isNotPurePVP1 = (PlayerPrefs.GetInt(SkinManager.instance.osiagniecia[SkinManager.NOTPURE1KPVP].ID) != 0);
        tmpAchievement = SkinManager.instance.osiagniecia[SkinManager.NOTPURE1KPVP];
        tmpAchievement.Progress = PureSolo;
        SkinManager.instance.osiagniecia[SkinManager.NOTPURE1KPVP] = tmpAchievement;

        isPureSolo2 = (PlayerPrefs.GetInt(SkinManager.instance.osiagniecia[SkinManager.PURE2KSOLO].ID) != 0);
        tmpAchievement = SkinManager.instance.osiagniecia[SkinManager.PURE2KSOLO];
        tmpAchievement.Progress = PureSolo;
        SkinManager.instance.osiagniecia[SkinManager.PURE2KSOLO] = tmpAchievement;

        isNotPureSolo2 = (PlayerPrefs.GetInt(SkinManager.instance.osiagniecia[SkinManager.NOTPURE5KSOLO].ID) != 0);
        tmpAchievement = SkinManager.instance.osiagniecia[SkinManager.NOTPURE5KSOLO];
        tmpAchievement.Progress = PureSolo;
        SkinManager.instance.osiagniecia[SkinManager.NOTPURE5KSOLO] = tmpAchievement;

        isPureSI2 = (PlayerPrefs.GetInt(SkinManager.instance.osiagniecia[SkinManager.PURE2KSI].ID) != 0);
        tmpAchievement = SkinManager.instance.osiagniecia[SkinManager.PURE2KSI];
        tmpAchievement.Progress = PureSolo;
        SkinManager.instance.osiagniecia[SkinManager.PURE2KSI] = tmpAchievement;

        isNotPureSI2 = (PlayerPrefs.GetInt(SkinManager.instance.osiagniecia[SkinManager.NOTPURE5KSI].ID) != 0);
        tmpAchievement = SkinManager.instance.osiagniecia[SkinManager.NOTPURE5KSI];
        tmpAchievement.Progress = PureSolo;
        SkinManager.instance.osiagniecia[SkinManager.NOTPURE5KSI] = tmpAchievement;

        isPurePVP2 = (PlayerPrefs.GetInt(SkinManager.instance.osiagniecia[SkinManager.PURE2KPVP].ID) != 0);
        tmpAchievement = SkinManager.instance.osiagniecia[SkinManager.PURE2KPVP];
        tmpAchievement.Progress = PureSolo;
        SkinManager.instance.osiagniecia[SkinManager.PURE2KPVP] = tmpAchievement;

        isNotPurePVP2 = (PlayerPrefs.GetInt(SkinManager.instance.osiagniecia[SkinManager.NOTPURE5KPVP].ID) != 0);
        tmpAchievement = SkinManager.instance.osiagniecia[SkinManager.NOTPURE5KPVP];
        tmpAchievement.Progress = PureSolo;
        SkinManager.instance.osiagniecia[SkinManager.NOTPURE5KPVP] = tmpAchievement;

        isPureSolo3 = (PlayerPrefs.GetInt(SkinManager.instance.osiagniecia[SkinManager.PURE4KSOLO].ID) != 0);
        tmpAchievement = SkinManager.instance.osiagniecia[SkinManager.PURE4KSOLO];
        tmpAchievement.Progress = PureSolo;
        SkinManager.instance.osiagniecia[SkinManager.PURE4KSOLO] = tmpAchievement;

        isNotPureSolo3 = (PlayerPrefs.GetInt(SkinManager.instance.osiagniecia[SkinManager.NOTPURE10KSOLO].ID) != 0);
        tmpAchievement = SkinManager.instance.osiagniecia[SkinManager.NOTPURE10KSOLO];
        tmpAchievement.Progress = PureSolo;
        SkinManager.instance.osiagniecia[SkinManager.NOTPURE10KSOLO] = tmpAchievement;

        isPureSI3 = (PlayerPrefs.GetInt(SkinManager.instance.osiagniecia[SkinManager.PURE4KSI].ID) != 0);
        tmpAchievement = SkinManager.instance.osiagniecia[SkinManager.PURE4KSI];
        tmpAchievement.Progress = PureSolo;
        SkinManager.instance.osiagniecia[SkinManager.PURE4KSI] = tmpAchievement;

        isNotPureSI3 = (PlayerPrefs.GetInt(SkinManager.instance.osiagniecia[SkinManager.NOTPURE10KSI].ID) != 0);
        tmpAchievement = SkinManager.instance.osiagniecia[SkinManager.NOTPURE10KSI];
        tmpAchievement.Progress = PureSolo;
        SkinManager.instance.osiagniecia[SkinManager.NOTPURE10KSI] = tmpAchievement;

        isPurePVP3 = (PlayerPrefs.GetInt(SkinManager.instance.osiagniecia[SkinManager.PURE4KPVP].ID) != 0);
        tmpAchievement = SkinManager.instance.osiagniecia[SkinManager.PURE4KPVP];
        tmpAchievement.Progress = PureSolo;
        SkinManager.instance.osiagniecia[SkinManager.PURE4KPVP] = tmpAchievement;

        isNotPurePVP3 = (PlayerPrefs.GetInt(SkinManager.instance.osiagniecia[SkinManager.NOTPURE10KPVP].ID) != 0);
        tmpAchievement = SkinManager.instance.osiagniecia[SkinManager.NOTPURE10KPVP];
        tmpAchievement.Progress = PureSolo;
        SkinManager.instance.osiagniecia[SkinManager.NOTPURE10KPVP] = tmpAchievement;

        PureGame = (PlayerPrefs.GetInt(SkinManager.instance.osiagniecia[SkinManager.PUREGAME].ID) != 0);

        //achievement
        for (int i = 1; i < SkinManager.instance.skorki.Count; ++i)
        {
            isUnlockAllSkins = ((isUnlockAllSkins) && (PlayerPrefs.GetInt(SkinManager.instance.skorki[i].Name) != 0));
        }
        for (int i = 1; i < SkinManager.instance.ramki.Count; ++i)
        {
            isUnlockAllSkins = isUnlockAllSkins && (PlayerPrefs.GetInt(SkinManager.instance.ramki[i].Name) != 0);
        }
        for (int i = 1; i < SkinManager.instance.tla.Count; ++i)
        {
            isUnlockAllSkins = isUnlockAllSkins && (PlayerPrefs.GetInt(SkinManager.instance.tla[i].Name) != 0);
        }
        for (int i = 1; i < SkinManager.instance.muzyki.Count; ++i)
        {
            isUnlockAllSkins = isUnlockAllSkins && (PlayerPrefs.GetInt(SkinManager.instance.muzyki[i].Name) != 0);
        }

        if (isUnlockAllSkins)
        {
            if (!SkinManager.instance.UnlockAllSkins)
            {
                PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[SkinManager.UNLOCKALLSKINS].ID, true ? 1 : 0);
                SkinManager.instance.SetUnlockAllSkins(true);
                //AddCash(SkinManager.instance.osiagniecia[SkinManager.UNLOCKALLSKINS].Reward);
                //ShowAchievementPanel(SkinManager.LONGWAY);
            }
        }


    }

    public void SetLongWay(bool Value)
    {
        LongWay = Value;
    }

    public void SetLucky(bool Value)
    {
        Lucky = Value;
    }

    public void SetUnlockAllSkins(bool Value)
    {
        UnlockAllSkins = Value;
    }

    public void SetPureGame(bool Value)
    {
        PureGame = Value;
    }

    public void SetIsPureSolo1(bool Value)
    {
        isPureSolo1 = Value;
    }
    public void SetIsNotPureSolo1(bool Value)
    {
        isNotPureSolo1 = Value;
    }
    public void SetIsPureSI1(bool Value)
    {
        isPureSI1= Value;
    }
    public void SetIsNotPureSI1(bool Value)
    {
        isNotPureSI1 = Value;
    }
    public void SetIsPurePVP1(bool Value)
    {
        isPurePVP1 = Value;
    }
    public void SetIsNotPurePVP1(bool Value)
    {
        isNotPurePVP1 = Value;
    }
    public void SetIsPureSolo2(bool Value)
    {
        isPureSolo2 = Value;
    }
    public void SetIsNotPureSolo2(bool Value)
    {
        isNotPureSolo2 = Value;
    }
    public void SetIsPureSI2(bool Value)
    {
        isPureSI2 = Value;
    }
    public void SetIsNotPureSI2(bool Value)
    {
        isNotPureSI2 = Value;
    }
    public void SetIsPurePVP2(bool Value)
    {
        isPurePVP2 = Value;
    }
    public void SetIsNotPurePVP2(bool Value)
    {
        isNotPurePVP2 = Value;
    }
    public void SetIsPureSolo3(bool Value)
    {
        isPureSolo3 = Value;
    }
    public void SetIsNotPureSolo3(bool Value)
    {
        isNotPureSolo3 = Value;
    }
    public void SetIsPureSI3(bool Value)
    {
        isPureSI3 = Value;
    }
    public void SetIsNotPureSI3(bool Value)
    {
        isNotPureSI3 = Value;
    }
    public void SetIsPurePVP3(bool Value)
    {
        isPurePVP3 = Value;
    }
    public void SetIsNotPurePVP3(bool Value)
    {
        isNotPurePVP3 = Value;
    }
    public void SetBestResult(float Value)
    {
        BestResult = Value;
    }

    public void SetPureSolo(float Value)
    {
        PureSolo = Value;
    }
    public void SetNotPureSolo(float Value)
    {
        NotPureSolo = Value;
    }
    public void SetPureSI(float Value)
    {
        PureSI = Value;
    }
    public void SetNotPureSI(float Value)
    {
        NotPureSI = Value;
    }
    public void SetPurePVP(float Value)
    {
        PurePVP = Value;
    }
    public void SetNotPurePVP(float Value)
    {
        NotPurePVP = Value;
    }

    public void SetAllSkins(bool Value)
    {
        AllSkins = Value;
    }
    public void SetMiddlePass(bool Value)
    {
        MiddlePass = Value;
    }
    public void SetLatePass(bool Value)
    {
        LatePass = Value;
    }
    public void SetFasterThanLightMiddle(bool Value)
    {
        FasterThanLightMiddle = Value;
    }
    public void SetFasterThanLightLate(bool Value)
    {
        FasterThanLightLate = Value;
    }
    public void SetMultiplyTwice(bool Value)
    {
        MultiplyTwice = Value;
    }
    public void SetMultiplyThree(bool Value)
    {
        MultiplyThree = Value;
    }
    public void SetMultiplyFour(bool Value)
    {
        MultiplyFour = Value;
    }
    public void SetWinSolo(bool Value)
    {
        WinSolo = Value;
    }
    public void SetWinSI(bool Value)
    {
        WinSI = Value;
    }
    public void SetWinPVP(bool Value)
    {
        WinPVP = Value;
    }

    public void SetAIPToShow(string Value)
    {
        AIPToShow += Value;
    }
    
    public string GetAIPToShow()
    {
        return AIPToShow;
    }

    public void SetDebugToShow(string Value)
    {
        DebugToShow += Value;
    }

    /*public void SetActiveBestResult(int Value)
    {
        BestResult = Value;
    }*/

    public void SetAIDifficulty(int Value)
    {
        AIDifficulty = Value;
    }

    public void SetActivePlayerMode(int Value)
    {
        ActivePlayerMode = Value;
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

    public void SetActivePlayerTurnConditions(int Value)
    {
        ActivePlayerTurnConditions = Value;
    }

    public void SetActivePlayerEndTime(int Value)
    {
        ActivePlayerEndTime = Value;
    }


    public void SetCurrentCash(int Value)
    {
        CurrentCash = Value;
    }
    


    void Awake()
    {
        //!!root game object only
        //DontDestroyOnLoad(transform.gameObject);
        if (osiagniecia.Count > 0)
        LoadUserData();
    }

    void OnEnable()
    {
        if (osiagniecia.Count > 0)
        LoadUserData();
    }
}
