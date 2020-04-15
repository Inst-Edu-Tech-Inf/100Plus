//#define HTML5

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuButtons : MonoBehaviour
{
    void OnGUI()
    {
        const int ILE_ELEMENTOW_MENU = 6;
        const int SZEROKOSC_POLA = 250;
        int offset = 20;
        int szerokoscPrzycisku = 100;
        Scene scene;
        scene = SceneManager.GetActiveScene();

        //sprawdzenie czy aktualna scena to menu zeby nie wyswietlac menu w grze

        if (scene == SceneManager.GetSceneByName("Menu"))
        {
            GUIStyle style = new GUIStyle(GUI.skin.GetStyle("button"));
            style.fontSize = 50;
            szerokoscPrzycisku = Screen.height / (ILE_ELEMENTOW_MENU + 1);
            offset = szerokoscPrzycisku / ILE_ELEMENTOW_MENU;

            //wyswietlnie samych guzikow 

            if (GUI.Button(new Rect(Screen.width - SZEROKOSC_POLA, 0, SZEROKOSC_POLA, szerokoscPrzycisku), "Start", style))
            {
                SceneManager.LoadScene("Game");
            }
#if !HTML5 
            if (GUI.Button(new Rect(Screen.width - SZEROKOSC_POLA, szerokoscPrzycisku + offset, SZEROKOSC_POLA, szerokoscPrzycisku), "Multiplayer", style))
            {
                SceneManager.LoadScene("Multiplayer");
            }

            if (GUI.Button(new Rect(Screen.width - SZEROKOSC_POLA, 2 * (szerokoscPrzycisku + offset), SZEROKOSC_POLA, szerokoscPrzycisku), "Options", style))
            {
                //GUI.Slider()
                SceneManager.LoadScene("GameSetting");
            }

            if (GUI.Button(new Rect(Screen.width - SZEROKOSC_POLA, 3 * (szerokoscPrzycisku + offset), SZEROKOSC_POLA, szerokoscPrzycisku), "Skins", style))
            {
                SceneManager.LoadScene("Skins");
            }

            if (GUI.Button(new Rect(Screen.width - SZEROKOSC_POLA, 4 * (szerokoscPrzycisku + offset), SZEROKOSC_POLA, szerokoscPrzycisku), "About", style))
            {
                SceneManager.LoadScene("About");
            }
#endif
            if (GUI.Button(new Rect(Screen.width - SZEROKOSC_POLA, 5 * (szerokoscPrzycisku + offset), SZEROKOSC_POLA, szerokoscPrzycisku), "Quit", style))
            {
                Application.Quit();
            }
        }
    }

}
