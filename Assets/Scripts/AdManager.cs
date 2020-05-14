using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.Advertisements;
using static UnityEngine.Advertisements.BannerOptions;
//using u
//using UnityEngine.Advertisements.Purc;

public class AdManager : MonoBehaviour
{

    //gameId = 3084753
    public const string bannerPlacement = "Banner";

    //public const string videoADID = "Banner";

    [SerializeField] bool testMode = false;

    public const string gameID = "3560373";

    public static AdManager instance;

    BannerOptions bannerOptions;

    BannerCallback callback;

    void Awake()
    {
        //if (instance != null)
        //{
        //    Destroy( gameObject );
        //}

        //DontDestroyOnLoad( gameObject );
        instance = this;



        Advertisement.Initialize( gameID, false );

        Advertisement.Banner.SetPosition( BannerPosition.BOTTOM_CENTER );

    }


    // Start is called before the first frame update
    void Start()
    {
        //bannerOptions.
        //callback = Test;
        //BannerOptions.BannerCallback
        //Advertisement.Initialize()


        //Debug.Log( "adsssssssssss" );
        //Advertisement.Banner.SetPosition( BannerPosition.BOTTOM_CENTER );
    }

    public void ShowBanerAd()
    {
        StartCoroutine( ShowBannerWhenReady() );
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }

    public void ShowInterstitial()
    {
        StartCoroutine( ShowInterstitialWhenReady() );
    }
    IEnumerator ShowInterstitialWhenReady()
    {
        while (!Advertisement.IsReady())
        {
            //Advertisement.Load()
            yield return new WaitForSeconds( 0.5f );
        }
        //if (Advertisement.Banner.isLoaded)
        Advertisement.Show();

    }




    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady( bannerPlacement ))
        {
            //Advertisement.Load()
            yield return new WaitForSeconds( 0.5f );
        }
        //if (Advertisement.Banner.isLoaded)
        Advertisement.Banner.Show( bannerPlacement );

    }

    public void Test()
    {

    }





    // Update is called once per frame
    void Update()
    {

    }
}
