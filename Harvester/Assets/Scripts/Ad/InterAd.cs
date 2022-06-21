using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Collections;

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

    private void OnLevelWasLoaded(int level) {
        RequestAndLoadInterstitialAd();
    }

    public void RequestAndLoadInterstitialAd() {
        if(interstitialAd == null || !interstitialAd.IsLoaded()) {
            interstitialAd = new InterstitialAd(adUnitId);
            AdRequest adRequest = new AdRequest.Builder().Build();
            interstitialAd.LoadAd(adRequest);
        }       
    }

    public void ShowAd() {
        PlayerPrefs.SetInt("InterstitialShowed", PlayerPrefs.GetInt("InterstitialShowed", 0) + 1);
        if (PlayerPrefs.GetInt("InterstitialShowed", 0) % 4 == 0 && PlayerPrefs.GetInt("TutorialShowed") != 0) {
            if (interstitialAd.IsLoaded()) {
            interstitialAd.Show();
            RequestAndLoadInterstitialAd();
        }
        else {
            interstitialAd.Show();
            RequestAndLoadInterstitialAd();
        }
        }
    }

    public void ShowAdInGame() {
        if (PlayerPrefs.GetInt("GamesPlayed", 0) % 2 == 0 && PlayerPrefs.GetInt("TutorialShowed") != 0) {
            if (interstitialAd.IsLoaded()) {
            interstitialAd.Show();
            RequestAndLoadInterstitialAd();
        }
        }
    }

    //private IEnumerator ShowAdCo() {
    //    PlayerPrefs.SetInt("InterstitialShowed", PlayerPrefs.GetInt("InterstitialShowed", 0) + 1);
    //    yield return new WaitForSeconds(0.2f);
    //    if (PlayerPrefs.GetInt("InterstitialShowed", 0) % 4 == 0 && PlayerPrefs.GetInt("TutorialShowed") != 0) {
    //        if (interstitialAd.IsLoaded()) {
    //            interstitialAd.Show();
    //            RequestAndLoadInterstitialAd();
    //        }
    //    }
    //}

}
