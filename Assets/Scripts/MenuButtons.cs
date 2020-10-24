//#define HTML5
#define UNITY_ANDROID
//#define UNITY_IOS
//#define UDP_RELEASE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEngine.UDP;
//using Unity.Notifications; 
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
#if UNITY_IOS
using Unity.Notifications.iOS;
#endif
using UnityEngine.Networking;

public class MenuButtons : MonoBehaviour
{
	[Header("Background"), SerializeField]
    public GameObject panelWorking;
	public Image backgroundImage;
    //public GameObject panelConnect;
    public Text connectingText;
    public Image connectingImage;
    bool  isNotificationsAdded;// = false;
    

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


    IEnumerator UczenSprawdzClick(string uczenPass)
    {
        //panelWorking.SetActive(true);
        connectingImage.gameObject.SetActive(true);
        //panelConnect.SetActive(true);
        WWWForm form = new WWWForm();
        // string[] strArr;
        form.AddField("uczenPass", uczenPass);

        using (UnityWebRequest www = UnityWebRequest.Post("http://summon.ieti.pl/dbSummOn/SprawdzUczen.php", form))
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
#if UNITY_IOS
if (Application.platform == RuntimePlatform.IPhonePlayer)
{
                    //moj IPhone
                   /* if (SkinManager.instance.UserID == "3e5c6105dfe38c5c0c16ce622690b6bfa2f53998")
                    {
                        DateTime dzisiaj9 = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day);

                        System.DateTime dataPowiadomienia9 = new System.DateTime(2020, 10, 20);
                        TimeSpan kiedy9 = dataPowiadomienia9 - dzisiaj9;

                        var timeTrigger9 = new iOSNotificationTimeIntervalTrigger()
                        {
                            TimeInterval = new TimeSpan(kiedy9.Days, 0, 0, 0),
                            Repeats = false
                        };

                        var notification9 = new iOSNotification()
                        {
                            // You can optionally specify a custom identifier which can later be 
                            // used to cancel the notification, if you don't set one, a unique 
                            // string will be generated automatically.
                            Identifier = "_notification_01",
                            Title = SkinManager.instance.MenuLang[SkinManager.ZAGRAJ],
                            Body = SkinManager.instance.MenuLang[SkinManager.ZAGRAJ],//"Scheduled at: " + DateTime.Now.ToShortDateString() + " triggered in 5 seconds",
                            Subtitle = "",//"This is a subtitle, something, something important...",
                            ShowInForeground = true,
                            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                            CategoryIdentifier = "category_a",
                            ThreadIdentifier = "thread1",
                            Trigger = timeTrigger9,
                        };

                        iOSNotificationCenter.ScheduleNotification(notification9);
                    }*/
}
#endif
#if UNITY_ANDROID
                    if (Application.platform == RuntimePlatform.Android)
                    {
                        //moj Android
                        DateTime dataPowiadomienia8 = new System.DateTime(2020, 12, 1);
                        //add notifications Nauczyciel 19.26,2,9,16,23,30
                        /*if (SkinManager.instance.UserID == "482e7ce34536663f4fdd8cea7717cd4a09d8981f")
                        {
                            var c2 = new AndroidNotificationChannel()
                            {
                                Id = "mk_channel_id",
                                Name = "mk Channel ",
                                Importance = Importance.High,
                                Description = SkinManager.instance.MenuLang[SkinManager.ZAGRAJ],
                            };

                            AndroidNotificationCenter.RegisterNotificationChannel(c2);

                            var notificationA2 = new AndroidNotification();
                            dataPowiadomienia8 = new System.DateTime(2020, 10, 17, 15,0,0);
                            notificationA2.Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE];
                            notificationA2.Text = SkinManager.instance.MenuLang[SkinManager.ZAGRAJ];
                            notificationA2.FireTime = dataPowiadomienia8;
                            notificationA2.LargeIcon = "icon_0";
                            AndroidNotificationCenter.SendNotification(notificationA2, "mk_channel_id");
                        }*/
                    }
#endif
                    //add notifications Uczen
                    DateTime dataPowiadomienia = new System.DateTime(2020, 12, 1);
                    //add notifications Nauczyciel 19.26,2,9,16,23,30
#if UNITY_ANDROID
                    if (Application.platform == RuntimePlatform.Android)
                    {
                        var notificationA = new AndroidNotification();

                        
                        dataPowiadomienia = new System.DateTime(2020, 12, 1);
                        if (dataPowiadomienia > System.DateTime.Now)
                        {
                            var c2 = new AndroidNotificationChannel()
                            {
                                Id = "uczen_channel_id2",
                                Name = "Uczen Channel2",
                                Importance = Importance.High,
                                Description = SkinManager.instance.MenuLang[SkinManager.ZAGRAJ],
                            };

                            AndroidNotificationCenter.RegisterNotificationChannel(c2);

                            notificationA = new AndroidNotification();
                            //dataPowiadomienia = new System.DateTime(2021, 1, 1);
                            notificationA.Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE];
                            notificationA.Text = SkinManager.instance.MenuLang[SkinManager.ZAGRAJ];
                            notificationA.FireTime = dataPowiadomienia;
                            notificationA.LargeIcon = "icon_0";
                            AndroidNotificationCenter.SendNotification(notificationA, "uczen_channel_id2");
                        }

                        dataPowiadomienia = new System.DateTime(2021, 1, 1);
                        if (dataPowiadomienia > System.DateTime.Now)
                        {
                            var c = new AndroidNotificationChannel()
                            {
                                Id = "uczen_channel_id",
                                Name = "Uczen Channel",
                                Importance = Importance.High,
                                Description = SkinManager.instance.MenuLang[SkinManager.ZAGRAJ],
                            };

                            AndroidNotificationCenter.RegisterNotificationChannel(c);

                            notificationA = new AndroidNotification();
                            //dataPowiadomienia = new System.DateTime(2021, 1, 1);
                            notificationA.Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE];
                            notificationA.Text = SkinManager.instance.MenuLang[SkinManager.ZAGRAJ];
                            notificationA.FireTime = dataPowiadomienia;
                            notificationA.LargeIcon = "icon_0";
                            AndroidNotificationCenter.SendNotification(notificationA, "uczen_channel_id");
                        }

