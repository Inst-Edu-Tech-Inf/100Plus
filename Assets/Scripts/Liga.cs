using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
//using System;
//using System.Data;
//using MySql.Data;
//using MySql.Data.MySqlClient;
//using System.IO;



public class Liga : MonoBehaviour
{
    public struct KlasaStruct
    {
        public int ID; 
        public string Name;

        //Constructor (not necessary, but helpful)
        public KlasaStruct(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
    }

    //bool activeAddClass = false;
    //string connStr = "server=s69.cyber-folks.pl;user=kolacz_zdalny;database=kolacz_jos1;port=3306;password=";
    bool czyNauczycielIstnieje;// = false;
    int nrNauczyciela;// = 0;
    int nrSzkoly;// = 0;
    int nrKlasy ;//= 0;
    int nrUcznia;// = 0;
    int nrUczniaTeams ;//= 0;
    public List<KlasaStruct> klasy = new List<KlasaStruct>();
    public List<int> indeksyKlas = new List<int>();
    string nazwaSzkoly;// = "";
    string skrotSzkoly = "";
    string kodZajety = "";

    [Header("Background"), SerializeField]
    public Image backgroundImage;
    public Text nazwaSzkolyText;
    public Text nazwaKlasyText;
    public Text iloscUczniowText;
    public Text iloscUczniowValue;
    public Text kodUczniaText;
    public Text wiekUczniowText;
    public Text wiekUczniowValue;
    public InputField skrotSzkolyText;
    public InputField nazwaSzkolyInput;
    public InputField nazwaKlasyInput;
    public InputField kodUczniaInput;
    public Button uczenBtn;
    public Button nauczycielBtn;
    public GameObject szkolaPanel;
    public GameObject klasaPanel;
    public GameObject uczenPanel;
    public GameObject workingPanel;
    public Dropdown listaKlas;
    public Button dodajSzkoleBtn;
    public Button dodajKlaseBtn;
    public Button dodajUczniaBtn;
    public Slider ileUczniowSlider;
    public Slider wiekUczniowSlider;
    public InputField kodyKlas1Input;
    public Text kodyKlas1;
    public Text kodyKlas2;
    public Text kodyKlas3;
    public Text kodyKlas4;
    public Text userIDText;
    public Text debugInfo;

   // private string secretKey = "mySecretKey"; // Edit this value and make sure it's the same as the one stored on the server
   // public string addScoreURL = "http://summon.ieti.pl/addscore.php";//"http://localhost/unity_test/addscore.php?"; //be sure to add a ? to your url
   // public string highscoreURL = "http://summon.ieti.pl/display.php";//"http://localhost/unity_test/display.php";

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


   /* IEnumerator connectSimple()
    {

        var form = new WWWForm();
        form.AddField("action", "post_to_table");
        form.AddField("tableId", "name_of_table_you want_to_access");
        var w = new WWW("http://summon.ieti.pl/mysql_connect.php", form);
        yield return w;
        Debug.Log(w);
    }*/

    IEnumerator connectSimple()
    {
        //UnityWebRequest www = UnityWebRequest.Get("http://summon.ieti.pl/GetDate.php");
        UnityWebRequest www = UnityWebRequest.Get("http://summon.ieti.pl/dbSummOn/GetUsers.php");
        yield return www.Send();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Debug.Log(www.downloadHandler.text);
            byte[] results = www.downloadHandler.data;
        }
    }

    public void SendPHP()
    {
        
        //StartCoroutine(connectSimple());
        //StartCoroutine(NauczycielWebClick(SkinManager.instance.UserID));
        //StartCoroutine(DodajSzkoleWebClick(nazwaSzkolyInput.text.ToString(), skrotSzkoly.text, SkinManager.instance.UserID));
        List<string> tmpSkrot = new List<string>();
        for (int i = 1; i <= 40; ++i)
        {
            tmpSkrot.Add(SkinManager.Hash(System.DateTime.Now.ToString() + UnityEngine.Random.Range(0.0f, 1000.0f)));
        }
        //skrotSzkolyText.text
        StartCoroutine(DodajKlaseWebClick(SkinManager.instance.UserID, skrotSzkoly, nazwaKlasyInput.text, wiekUczniowValue.text, ileUczniowSlider.value.ToString(), tmpSkrot[1], tmpSkrot[2], tmpSkrot[3], tmpSkrot[4], tmpSkrot[5], tmpSkrot[6], tmpSkrot[7], tmpSkrot[8], tmpSkrot[9], tmpSkrot[10], tmpSkrot[11], tmpSkrot[12], tmpSkrot[13], tmpSkrot[14], tmpSkrot[15], tmpSkrot[16], tmpSkrot[17], tmpSkrot[18], tmpSkrot[19], tmpSkrot[20], tmpSkrot[21], tmpSkrot[22], tmpSkrot[23], tmpSkrot[24], tmpSkrot[25], tmpSkrot[26], tmpSkrot[27], tmpSkrot[28], tmpSkrot[29], tmpSkrot[30], tmpSkrot[31], tmpSkrot[32], tmpSkrot[33], tmpSkrot[34], tmpSkrot[35], tmpSkrot[36], tmpSkrot[37], tmpSkrot[38], tmpSkrot[39], tmpSkrot[40]));
        //StartCoroutine(DodajKlaseWebClick(nazwaSzkolyInput.text, skrotSzkoly, SkinManager.instance.UserID));
       /*var form = new WWWForm();
        form.AddField("action", "post_to_table");
        form.AddField("tableId", "name_of_table_you want_to_access");
        var w = new WWW("http://summon.ieti.pl/mysql_connect.php", form);
        yield  return w;
        connectSimple();*/
        //workingPanel.SetActive(false);

    }

