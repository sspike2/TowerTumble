using System.Collections; using System.Collections.Generic; using UnityEngine; using UnityEngine.SceneManagement; using DoozyUI; using System.IO; using UnityEngine.UI; using UnityEngine.Rendering; using TMPro; using BayatGames.SaveGameFree;
using UnityEngine.Analytics;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;

public class UIScript : MonoBehaviour {     public static UIScript instance;      [Header( "UIELEMENTS" )]     [Space]      [SerializeField] UIElement MainMenuScreen;      [SerializeField] UIElement HUDScreen;      [SerializeField] UIElement DeathScreen;      [SerializeField] UIElement pauseScreen;      [SerializeField] UIElement privacyPolicy;      [SerializeField] UIElement helpWindow;       [Space,Header("Misc")]      [SerializeField] Sprite soundOnSprite;     [SerializeField] Sprite soundOffSprite;      [SerializeField] Image[] soundIcons;      [SerializeField] GameObject[] IAPButtonsObjs;       [SerializeField] bool isSoundOn;      [Space]       //public Material MainmenuBGMat;     //public RawImage MainMenuBG;     //public Camera ScreenshotCamera;     //Screenshot screenShotCamScript;      public TextMeshProUGUI[] scoreTexts;     public TextMeshProUGUI[] highScoreTexts;      public GameObject GameManager;     Texture2D tex;     public string filePath;       [SerializeField] TextMeshProUGUI purchasestatus;
    [SerializeField] TextMeshProUGUI loginStatus;       GameObject ScoreAddText;      int adCounter;      [SerializeField] private static int purchasedNoAds;

    public static PlayGamesPlatform playGamesPlatform;