                        dataPowiadomienia = new System.DateTime(2021, 2, 1);
                        if (dataPowiadomienia > System.DateTime.Now)
                        {
                            var c3 = new AndroidNotificationChannel()
                            {
                                Id = "uczen_channel_id3",
                                Name = "Uczen Channel3",
                                Importance = Importance.High,
                                Description = SkinManager.instance.MenuLang[SkinManager.ZAGRAJ],
                            };

                            AndroidNotificationCenter.RegisterNotificationChannel(c3);

                            notificationA = new AndroidNotification();
                            //dataPowiadomienia = new System.DateTime(2021, 2, 1);
                            notificationA.Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE];
                            notificationA.Text = SkinManager.instance.MenuLang[SkinManager.ZAGRAJ];
                            notificationA.FireTime = dataPowiadomienia;
                            notificationA.LargeIcon = "icon_0";
                            AndroidNotificationCenter.SendNotification(notificationA, "uczen_channel_id3");
                        }

                        dataPowiadomienia = new System.DateTime(2021, 3, 1);
                        if (dataPowiadomienia > System.DateTime.Now)
                        {
                            var c4 = new AndroidNotificationChannel()
                            {
                                Id = "uczen_channel_id4",
                                Name = "Uczen Channel4",
                                Importance = Importance.High,
                                Description = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE],
                            };

                            AndroidNotificationCenter.RegisterNotificationChannel(c4);
                            notificationA = new AndroidNotification();
                            //dataPowiadomienia = new System.DateTime(2021, 3, 1);
                            notificationA.Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE];
                            notificationA.Text = SkinManager.instance.MenuLang[SkinManager.ZAGRAJ];
                            notificationA.FireTime = dataPowiadomienia;
                            notificationA.LargeIcon = "icon_0";
                            AndroidNotificationCenter.SendNotification(notificationA, "uczen_channel_id4");
                        }

                        dataPowiadomienia = new System.DateTime(2021, 4, 1);
                        if (dataPowiadomienia > System.DateTime.Now)
                        {
                            var c5 = new AndroidNotificationChannel()
                            {
                                Id = "uczen_channel_id5",
                                Name = "Uczen Channel5",
                                Importance = Importance.High,
                                Description = SkinManager.instance.MenuLang[SkinManager.ZAGRAJ],
                            };

                            AndroidNotificationCenter.RegisterNotificationChannel(c5);
                            notificationA = new AndroidNotification();
                            //dataPowiadomienia = new System.DateTime(2021, 4, 1);
                            notificationA.Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE];
                            notificationA.Text = SkinManager.instance.MenuLang[SkinManager.ZAGRAJ];
                            notificationA.FireTime = dataPowiadomienia;
                            notificationA.LargeIcon = "icon_0";
                            AndroidNotificationCenter.SendNotification(notificationA, "uczen_channel_id5");
                        }

                        dataPowiadomienia = new System.DateTime(2021, 5, 1);
                        if (dataPowiadomienia > System.DateTime.Now)
                        {
                            var c6 = new AndroidNotificationChannel()
                            {
                                Id = "uczen_channel_id6",
                                Name = "Uczen Channel6",
                                Importance = Importance.High,
                                Description = SkinManager.instance.MenuLang[SkinManager.ZAGRAJ],
                            };

                            AndroidNotificationCenter.RegisterNotificationChannel(c6);
                            notificationA = new AndroidNotification();
                            //dataPowiadomienia = new System.DateTime(2021, 5, 1);
                            notificationA.Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE];
                            notificationA.Text = SkinManager.instance.MenuLang[SkinManager.ZAGRAJ];
                            notificationA.FireTime = dataPowiadomienia;
                            notificationA.LargeIcon = "icon_0";
                            AndroidNotificationCenter.SendNotification(notificationA, "uczen_channel_id6");
                        }


                    }
