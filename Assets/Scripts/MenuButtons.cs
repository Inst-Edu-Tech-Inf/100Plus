using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuButtons : MonoBehaviour
{


    void OnGUI()
    {
        Scene scene;
        scene = SceneManager.GetActiveScene();

        //sprawdzenie czy aktualna scena to menu zeby nie wyswietlac menu w grze

        if (scene == SceneManager.GetSceneByName("Menu"))
        {
            GUIStyle style = new GUIStyle(GUI.skin.GetStyle("button"));
            style.fontSize = 50;


            //wyswietlnie samych guzikow 

            if (GUI.Button(new Rect(1200, 200, 500, 130), "Start", style))
            {
                SceneManager.LoadScene("Game");
            }

            if (GUI.Button(new Rect(1200, 400, 500, 130), "Multiplayer", style))
            {

            }

            if (GUI.Button(new Rect(1200, 600, 500, 130), "Options", style))
            {

            }

            if (GUI.Button(new Rect(1200, 800, 500, 130), "Quit", style))
            {
                Application.Quit();
            }


        }



    }

}
