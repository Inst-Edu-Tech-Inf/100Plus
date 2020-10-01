using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;


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
    string connStr = "server=s69.cyber-folks.pl;user=kolacz_zdalny;database=kolacz_jos1;port=3306;password=SummOn2020.";
    bool czyNauczycielIstnieje = false;
    int nrNauczyciela = 0;
    int nrSzkoly = 0;
    int nrKlasy = 0;
    int nrUcznia = 0;
    int nrUczniaTeams = 0;
    public List<KlasaStruct> klasy = new List<KlasaStruct>();
    public List<int> indeksyKlas = new List<int>();
    string nazwaSzkoly = "";
    string skrotSzkoly = "";

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
                break;
            case SystemLanguage.Polish:
                nazwaSzkolyText.text = SkinManager.MENU_PL[SkinManager.NAZWA_SZKOLY];
                nazwaKlasyText.text = SkinManager.MENU_PL[SkinManager.NAZWA_KLASY];
                iloscUczniowText.text = SkinManager.MENU_PL[SkinManager.ILOSC_UCZNIOW];
                kodUczniaText.text = SkinManager.MENU_PL[SkinManager.KOD_UCZNIA];
                wiekUczniowText.text = SkinManager.MENU_PL[SkinManager.WIEK_UCZNIA];
                break;
            default:
                nazwaSzkolyText.text = SkinManager.MENU_EN[SkinManager.NAZWA_SZKOLY];
                nazwaKlasyText.text = SkinManager.MENU_EN[SkinManager.NAZWA_KLASY];
                iloscUczniowText.text = SkinManager.MENU_EN[SkinManager.ILOSC_UCZNIOW];
                kodUczniaText.text = SkinManager.MENU_EN[SkinManager.KOD_UCZNIA];
                wiekUczniowText.text = SkinManager.MENU_EN[SkinManager.WIEK_UCZNIA];
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
        MySqlConnection conn = new MySqlConnection(connStr);
        try
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

        conn.Close();

        if (czyNauczycielIstnieje)//szko³a istnieje, nauczyciel dodany, dodawane klas aktywne
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
                        //, reader["u"+i.ToString()+"kod"].ToString()
                        /*if (i==1)
                        {
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
                        }*/
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
        }

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

        MySqlConnection conn = new MySqlConnection(connStr);
        //int ktoraKlasa = listaKlas.value;
        try
        {
            conn.Open();
            string sql = "SELECT * FROM `uczen` WHERE `identyfikator` LIKE '" + SkinManager.instance.UserID.ToString() + "';";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            System.Data.IDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                kodUczniaInput.text = reader["skrot"].ToString();                

            }

        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }

        conn.Close();
    }

    public void DodajSzkole()
    {
        

        MySqlConnection conn = new MySqlConnection(connStr);
        /*try
        {
            conn.Open();
            string sql = "SELECT * FROM `nauczyciel` WHERE `identyfikator` LIKE '"+ SkinManager.instance.UserID + "';";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            object result = cmd.ExecuteScalar();
            
            if (result != null)
            {
                //int r = Convert.ToInt32(result);
                Debug.Log(cmd);
                czyNauczycielIstnieje = true;
            }
            else
            {
                //Debug.Log("Nie znaleziony");
                czyNauczycielIstnieje = false;
            }

        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }

        conn.Close();*/

        //if (!czyNauczycielIstnieje)//dodaj nauczyciela
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

            conn.Close();
            //activeAddClass = true;
            klasaPanel.SetActive(true);
        }
        //else//wczytaj nauczyciela i uaktwynij dodawanie klas
        {
            /*
            string _name = "";
            string _description = "";
            int _offense = 0;
            int _defense = 0;
            int id = 1;
            string table = "map_block_type";
            dbconn.command.CommandText = string.Format("SELECT * FROM `{0}` WHERE `id` = {1} LIMIT 1;", table, id.ToString());
            System.Data.IDataReader reader = dbconn.command.ExecuteReader();
            while (reader.Read())
            {
                _name = reader["name"].ToString();
                _description = reader["description"].ToString();
                _offense = Convert.ToInt32(reader["offense"]);
                _defense = Convert.ToInt32(reader["defense"]);
                string[] textures = reader["textures"].ToString().Split('|');
                foreach (string t in textures)
                {
                    _textures.Add(t);
                }
                */
        }
        
        
    }

    public void DodajKlase()
    {
        List<int> nowiUczniowieID = new List<int>();

        workingPanel.SetActive(true);
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

        workingPanel.SetActive(false);

        List<string> tmpNazwa = new List<string>();
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
        }
        listaKlas.value = listaKlas.options.FindIndex(option => option.text == nazwaKlasyInput.text);
        //listaKlas.value = listAvailableStrings.IndexOf(nazwaKlasyInput.text);
        nazwaKlasyInput.text = "";

        //kodyKlas1Input.text = kodyKlas1.text;
        //kodyKlas1Input.text += kodyKlas1.text.ToString();
    }

    public void WyswietlKodyKlasy()
    {
        int nrAktywejKlasy = 0;// listaKlas.value;
        MySqlConnection conn = new MySqlConnection(connStr);
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
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }

            conn.Close();
            //Debug.Log("pokazKody");
    }
    public void DodajUcznia()
    {

    }

    public void ConnectDatabase()
    {
        //string connStr = "server=localhost;user=root;database=world;port=3306;password=******";
        string connStr = "server=s69.cyber-folks.pl;user=kolacz_zdalny;database=kolacz_jos1;port=3306;password=SummOn2020.";
        
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
        Debug.Log("Done");
    }

    public void WyslijKodUcznia()
    {
        //kodUczniaInput.text="";
        bool czyJest = false;
        int nrUczniaRejestracja = 0;
        int nrUczniaDlaTeam = 0;
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
                //uczenZajety = reader["identyfikator"].ToString();
                czyJest = true;
            }

        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }

        conn.Close();

       // if (!czyJest)
        //    kodUczniaInput.text = "";

        //if (uczenZajety.Length <= 1)
        {
            conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                //string sql = "SELECT * FROM `uczen` WHERE `skrot` LIKE '" + kodUczniaInput.text.ToString() + "';";
                string sql = "UPDATE `uczen` SET `identyfikator` = '" + SkinManager.instance.UserID.ToString() + "' WHERE `uczen`.`id` = " + nrUczniaRejestracja.ToString() + ";";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                System.Data.IDataReader reader = cmd.ExecuteReader();

                /* while (reader.Read())
                 {
                     //Convert.ToInt32(reader["id"]), reader["nazwa"].ToString()
                     nrUczniaRejestracja = Convert.ToInt32(reader["id"]);
                     Debug.Log("nr ucznia:" + nrUczniaRejestracja);
                     czyJest = true;
                 }*/

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
            }

            conn.Close();
        }
        //else
        //{
        //    kodUczniaInput.text = "!!";
        //}
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