#endif
#if UNITY_IOS
                    if (Application.platform == RuntimePlatform.IPhonePlayer)
                    {
                        DateTime dzisiaj = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day);

                        dataPowiadomienia = new System.DateTime(2020, 12, 1);
                        TimeSpan kiedy = dataPowiadomienia - dzisiaj;

                        if (kiedy.Days > 0)
                        {
                            var timeTrigger = new iOSNotificationTimeIntervalTrigger()
                            {
                                TimeInterval = new TimeSpan(kiedy.Days, 0, 0, 0),
                                Repeats = false
                            };

                            var notification = new iOSNotification()
                            {
                                // You can optionally specify a custom identifier which can later be 
                                // used to cancel the notification, if you don't set one, a unique 
                                // string will be generated automatically.
                                Identifier = "_notification_01",
                                Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE],
                                Body = SkinManager.instance.MenuLang[SkinManager.ZAGRAJ],//"Scheduled at: " + DateTime.Now.ToShortDateString() + " triggered in 5 seconds",
                                Subtitle = "",//"This is a subtitle, something, something important...",
                                ShowInForeground = true,
                                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                                CategoryIdentifier = "category_a",
                                ThreadIdentifier = "thread1",
                                Trigger = timeTrigger,
                            };

                            iOSNotificationCenter.ScheduleNotification(notification);
                        }

                        dataPowiadomienia = new System.DateTime(2021, 1, 1);
                        kiedy = dataPowiadomienia - dzisiaj;

                        if (kiedy.Days > 0)
                        {
                            var timeTrigger1 = new iOSNotificationTimeIntervalTrigger()
                            {
                                TimeInterval = new TimeSpan(kiedy.Days, 0, 0, 0),
                                Repeats = false
                            };

                            var notification2 = new iOSNotification()
                            {
                                // You can optionally specify a custom identifier which can later be 
                                // used to cancel the notification, if you don't set one, a unique 
                                // string will be generated automatically.
                                Identifier = "_notification_02",
                                Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE],
                                Body = SkinManager.instance.MenuLang[SkinManager.ZAGRAJ],//"Scheduled at: " + DateTime.Now.ToShortDateString() + " triggered in 5 seconds",
                                Subtitle = "",//"This is a subtitle, something, something important...",
                                ShowInForeground = true,
                                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                                CategoryIdentifier = "category_a",
                                ThreadIdentifier = "thread1",
                                Trigger = timeTrigger1,
                            };

                            iOSNotificationCenter.ScheduleNotification(notification2);
                        }

                        dataPowiadomienia = new System.DateTime(2021, 2, 1);
                        kiedy = dataPowiadomienia - dzisiaj;

                        if (kiedy.Days > 0)
                        {
                            var timeTrigger2 = new iOSNotificationTimeIntervalTrigger()
                            {
                                TimeInterval = new TimeSpan(kiedy.Days, 0, 0, 0),
                                Repeats = false
                            };

                            var notification3 = new iOSNotification()
                            {
                                // You can optionally specify a custom identifier which can later be 
                                // used to cancel the notification, if you don't set one, a unique 
                                // string will be generated automatically.
                                Identifier = "_notification_03",
                                Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE],
                                Body = SkinManager.instance.MenuLang[SkinManager.ZAGRAJ],//"Scheduled at: " + DateTime.Now.ToShortDateString() + " triggered in 5 seconds",
                                Subtitle = "",//"This is a subtitle, something, something important...",
                                ShowInForeground = true,
                                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                                CategoryIdentifier = "category_a",
                                ThreadIdentifier = "thread1",
                                Trigger = timeTrigger2,
                            };

                            iOSNotificationCenter.ScheduleNotification(notification3);
                        }

                        dataPowiadomienia = new System.DateTime(2021, 3, 1);
                        kiedy = dataPowiadomienia - dzisiaj;

                        if (kiedy.Days > 0)
                        {
                            var timeTrigger4 = new iOSNotificationTimeIntervalTrigger()
                            {
                                TimeInterval = new TimeSpan(kiedy.Days, 0, 0, 0),
                                Repeats = false
                            };

                            var notification4 = new iOSNotification()
                            {
                                // You can optionally specify a custom identifier which can later be 
                                // used to cancel the notification, if you don't set one, a unique 
                                // string will be generated automatically.
                                Identifier = "_notification_04",
                                Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE],
                                Body = SkinManager.instance.MenuLang[SkinManager.ZAGRAJ],//"Scheduled at: " + DateTime.Now.ToShortDateString() + " triggered in 5 seconds",
                                Subtitle = "",//"This is a subtitle, something, something important...",
                                ShowInForeground = true,
                                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                                CategoryIdentifier = "category_a",
                                ThreadIdentifier = "thread1",
                                Trigger = timeTrigger4,
                            };

                            iOSNotificationCenter.ScheduleNotification(notification4);
                        }

                        dataPowiadomienia = new System.DateTime(2021, 4, 1);
                        kiedy = dataPowiadomienia - dzisiaj;

                        if (kiedy.Days > 0)
                        {
                            var timeTrigger5 = new iOSNotificationTimeIntervalTrigger()
                            {
                                TimeInterval = new TimeSpan(kiedy.Days, 0, 0, 0),
                                Repeats = false
                            };

                            var notification5 = new iOSNotification()
                            {
                                // You can optionally specify a custom identifier which can later be 
                                // used to cancel the notification, if you don't set one, a unique 
                                // string will be generated automatically.
                                Identifier = "_notification_05",
                                Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE],
                                Body = SkinManager.instance.MenuLang[SkinManager.ZAGRAJ],//"Scheduled at: " + DateTime.Now.ToShortDateString() + " triggered in 5 seconds",
                                Subtitle = "",//"This is a subtitle, something, something important...",
                                ShowInForeground = true,
                                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                                CategoryIdentifier = "category_a",
                                ThreadIdentifier = "thread1",
                                Trigger = timeTrigger5,
                            };

                            iOSNotificationCenter.ScheduleNotification(notification5);
                        }

                        dataPowiadomienia = new System.DateTime(2021, 5, 1);
                        kiedy = dataPowiadomienia - dzisiaj;

                        if (kiedy.Days > 0)
                        {
                            var timeTrigger6 = new iOSNotificationTimeIntervalTrigger()
                            {
                                TimeInterval = new TimeSpan(kiedy.Days, 0, 0, 0),
                                Repeats = false
                            };

                            var notification6 = new iOSNotification()
                            {
                                // You can optionally specify a custom identifier which can later be 
                                // used to cancel the notification, if you don't set one, a unique 
                                // string will be generated automatically.
                                Identifier = "_notification_06",
                                Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE],
                                Body = SkinManager.instance.MenuLang[SkinManager.ZAGRAJ],//"Scheduled at: " + DateTime.Now.ToShortDateString() + " triggered in 5 seconds",
                                Subtitle = "",//"This is a subtitle, something, something important...",
                                ShowInForeground = true,
                                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                                CategoryIdentifier = "category_a",
                                ThreadIdentifier = "thread1",
                                Trigger = timeTrigger6,
                            };

                            iOSNotificationCenter.ScheduleNotification(notification6);
                        }


                    }//end iPhone