    IEnumerator NauczycielWebClick(string nauczycielPass)
    {
        workingPanel.SetActive(true);
        WWWForm form = new WWWForm();
        string[] strArr;
        form.AddField("nauczycielPass", nauczycielPass);
        //form.AddField("nauczycielPass", "");

        //UnityWebRequest www = UnityWebRequest.Post("http://summon.ieti.pl/GetNauczycielCount.php");
        using (UnityWebRequest www = UnityWebRequest.Post("http://summon.ieti.pl/dbSummOn/NauczycielClick.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                //Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text == "false")
                    czyNauczycielIstnieje = false;
                else
                {
                    
                    czyNauczycielIstnieje = true;
                    
                    strArr = www.downloadHandler.text.ToString().Split('*');
                    
                    nazwaSzkolyInput.text = strArr[0];
                    skrotSzkolyText.text = strArr[1];

                    Dropdown.OptionData tmpDropdown = new Dropdown.OptionData();
                    List<string> tmpNazwaKl = new List<string>();
                

                    klasy.Clear();
                    indeksyKlas.Clear();
                    listaKlas.options.Clear();

                    for (int i = 2; i < strArr.Length; i++ )
                    {
                        //Debug.Log(strArr[i]);
                        if (strArr[i] != "")
                            tmpNazwaKl.Add(strArr[i]);
                    }
                    if (tmpNazwaKl.Count >=1)
                        listaKlas.AddOptions(tmpNazwaKl);
                    listaKlas.value = 0;
                    WyswietlKodyKlasy();
                    klasaPanel.SetActive(true);
                    dodajSzkoleBtn.gameObject.SetActive(false);


                }
                //byte[] results = www.downloadHandler.data;
            }
            //Debug.Log("CzyNistnieje:" + czyNauczycielIstnieje);
        }
        workingPanel.SetActive(false);
    }

    IEnumerator UczenWebClick(string uczenPass)
    {
        workingPanel.SetActive(true);
        WWWForm form = new WWWForm();
       // string[] strArr;
        form.AddField("uczenPass", uczenPass);

        //UnityWebRequest www = UnityWebRequest.Post("http://summon.ieti.pl/GetNauczycielCount.php");
        using (UnityWebRequest www = UnityWebRequest.Post("http://summon.ieti.pl/dbSummOn/UczenClick.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text == "false")
                {

                }
                else
                {
                    kodUczniaInput.text = www.downloadHandler.text;
                }
            }
        }
        workingPanel.SetActive(false);
    }

