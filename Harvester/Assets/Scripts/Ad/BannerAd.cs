using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class BannerAd : MonoBehaviour {
    private BannerView bannerView;

    private void Start() {
        RequestBannerAd();
    }

    private void OnLevelWasLoaded(int level) {
        RequestBannerAd();
    }

    public void RequestBannerAd() {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif
        if (PlayerData.instance.GetRemoveAdsStatus()) {
            
            if (bannerView != null) {
                bannerView.Destroy();
            }

            // Create a 320x50 banner at top of the screen
            bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

            AdRequest request = new AdRequest.Builder().Build();

            bannerView.LoadAd(request);
        }
    }
}