#endif
                }
            }
        }
        //panelWorking.SetActive(false);
        connectingImage.gameObject.SetActive(false);
        //panelConnect.SetActive(false);
    }

    IEnumerator NauczycielSprawdzClick(string uczenPass)
    {
        //panelWorking.SetActive(true);
        connectingImage.gameObject.SetActive(true);
        WWWForm form = new WWWForm();
        // string[] strArr;
        form.AddField("uczenPass", uczenPass);

        using (UnityWebRequest www = UnityWebRequest.Post("http://summon.ieti.pl/dbSummOn/SprawdzNauczyciel.php", form))
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
                    DateTime dataPowiadomienia = new System.DateTime(2020, 10, 19);
                    //add notifications Nauczyciel 19.26,2,9,16,23,30
#if UNITY_ANDROID
                    if (Application.platform == RuntimePlatform.Android)
                    {
                        var notificationA = new AndroidNotification();
                        /*var c = new AndroidNotificationChannel()
                        {
                            Id = "channel_id",
                            Name = "Default Channel",
                            Importance = Importance.High,
                            Description = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE],
                        };

                        AndroidNotificationCenter.RegisterNotificationChannel(c);*/

                        dataPowiadomienia = new System.DateTime(2020, 10, 19);
                        if (dataPowiadomienia > System.DateTime.Now)
                        {
                            var c3 = new AndroidNotificationChannel()
                            {
                                Id = "channel_id_c",
                                Name = "Default Channel c",
                                Importance = Importance.High,
                                Description = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE],
                            };
                            AndroidNotificationCenter.RegisterNotificationChannel(c3);

                            notificationA = new AndroidNotification();
                            //dataPowiadomienia = new System.DateTime(2020, 10, 19);
                            notificationA.Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE];
                            notificationA.Text = SkinManager.instance.MenuLang[SkinManager.KODY_DLA_UCZNIOW];
                            notificationA.FireTime = dataPowiadomienia;
                            notificationA.LargeIcon = "icon_0";
                            AndroidNotificationCenter.SendNotification(notificationA, "channel_id_c");
                        }
                      
                        
                        dataPowiadomienia = new System.DateTime(2020, 10, 26);
                        if (dataPowiadomienia > System.DateTime.Now)
                        {
                            var c4 = new AndroidNotificationChannel()
                            {
                                Id = "channel_id_d",
                                Name = "Default Channel d",
                                Importance = Importance.High,
                                Description = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE],
                            };
                            AndroidNotificationCenter.RegisterNotificationChannel(c4);

                            notificationA = new AndroidNotification();
                            //dataPowiadomienia = new System.DateTime(2020, 10, 26);
                            notificationA.Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE];
                            notificationA.Text = SkinManager.instance.MenuLang[SkinManager.KODY_DLA_UCZNIOW];
                            notificationA.FireTime = dataPowiadomienia;
                            notificationA.LargeIcon = "icon_0";
                            AndroidNotificationCenter.SendNotification(notificationA, "channel_id_d");
                        }

                        dataPowiadomienia = new System.DateTime(2020, 11, 2);
                        if (dataPowiadomienia > System.DateTime.Now)
                        {
                            var c5 = new AndroidNotificationChannel()
                            {
                                Id = "channel_id_e",
                                Name = "Default Channel e",
                                Importance = Importance.High,
                                Description = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE],
                            };
                            AndroidNotificationCenter.RegisterNotificationChannel(c5);
                            notificationA = new AndroidNotification();
                            //dataPowiadomienia = new System.DateTime(2020, 11, 2);
                            notificationA.Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE];
                            notificationA.Text = SkinManager.instance.MenuLang[SkinManager.KODY_DLA_UCZNIOW];
                            notificationA.FireTime = dataPowiadomienia;
                            notificationA.LargeIcon = "icon_0";
                            AndroidNotificationCenter.SendNotification(notificationA, "channel_id_e");
                        }

                        dataPowiadomienia = new System.DateTime(2020, 11, 9);
                        if (dataPowiadomienia > System.DateTime.Now)
                        {
                            var c6 = new AndroidNotificationChannel()
                            {
                                Id = "channel_id_f",
                                Name = "Default Channel f",
                                Importance = Importance.High,
                                Description = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE],
                            };
                            AndroidNotificationCenter.RegisterNotificationChannel(c6);

                            notificationA = new AndroidNotification();
                            //dataPowiadomienia = new System.DateTime(2020, 11, 9);
                            notificationA.Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE];
                            notificationA.Text = SkinManager.instance.MenuLang[SkinManager.KODY_DLA_UCZNIOW];
                            notificationA.FireTime = dataPowiadomienia;
                            notificationA.LargeIcon = "icon_0";
                            AndroidNotificationCenter.SendNotification(notificationA, "channel_id_f");
                        }

                        dataPowiadomienia = new System.DateTime(2020, 11, 16);
                        if (dataPowiadomienia > System.DateTime.Now)
                        {
                            var c7 = new AndroidNotificationChannel()
                            {
                                Id = "channel_id_g",
                                Name = "Default Channel g",
                                Importance = Importance.High,
                                Description = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE],
                            };
                            AndroidNotificationCenter.RegisterNotificationChannel(c7);

                            notificationA = new AndroidNotification();
                           // dataPowiadomienia = new System.DateTime(2020, 11, 16);
                            notificationA.Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE];
                            notificationA.Text = SkinManager.instance.MenuLang[SkinManager.KODY_DLA_UCZNIOW];
                            notificationA.FireTime = dataPowiadomienia;
                            notificationA.LargeIcon = "icon_0";
                            AndroidNotificationCenter.SendNotification(notificationA, "channel_id_g");
                        }

                        dataPowiadomienia = new System.DateTime(2020, 11, 23);
                        if (dataPowiadomienia > System.DateTime.Now)
                        {
                            var c8 = new AndroidNotificationChannel()
                            {
                                Id = "channel_id_h",
                                Name = "Default Channel h",
                                Importance = Importance.High,
                                Description = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE],
                            };
                            AndroidNotificationCenter.RegisterNotificationChannel(c8);

                            notificationA = new AndroidNotification();
                            //dataPowiadomienia = new System.DateTime(2020, 11, 23);
                            notificationA.Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE]; ;
                            notificationA.Text = SkinManager.instance.MenuLang[SkinManager.KODY_DLA_UCZNIOW];
                            notificationA.FireTime = dataPowiadomienia;
                            notificationA.LargeIcon = "icon_0";
                            AndroidNotificationCenter.SendNotification(notificationA, "channel_id_h");
                        }

                        dataPowiadomienia = new System.DateTime(2020, 11, 30);
                        if (dataPowiadomienia > System.DateTime.Now)
                        {
                            var c9 = new AndroidNotificationChannel()
                            {
                                Id = "channel_id_d_i",
                                Name = "Default Channel i",
                                Importance = Importance.High,
                                Description = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE],
                            };
                            AndroidNotificationCenter.RegisterNotificationChannel(c9);

                            notificationA = new AndroidNotification();
                            //dataPowiadomienia = new System.DateTime(2020, 11, 30);
                            notificationA.Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE];
                            notificationA.Text = SkinManager.instance.MenuLang[SkinManager.KODY_DLA_UCZNIOW];
                            notificationA.FireTime = dataPowiadomienia;
                            notificationA.LargeIcon = "icon_0";
                            AndroidNotificationCenter.SendNotification(notificationA, "channel_id_i");
                        }
                    }