    IEnumerator DodajSzkoleWebClick(string nazwaSzkolyPass, string skrotSzkolyPass, string userID)
    {
        workingPanel.SetActive(true);
        WWWForm form = new WWWForm();
        //string[] strArr;
        if (nazwaSzkolyPass == "")
            nazwaSzkolyPass = "?";
        if (skrotSzkolyPass == "")
            skrotSzkolyPass = "?";
        form.AddField("nazwaSzkolyPass", nazwaSzkolyPass);
        form.AddField("skrotSzkolyPass", skrotSzkolyPass);
        form.AddField("userID", userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://summon.ieti.pl/dbSummOn/DodajSzkole.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text == "false")
                {

                }
                else
                {
                    //kodUczniaInput.text = www.downloadHandler.text;
                    dodajSzkoleBtn.gameObject.SetActive(false);
                    klasaPanel.SetActive(true);
                }
            }
        }
        workingPanel.SetActive(false);
    }

    IEnumerator DodajKlaseWebClick(string nauczycielPass, string skrotSzkoly, string skrotKlasyPass, string wiekUczniow, string iluUczniowDodac, string skrotU1, string skrotU2, string skrotU3, string skrotU4, string skrotU5, string skrotU6, string skrotU7, string skrotU8, string skrotU9, string skrotU10, string skrotU11, string skrotU12, string skrotU13, string skrotU14, string skrotU15, string skrotU16, string skrotU17, string skrotU18, string skrotU19, string skrotU20, string skrotU21, string skrotU22, string skrotU23, string skrotU24, string skrotU25, string skrotU26, string skrotU27, string skrotU28, string skrotU29, string skrotU30, string skrotU31, string skrotU32, string skrotU33, string skrotU34, string skrotU35, string skrotU36, string skrotU37, string skrotU38, string skrotU39, string skrotU40)
    {
        workingPanel.SetActive(true);
        WWWForm form = new WWWForm();
       /* string[] strArr;
        if (nazwaSzkolyPass == "")
            nazwaSzkolyPass = "?";
        if (skrotSzkolyPass == "")
            skrotSzkolyPass = "?";*/
        form.AddField("nauczycielPass", nauczycielPass);
        form.AddField("skrotSzkoly", skrotSzkoly);
        form.AddField("skrotKlasyPass", skrotKlasyPass);
        form.AddField("wiekUczniow", wiekUczniow);
        form.AddField("iluUczniowDodac", iluUczniowDodac);
        form.AddField("skrotU1", skrotU1);
        form.AddField("skrotU2", skrotU2);
        form.AddField("skrotU3", skrotU3);
        form.AddField("skrotU4", skrotU4);
        form.AddField("skrotU5", skrotU5);
        form.AddField("skrotU6", skrotU6);
        form.AddField("skrotU7", skrotU7);
        form.AddField("skrotU8", skrotU8);
        form.AddField("skrotU9", skrotU9);
        form.AddField("skrotU10", skrotU10);
        form.AddField("skrotU11", skrotU11);
        form.AddField("skrotU12", skrotU12);
        form.AddField("skrotU13", skrotU13);
        form.AddField("skrotU14", skrotU14);
        form.AddField("skrotU15", skrotU15);
        form.AddField("skrotU16", skrotU16);
        form.AddField("skrotU17", skrotU17);
        form.AddField("skrotU18", skrotU18);
        form.AddField("skrotU19", skrotU19);
        form.AddField("skrotU20", skrotU20);
        form.AddField("skrotU21", skrotU21);
        form.AddField("skrotU22", skrotU22);
        form.AddField("skrotU23", skrotU23);
        form.AddField("skrotU24", skrotU24);
        form.AddField("skrotU25", skrotU25);
        form.AddField("skrotU26", skrotU26);
        form.AddField("skrotU27", skrotU27);
        form.AddField("skrotU28", skrotU28);
        form.AddField("skrotU29", skrotU29);
        form.AddField("skrotU30", skrotU30);
        form.AddField("skrotU31", skrotU31);
        form.AddField("skrotU32", skrotU32);
        form.AddField("skrotU33", skrotU33);
        form.AddField("skrotU34", skrotU34);
        form.AddField("skrotU35", skrotU35);
        form.AddField("skrotU36", skrotU36);
        form.AddField("skrotU37", skrotU37);
        form.AddField("skrotU38", skrotU38);
        form.AddField("skrotU39", skrotU39);
        form.AddField("skrotU40", skrotU40);

        using (UnityWebRequest www = UnityWebRequest.Post("http://summon.ieti.pl/dbSummOn/DodajKlase.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("DodajKlase:"+www.downloadHandler.text);
                if (www.downloadHandler.text == "false")
                {

                }
                else
                {
                    //kodUczniaInput.text = www.downloadHandler.text;
                    //klasaPanel.SetActive(true);
                }
            }
            //Debug.Log("DodajKlase2:" + www.downloadHandler.text);
        }
        List<string> tmpNazwa = new List<string>();
        //if (nazwaKlasyInput.text != "")
        {
            //tmpNazwa.Add(nazwaKlasyInput.text);
            tmpNazwa.Add(skrotKlasyPass);
            listaKlas.AddOptions(tmpNazwa);
        }

        listaKlas.value = listaKlas.options.FindIndex(option => option.text == skrotKlasyPass);
        
        WyswietlKodyKlasy();
        nazwaKlasyInput.text = "";
        workingPanel.SetActive(false);
        
    }

    IEnumerator KodyKlasWebClick(string nauczycielPass, string aktywnaKlasa)
    {
        string doBufora;
        workingPanel.SetActive(true);
        WWWForm form = new WWWForm();
        string[] strArr;
        
        form.AddField("nauczycielPass", nauczycielPass);
        form.AddField("aktywnaKlasa", aktywnaKlasa);

        using (UnityWebRequest www = UnityWebRequest.Post("http://summon.ieti.pl/dbSummOn/KodyKlasy2.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("KodyKlasy:"+ www.downloadHandler.text);
                if (www.downloadHandler.text == "false")
                {

                }
                else
                {
                    //Debug.Log("KodyKlasy:" + www.downloadHandler.text);
                    strArr = www.downloadHandler.text.ToString().Split(' ');

                    //nrSzkoly = strArr[0];
                    //nazwaSzkolyInput.text = strArr[1];
                    for (int i = 1; i <= 20; ++i)
                    {
                        //indeksyKlas.Add(Convert.ToInt32(reader["id"]));                        
                        // klasy.Add(new KlasaStruct(Convert.ToInt32(reader["id"]), reader["nazwa"].ToString()));

                        // if (nrAktywejKlasy - 1 == listaKlas.value)
                        //{
                        //Debug.Log("i:" + i);
                        //Debug.Log(listaKlas.value);

                        kodyKlas1.text = "";
                        kodyKlas2.text = "";
                        kodyKlas3.text = "";
                        kodyKlas4.text = "";
                        for (int j = 1; j <= 10; ++j)
                        {
                            //Debug.Log("J:" + j);
                            //Debug.Log(strArr[j-1].ToString());
                            //Debug.Log(strArr[j +10- 1].ToString());
                            //Debug.Log(strArr[j +20- 1].ToString());
                            //Debug.Log(strArr[j +30- 1].ToString());
                            /*if (strArr[j-1] != "0")
                                kodyKlas1.text += "<color=red>" + strArr[j-1].ToString() + "</color> \n";//todo red/green if uczen zalogowany
                            if (strArr[j+10-1] != "0")
                                kodyKlas2.text += "<color=red>" + strArr[j+10-1].ToString()  + "</color> \n";//todo red/green if uczen zalogowany
                            if (strArr[j+20-1] != "0")
                                kodyKlas3.text += "<color=red>" + strArr[j+20-1].ToString()  + "</color> \n";//todo red/green if uczen zalogowany
                            if (strArr[j+30-1] != "0")
                                kodyKlas4.text += "<color=red>" + strArr[j+30-1].ToString()  + "</color> \n";//todo red/green if uczen zalogowany*/
                            if (strArr[j - 1] != "0")
                                kodyKlas1.text += strArr[j - 1].ToString();//todo red/green if uczen zalogowany

                            if (strArr[j + 10 - 1] != "0")
                                kodyKlas2.text += strArr[j + 10 - 1].ToString();//todo red/green if uczen zalogowany
                            if (strArr[j + 20 - 1] != "0")
                                kodyKlas3.text += strArr[j + 20 - 1].ToString();//todo red/green if uczen zalogowany
                            if (strArr[j + 30 - 1] != "0")
                                kodyKlas4.text += strArr[j + 30 - 1].ToString();//todo red/green if uczen zalogowany

                        }
                        //}
                    }
                    //nazwa bêdzie rekord
                    doBufora = kodyKlas1.text + "\n" + kodyKlas2.text + "\n" + kodyKlas3.text + "\n" + kodyKlas4.text + "\n";
                    doBufora = doBufora.Replace("<color=red>", "");
                    doBufora = doBufora.Replace("<color=green>", "");
                    doBufora = doBufora.Replace("</color>", "");
                    GUIUtility.systemCopyBuffer = doBufora.ToString();
                }
            }
		}
		workingPanel.SetActive(false);
    }

    IEnumerator WyslijKodUczniaWebClick(string nauczycielPass, string skrotUcznia)
    {
        workingPanel.SetActive(true);
        WWWForm form = new WWWForm();
        //string[] strArr;
        form.AddField("nauczycielPass", nauczycielPass);
        form.AddField("skrotUcznia", skrotUcznia);

        //UnityWebRequest www = UnityWebRequest.Post("http://summon.ieti.pl/GetNauczycielCount.php");
        using (UnityWebRequest www = UnityWebRequest.Post("http://summon.ieti.pl/dbSummOn/WyslijKodUcznia.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text == "false")
                    kodUczniaInput.text = kodZajety;
                else
                {

                }
                //byte[] results = www.downloadHandler.data;
            }
        }
        workingPanel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //changeBackground();
        userIDText.text = SkinManager.instance.UserID;
        //nauczycielBtn.SetActive(true);
        //uczenBtn.SetActive(true);
        SystemLanguage iLang = Application.systemLanguage;
        switch (iLang)
        {
            case SystemLanguage.English:
                nazwaSzkolyText.text = SkinManager.MENU_EN[SkinManager.NAZWA_SZKOLY];
                nazwaKlasyText.text = SkinManager.MENU_EN[SkinManager.NAZWA_KLASY];
                iloscUczniowText.text = SkinManager.MENU_EN[SkinManager.ILOSC_UCZNIOW];
                kodUczniaText.text = SkinManager.MENU_EN[SkinManager.KOD_UCZNIA];
                wiekUczniowText.text = SkinManager.MENU_EN[SkinManager.WIEK_UCZNIA];
                kodZajety = SkinManager.MENU_EN[SkinManager.KOD_ZAJETY];
                break;
            case SystemLanguage.Polish:
                nazwaSzkolyText.text = SkinManager.MENU_PL[SkinManager.NAZWA_SZKOLY];
                nazwaKlasyText.text = SkinManager.MENU_PL[SkinManager.NAZWA_KLASY];
                iloscUczniowText.text = SkinManager.MENU_PL[SkinManager.ILOSC_UCZNIOW];
                kodUczniaText.text = SkinManager.MENU_PL[SkinManager.KOD_UCZNIA];
                wiekUczniowText.text = SkinManager.MENU_PL[SkinManager.WIEK_UCZNIA];
                kodZajety = SkinManager.MENU_PL[SkinManager.KOD_ZAJETY];
                break;
            default:
                nazwaSzkolyText.text = SkinManager.MENU_EN[SkinManager.NAZWA_SZKOLY];
                nazwaKlasyText.text = SkinManager.MENU_EN[SkinManager.NAZWA_KLASY];
                iloscUczniowText.text = SkinManager.MENU_EN[SkinManager.ILOSC_UCZNIOW];
                kodUczniaText.text = SkinManager.MENU_EN[SkinManager.KOD_UCZNIA];
                wiekUczniowText.text = SkinManager.MENU_EN[SkinManager.WIEK_UCZNIA];
                kodZajety = SkinManager.MENU_EN[SkinManager.KOD_ZAJETY];
                break;
        }

        
     /*
         string connStr = "server=localhost;user=root;database=world;port=3306;password=******";
         MySqlConnection conn = new MySqlConnection(connStr);
         try
         {
             Console.WriteLine("Connecting to MySQL...");
             conn.Open();

             string sql = "SELECT COUNT(*) FROM Country";
             MySqlCommand cmd = new MySqlCommand(sql, conn);
             object result = cmd.ExecuteScalar();
             if (result != null)
             {
                 int r = Convert.ToInt32(result);
                 Console.WriteLine("Number of countries in the world database is: " + r);
             }

         }
         catch (Exception ex)
         {
             Console.WriteLine(ex.ToString());
         }

         conn.Close();
         Console.WriteLine("Done.");*/
     
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NauczycielClick()
    {
        szkolaPanel.SetActive(true);
        iloscUczniowValue.text = ileUczniowSlider.value.ToString();
        wiekUczniowValue.text = wiekUczniowSlider.value.ToString();
        //MySqlConnection conn = new MySqlConnection(connStr);
        /*try
        {
            conn.Open();
            string sql = "SELECT * FROM `nauczyciel` WHERE `identyfikator` LIKE '" + SkinManager.instance.UserID + "';";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            object result = cmd.ExecuteScalar();

            if (result != null)
            {
                czyNauczycielIstnieje = true;
            }
            else
            {
                czyNauczycielIstnieje = false;
            }

        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }

        conn.Close();*/
        StartCoroutine(NauczycielWebClick(SkinManager.instance.UserID));
        //StartCoroutine(NauczycielWebClick(""));
        

        /*if (czyNauczycielIstnieje)//szko³a istnieje, nauczyciel dodany, dodawane klas aktywne
        {
            //czytaj nr szko³y
            try
            {
                conn.Open();
                string sql = "SELECT * FROM `nauczyciel` WHERE `identyfikator` LIKE '" + SkinManager.instance.UserID + "';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                System.Data.IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    nrSzkoly = Convert.ToInt32(reader["szkola"]);
                }
                //skrotSzkolyText.text = nrSzkoly.ToString();
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }

            conn.Close();

            //czytaj nazwe  skrot szkoly
            try
            {
                conn.Open();
                string sql = "SELECT * FROM `szkola` WHERE `id` LIKE '" + nrSzkoly.ToString() + "';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                System.Data.IDataReader reader = cmd.ExecuteReader();
                listaKlas.ClearOptions();
                List<string> tmpNazwyKlas = new List<string>();
                while (reader.Read())
                {
                    nazwaSzkoly = reader["nazwa"].ToString();
                    skrotSzkoly = reader["skrot"].ToString();
                    for (int i=1; i <= 20; ++i)
                    {
                        if (reader["kl"+i.ToString()].ToString() != "")
                        {
                            tmpNazwyKlas.Add(reader["kl" + i.ToString()].ToString());                            
                        }
                    }
                    //listaKlas.AddOptions(tmpNazwyKlas);
                }
                skrotSzkolyText.text = skrotSzkoly;
                nazwaSzkolyInput.text = nazwaSzkoly;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }

            conn.Close();

            klasaPanel.SetActive(true);
            dodajSzkoleBtn.gameObject.SetActive(false);

            //sprawdŸ klasy
            try
            {
                int tmpNrKlasy = 0;
                Dropdown.OptionData tmpDropdown = new Dropdown.OptionData();
                List<string> tmpNazwaKl = new List<string>();
                

                klasy.Clear();
                indeksyKlas.Clear();
                listaKlas.options.Clear();
                conn.Open();
                string sql = "SELECT * FROM `klasa` WHERE `szkola` LIKE '" + nrSzkoly.ToString() + "';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                System.Data.IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tmpDropdown.text = reader["nazwa"].ToString();
                    //listaKlas.options.Add(tmpDropdown);
                    tmpNazwaKl.Add(reader["nazwa"].ToString());
                    

                    for (int i = 1; i <= 20; ++i)
                    {
                        indeksyKlas.Add(Convert.ToInt32(reader["id"]));                        
                        klasy.Add(new KlasaStruct(Convert.ToInt32(reader["id"]), reader["nazwa"].ToString()));
                   
                    }
                    //nazwa bêdzie rekord
                }

                listaKlas.AddOptions(tmpNazwaKl);
                listaKlas.value = 0;
                WyswietlKodyKlasy();
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }

            conn.Close();
        }
        else
        {
            dodajSzkoleBtn.gameObject.SetActive(true);
            klasaPanel.SetActive(false);
        }*/

        nauczycielBtn.gameObject.SetActive(false);
        uczenBtn.gameObject.SetActive(false);
        uczenPanel.SetActive(false);
        
    }

    public void onSliderChanged()
    {
        iloscUczniowValue.text = ileUczniowSlider.value.ToString();
    }

    public void onSliderWiekChanged()
    {
        wiekUczniowValue.text = wiekUczniowSlider.value.ToString();
    }
    public void UczenClick()
    {
        nauczycielBtn.gameObject.SetActive(false);
        uczenBtn.gameObject.SetActive(false);
        szkolaPanel.SetActive(false);
        uczenPanel.SetActive(true);
        StartCoroutine(UczenWebClick(SkinManager.instance.UserID));
      /*  //debugInfo.text += connStr + "__";
        //Debug.Log("Przed");
        try
        {
            MySqlConnection conn2 = new MySqlConnection(connStr);
            Debug.Log("TworzeSQL");
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            debugInfo.text += ex.ToString() + "__";

        }

        MySqlConnection conn = new MySqlConnection(connStr);
        Debug.Log("Stworzone");
        
        //int ktoraKlasa = listaKlas.value;
        try
        {
            conn.Open();
            string sql = "SELECT * FROM `uczen` WHERE `identyfikator` LIKE '" + SkinManager.instance.UserID.ToString() + "';";
            debugInfo.text +=  "oppened__"; 
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            System.Data.IDataReader reader = cmd.ExecuteReader();
            

            while (reader.Read())
            {
                debugInfo.text += "whileIN__"; 
                kodUczniaInput.text = reader["skrot"].ToString();
                debugInfo.text += kodUczniaInput.text + "KodU__"; 
                //debugInfo.text += kodUczniaInput.text + "__"; 
            }

        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            debugInfo.text += ex.ToString() + "__"; 
        }

        conn.Close();
        debugInfo.text += "closed__"; */
    }

    public void DodajSzkole()
    {
        

       /* MySqlConnection conn = new MySqlConnection(connStr);
        
        {
            conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                string sql = "SELECT COUNT(*) FROM `szkola`";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    nrSzkoly = Convert.ToInt32(result);
                }

            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }

            conn.Close();

            conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
             //   string sql = "INSERT INTO `szkola` (`id`, `nazwa`, `kl1`, `kl2`, `kl3`, `kl4`, `kl5`, `kl6`, `kl7`, `kl8`, `kl9`, `kl10`, `kl11`, `kl12`, `kl13`, `kl14`, `kl15`, `kl16`, `kl17`, `kl18`, `kl19`, `kl20`, `skrot`) " +
             //       "VALUES ('"+nrSzkoly.ToString()+"', '"+ nazwaSzkolyInput.text + "', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '"+ skrotSzkolyText.text + "');";

                string sql = "INSERT INTO `szkola` (`id`, `nazwa`, `kl1`, `kl2`, `kl3`, `kl4`, `kl5`, `kl6`, `kl7`, `kl8`, `kl9`, `kl10`," +
                    " `kl11`, `kl12`, `kl13`, `kl14`, `kl15`, `kl16`, `kl17`, `kl18`, `kl19`, `kl20`, `skrot`," +
                    " `kl1nazwa`, `kl2nazwa`, `kl3nazwa`, `kl4nazwa`, `kl5nazwa`, `kl6nazwa`, `kl7nazwa`, `kl8nazwa`, `kl9nazwa`, `kl10nazwa`, " +
                    "`kl11nazwa`, `kl12nazwa`, `kl13nazwa`, `kl14nazwa`, `kl15nazwa`, `kl16nazwa`, `kl17nazwa`, `kl18nazwa`, `kl19nazwa`, `kl20nazwa`) " +
                    "VALUES('" + nrSzkoly.ToString() + "', '" + nazwaSzkolyInput.text + "', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', " +
                    "'" + skrotSzkolyText.text + "', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '');";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                   // nrSzkoly = Convert.ToInt32(result);
                }

            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }

            conn.Close();
     

            conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                string sql = "SELECT COUNT(*) FROM `nauczyciel`";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    nrNauczyciela = Convert.ToInt32(result);
                }

            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }

            conn.Close();

            conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                string sql = "INSERT INTO `nauczyciel` (`id`, `identyfikator`, `szkola`) " +
                    "VALUES('"+nrNauczyciela.ToString()+"', '" + SkinManager.instance.UserID + "', '"+nrSzkoly.ToString()+"');";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    //nrNauczyciela = Convert.ToInt32(result);
                }

            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }

            conn.Close();*/
            //activeAddClass = true;
            StartCoroutine(DodajSzkoleWebClick(nazwaSzkolyInput.text, skrotSzkolyText.text, SkinManager.instance.UserID));
            klasaPanel.SetActive(true);
 
    }

    public void DodajKlase()
    {
        //List<int> nowiUczniowieID = new List<int>();

       /* //workingPanel.SetActive(true);
        //nrKlasy
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();

            string sql2 = "SELECT COUNT(*) FROM `klasa`";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            object result = cmd2.ExecuteScalar();

            if (result != null)
            {
                nrKlasy = Convert.ToInt32(result);
            }

        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }

        conn.Close();

        List<string> tmpSkrot = new List<string>();
        for (int i = 1; i <= 40; ++i)
        {
            tmpSkrot.Add(SkinManager.Hash(System.DateTime.Now.ToString() + UnityEngine.Random.Range(0.0f, 1000.0f)));
        }
        //dodaj klase
        conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();
            //string sql = "SELECT * FROM `klasa` WHERE `szkola` LIKE '" + nrSzkoly.ToString() + "';";
            string sql3;

            sql3 = "INSERT INTO `klasa` (`id`, `szkola`, `ucz1`, `ucz2`, `ucz3`, `ucz4`, `ucz5`, `ucz6`, `ucz7`, `ucz8`, `ucz9`, `ucz10`, `ucz11`, `ucz12`, `ucz13`, `ucz14`, `ucz15`, `ucz16`, `ucz17`, `ucz18`, `ucz19`, `ucz20`, `ucz21`, `ucz22`, `ucz23`, `ucz24`, `ucz25`, `ucz26`, `ucz27`, `ucz28`, `ucz29`, `ucz30`, `ucz31`, `ucz32`, `ucz33`, `ucz34`, `ucz35`, `ucz36`, `ucz37`, `ucz38`, `ucz39`, `ucz40`, `nazwa`, `u1kod`, `u2kod`, `u3kod`, `u4kod`, `u5kod`, `u6kod`, `u7kod`, `u8kod`, `u9kod`, `u10kod`, `u11kod`, `u12kod`, `u13kod`, `u14kod`, `u15kod`, `u16kod`, `u17kod`, `u18kod`, `u19kod`, `u20kod`, `u21kod`, `u22kod`, `u23kod`, `u24kod`, `u25kod`, `u26kod`, `u27kod`, `u28kod`, `u29kod`, `u30kod`, `u31kod`, `u32kod`, `u33kod`, `u34kod`, `u35kod`, `u36kod`, `u37kod`, `u38kod`, `u39kod`, `u40kod`, `wiek`)"+
                " VALUES ('" + nrKlasy.ToString() + "', '" + nrSzkoly.ToString() + "', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '" + nazwaKlasyInput.text + "', '" + tmpSkrot[0] + "', '" + tmpSkrot[1] + "', '" + tmpSkrot[2] + "', '" + tmpSkrot[3] + "', '" + tmpSkrot[4] + "', '" + tmpSkrot[5] + "', '" + tmpSkrot[6] + "', '" + tmpSkrot[7] + "', '" + tmpSkrot[8] + "', '" + tmpSkrot[9] + "', '" + tmpSkrot[10] + "', '" + tmpSkrot[11] + "', '" + tmpSkrot[12] + "', '" + tmpSkrot[13] + "', '" + tmpSkrot[14] + "', '" + tmpSkrot[15] + "', '" + tmpSkrot[16] + "', '" + tmpSkrot[17] + "', '" + tmpSkrot[18] + "', '" + tmpSkrot[19] + "', '" + tmpSkrot[20] + "', '" + tmpSkrot[21] + "', '" + tmpSkrot[22] + "', '" + tmpSkrot[23] + "', '" + tmpSkrot[24] + "', '" + tmpSkrot[25] + "', '" + tmpSkrot[26] + "', '" + tmpSkrot[27] + "', '" + tmpSkrot[28] + "', '" + tmpSkrot[29] + "', '" + tmpSkrot[30] + "', '" + tmpSkrot[31] + "', '" + tmpSkrot[32] + "', '" + tmpSkrot[33] + "', '" + tmpSkrot[34] + "', '" + tmpSkrot[35] + "', '" + tmpSkrot[36] + "', '" + tmpSkrot[37] + "', '" + tmpSkrot[38] + "', '" + tmpSkrot[39] + "', '" + wiekUczniowValue.text + "');";
             MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
            System.Data.IDataReader reader = cmd3.ExecuteReader();

        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }

        conn.Close();
        //stworz uczniow

        //nrUcznia

         conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();

            string sql4 = "SELECT COUNT(*) FROM `uczen`";
            MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
            object result = cmd4.ExecuteScalar();

            if (result != null)
            {
                nrUcznia = Convert.ToInt32(result);
            }

        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }

        conn.Close();

        conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();

            string sql4 = "SELECT COUNT(*) FROM `jos_djl_teams`";
            MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
            object result = cmd4.ExecuteScalar();

            if (result != null)
            {
                nrUczniaTeams = Convert.ToInt32(result);
            }

        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }

        conn.Close();


        int Licznik = 0;
        int tmpNrUcznia = nrUcznia-1;
        int tmpNrUczniaTeams = nrUczniaTeams;
        
       for (int i=1; i<=ileUczniowSlider.value; ++i)
        {
            Licznik = i;
            //tmpNrUcznia = nrUcznia + Licznik - 1;
            tmpNrUcznia++;
            tmpNrUczniaTeams++;
            try
            {
                conn.Open();
                string sql5 = "INSERT INTO `uczen` (`id`, `klasa`, `identyfikator`, `team_nr`, `parametry`, `skrot`) " +
                    "VALUES('" + tmpNrUcznia.ToString() + "', '"+nrKlasy.ToString()+ "', '', '"+tmpNrUczniaTeams.ToString()+"', '', '"+tmpSkrot[Licznik-1]+"');";
                MySqlCommand cmd5 = new MySqlCommand(sql5, conn);
                System.Data.IDataReader reader = cmd5.ExecuteReader();
                //nrUcznia++;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }

            conn.Close();

            try
            {
                conn.Open();

                string sql5 = "UPDATE `klasa` SET `ucz" + Licznik.ToString() + "` = '" + tmpNrUcznia.ToString() + "' WHERE `klasa`.`id` = " + nrKlasy.ToString() + ";";
                MySqlCommand cmd5 = new MySqlCommand(sql5, conn);
                System.Data.IDataReader reader = cmd5.ExecuteReader();
                
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }

            conn.Close();

            try
            {
                conn.Open();
                nrUczniaTeams++;
                string nazwaUcznia = skrotSzkoly + "/" + nazwaKlasyInput.text + "/" + Licznik.ToString();
                string sql5 = "INSERT INTO `jos_djl_teams` (`id`, `name`, `alias`, `logo`, `city`, `venue`, `checked_out`, `checked_out_time`, `created`, `created_by`, `params`) " +
                  //  "VALUES('"+nrUczniaTeams.ToString()+"', "+nazwaUcznia+"', '', '', '', '', '0', '0000-00-00 00:00:00.000000', '0000-00-00 00:00:00.000000', '', NULL);";
                    "VALUES('" + nrUczniaTeams.ToString() + "', '" + nazwaUcznia + "', '', '', '', '', '0', '', '', '', '');";
                MySqlCommand cmd5 = new MySqlCommand(sql5, conn);
                System.Data.IDataReader reader = cmd5.ExecuteReader();
                
               
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }

            conn.Close();

            
        }//endfor ileUczniowSlider

        //workingPanel.SetActive(false);*/
        List<string> tmpSkrot = new List<string>();
        for (int i = 1; i <= 40; ++i)
        {
            tmpSkrot.Add(SkinManager.Hash(System.DateTime.Now.ToString() + UnityEngine.Random.Range(0.0f, 1000.0f)));
        }
        //skrotSzkolyText.text
        StartCoroutine(DodajKlaseWebClick(SkinManager.instance.UserID, skrotSzkolyText.text, nazwaKlasyInput.text, wiekUczniowValue.text, ileUczniowSlider.value.ToString(), tmpSkrot[0], tmpSkrot[1], tmpSkrot[2], tmpSkrot[3], tmpSkrot[4], tmpSkrot[5], tmpSkrot[6], tmpSkrot[7], tmpSkrot[8], tmpSkrot[9], tmpSkrot[10], tmpSkrot[11], tmpSkrot[12], tmpSkrot[13], tmpSkrot[14], tmpSkrot[15], tmpSkrot[16], tmpSkrot[17], tmpSkrot[18], tmpSkrot[19], tmpSkrot[20], tmpSkrot[21], tmpSkrot[22], tmpSkrot[23], tmpSkrot[24], tmpSkrot[25], tmpSkrot[26], tmpSkrot[27], tmpSkrot[28], tmpSkrot[29], tmpSkrot[30], tmpSkrot[31], tmpSkrot[32], tmpSkrot[33], tmpSkrot[34], tmpSkrot[35], tmpSkrot[36], tmpSkrot[37], tmpSkrot[38], tmpSkrot[39]));

        /*List<string> tmpNazwa = new List<string>();
        tmpNazwa.Add(nazwaKlasyInput.text);
        listaKlas.AddOptions(tmpNazwa);
        
        kodyKlas1.text = "";
        kodyKlas2.text = "";
        kodyKlas3.text = "";
        kodyKlas4.text = "";
        //kodyKlas1Input.text = "";
        for (int j = 1; j <= 10; ++j) 
        {
            if (j <= ileUczniowSlider.value)
            {
                kodyKlas1.text += "<color=red>" + tmpSkrot[j - 1].ToString() + "</color> \n";//todo red/green if uczen zalogowany
            }
            if (j + 10 <= ileUczniowSlider.value)
                kodyKlas2.text += "<color=red>" + tmpSkrot[j+10-1].ToString() + "</color> \n";//todo red/green if uczen zalogowany
            if (j + 20 <= ileUczniowSlider.value)
                kodyKlas3.text += "<color=red>" + tmpSkrot[j + 20 - 1].ToString() + "</color> \n";//todo red/green if uczen zalogowany
            if (j + 30 <= ileUczniowSlider.value)
                kodyKlas4.text += "<color=red>" + tmpSkrot[j + 30 - 1].ToString() + "</color> \n";//todo red/green if uczen zalogowany
        }*/
        listaKlas.value = listaKlas.options.FindIndex(option => option.text == nazwaKlasyInput.text);
        //listaKlas.value = listAvailableStrings.IndexOf(nazwaKlasyInput.text);
        nazwaKlasyInput.text = "";
        WyswietlKodyKlasy();

        //kodyKlas1Input.text = kodyKlas1.text;
        //kodyKlas1Input.text += kodyKlas1.text.ToString();
    }

    public void WyswietlKodyKlasy()
    {
        //int nrAktywejKlasy = 0;// listaKlas.value;
        //string doBufora = "";
        StartCoroutine(KodyKlasWebClick(SkinManager.instance.UserID, listaKlas.value.ToString()));
        /*MySqlConnection conn = new MySqlConnection(connStr);
        //int ktoraKlasa = listaKlas.value;
        try
        {
        conn.Open();
                string sql = "SELECT * FROM `klasa` WHERE `szkola` LIKE '" + nrSzkoly.ToString() + "';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                System.Data.IDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    nrAktywejKlasy++;
                    //tmpDropdown.text = reader["nazwa"].ToString();
                    //listaKlas.options.Add(tmpDropdown);
                    //tmpNazwaKl.Add(reader["nazwa"].ToString());
                    

                    for (int i = 1; i <= 20; ++i)
                    {
                        indeksyKlas.Add(Convert.ToInt32(reader["id"]));                        
                        klasy.Add(new KlasaStruct(Convert.ToInt32(reader["id"]), reader["nazwa"].ToString()));
                        //, reader["u"+i.ToString()+"kod"].ToString()
                        //Debug.Log("kod nr" + i + ":" + Convert.ToInt32(reader["ucz1"]) + ":" + reader["u" + i.ToString() + "kod"].ToString());

                        if (nrAktywejKlasy - 1 == listaKlas.value)
                        {
                            //Debug.Log("i:" + i);
                            //Debug.Log(listaKlas.value);

                            kodyKlas1.text = "";
                            kodyKlas2.text = "";
                            kodyKlas3.text = "";
                            kodyKlas4.text = "";
                            for (int j = 1; j <= 10; ++j)
                            {
                                if (Convert.ToInt32(reader["ucz" + j.ToString()]) != 0)
                                    kodyKlas1.text += "<color=red>" + reader["u" + j.ToString() + "kod"].ToString() + "</color> \n";//todo red/green if uczen zalogowany
                                if (Convert.ToInt32(reader["ucz" + (j+10).ToString()]) != 0)
                                    kodyKlas2.text += "<color=red>" + reader["u" + (j+10).ToString() + "kod"].ToString() + "</color> \n";//todo red/green if uczen zalogowany
                                if (Convert.ToInt32(reader["ucz" + (j + 20).ToString()]) != 0)
                                    kodyKlas3.text += "<color=red>" + reader["u" + (j + 20).ToString() + "kod"].ToString() + "</color> \n";//todo red/green if uczen zalogowany
                                if (Convert.ToInt32(reader["ucz" + (j + 30).ToString()]) != 0)
                                    kodyKlas4.text += "<color=red>" + reader["u" + (j + 30).ToString() + "kod"].ToString() + "</color> \n";//todo red/green if uczen zalogowany
                            }
                        }
                    }
                    //nazwa bêdzie rekord
                }

                //listaKlas.AddOptions(tmpNazwaKl);
                //listaKlas.value = 0;
                
                    doBufora = kodyKlas1.text + "\n" + kodyKlas2.text + "\n" + kodyKlas3.text + "\n" + kodyKlas4.text + "\n";
                    doBufora = doBufora.Replace("<color=red>", "");
                    doBufora = doBufora.Replace("</color>", "");
                    GUIUtility.systemCopyBuffer = doBufora.ToString();
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }

            conn.Close();
            //Debug.Log("pokazKody");*/
    }
    public void DodajUcznia()
    {

    }

    public void ConnectDatabase()
    {
        //string connStr = "server=localhost;user=root;database=world;port=3306;password=******";
      /*  string connStr = "server=s69.cyber-folks.pl;user=kolacz_zdalny;database=kolacz_jos1;port=3306;password=";
        
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");
            Debug.Log("Connecting to MySQL...");
            conn.Open();

            string sql = "SELECT COUNT(*) FROM jos_djl_games";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                int r = Convert.ToInt32(result);
                Console.WriteLine("Number of countries in the world database is: " + r);
                Debug.Log("Number of games in the world database is: " + r);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Debug.Log(ex.ToString());
        }

        conn.Close();
        Console.WriteLine("Done.");
        Debug.Log("Done");*/
    }

    public void WyslijKodUcznia()
    {
        Debug.Log(kodUczniaInput.text.ToString());
        StartCoroutine(WyslijKodUczniaWebClick(SkinManager.instance.UserID, kodUczniaInput.text.ToString()));
       /* //kodUczniaInput.text="";
        bool czyJest = false;
        int nrUczniaRejestracja = 0;
        int nrUczniaDlaTeam = 0;
        int nrWTabeli = 0;
        string leagueParameter = "";
        string uczenZajety = "";
       // string leagueParameterBefore = "";
       // string leagueParameterAfter = "";

        MySqlConnection conn = new MySqlConnection(connStr);
        //int ktoraKlasa = listaKlas.value;
        try
        {
            conn.Open();
            string sql = "SELECT * FROM `uczen` WHERE `skrot` LIKE '" + kodUczniaInput.text.ToString() + "';";
            //string sql = "UPDATE `uczen` SET `identyfikator` = 'aaa' WHERE `uczen`.`id` = 1;";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            System.Data.IDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                //Convert.ToInt32(reader["id"]), reader["nazwa"].ToString()
                nrUczniaRejestracja = Convert.ToInt32(reader["id"]);
                nrUczniaDlaTeam = Convert.ToInt32(reader["team_nr"]);
                uczenZajety = reader["identyfikator"].ToString();
                czyJest = true;
            }
            debugInfo.text += czyJest.ToString() + "__"; 

        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            debugInfo.text += ex.ToString() + "__"; 
        }

        conn.Close();

        if (!czyJest)
        {
            kodUczniaInput.text = "";
            return;
        }

        if (uczenZajety.Length <= 1)
        {
            conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                //string sql = "SELECT * FROM `uczen` WHERE `skrot` LIKE '" + kodUczniaInput.text.ToString() + "';";
                string sql = "UPDATE `uczen` SET `identyfikator` = '" + SkinManager.instance.UserID.ToString() + "' WHERE `uczen`.`id` = " + nrUczniaRejestracja.ToString() + ";";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                System.Data.IDataReader reader = cmd.ExecuteReader();

                // while (reader.Read())
                // {
                     //Convert.ToInt32(reader["id"]), reader["nazwa"].ToString()
                //     nrUczniaRejestracja = Convert.ToInt32(reader["id"]);
                //     Debug.Log("nr ucznia:" + nrUczniaRejestracja);
                //     czyJest = true;
                // }

            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
                debugInfo.text += ex.ToString() + "__"; 
            }

            conn.Close();


            conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = "SELECT * FROM `jos_djl_leagues` WHERE `id` LIKE '2';";
                //string sql = "UPDATE `jos_djl_leagues` SET `params` = '{\"teams\":\"4,1,3\",\"rounds\":\"0\",\"win\":\"3\",\"lose\":\"-3\",\"tie\":\"1\"}' WHERE `jos_djl_leagues`.`id` = 2;";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                System.Data.IDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    leagueParameter = reader["params"].ToString();
                }

            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
                debugInfo.text += ex.ToString() + "__"; 
            }

            conn.Close();


            conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();//","rounds
                leagueParameter = leagueParameter.Replace("\",\"rounds", "," + nrUczniaDlaTeam.ToString() + "\",\"rounds");
                //string sql = "SELECT * FROM `uczen` WHERE `skrot` LIKE '" + kodUczniaInput.text.ToString() + "';";
                string sql = "UPDATE `jos_djl_leagues` SET `params` = '" + leagueParameter.ToString() + "' WHERE `jos_djl_leagues`.`id` = 2;";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                System.Data.IDataReader reader = cmd.ExecuteReader();

                //while (reader.Read())
                {

                }

            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
                debugInfo.text += ex.ToString() + "__"; 
            }

            conn.Close();

            //teraz dodanie do tabeli wynikow
            conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();

            string sql = "SELECT COUNT(*) FROM `jos_djl_tables`";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            object result = cmd.ExecuteScalar();

            if (result != null)
            {
                nrWTabeli = Convert.ToInt32(result);
            }

        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            debugInfo.text += ex.ToString() + "__"; 
        }

        conn.Close();

            conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                //   string sql = "INSERT INTO `szkola` (`id`, `nazwa`, `kl1`, `kl2`, `kl3`, `kl4`, `kl5`, `kl6`, `kl7`, `kl8`, `kl9`, `kl10`, `kl11`, `kl12`, `kl13`, `kl14`, `kl15`, `kl16`, `kl17`, `kl18`, `kl19`, `kl20`, `skrot`) " +
                //       "VALUES ('"+nrSzkoly.ToString()+"', '"+ nazwaSzkolyInput.text + "', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '"+ skrotSzkolyText.text + "');";

                string sql = "INSERT INTO `jos_djl_tables` (`id`, `league_id`, `team_id`, `extra_points`, `ordering`) "+
                    "VALUES ('"+nrWTabeli.ToString()+"', '2', '"+nrUczniaDlaTeam.ToString()+"', '0', '0');";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    // nrSzkoly = Convert.ToInt32(result);
                }

            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
                debugInfo.text += ex.ToString() + "__"; 
            }

            conn.Close();

        }
        else
        {
            kodUczniaInput.text = kodZajety;
        }*/
    }

    public void Back()
    {
        if (nauczycielBtn.gameObject.activeSelf)
        {          
            SceneManager.LoadScene("Menu");
        }
        szkolaPanel.SetActive(false);
        uczenPanel.SetActive(false);
        nauczycielBtn.gameObject.SetActive(true);
        uczenBtn.gameObject.SetActive(true);
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
