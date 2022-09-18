using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    private InterstitialAd interstitial;
    private RewardedInterstitialAd rewardedInterstitial;
    private RewardedAd rewarded;

    public static AdManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        MobileAds.Initialize(InitializationStatus => { });
    }
    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    #region Interstitial Ads

    public void HandleIntestitialAdClosed(object sender, EventArgs args)
    {
        EventManager.current.CloseInterstitialAd();
    }

    private void Interstitial_OnAdOpening(object sender, EventArgs e)
    {
        EventManager.current.OpenInterstitialAd();
    }
    
    public void RequestInterstitial()
    {
        string adUnityId = "ca-app-pub-3940256099942544/1033173712";

        if (this.interstitial != null)
            this.interstitial.Destroy();

        this.interstitial = new InterstitialAd(adUnityId);

        this.interstitial.OnAdClosed += HandleIntestitialAdClosed;

        this.interstitial.OnAdOpening += Interstitial_OnAdOpening;


        this.interstitial.LoadAd(this.CreateAdRequest());
    }

 

    public void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        else
        {
            Debug.Log("Interstitial is not ready yet");
        }
    }

    #endregion

    #region Rewarded Ads
    public void RequestRewardAd()
    {
        string adUnityId = "ca-app-pub-3940256099942544/5224354917";

        if (this.rewarded != null)
            this.rewarded.Destroy();

        this.rewarded = new RewardedAd(adUnityId);

        this.rewarded.OnAdClosed += HandleRewardedAdClosed;

        this.rewarded.OnAdOpening += OnAdOpening;

        this.rewarded.LoadAd(this.CreateAdRequest());
    }

  

    public void ShowRewardAd()
    {
        if (this.rewarded.IsLoaded())
        {
            rewarded.Show();
        }
        else
        {
            Debug.Log("Reward Ad is not ready yet");
        }
    }

    private void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        EventManager.current.CloseRewardedAd();
    }

    private void OnAdOpening(object sender, EventArgs args)
    {
        EventManager.current.OpenRewardedAd();
    }
    #endregion
}