#endif
#if UNITY_IOS
                    if (Application.platform == RuntimePlatform.IPhonePlayer)
                    {
                        DateTime dzisiaj = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day);
                        
                        dataPowiadomienia = new System.DateTime(2020, 10, 19);
                        TimeSpan kiedy = dataPowiadomienia - dzisiaj;
                        if (kiedy.Days > 0)
                        {
                            var timeTrigger = new iOSNotificationTimeIntervalTrigger()
                            {
                                TimeInterval = new TimeSpan(kiedy.Days, 0, 0, 0),
                                Repeats = false
                            };

                            var notification = new iOSNotification()
                            {
                                // You can optionally specify a custom identifier which can later be 
                                // used to cancel the notification, if you don't set one, a unique 
                                // string will be generated automatically.
                                Identifier = "_notification_01",
                                Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE],
                                Body = SkinManager.instance.MenuLang[SkinManager.KODY_DLA_UCZNIOW],//"Scheduled at: " + DateTime.Now.ToShortDateString() + " triggered in 5 seconds",
                                Subtitle = "",//"This is a subtitle, something, something important...",
                                ShowInForeground = true,
                                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                                CategoryIdentifier = "category_a",
                                ThreadIdentifier = "thread1",
                                Trigger = timeTrigger,
                            };

                            iOSNotificationCenter.ScheduleNotification(notification);
                        }

                        dataPowiadomienia = new System.DateTime(2020, 10, 26);
                        kiedy = dataPowiadomienia - dzisiaj;
                        if (kiedy.Days > 0)
                        {
                            var timeTrigger1 = new iOSNotificationTimeIntervalTrigger()
                            {
                                TimeInterval = new TimeSpan(kiedy.Days, 0, 0, 0),
                                Repeats = false
                            };

                            var notification2 = new iOSNotification()
                            {
                                // You can optionally specify a custom identifier which can later be 
                                // used to cancel the notification, if you don't set one, a unique 
                                // string will be generated automatically.
                                Identifier = "_notification_02",
                                Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE],
                                Body = SkinManager.instance.MenuLang[SkinManager.KODY_DLA_UCZNIOW],//"Scheduled at: " + DateTime.Now.ToShortDateString() + " triggered in 5 seconds",
                                Subtitle = "",//"This is a subtitle, something, something important...",
                                ShowInForeground = true,
                                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                                CategoryIdentifier = "category_a",
                                ThreadIdentifier = "thread1",
                                Trigger = timeTrigger1,
                            };

                            iOSNotificationCenter.ScheduleNotification(notification2);
                        }

                        dataPowiadomienia = new System.DateTime(2020, 11, 2);
                        kiedy = dataPowiadomienia - dzisiaj;
                        if (kiedy.Days > 0)
                        {
                            var timeTrigger2 = new iOSNotificationTimeIntervalTrigger()
                            {
                                TimeInterval = new TimeSpan(kiedy.Days, 0, 0, 0),
                                Repeats = false
                            };

                            var notification3 = new iOSNotification()
                            {
                                // You can optionally specify a custom identifier which can later be 
                                // used to cancel the notification, if you don't set one, a unique 
                                // string will be generated automatically.
                                Identifier = "_notification_03",
                                Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE],
                                Body = SkinManager.instance.MenuLang[SkinManager.KODY_DLA_UCZNIOW],//"Scheduled at: " + DateTime.Now.ToShortDateString() + " triggered in 5 seconds",
                                Subtitle = "",//"This is a subtitle, something, something important...",
                                ShowInForeground = true,
                                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                                CategoryIdentifier = "category_a",
                                ThreadIdentifier = "thread1",
                                Trigger = timeTrigger2,
                            };

                            iOSNotificationCenter.ScheduleNotification(notification3);
                        }

                        dataPowiadomienia = new System.DateTime(2020, 11, 9);
                        kiedy = dataPowiadomienia - dzisiaj;
                        if (kiedy.Days > 0)
                        {
                            var timeTrigger4 = new iOSNotificationTimeIntervalTrigger()
                            {
                                TimeInterval = new TimeSpan(kiedy.Days, 0, 0, 0),
                                Repeats = false
                            };

                            var notification4 = new iOSNotification()
                            {
                                // You can optionally specify a custom identifier which can later be 
                                // used to cancel the notification, if you don't set one, a unique 
                                // string will be generated automatically.
                                Identifier = "_notification_04",
                                Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE],
                                Body = SkinManager.instance.MenuLang[SkinManager.KODY_DLA_UCZNIOW],//"Scheduled at: " + DateTime.Now.ToShortDateString() + " triggered in 5 seconds",
                                Subtitle = "",//"This is a subtitle, something, something important...",
                                ShowInForeground = true,
                                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                                CategoryIdentifier = "category_a",
                                ThreadIdentifier = "thread1",
                                Trigger = timeTrigger4,
                            };

                            iOSNotificationCenter.ScheduleNotification(notification4);
                        }

                        dataPowiadomienia = new System.DateTime(2020, 11, 16);
                        kiedy = dataPowiadomienia - dzisiaj;
                        if (kiedy.Days > 0)
                        {
                            var timeTrigger5 = new iOSNotificationTimeIntervalTrigger()
                            {
                                TimeInterval = new TimeSpan(kiedy.Days, 0, 0, 0),
                                Repeats = false
                            };

                            var notification5 = new iOSNotification()
                            {
                                // You can optionally specify a custom identifier which can later be 
                                // used to cancel the notification, if you don't set one, a unique 
                                // string will be generated automatically.
                                Identifier = "_notification_05",
                                Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE],
                                Body = SkinManager.instance.MenuLang[SkinManager.KODY_DLA_UCZNIOW],//"Scheduled at: " + DateTime.Now.ToShortDateString() + " triggered in 5 seconds",
                                Subtitle = "",//"This is a subtitle, something, something important...",
                                ShowInForeground = true,
                                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                                CategoryIdentifier = "category_a",
                                ThreadIdentifier = "thread1",
                                Trigger = timeTrigger5,
                            };

                            iOSNotificationCenter.ScheduleNotification(notification5);
                        }

                        dataPowiadomienia = new System.DateTime(2020, 11, 23);
                        kiedy = dataPowiadomienia - dzisiaj;
                        if (kiedy.Days > 0)
                        {
                            var timeTrigger6 = new iOSNotificationTimeIntervalTrigger()
                            {
                                TimeInterval = new TimeSpan(kiedy.Days, 0, 0, 0),
                                Repeats = false
                            };

                            var notification6 = new iOSNotification()
                            {
                                // You can optionally specify a custom identifier which can later be 
                                // used to cancel the notification, if you don't set one, a unique 
                                // string will be generated automatically.
                                Identifier = "_notification_06",
                                Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE],
                                Body = SkinManager.instance.MenuLang[SkinManager.KODY_DLA_UCZNIOW],//"Scheduled at: " + DateTime.Now.ToShortDateString() + " triggered in 5 seconds",
                                Subtitle = "",//"This is a subtitle, something, something important...",
                                ShowInForeground = true,
                                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                                CategoryIdentifier = "category_a",
                                ThreadIdentifier = "thread1",
                                Trigger = timeTrigger6,
                            };

                            iOSNotificationCenter.ScheduleNotification(notification6);
                        }

                        dataPowiadomienia = new System.DateTime(2020, 11, 30);
                        kiedy = dataPowiadomienia - dzisiaj;
                        if (kiedy.Days > 0)
                        {
                            var timeTrigger7 = new iOSNotificationTimeIntervalTrigger()
                            {
                                TimeInterval = new TimeSpan(kiedy.Days, 0, 0, 0),
                                Repeats = false
                            };

                            var notification7 = new iOSNotification()
                            {
                                // You can optionally specify a custom identifier which can later be 
                                // used to cancel the notification, if you don't set one, a unique 
                                // string will be generated automatically.
                                Identifier = "_notification_07",
                                Title = SkinManager.instance.MenuLang[SkinManager.SUMMON_LEAGUE],
                                Body = SkinManager.instance.MenuLang[SkinManager.KODY_DLA_UCZNIOW],//"Scheduled at: " + DateTime.Now.ToShortDateString() + " triggered in 5 seconds",
                                Subtitle = "",//"This is a subtitle, something, something important...",
                                ShowInForeground = true,
                                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                                CategoryIdentifier = "category_a",
                                ThreadIdentifier = "thread1",
                                Trigger = timeTrigger7,
                            };

                            iOSNotificationCenter.ScheduleNotification(notification7);
                        }
                    }//end iPhone
