using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class InterAd : MonoBehaviour {
    private InterstitialAd interstitialAd;

#if UNITY_EDITOR
    string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

    private void Awake() {
        RequestAndLoadInterstitialAd();
    }

    public void RequestAndLoadInterstitialAd() {
        interstitialAd = new InterstitialAd(adUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(adRequest);
    }

    public void ShowAd() {
        if (interstitialAd == null) {
            RequestAndLoadInterstitialAd();
        }
        if (interstitialAd.IsLoaded()) {
            interstitialAd.Show();
            RequestAndLoadInterstitialAd();
        }
    }

}
