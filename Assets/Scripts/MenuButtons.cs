using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuButtons : MonoBehaviour
{
    void OnGUI()
    {
        const int ILE_ELEMENTOW_MENU = 5;
        const int SZEROKOSC_POLA = 250;
        const int OFFSET = 20;
        int szerokoscPrzycisku = 100;
        Scene scene;
        scene = SceneManager.GetActiveScene();

        //sprawdzenie czy aktualna scena to menu zeby nie wyswietlac menu w grze

        if (scene == SceneManager.GetSceneByName("Menu"))
        {
            GUIStyle style = new GUIStyle(GUI.skin.GetStyle("button"));
            style.fontSize = 50;
            szerokoscPrzycisku = Screen.height / (ILE_ELEMENTOW_MENU + 1);


            //wyswietlnie samych guzikow 

            if (GUI.Button(new Rect(Screen.width - SZEROKOSC_POLA, 0, SZEROKOSC_POLA, szerokoscPrzycisku), "Start", style))
            {
                SceneManager.LoadScene("Game");
            }

            if (GUI.Button(new Rect(Screen.width - SZEROKOSC_POLA, szerokoscPrzycisku + OFFSET, SZEROKOSC_POLA, szerokoscPrzycisku), "Multiplayer", style))
            {

            }

            if (GUI.Button(new Rect(Screen.width - SZEROKOSC_POLA, 2 * (szerokoscPrzycisku + OFFSET), SZEROKOSC_POLA, szerokoscPrzycisku), "Options", style))
            {
                //GUI.Slider()
            }

            if (GUI.Button(new Rect(Screen.width - SZEROKOSC_POLA, 3 * (szerokoscPrzycisku + OFFSET), SZEROKOSC_POLA, szerokoscPrzycisku), "Testy", style))
            {
                
            }

            if (GUI.Button(new Rect(Screen.width - SZEROKOSC_POLA, 4 * (szerokoscPrzycisku + OFFSET), SZEROKOSC_POLA, szerokoscPrzycisku), "Quit", style))
            {
                Application.Quit();
            }
        }
    }

}
