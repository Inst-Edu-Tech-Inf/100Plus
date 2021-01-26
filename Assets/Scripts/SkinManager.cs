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
    public const int VICTORY_CONDITIONS_MODE_5MIN = 0;
    public const int VICTORY_CONDITIONS_MODE_15MIN = 1;
    public const int VICTORY_CONDITIONS_MODE_30MIN = 2;
    public const int VICTORY_CONDITIONS_MODE_20PKT = 3;
    public const int VICTORY_CONDITIONS_MODE_100PKT = 4;
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
    //public const int TUTORIALPASS = 32;
    //public const int SKIN_TUTORIALPASS = 33;
    public const int AI_EASY = 0;
    public const int AI_MEDIUM = 1;
    public const int AI_HARD = 2;
    public const int AI_IMPOSSIBLE = 3;


    public const int SAMOUCZEK_POCZATEK = 0;
    public const int SAMOUCZEK_KOLEJNE_ZADANIE = 1;
    public const int SAMOUCZEK_BRAK_CZERWONYCH = 2;
    public const int SAMOUCZEK_ODRZUC_DWIE = 3;
    public const int SAMOUCZEK_KONIEC_TURY = 4;
    public const int SAMOUCZEK_ODRZUC_INNE_CZERWONE = 5;
    public const int SAMOUCZEK_ODRZUC_ZADANIE_ZIELONE15 = 6;
    public const int SAMOUCZEK_TAPNIJ_CZERWONE16_ZADANIE = 7;
    public const int SAMOUCZEK_ZDOBADZ_CZERWONE = 8;
    public const int SAMOUCZEK_OTRZYMALES_PUNKT = 9;
    public const int SAMOUCZEK_ODRZUC_ZIELONE = 10;
    public const int SAMOUCZEK_TAPNIJ_CZERWONE11_ZADANIE = 11;
    public const int SAMOUCZEK_ZDOBADZ_CZERWONE11 = 12;
    public const int SAMOUCZEK_OTRZYMALES_NIECALY_PUNKT = 13;
    public const int SAMOUCZEK_ZMIEN_KOLORY = 14;
    public const int SAMOUCZEK_ZDOBYLES_SZCZESCIARZ = 15;
    public const int SAMOUCZEK_ZDOBYLES_TRUDNY = 16;
    public const int SAMOUCZEK_TAPNIJ_CZERWONE33_ZADANIE = 17;
    public const int SAMOUCZEK_PRZEMNOZ = 18;
    public const int SAMOUCZEK_UKONCZ = 19;

    public const int SAMOUCZEK_SKLEP_POCZATEK = 0;
    public const int SAMOUCZEK_SKLEP_WLACZ_RAMKI = 1;
    public const int SAMOUCZEK_SKLEP_ZMIEN_RAMKI = 2;
    public const int SAMOUCZEK_SKLEP_WYBIERZ_RAMKE = 3;
    public const int SAMOUCZEK_SKLEP_UKONCZ = 4;

    public const int PROGRAMISCI = 0;
    public const int GRAFICY = 1;
    public const int TESTERZY = 2;
    public const int KONCEPCJA_GRY = 3;
    public const int WARUNKI_ZWYCIESTWA = 4;
    public const int KONIEC_TURY_GRACZA = 5;
    public const int DZWIEK = 6;
    public const int TRYB_GRY = 7;
    public const int GOTOWY = 8;
    public const int NAZWA_SZKOLY = 9;
    public const int NAZWA_KLASY = 10;
    public const int ILOSC_UCZNIOW = 11;
    public const int KOD_UCZNIA = 12;
    public const int WIEK_UCZNIA = 13;
    public const int KOD_ZAJETY = 14;
    public const int BRAK_SKROCONA_NAZWA_SZKOLY = 15;
    public const int BRAK_SKROCONA_NAZWA_KLASY = 16;
    public const int KODY_DLA_UCZNIOW = 17;
    public const int ZAGRAJ = 18;
    public const int SUMMON_LEAGUE = 19;
    public const int TLUMACZE = 20;
    public const int JESTES_PEWNY = 21;
    public const int KOD_OD_NAUCZYCIELA = 22;
    public const int RESTART_GRY = 23;
    public const int REJESTRUJ_SIE = 24;
    public const int SKIP_TUTORIAL = 25;
    public const int GRACZ = 26;
    public const int SZKOLA = 27;

    public static string[] SKORKI_PL = new string[] { "Widzę ogień", "Władca Pierścieni", "Ziuuuu...", "Widziałem ogień", "Władca pierścieni", "ziuuuu...", "Pierścionek",
        "Aaaaaaa! Troll!","Jednorożec","Zagrajmy","Wszyscy razem", "Zimorodek"};
    public static string[] RAMKI_PL = new string[] { "Złoty prostokąt", "Biały kociak", "Hello kitty","Jak w albumie","Jak na dawnej fotografii", "Krok po kroku",
        "Razem","Pokój","Pianino","Fale","Złote fale","Podniebne fale","Trawiaste fale","Różowe fale","Ogniste fale"};
    public static string[] TLA_PL = new string[] { "W sumie...", "Gwieździsta noc", "Głęboka przestrzeń", "Jaśniejszy Summ On",  "Buuum","Galaktyka spiralna","Tęcza","Mroźnie",
        "Gotów, chwyć, rysuj","Palma","Storczyk","Na Księżyc!","Ziemia","Jezioro w lesie","Tak różowo...","Kwiat","Jesień","Kasztany","Róża", "Żółw",
        "Saturn","System słoneczny","Dziki i groźny","Fajerwerki","Gdzie jest skarb?"};
    public static string[] MUZYKI_PL = new string[] { "Jak słodko", "Szalona", "Jak miło" };
    public static string[] OSIAGNIECIA_PL = new string[] { "Trudne początki", "Zmiana kart", "Szybki","Szybszy","Podwójnie",
        "Potrójnie","Poczwórnie","Zwycięzca","Bystry","Bystrzejszy","Idealny Uczeń",
        "Niechlujny Uczeń","Idealny Pracownik",
        "Niechlujny Pracownik", "Idealny Akolita",
        "Niechlujny Akolita","Idealny Czeladnik",
        "Niechlujny Czeladnik","Idealny Rzemieślnik",
        "Niechlujny Rzemieślnik","Idealny Magik",
        "Niechlujny Magik","Idealny Master",
        "Niechlujny Master", "Idealny Artysta",
        "Niechlujny Artysta","Idealny Czarodziej",
        "Niechlujny Czarodziej","Bogactwo","Doskonale","Szczęściarz", 
        "Daleka droga"};
    public static string[] OSIAGNIECIA_OPIS_PL = new string[] { "Ukończony samouczek", "Odblokowane zaawansowane karty mnożników", "Szybszy niż błyskawica","Szybszy niż światło","Pomnóż dwukrotnie",
        "Pomnóż trzykrotnie","Pomnóż czterokrotnie","Wygraj grę solo","Wygraj grę z komputerem (poziom hard)", "Wygraj grę z innym graczem","Uzbieraj sam " + ACHIEVEMENT_PURE_1ST.ToString() + " punktów - tylko idealne wyniki",
        "Uzbieraj sam " + ACHIEVEMENT_NOT_PURE_1ST.ToString() + " punktów - tylko za duże wyniki","Uzbieraj przeciw SI " + ACHIEVEMENT_PURE_1ST.ToString() + " punktów - tylko idealne wyniki",
        "Uzbieraj przeciw SI " + ACHIEVEMENT_NOT_PURE_1ST.ToString() + " punktów - tylko za duże wyniki","Uzbieraj przeciw innym " + ACHIEVEMENT_PURE_1ST.ToString() + " punktów - tylko idealne wyniki",
        "Uzbieraj przeciw innym " + ACHIEVEMENT_NOT_PURE_1ST.ToString() + " punktów - tylko za duże wyniki","Uzbieraj sam " + ACHIEVEMENT_PURE_2ND.ToString() + " punktów - tylko idealne wyniki",
        "Uzbieraj sam " + ACHIEVEMENT_NOT_PURE_2ND.ToString() + " punktów - tylko za duże wyniki","Uzbieraj przeciw SI " + ACHIEVEMENT_PURE_2ND.ToString() + " punktów - tylko idealne wyniki",
        "Uzbieraj przeciw SI " + ACHIEVEMENT_NOT_PURE_2ND.ToString() + " punktów - tylko za duże wyniki","Uzbieraj przeciw innym " + ACHIEVEMENT_PURE_2ND.ToString() + " punktów - tylko idealne wyniki",
        "Uzbieraj przeciw innym " + ACHIEVEMENT_NOT_PURE_2ND.ToString() + " punktów - tylko za duże wyniki","Uzbieraj sam " + ACHIEVEMENT_PURE_3RD.ToString() + " punktów - tylko idealne wyniki",
        "Uzbieraj sam " + ACHIEVEMENT_NOT_PURE_3RD.ToString() + " punktów - tylko za duże wyniki","Uzbieraj przeciw SI " + ACHIEVEMENT_PURE_3RD.ToString() + " punktów - tylko idealne wyniki",
        "Uzbieraj przeciw SI " + ACHIEVEMENT_NOT_PURE_3RD.ToString() + " punktów - tylko za duże wyniki","Uzbieraj przeciw innym " + ACHIEVEMENT_PURE_3RD.ToString() + " punktów - tylko idealne wyniki",
        "Uzbieraj przeciw innym " + ACHIEVEMENT_NOT_PURE_3RD.ToString() + " punktów - tylko za duże wyniki","Jestem bogaty...","Po prostu perfekcyjna gra", "Ryzyko czasem popłaca",
        "Użyj 5 kart na raz, żeby zebrać idealny wynik"};
    public static string[] MENU_PL = new string[] { "Programiści", "Graficy", "Testerzy", "Koncepcja gry", "Warunki zwycięstwa", "Koniec tury gracza", "Dźwięk", "Tryb gry", 
        "Gotowy", "Nazwa szkoły", "Symbol klasy (np.4a)", "Ilość uczniów w klasie", "Kod ucznia", "Wiek", "Kod zajęty lub nieprawidłowy",
        "Podaj skróconą nazwę szkoły, np. SP1", "Podaj skróconą nazwę klasy, np. 4a", 
        "Udostępnij kody uczniom", "Zagraj mecz ligowy", "SummOn Liga", "Tłumaczenie",
        "Czy jesteś pewny?", "tu wpisz kod, który otrzymałeś od nauczyciela",
        "Musisz zrestartować SummOn żeby zagrać w trybie dwóch graczy!", "Zarejestruj się", "Pomiń samouczek",
        "Gracz", "Szkoła"};
    // public sstring[] MENU_PL = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i" };
    public static string[] SAMOUCZEK_PL = new string[] { 
        "Twoim zadaniem jest zebrać odpowiednią ilość punktów w odpowiednim kolorze, Na razie nie masz czerwonych kart, dlatego zakończ turę przyciskiem z prawej strony",
        "Pojawiło się kolejne zadanie oraz dwie nowe karty, ponownie zakończ turę",
        "Nadal brakuje Ci czerwonych kart, ponownie zakończ turę",
        "Ponieważ w pasku dolnym masz za dużo kart, musisz odrzucić dwie do kosza",
        "Zakończ turę",
        "Odrzuć dwie karty w kolorze innym niż czerwony",
        "Ponieważ zebrało się za dużo zadań, usuń zielone zadanie, tapnij je",
        "Żeby zdobyć punkty za czerwoną kartę (16) zadań, tapnij ją",
        "Tapnij po kolei obie karty z dolnego przybornika i zaakceptuj tapnięciem w zielony znak √",
        "Otrzymałeś dokładnie jeden punkt. Zakończ turę",
        "Odrzuć dwie karty w kolorze innym niż czerwony",
        "Tapnij czerwone zadanie o wartości 11",
        "Tapnij po kolei dwie karty (6) z dolnego przybornika i zaakceptuj tapnięciem w zielony znak √",
        "Ponieważ 6+6=12 jest większe od 11, otrzymałeś jedynie 0,48 punktu. Zakończ turę",
        "\n \n \n \n \n \nPonieważ dużo zadań jest w tym samym kolorze, możesz zmienić je, wydając 1 punkt. Tapnij obraz z podkową i kostkami",
        "Zdobyłeś osiągnięcie '"+OSIAGNIECIA_PL[LUCKY]+"' i 5 monet na zakup skórek. Graj dalej, żeby zdobyć 6 punktów",
        "Zdobyłeś osiągnięcie '"+OSIAGNIECIA_PL[LATEPASS]+"' i 5 monet na zakup skórek. Zakończ turę",
        "Tapnij czerwone zadanie 33",
        "Tapnij kartę 11 czerwone z dolnego przybornika, przeciągnij na nią kartę '3x' z prawego rogu i zaakceptuj znakiem √",
        "Zdobyłeś 3 punkty i ukończyłeś samouczek. Możesz dokończyć grę i wydać monety w sklepie ze skórkami"
        };
    public static string[] SAMOUCZEK_SKLEP_PL = new string[] { 
        "Zmień wygląd karty. Tapnij obrazek → lub ←",
        "Ponieważ nie został jeszcze odblokowany, widzisz ikonę monet. Koszt ($) zmienił kolor na czerwony. Tapnij obrazek ramki obok motyla",
        "Teraz zmień wygląd ramki. Tapnij obrazek → lub ←",
        "Koszt zmienił kolor na zielony. Wybierz dowolną ramkę i kup ją tapnięciem na ikonę monet tuż obok wybranej ramki",
        "Kupiona ramka jest aktywna. Aktywne skórki ustawiasz tapnięciem na znak √"
        };

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
    public static string[] MENU_EN = new string[] { "Programmers", "Graphics", "Testers", "Game concept", "Victory settings", "Player end turn", "Sound settings", "Game mode",
        "Ready", "School name", "Class symbol (i.e. 4a)", "Number of students in this class", "Student code", "Age", "Code already used or invalid",
        "Short school name can't be empty (i.e.PS1)", "Short class name can't be empty (i.e.4a)", 
        "Give students the codes", "Play league match", "SummOn League" , "Translators",
        "Are you sure?", "the code received from your teacher write here",
        "You need to restart SummOn to play multiplayer mode!", "Remember to registry", "Skip Tutorial",
        "Player", "School"};
    public static string[] SAMOUCZEK_EN = new string[] { 
        "Your task is collect enough points at correct colour. You don't have red cards yet. End turn now by tapping button on right edge of screen",
        "You see next task and two new cards. Again end turn",
        "You still empty with red cards. Again end turn",
        "Because you have too many cards you need to discard 2 cards to the trash",
        "End turn now",
        "Discard two cards in colour different than red",
        "Because you have too many task, discard green task, just tap it",
        "Tap red task card (16) to collect points",
        "Tap both cards from bottom tray, one by one and accept by tapping at green mark √",
        "You received exactly one point. End turn now.",
        "Discard two cards in colour different than czerwony",
        "Tap red task with a value 11",
        "Tap two cards (2) from bottom tray, one by one and accept by tapping at green mark √",
        "Because 6+6=12 is greater than 11, you received only 0,48 point. End turn now",
        "Because of many task are the same colour you can change it by cost of 1 point. Tap the horseshoe and dice picture",
        "You earned the achievement '"+OSIAGNIECIA_PL[LUCKY]+"' and 5 coins to buy skins. keep playing until you get 6 points",
        "You earned the achievement '"+OSIAGNIECIA_EN[LATEPASS]+"' and 5 coins to buy skins. End turn now",
        "Tap red task with a value 33",
        "Tap red card 11 at bottom tray, then drag on it the card '3x' from right bottom corner and accept by tapping green mark √",
        "You earned 3 points and finished tutorial. You can continue game and spend coins at the skins shop"
        };
    public static string[] SAMOUCZEK_SKLEP_EN = new string[] { 
        "Change the cards look. Tap the picture → or ←",
        "Because skin isn't unlock, you see coins icon. The price($) changed colour to red. Tap frame picture near butterfly",
        "Change the frame look now. Tap the picture → lub ←",
        "The price($) changed colour to green. Choose any frame and buy it by tapping coins icon near choosen frame",
        "The bought frame is now active. You set active skins by tapping on the mark √"
        };
    //Russian
    //Скины
    public static string[] SKORKI_RU = new string[] { "Вижу огонь", "Властелин Колец", "Зиуууу...", "Я видел огонь", "Властелин колец", "зиуууу...", "Кольцо",
        "Aaaaaaa! Тролль!","Единорог","сыграем","все вместе", "Зимородок"};
    //Рамки
    public static string[] RAMKI_RU = new string[] { "Золотой прямоугольник", "белый котенок", "Hello kitty","как в альбоме","как на старой фотографии", "шаг за шагом",
        "Вместе","Вместе","Пианино","Волны","Золотые волны","Небесные волны","Травянистые волны","Розовые волны","Огненные волны"};
    //Фон
    public static string[] TLA_RU = new string[] { "Итого...", "Итого...", "глубокое пространство", "ярче Summ On",  "Бууум","спиральная галактика","Радуга","Мороз",
        "Готов, хватай, рисуй","Пальма","Орхидея","на Луну!","Земля","озеро в лесу","так розово...","Цветок","Осень","Каштаны","Роза", "Черепаха",
        "Сатурн","Солнечная система","дикий и грозный","фейерверк","где сокровище?"};
    //Музика
    public static string[] MUZYKI_RU = new string[] { "Как  сладко", "сумасшедшая", "как мило" };
    //Достижения
    public static string[] OSIAGNIECIA_RU = new string[] { "Трудное начало", "смена карт", "быстрый","более быстрый","двойной",
        "Тройной","Четверной","Победитель","Быстрый","Более быстрый","Идеальный Ученик",
        "Неаккуратный Студент","Идеальный Работник",
        "Неаккуратный Работник", "Идеальный Помощник",
        "Неаккуратный Помощник","Идеальный Подмастерье",
        "Неряшливый Подмастерье","Идеальный Ремесленник",
        "Неряшливый Мастер","Идеальный Маг",
        "Неаккуратный Маг","Идеальный Мастер",
        "Грязный Мастер", "Идеальный Художник",
        "Неряшливый Художник","Идеальный Волшебник",
        "Неаккуратный Волшебник","Богатство","Отлично","Счастливчик",
        "Далекий путь"};
    //Описание достижений
    public static string[] OSIAGNIECIA_OPIS_RU = new string[] { "Завершенный учебный уровень", "разблокированные расширенные карты множителей", "быстрее молнии","быстрее света","умножь дважды",
        "Умножь в три раза","умножь в четыре раза","выиграй сольную игру","выиграй игру с компьютером (сложный уровень)", "выиграй игру с другим игроком","получи  " + ACHIEVEMENT_PURE_1ST.ToString() + " очков в одиночку - только идеальные результаты",
        "получи  " + ACHIEVEMENT_NOT_PURE_1ST.ToString() + " очков в одиночку - только за превышены результаты","Набери " + ACHIEVEMENT_PURE_1ST.ToString() + " очков против Си - только идеальные результаты",
        "Набери " + ACHIEVEMENT_NOT_PURE_1ST.ToString() + " очков против Си - только превышены результаты","Набери против других " + ACHIEVEMENT_PURE_1ST.ToString() + " очков - только идеальные результаты",
        "Набери против других " + ACHIEVEMENT_NOT_PURE_1ST.ToString() + " очков, только превышены результаты","получи  " + ACHIEVEMENT_PURE_2ND.ToString() + " очков в одиночку - только идеальные результаты",
        "получи  " + ACHIEVEMENT_NOT_PURE_2ND.ToString() + " очков в одиночку - только за превышены результаты","Набери " + ACHIEVEMENT_PURE_2ND.ToString() + " очков против Си - только идеальные результаты",
        "Набери " + ACHIEVEMENT_NOT_PURE_2ND.ToString() + " очков против Си - только превышены результаты","Набери против других " + ACHIEVEMENT_PURE_2ND.ToString() + " очков - только идеальные результаты",
        "Набери против других " + ACHIEVEMENT_NOT_PURE_2ND.ToString() + " очков, только превышены результаты","получи  " + ACHIEVEMENT_PURE_3RD.ToString() + " очков в одиночку - только идеальные результаты",
        "получи  " + ACHIEVEMENT_NOT_PURE_3RD.ToString() + " очков в одиночку - только за превышены результаты","Набери " + ACHIEVEMENT_PURE_3RD.ToString() + " очков против Си - только идеальные результаты",
        "Набери " + ACHIEVEMENT_NOT_PURE_3RD.ToString() + " очков против Си - только превышены результаты","Набери против других " + ACHIEVEMENT_PURE_3RD.ToString() + " очков - только идеальные результаты",
        "Набери против других " + ACHIEVEMENT_NOT_PURE_3RD.ToString() + " очков, только превышены результаты","я богат...","Просто идеальная игра", "риск иногда окупается",
        "Используй 5 карт за раз, чтобы собрать идеальный результат"};
    //Меню
    public static string[] MENU_RU = new string[] { "Программисты", "Графические дизайнеры", "Тестеры сайтов", "Концепция игры", "Условия победы", "Конец тура игрока", "Звук", "Режим игры",
        "Готовый", "Название школы", "Символ класса (например 4a) ", "Количество учеников в классе", "Код ученика", "Возраст", "занятий  или неправильный код",
        "Подай сокращенное название школы, напр. SP1", "Подай сокращенное название класса, напр. 4a",
        "Отправь код ученикам", "Сыграй лиговый матч ", "SummOn Liga", "Перевод",
        "Или ты определен?", "введите сюда код, который вы получили от учителя",
        "Ты должен зарегистрировать игру, чтобы сыграть в порядке для многих игроков!", "Зарегистрируйся", "пропустить руководство",
        "Игрок", "Школа"};
    //Самоучка
    public static string[] SAMOUCZEK_RU = new string[] {
        "Твоим заданием является собрать соответствующее количество очков в соответствующем цвете, пока что у тебя нет красных карт, поэтому закончи тур кнопкой с правой стороны",
        "Появилось следующее задание также две новых карты, опять закончи тур",
        "’По-прежнему тебе недостаточно  красных карт, опять закончи тур",
        "Поскольку в нижнем поясе ты имеешь слишком много карт, ты должен отбросить две в корзину",
        "Закончи тур",
        "Отбрось две карты  другого цвета  чем красный",
        "Поскольку собралось слишком много заданий, устрани зеленое задание, щелкни по ней ",
        "Чтобы получить пункты за красную карту (16) заданий, щелкни по ней",
        "Щелкни по порядку обе карты из нижней готовальни и одобри щелчком по зеленому знаку √",
        "Ты  получил точно одно очко. Закончи тур",
        "Отбрось две карты другого цвета чем красный",
        "Нажми красное задание под номером 11",
        "Нажми по очереди две карты (6) из нижней панели инструментов и примите, нажав на зеленый знак √",
        "Поскольку 6+6=12 больше 11, ты получил только 0,48 очка. Завершить ход",
        "\n \n \n \n \n \nИз-за того что многие задачи одного цвета, ты можешь изменить их, потратив 1 балл. Нажми изображение с подковами и кубиками",
        "Ты заработал достижение '"+OSIAGNIECIA_PL[LUCKY]+"' и 5 монет, чтобы купить скины. Продолжай играть, чтобы набрать 6 очков",
        "Ты заработал достижение '"+OSIAGNIECIA_PL[LATEPASS]+"' и 5 монет, чтобы купить скины. Завершить ход",
        "Нажми красное задание под номером  33",
        "Нажми на красную карту номер 11 из нижней панели инструментов, перетащи на нее карту' 3x ' из правого угла и прими знак √",
        "Ты набрал 3 очка и завершил учебный уровень. Ты можете закончить игру и потратить монеты в магазине скинов"
        };
    //Магазин
    public static string[] SAMOUCZEK_SKLEP_RU = new string[] {
        "Измени внешний вид карты. Нажми изображение → или ←",
        "Поскольку он еще не разблокирован, ты видишь значок монет. Стоимость ( $ ) стала красной. Нажми изображение рамки рядом с бабочкой",
        "Теперь измени  внешний вид рамки. Нажми на изображение → или ←",
        "Стоимость стала зеленой. Выбери любую рамку и купи ее, нажав на значок монеты рядом с выбранной рамкой",
        "Купленная рамка активна. Активные скины устанавливаются нажатием на символ √"
        };

    //France
    //Skins
    public static string[] SKORKI_FR = new string[] { "Je vois le feu", "Le Seigneur des anneaux", "Ziuuuu...", "J'ai vu le feu", "Le Seigneur des anneaux", "ziuuuu...", "Anneau",
        "Aaaaaaa! Troll!","Licorne","Jouons","Tous ensemble", "Martin-pêcheur"};
    //Cadres
    public static string[] RAMKI_FR = new string[] { "Rectangle d'or", "chaton blanc", "Hello kitty","Comme dans un album","Comme sur une vieille photo", "Pas à pas",
        "Ensemble","Paix","Piano","Vagues","Vagues dorées","Vagues célestes","Vagues d'herbe","Vagues roses","Vagues de feu"};
    //Fonds
    public static string[] TLA_FR = new string[] { "Dans l'ensemble ... ", "Nuit étoilée", "Espace", "Summ On plus clair ",  "Buuum","Galaxie en spirale","Arc-en-ciel","Glacial",
        "Prêt, attrape, dessine","Palmier","Orchidée","Vers la lune !","Terre","Lac dans la forêt","Si rose ...","Fleur","Automne","Châtaignes","Rose", "Tortue",
        "Saturne","Système solaire","Sauvage et dangereux","Feu d'artifice","Où est le trésor ?"};
    //Musique
    public static string[] MUZYKI_FR = new string[] { "Comme c'est doux", "Folle", "Comme c'est gentil" };
    //Résultats
    public static string[] OSIAGNIECIA_FR = new string[] { "Débuts difficiles", "Changer de carte", "Rapide","Plus rapide","Double",
        "Triple","Quadruple","Gagnant","intelligent","Plus intelligent","Étudiant idéal",
        "Élève Bâclé","Travailleur parfait",
        "Travailleur Bâclé", "Acolyte parfait",
        "Acolyte Bâclé","Ouvrier parfait",
        "Ouvrier bâclé","Artisan parfait",
        "Artisan Bâclé","Magicien Parfait",
        "Magicien Bâclé","Master Parfait",
        "Master Bâclé", "Artiste Parfait",
        "Artiste Bâclé","Mage Parfait",
        "Mage Bâclé","Fortune","Parfait","Chanceux",
        "Très loin"};
    //Description des réalisations
    public static string[] OSIAGNIECIA_OPIS_FR = new string[] { "Tutoriel terminé", "Cartes multiplicateurs avancées déverrouillées", "Plus rapide que l'éclair","Plus rapide que la lumière","Multipliez deux fois",
        "Multipliez trois fois","Multipliez quatre fois","Gagnez une partie en solo","Gagnez une partie contre l'ordinateur (niveau difficile)", "Gagnez une partie avec un autre joueur","Collectez seulement " + ACHIEVEMENT_PURE_1ST.ToString() + " points - résultats parfaits uniquement",
        "Collectez seulement " + ACHIEVEMENT_NOT_PURE_1ST.ToString() + " points - seulement des résultats élevés","Collectez " + ACHIEVEMENT_PURE_1ST.ToString() + " points contre l'IA - seulement des résultats parfaits ",
        "Collectez " + ACHIEVEMENT_NOT_PURE_1ST.ToString() + " points contre l'IA - uniquement des scores élevés","Collectez " + ACHIEVEMENT_PURE_1ST.ToString() + " points contre d'autres IA - uniquement des résultats parfaits ",
        "Collectez " + ACHIEVEMENT_NOT_PURE_1ST.ToString() + " points contre les autres - seulement des scores élevés","Collectez seulement " + ACHIEVEMENT_PURE_2ND.ToString() + " points - résultats parfaits uniquement",
        "Collectez seulement " + ACHIEVEMENT_NOT_PURE_2ND.ToString() + " points - seulement des résultats élevés ","Collectez  " + ACHIEVEMENT_PURE_2ND.ToString() + " points contre l'IA - seulement des résultats parfaits ",
        "Collectez " + ACHIEVEMENT_NOT_PURE_2ND.ToString() + " points contre l'IA - uniquement des scores élevés","Collectez " + ACHIEVEMENT_PURE_2ND.ToString() + " points contre d'autres IA - uniquement des résultats parfaits ",
        "Collectez " + ACHIEVEMENT_NOT_PURE_2ND.ToString() + " points contre les autres - seulement des scores élevés","Collectez seulement " + ACHIEVEMENT_PURE_3RD.ToString() + " points - résultats parfaits uniquement",
        "Collectez seulement " + ACHIEVEMENT_NOT_PURE_3RD.ToString() + " points - seulement des résultats élevés ","Collectez " + ACHIEVEMENT_PURE_3RD.ToString() + " points contre l'IA - seulement des résultats parfaits ",
        "Collectez " + ACHIEVEMENT_NOT_PURE_3RD.ToString() + " points contre l'IA - uniquement des scores élevés","Collectez " + ACHIEVEMENT_PURE_3RD.ToString() + " points contre d'autres IA - uniquement des résultats parfaits ",
        "Collectez " + ACHIEVEMENT_NOT_PURE_3RD.ToString() + " points contre les autres - seulement des scores élevés","Je suis riche ...","C'est juste un jeu parfait", "Le risque est parfois payant",
        "Utilisez 5 cartes à la fois pour obtenir un score parfait"};
    //Menu
    public static string[] MENU_FR = new string[] { "Programmeurs", "Artistes", "Testeurs", "Concept de jeu", "Conditions de victoire", "Fin du tour du joueur", "Son", "Mode de jeu",
        "Prêt", "Nom de l'école", "Symbole de la classe (par exemple 4a)", "Nombre d'élèves dans la classe", "Code étudiant", "Âge", "Code occupé ou invalide",
        "Entrez le nom abrégé de l'école, par exemple SP1", "Entrez le nom abrégé de la classe, par exemple 4a",
        "Partager des codes avec les élèves", "Jouer un match de championnat", "SummOn League", "Traduction",
        "Êtes-vous sûr?", "entrez ici le code que vous avez reçu de l'enseignant",
        "Vous devez redémarrer le jeu pour jouer en multijoueur!", "S'inscrire", "Passer le tutoriel",
        "Joueur", "École"};
    // public sstring[] MENU_PL = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i" };
    //Didacticiel
    public static string[] SAMOUCZEK_FR = new string[] {
        "Votre tâche est de collecter le bon nombre de points dans la bonne couleur. Vous n'avez pas encore de cartons rouges, alors terminez le tour avec le bouton à droite",
        "Une autre tâche et deux nouvelles cartes sont apparues, terminez à nouveau le tour",
        "Vous n'avez toujours pas de cartes rouges, terminez encore une fois votre tour",
        "Comme vous avez trop de cartes dans la barre du bas, vous devez en jeter deux dans la corbeille",
        "Finis ton tour",
        "Rejetez deux cartes d'une couleur autre que le rouge",
        "Étant donné que trop de tâches se sont accumulées, supprimez la tâche verte, appuyez dessus",
        "Pour marquer des points pour une carte de quête rouge (16), appuyez dessus",
        "Appuyez successivement sur les deux cartes dans la boîte à outils inférieure et acceptez en appuyant sur le signe √ vert",
        "Vous avez exactement un point. Terminez votre tour",
        "Rejetez deux cartes d'une couleur autre que le rouge",
        "Appuyez sur la tâche rouge avec une valeur de 11",
        "Cliquer sur deux cartes (6) de la boîte à outils du bas et acceptez en appuyant sur le signe √ vert",
        "Puisque 6 + 6 = 12 est supérieur à 11, vous n'avez obtenu que 0,48 point. Terminez votre tour",
        "\n \n \n \n \n \n Si vous avez trop de taches de la même couleur, vous pouvez les modifier en dépensant 1 point. Appuyez sur l'image avec un fer à cheval et des dés",
        "Vous marquez  '"+OSIAGNIECIA_PL[LUCKY]+"' des points et 5 pièces pour acheter des skins. Continuez à jouer pour obtenir 6 points",
        "Vous avez le succès  '"+OSIAGNIECIA_PL[LATEPASS]+"' et 5 pièces pour acheter des skins. Terminez votre tour",
        "Appuyez sur la tâche rouge 33",
        "Appuyez sur la carte rouge 11 de la boîte à outils inférieure, faites glisser la carte '3x' du coin droit dessus et acceptez avec le signe √",
        "Vous avez marqué 3 points et terminé le didacticiel. Vous pouvez terminer le jeu et dépenser des pièces dans la boutique de skins"
        };
    //Boutique
    public static string[] SAMOUCZEK_SKLEP_FR = new string[] {
        "Changer l'apparence de la carte. Appuyez sur l'image → ou ←",
        "Comme il n'a pas encore été déverrouillé, vous pouvez voir l'icône de la pièce. Le coût ($) est devenu rouge. Appuyez sur l'image de la bordure à côté du papillon",
        "Modifiez maintenant l'apparence du cadre. Appuyez sur l'image → ou ←",
        "Le coût est devenu vert. Choisissez n'importe quel cadre et achetez-le en appuyant sur l'icône de pièce à côté du cadre sélectionné",
        "Le cadre acheté est actif. Les skins actifs peuvent être définis en appuyant sur la marque √"
        };

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
    public List<SkinsInfo> skorkiAnim = new List<SkinsInfo>();
    public List<SkinsInfo> skorkiStat = new List<SkinsInfo>();

    public int skorkiSize;
    public int statSize; 
    public int animSize; 

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
    public bool SkipTutorial = false;

    public static SkinManager instance;
    public int CurrentScore;
    public string DebugToShow;
    public string AIPToShow;
    public string UserID;

    public bool isTutorialPass = false;
    public bool isSkinTutorialPass = false;
    public string[] TutorialLang;
    public string[] SkinTutorialLang;
    public string[] MenuLang;
    public SystemLanguage iLang;
    public static bool isNotificationsAdded = false;


    // Start is called before the first frame update
    void Start()
    {
        string[] skorkiLang, ramkiLang, tlaLang, muzykiLang, osiagnieciaLang, osiagnieciaOpisLang;
        iLang = Application.systemLanguage;
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
                TutorialLang = SAMOUCZEK_EN;
                SkinTutorialLang = SAMOUCZEK_SKLEP_EN;
                MenuLang = MENU_EN;
                break;
            case SystemLanguage.Polish:
                skorkiLang = SKORKI_PL;
                ramkiLang = RAMKI_PL;
                tlaLang = TLA_PL;
                muzykiLang = MUZYKI_PL;
                osiagnieciaLang = OSIAGNIECIA_PL;
                osiagnieciaOpisLang = OSIAGNIECIA_OPIS_PL;
                TutorialLang = SAMOUCZEK_PL;
                SkinTutorialLang = SAMOUCZEK_SKLEP_PL;
                MenuLang = MENU_PL;
                break;
            case SystemLanguage.Russian:
                skorkiLang = SKORKI_RU;
                ramkiLang = RAMKI_RU;
                tlaLang = TLA_RU;
                muzykiLang = MUZYKI_RU;
                osiagnieciaLang = OSIAGNIECIA_RU;
                osiagnieciaOpisLang = OSIAGNIECIA_OPIS_RU;
                TutorialLang = SAMOUCZEK_RU;
                SkinTutorialLang = SAMOUCZEK_SKLEP_RU;
                MenuLang = MENU_RU;
                break;
            case SystemLanguage.French:
                skorkiLang = SKORKI_FR;
                ramkiLang = RAMKI_FR;
                tlaLang = TLA_FR;
                muzykiLang = MUZYKI_FR;
                osiagnieciaLang = OSIAGNIECIA_FR;
                osiagnieciaOpisLang = OSIAGNIECIA_OPIS_FR;
                TutorialLang = SAMOUCZEK_FR;
                SkinTutorialLang = SAMOUCZEK_SKLEP_FR;
                MenuLang = MENU_FR;
                break;
            default:
                skorkiLang = SKORKI_EN;
                ramkiLang = RAMKI_EN;
                tlaLang = TLA_EN;
                muzykiLang = MUZYKI_EN;
                osiagnieciaLang = OSIAGNIECIA_EN;
                osiagnieciaOpisLang = OSIAGNIECIA_OPIS_EN;
                TutorialLang = SAMOUCZEK_EN;
                SkinTutorialLang = SAMOUCZEK_SKLEP_EN;
                MenuLang = MENU_EN;
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
        
        foreach (SkinsInfo skorka in skorki){
            if (skorka.Type == GameManager.KARTA_DYNAMICZNA) skorkiAnim.Add(skorka);
        }
        foreach (SkinsInfo skorka in skorki)
        {
            if (skorka.Type == GameManager.KARTA_STATYCZNA) skorkiStat.Add(skorka);
        }
        skorkiSize = SkinManager.instance.skorki.Count;
        statSize = SkinManager.instance.skorkiStat.Count;
        animSize = SkinManager.instance.skorkiAnim.Count;
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
        tla.Add(new SkinsInfo("NGC_5477_Hubble", GameManager.BACKGROUND_STATIC, BACKGROUND_PRICE, tlaLang[1]));
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


        osiagniecia.Add(new AchievementInfo("MiddlePass", NORMAL_ACHIEVEMENT, 0, 5, osiagnieciaLang[MIDDLEPASS], osiagnieciaOpisLang[MIDDLEPASS]));//ID, type, progress, reward, descripton 
        osiagniecia.Add(new AchievementInfo("LatePass", NORMAL_ACHIEVEMENT, 0, 5, osiagnieciaLang[LATEPASS], osiagnieciaOpisLang[LATEPASS]));
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
        osiagniecia.Add(new AchievementInfo("Lucky", NORMAL_ACHIEVEMENT, 0, 5, osiagnieciaLang[LUCKY], osiagnieciaOpisLang[LUCKY]));
        osiagniecia.Add(new AchievementInfo("LongWay", NORMAL_ACHIEVEMENT, 0, 5, osiagnieciaLang[31], osiagnieciaOpisLang[31]));
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

    public static string Hash(string str)
    {
        var allowedSymbols = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
        var hash = new char[8];

        for (int i = 0; i < str.Length; i++)
        {
            hash[i % 8] = (char)(hash[i % 8] ^ str[i]);
        }

        for (int i = 0; i < 8; i++)
        {
            hash[i] = allowedSymbols[hash[i] % allowedSymbols.Length];
        }

        return new string(hash);
    }

    public static string Md5SumShort(string strToEncrypt)
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
        //hashString += System.Convert.ToBase64String(hashBytes);
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
        //SkinManager.instance.UserID = "482e7ce34536663f4fdd8cea7717cd4a09d8981f";

        /*if ((pom.Contains("0")) || (pom.Contains("1")) || (pom.Contains("2")) || (pom.Contains("3")) || (pom.Contains("4")) || (pom.Contains("5"))
            || (pom.Contains("6")) || (pom.Contains("7")) || (pom.Contains("8")) || (pom.Contains("9")))
        {
            pom = pom.Replace("0", "g");
            pom = pom.Replace("1", "h");
            pom = pom.Replace("2", "i");
            pom = pom.Replace("3", "j");
            pom = pom.Replace("4", "k");
            pom = pom.Replace("5", "l");
            pom = pom.Replace("6", "m");
            pom = pom.Replace("7", "n");
            pom = pom.Replace("8", "o");
            pom = pom.Replace("9", "p");
            //Debug.Log(pom);
            PlayerPrefs.SetString("UserID", pom);
            SkinManager.instance.UserID = pom;
        }*/
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

    public void ResetTutorial()
    {
        PlayerPrefs.SetInt(SkinManager.instance.osiagniecia[0].ID, 0);
        PlayerPrefs.SetInt("isTutorialPass", false ? 1 : 0);
        //PlayerPrefs.SetInt("MiddlePass", false ? 1 : 0);
        PlayerPrefs.SetInt("Lucky", false ? 1 : 0);
        string pom = Hash(SkinManager.instance.UserID);
        PlayerPrefs.SetString("UserID", pom);
        SkinManager.instance.UserID = pom;
    }

    public void ResetSkinTutorial()
    {
        PlayerPrefs.SetInt("isSkinTutorialPass", false ? 1 : 0);
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
        Debug.Log("ActiveVictoryConditions:" + ActiveVictoryConditions);
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
        SkipTutorial = (PlayerPrefs.GetInt("SkipTutorial") != 0);
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

        if (MiddlePass)
        {
            PlayerPrefs.SetInt("isTutorialPass", true ? 1 : 0);
        }
        isSkinTutorialPass = (PlayerPrefs.GetInt("isSkinTutorialPass") != 0);
        isTutorialPass = (PlayerPrefs.GetInt("isTutorialPass") != 0);

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

        //SetMiddlePass(false);


    }

    public void SetIsNotificationsAdded(bool Value)
    {
        isNotificationsAdded = Value;
    }

    public bool  GetIsNotificationsAdded()
    {
        return isNotificationsAdded;
    }

    public void SetIsTutorialPass(bool Value)
    {
        isTutorialPass = Value;
    }

    public void SetIsSkinTutorialPass(bool Value)
    {
        isSkinTutorialPass = Value;
    }

    public void SetLongWay(bool Value)
    {
        LongWay = Value;
    }

    public void SetLucky(bool Value)
    {
        Lucky = Value;
    }

    public void SetSkipTutorial(bool Value)
    {
        SkipTutorial = Value;
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
        isPureSI1 = Value;
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