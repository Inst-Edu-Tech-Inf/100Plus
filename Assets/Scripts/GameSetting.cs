﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameSetting : MonoBehaviour
{
    [Header("Game Settings"), SerializeField]
    public Slider sliderSound;
    public Slider sliderSFX;
    public Dropdown victoryList;
    public Dropdown playerTurnList;
    public Image backgroundImage;
    // Start is called before the first frame update

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

    void Start()
    {
        try
        {
            Color color = new Color(0f / 255f, 255f / 255f, 0f / 255f);
            sliderSFX.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
            sliderSound.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
            //changeBackground();
            sliderSFX.value = SkinManager.instance.ActiveSFXValue;
            sliderSound.value = SkinManager.instance.ActiveSoundValue;
            victoryList.value = SkinManager.instance.ActiveVictoryConditions;
            playerTurnList.value = SkinManager.instance.ActivePlayerTurnConditions;
        }
        catch (System.Exception exception)
        {
            Debug.LogWarning(exception);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void changeBackground()
    {
       // backgroundImage.sprite = Resources.Load<Sprite>(SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name);

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

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SoundVolume()
    {
        SkinManager.instance.SetActiveSoundValue(sliderSound.value);
        PlayerPrefs.SetFloat("ActiveSoundValue", sliderSound.value);
    }

    public void SFXVolume()
    {
        SkinManager.instance.SetActiveSFXValue(sliderSFX.value); 
        PlayerPrefs.SetFloat("ActiveSFXValue", sliderSFX.value);
    }

    public void VictoryConditionsChange(int conditions)
    {
        print($"VictoryConditionsChange() called (conditions: {conditions}, PlayerPrefs: ({PlayerPrefs.GetInt("ActiveVictoryConditions")})");
        SkinManager.instance.SetActiveVictoryConditions(conditions);

        PlayerPrefs.SetInt("ActiveVictoryConditions", conditions);  
        if (conditions == 0)
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
        else
        {
            if (conditions == 1)
            {
                SkinManager.instance.SetIsVictoryTimePass(true);
                SkinManager.instance.SetIsVictoryPointFirst(false);
                SkinManager.instance.SetVictoryTimePassValue(15 * 60);
                SkinManager.instance.SetVictoryPointFirstValue(0);
                PlayerPrefs.SetInt("IsVictoryTimePass", true ? 1 : 0);
                PlayerPrefs.SetInt("IsVictoryPointFirst", false ? 1 : 0);
                PlayerPrefs.SetInt("VictoryTimePass", 15 * 60);
                PlayerPrefs.SetInt("VictoryPointFirst", 0);
            }
            else
            {
                if (conditions == 2)
                {
                    SkinManager.instance.SetIsVictoryTimePass(true);
                    SkinManager.instance.SetIsVictoryPointFirst(false);
                    SkinManager.instance.SetVictoryTimePassValue(30 * 60);
                    SkinManager.instance.SetVictoryPointFirstValue(0);
                    PlayerPrefs.SetInt("IsVictoryTimePass", true ? 1 : 0);
                    PlayerPrefs.SetInt("IsVictoryPointFirst", false ? 1 : 0);
                    PlayerPrefs.SetInt("VictoryTimePass", 30 * 60);
                    PlayerPrefs.SetInt("VictoryPointFirst", 0);
                }
                else
                {
                    if (conditions == 3)//points
                    {
                        SkinManager.instance.SetIsVictoryTimePass(false);
                        SkinManager.instance.SetIsVictoryPointFirst(true);
                        SkinManager.instance.SetVictoryTimePassValue(0);
                        SkinManager.instance.SetVictoryPointFirstValue(20);
                        PlayerPrefs.SetInt("IsVictoryTimePass", false ? 1 : 0);
                        PlayerPrefs.SetInt("IsVictoryPointFirst", true ? 1 : 0);
                        PlayerPrefs.SetInt("VictoryTimePass", 0);
                        PlayerPrefs.SetInt("VictoryPointFirst", SkinManager.VICTORY_CONDITIONS_20);
                    }
                    else
                    {
                        SkinManager.instance.SetIsVictoryTimePass(false);
                        SkinManager.instance.SetIsVictoryPointFirst(true);
                        SkinManager.instance.SetVictoryTimePassValue(0);
                        SkinManager.instance.SetVictoryPointFirstValue(100);
                        PlayerPrefs.SetInt("IsVictoryTimePass", false ? 1 : 0);
                        PlayerPrefs.SetInt("IsVictoryPointFirst", true ? 1 : 0);
                        PlayerPrefs.SetInt("VictoryTimePass", 0);
                        PlayerPrefs.SetInt("VictoryPointFirst", SkinManager.VICTORY_CONDITIONS_100);
                    }
                }
            }
        }//victoryList.value==0

    }

    public void PlayerTurnConditionsChange()
    {
        SkinManager.instance.SetActivePlayerTurnConditions(playerTurnList.value);

        PlayerPrefs.SetInt("ActivePlayerTurnConditions", playerTurnList.value);

        SkinManager.instance.SetActivePlayerEndTime(SkinManager.PLAYER_TURN[playerTurnList.value]);
        PlayerPrefs.SetInt("ActivePlayerEndTime", SkinManager.PLAYER_TURN[playerTurnList.value]);

       
       /* if (playerTurnList.value == 0)
        {
            SkinManager.instance.SetActivePlayerEndTime(0);
            PlayerPrefs.SetInt("ActivePlayerEndTime", 0);
        }
        else
        {
            if (playerTurnList.value == 1)
            {
                SkinManager.instance.SetActivePlayerEndTime(30);
                PlayerPrefs.SetInt("ActivePlayerEndTime", 30);
            }
            else
            {
                if (playerTurnList.value == 2)
                {
                    SkinManager.instance.SetActivePlayerEndTime(45);
                    PlayerPrefs.SetInt("ActivePlayerEndTime", 45);
                }
                else
                {
                    if (playerTurnList.value == 3)//points
                    {
                        SkinManager.instance.SetActivePlayerEndTime(60);
                        PlayerPrefs.SetInt("ActivePlayerEndTime", 60);
                    }
                    else
                    {
                        SkinManager.instance.SetActivePlayerEndTime(120);
                        PlayerPrefs.SetInt("ActivePlayerEndTime", 120);
                    }
                }
            }
        }//playerTurnList.value==0*/
        
    }
}