    // public Queue<AsyncGPUReadbackRequest> _requests = new Queue<AsyncGPUReadbackRequest>();
    private void Awake()     {         // DontDestroyOnLoad(gameObject);         // if (instance != null)         // {         //     Destroy(instance);         // }         // else         // {         instance = this;         // }        }       void InitializePlayGames()
    {
        if (playGamesPlatform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
          // enables saving game progress.
          //.EnableSavedGames()
          // registers a callback to handle game invitations received while the game is not running.
          //.WithInvitationDelegate(< callback method >)
          // registers a callback for turn based match notifications received while the
          // game is not running.
          //.WithMatchDelegate(< callback method >)
          // requests the email address of the player be available.
          // Will bring up a prompt for consent.
          //.RequestEmail()
          // requests a server auth code be generated so it can be passed to an
          //  associated back end server application and exchanged for an OAuth token.
          //.RequestServerAuthCode(false)
          // requests an ID token be generated.  This OAuth token can be used to
          //  identify the player to other services such as Firebase.
          //.RequestIdToken()
          .Build();

            PlayGamesPlatform.InitializeInstance(config);
            // recommended for debugging:
            PlayGamesPlatform.DebugLogEnabled = true;
            // Activate the Google Play Games platform
            playGamesPlatform = PlayGamesPlatform.Activate();

        }
    }

    // Start is called before the first frame update
    void Start()     {


        InitializePlayGames();
        GoogleLogin(); 
        //filePath = Application.persistentDataPath + "/" + "LastProgress.png";

        //screenShotCamScript = ScreenshotCamera.GetComponent<Screenshot>();

        //tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBA32, false);

        //ScoreAddText = Resources.Load( "ScoreAddText" ) as GameObject;

        // authenticate user:


        //if (File.Exists( filePath ser
        //{
        //    var fileData = File.ReadAllBytes( filePath );
        //    tex = new Texture2D( 2, 2 );
        //    tex.LoadImage( fileData ); //..this will auto-resize the texture dimensions.
        //    //Texture2D.
        //    MainmenuBGMat.mainTexture = tex;
        //    MainMenuBG.texture = tex;


        //}
        //else
        //{
        //    MainMenuBG.enabled = false;
        //}

         isSoundOn = PlayerPrefs.GetInt( "Sound", 1 ) == 1 ? true : false;          SoundManagerScript.SetSound( isSoundOn );          SetSoundIcons();          adCounter = PlayerPrefs.GetInt( "adCounter", 0 );          purchasedNoAds = SaveGame.Load<int>("Ads", 0, true);          if(purchasedNoAds == 1)
        {
            AdPurchased();
        }          var privacy = PlayerPrefs.GetInt("Privacy", 0);         if(privacy == 0)
        {
            ShowPrivacyPolicyWindow();
        }      }      public void GoogleLogin()
    {
        //Social.Active.localUser.Authenticate(success => { loginStatus.text = success + ""; });
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (result) => { loginStatus.text = result.ToString(); });
    }      public  void AdPurchased()
    {
        purchasedNoAds = 1;
        for (int i = 0; i < IAPButtonsObjs.Length; i++)
        {
            IAPButtonsObjs[i].SetActive(false);
        }

    }        public static int isAdPurchased()
    {
        return purchasedNoAds;
    }       void SetSoundIcons()     {         for (int i = 0 ; i < soundIcons.Length ; i++)         {               if (isSoundOn)             {                 soundIcons[i].sprite = soundOnSprite;                 soundIcons[i].color = Color.green;             }             else             {                 soundIcons[i].sprite = soundOffSprite;                 soundIcons[i].color = Color.red;             }         }     }      public void ToggleSound()     {         isSoundOn = !isSoundOn;          SetSoundIcons();         SoundManagerScript.SetSound( isSoundOn );     }       public void LeaderBoards()     {         if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();

        } else
        {
            GoogleLogin();
            //PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => { loginStatus.text = result.ToString(); });
            //Login
            Social.ShowLeaderboardUI();
        }
    }       public void AddToLeaderBoards(int value)
    {
        


        Social.ReportScore(value, GPGSId.leaderboard_high_score, (bool success) => {
            // handle success or failure
            //if(success)
            //{
            purchasestatus.text = "sucess " + value + success; 
            //}
            
        });
    }      public void PurchaseNoAds()     {         FindObjectOfType<IAPManager>().BuyNonConsumable();     }      public void SetPurchaseState(string t)     {         purchasestatus.text = t;     }        public void play()     {          HUDScreen.Show( false );         MainMenuScreen.Hide( false );         GameManager.SetActive( true );          if (purchasedNoAds != 1)
            AdManager.instance.ShowBanerAd();      }      // Update is called once per frame     void Update()     {      }      public void ScoreTextsUpdate(int score)     {         for (int i = 0 ; i < scoreTexts.Length ; i++)         {             scoreTexts[i].text = score + "";         }     }      public void HighScoreTextsUpdate(int score)     {         for (int i = 0 ; i < highScoreTexts.Length ; i++)         {             highScoreTexts[i].text = score + "";         }     }      public void NewHighScore()     {      }      public void GameOver()     {         Debug.Log( "gameover" );         //MainClass.instance.GameOver();         //Screenshot();         Invoke( "EnableDeathScreen", 0.5f );

            }      public void HelpWindowOn()
    {
        helpWindow.Show();
    }     public void HelpWindowOff()
    {
        helpWindow.Hide();
    }           void Screenshot()     {         // ScreenshotCamera.enabled = true;         //StartCoroutine(         //screenShotCamScript.TakeScreenShot();         //);     }      public void EnableDeathScreen()     {         DeathScreen.Show();

        if (purchasedNoAds != 1)
            AdManager.instance.HideBannerAd();       }     public void DisableDeathScreen()     {         DeathScreen.Hide();     }     public void Restart()     {         adCounter++;         PlayerPrefs.SetInt( "adCounter", adCounter );

        if (adCounter > 1)
        {
            if (purchasedNoAds != 1)
                AdManager.instance.ShowInterstitial();         }
        else
        {
            adCounter = 0;
        }             SceneManager.LoadSceneAsync( 0 );     }      public void Pause()     {         HUDScreen.Hide();         pauseScreen.Show();          AdManager.instance.HideBannerAd();     }      public void Resume()     {         HUDScreen.Show();         pauseScreen.Hide();

        if (purchasedNoAds != 1)
            AdManager.instance.ShowBanerAd();     }      void OnApplicationQuit()     {         PlayerPrefs.SetInt( "Sound", isSoundOn ? 1 : 0 );     }      public void ShowPrivacyPolicyWindow()
    {
        privacyPolicy.Show();
    }     public void HidePrivacyPolicyWindow()
    {
        PlayerPrefs.SetInt("Privacy", 1);
        privacyPolicy.Hide();

    }       public void TermsAndConditionsURL()
    {
        Application.OpenURL("https://tower-tumble-0.flycricket.io/terms.html");
    }


    public void PrivacyURL()
    {
        Application.OpenURL("https://tower-tumble-0.flycricket.io/privacy.html");
    }


    //public void 




    //public void AddScore()
    //{
    //    Instantiate( ScoreAddText );
    //}

    // public void saveScreenshot()
    // {

    //     Debug.Log("save");
    //     // RenderTexture currenttex = RenderTexture.active;
    //     // RenderTexture.active = screenshot;
    //     // tex.ReadPixels(new Rect(0, 0, screenshot.height, screenshot.width), 0, 0);// you get the center section
    //     // tex.ReadPixels(screenshot,)

    //     RenderTexture.active = screenshot;
    //     tex.ReadPixels(new Rect(0, 0, screenshot.width, screenshot.height), 0, 0);
    //     tex.Apply();



    //     // RenderTexture.active = currenttex;
    //     byte[] bytes;
    //     bytes = tex.EncodeToPNG();

    //     var destination = filePath;
    //     System.IO.FileStream fileSave;
    //     fileSave = new FileStream(destination, FileMode.Create);
    //     // System.IO.File.WriteAllBytes(destination, ScreenshotData); //BinaryWriteAllBytesToFile
    //     BinaryWriter bin = new BinaryWriter(fileSave);
    //     bin.Write(bytes);
    //     fileSave.Close();

    // }





















} 