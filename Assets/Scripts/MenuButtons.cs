﻿//#define HTML5

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using Unity.Notifications;
using Unity.Notifications.Android;
using Unity.Notifications.iOS;

public class MenuButtons : MonoBehaviour
{
	[Header("Background"), SerializeField]
	public Image backgroundImage;

	void changeBackground()
	{
		/*string pom = "SplashScreen.jpg";
        pom = System.IO.Path.Combine(Application.streamingAssetsPath, pom);
        byte[] pngBytes = System.IO.File.ReadAllBytes(pom);

        //Creates texture and loads byte array data to create image
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(pngBytes);

        //Creates a new Sprite based on the Texture2D
        Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

        //Assigns the UI sprite

        //backgroundImage.sprite = fromTex;
        GameObject.Find("Image").GetComponent<Image>().sprite = fromTex;*/
		//GameObject.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(SkinManager.instance.tla[SkinManager.instance.ActiveBackground].Name); 
	}

	IEnumerator Foo(string text, float delay)
	{
		yield return new WaitForSeconds(delay);

		// code here
		changeBackground();

	}

     
        

	void Start()
	{
        // StartCoroutine(Foo("Text", 10)); 
        //changeBackground();

        if (Application.platform == RuntimePlatform.Android)
        {
            var c = new AndroidNotificationChannel()
            {
                Id = "channel_id",
                Name = "Default Channel",
                Importance = Importance.High,
                Description = "Generic notifications",
            };

            AndroidNotificationCenter.RegisterNotificationChannel(c);

            var notificationA = new AndroidNotification();
            notificationA.Title = "Liga Matematyczna";
            notificationA.Text = "Zagraj mecz";
            notificationA.FireTime = System.DateTime.Now.AddMinutes(5);
            notificationA.LargeIcon = "icon_0";
            //repeat interval//if uczen registered// if liga startuje

            AndroidNotificationCenter.SendNotification(notificationA, "channel_id");
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            var timeTrigger = new iOSNotificationTimeIntervalTrigger()
            {
                TimeInterval = new TimeSpan(1, 0, 0, 0),
                Repeats = false
            };

            var notification = new iOSNotification()
            {
                // You can optionally specify a custom identifier which can later be 
                // used to cancel the notification, if you don't set one, a unique 
                // string will be generated automatically.
                Identifier = "_notification_01",
                Title = "Liga Matematyczna",
                Body = "Zagraj mecz",//"Scheduled at: " + DateTime.Now.ToShortDateString() + " triggered in 5 seconds",
                Subtitle = "codziennie",//"This is a subtitle, something, something important...",
                ShowInForeground = true,
                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                CategoryIdentifier = "category_a",
                ThreadIdentifier = "thread1",
                Trigger = timeTrigger,
            };

            iOSNotificationCenter.ScheduleNotification(notification);
        }
	}

	void Awake()
	{

	}

	public void exitButton()
	{
		Application.Quit();
	}

	public void aboutButton()
	{
		SceneManager.LoadScene("About");
	}

	public void multiplayerButton()
	{
		SceneManager.LoadScene("Multiplayer");
	}

	public void skinButton()
	{
		SceneManager.LoadScene("Skins");
	}

	public void optionButton()
	{
		SceneManager.LoadScene("GameSetting");
	}

	public void playButton()
	{
        if (SkinManager.instance.ActivePlayerMode == GameManager.GAME_CONDITION_SOLO)
        {
            SceneManager.LoadScene("Game");
        }
        else
        {
            if (SkinManager.instance.ActivePlayerMode == GameManager.GAME_CONDITION_AI)
            {
                SceneManager.LoadScene("Game");//like solo game
            }
            else
            {
                if (SkinManager.instance.ActivePlayerMode == GameManager.GAME_CONDITION_PVP)
                {
                    //2 player needed
                    SceneManager.LoadScene("Multiplayer");
                }
                else
                {
                    if (SkinManager.instance.ActivePlayerMode == GameManager.GAME_CONDITION_LEAGUE)
                    {
                        //2 player and 1 supervisor needed
                    }
                }
            }
        }
	}

	public void achievementButton()
	{
		SceneManager.LoadScene("Achievement");
	}

    public void ligaButton()
    {
        SceneManager.LoadScene("Liga");
    }

	/*  void OnGUI()
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
			  if (GUI.Button(new Rect(0, 0, SZEROKOSC_POLA, szerokoscPrzycisku), "TEST", style))
			  {
				  SceneManager.LoadScene("Test");
			  }


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
				  w
			  }
		  }
	  }
  */
}