#endif
                }
            }
        }
        //panelWorking.SetActive(false);
        connectingImage.gameObject.SetActive(false);
        connectingText.text = "";
    } 

	void Start()
	{
        // StartCoroutine(Foo("Text", 10)); 
        //changeBackground();
        //Debug.Log("PanelWorking:"+GameObject.FindGameObjectWithTag("connectingText"));
        //if (!SkinManager.instance.GetIsNotificationsAdded())
#if UDP_RELEASE
        try
        {
        //# Instantiate the listener
        IInitListener listener = new InitListener();
        //# Use the listener to initialize the UDP stuff
        StoreService.Initialize(listener);
        }
        catch (Exception ex)
        {
            Debug.Log("UDP Store error:"+ex);
        }
#endif

        

        try
        {
            connectingImage = GameObject.FindGameObjectWithTag("connectingImage").GetComponent<Image>();
            connectingText = GameObject.FindGameObjectWithTag("connectingText").GetComponent<Text>();
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
            
        
        if (!SkinManager.instance.GetIsNotificationsAdded())
        {
#if UNITY_ANDROID
        AndroidNotificationCenter.CancelAllNotifications();
#endif
#if UNITY_IOS
        iOSNotificationCenter.RemoveAllDeliveredNotifications();
        iOSNotificationCenter.RemoveAllScheduledNotifications();
#endif
       // Debug.Log("notifBEFORE:" + SkinManager.instance.GetIsNotificationsAdded());
        
            connectingImage.gameObject.SetActive(true);
            StartCoroutine(NauczycielSprawdzClick(SkinManager.instance.UserID));
            StartCoroutine(UczenSprawdzClick(SkinManager.instance.UserID));
            //Debug.Log("Wczytuję notifications");
        }
        else
        {
            //if (connectingImage != null)
            try
            {
                connectingImage.gameObject.SetActive(false);
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
                
            connectingText.text = "";
        }

        SkinManager.instance.SetIsNotificationsAdded(true);
        //isNotificationsAdded=true;        
        //Debug.Log("notifAFTER:" + SkinManager.instance.GetIsNotificationsAdded());
	}

	void Awake()
	{
        //isNotificationsAdded = true;
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
