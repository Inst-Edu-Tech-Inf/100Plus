using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour
{
    [Header("Game Settings"), SerializeField]
    public Slider sliderSound;
    public Slider sliderSFX;
    public Dropdown victoryList;
    // Start is called before the first frame update
    void Start()
    {
        sliderSFX.value = SkinManager.instance.ActiveSFXValue;
        sliderSound.value = SkinManager.instance.ActiveSoundValue;
        victoryList.value = SkinManager.instance.ActiveVictoryConditions;
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void VictoryConditionsChange()
    {
        SkinManager.instance.SetActiveVictoryConditions(victoryList.value);

        PlayerPrefs.SetInt("ActiveVictoryConditions", victoryList.value);  
        if (victoryList.value == 0)
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
            if (victoryList.value == 1)
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
                if (victoryList.value == 2)
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
                    if (victoryList.value == 3)//points
                    {
                        SkinManager.instance.SetIsVictoryTimePass(false);
                        SkinManager.instance.SetIsVictoryPointFirst(true);
                        SkinManager.instance.SetVictoryTimePassValue(0);
                        SkinManager.instance.SetVictoryPointFirstValue(20);
                        PlayerPrefs.SetInt("IsVictoryTimePass", false ? 1 : 0);
                        PlayerPrefs.SetInt("IsVictoryPointFirst", true ? 1 : 0);
                        PlayerPrefs.SetInt("VictoryTimePass", 0);
                        PlayerPrefs.SetInt("VictoryPointFirst", 20);
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
                        PlayerPrefs.SetInt("VictoryPointFirst", 100);
                    }
                }
            }
        }//victoryList.value==0

    }
}
