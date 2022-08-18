using UnityEngine;
using GoogleMobileAds.Api;
using Firebase.Analytics;

public class InterAd : MonoBehaviour {
    //Real id ca-app-pub-4018757636499144/5876126485
    //Test id ca-app-pub-3940256099942544/1033173712

    private InterstitialAd interstitialAd;

    private string adUnitId;


    private void Awake() {
        if (adUnitId == null) {
            adUnitId = GetAdId();
        }
        RequestAndLoadInterstitialAd();
    }

    private void OnLevelWasLoaded(int level) {
        if(adUnitId == null) {
            adUnitId = GetAdId();
        }
        RequestAndLoadInterstitialAd();
    }

    public void RequestAndLoadInterstitialAd() {
        if (interstitialAd == null || !interstitialAd.IsLoaded()) {
            interstitialAd = new InterstitialAd(adUnitId);
            AdRequest adRequest = new AdRequest.Builder().Build();
            interstitialAd.LoadAd(adRequest);
        }
    }

    public void ShowAd() {
        if (PlayerData.instance.GetRemoveAdsStatus()) {
            PlayerPrefs.SetInt("InterstitialShowed", PlayerPrefs.GetInt("InterstitialShowed", 0) + 1);
            if (PlayerPrefs.GetInt("InterstitialShowed", 0) % 10 == 0 && PlayerPrefs.GetInt("TutorialShowed") != 0 && PlayerPrefs.GetInt("InterstitialShowed", 0) > 50) {
                if (interstitialAd.IsLoaded()) {
                    interstitialAd.Show();
                    RequestAndLoadInterstitialAd();
                }
                else {
                    interstitialAd.Show();
                    RequestAndLoadInterstitialAd();
                }
                FirebaseAnalytics.LogEvent("inter_add_showed");
            }
        }

    }

    public void ShowAdInGame() {
        if (PlayerData.instance.GetRemoveAdsStatus()) {
            if (PlayerPrefs.GetInt("GamesPlayed", 0) % 2 == 0 && PlayerPrefs.GetInt("TutorialShowed") != 0) {
                if (interstitialAd.IsLoaded()) {
                    interstitialAd.Show();
                    RequestAndLoadInterstitialAd();
                }
            }
        }
    }

    public string GetAdId() {
        string adUnitId;
        if (Application.platform == RuntimePlatform.Android) {
            if (Debug.isDebugBuild) {
                adUnitId = "ca-app-pub-3940256099942544/1033173712";
            }
            else {
                adUnitId = "ca-app-pub-4018757636499144/5876126485";
            }
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer) {
            if (Debug.isDebugBuild) {
                adUnitId = "ca-app-pub-3940256099942544/4411468910";
            }
            else {
                adUnitId = "real-id-ios";
            }
        }
        else {
            adUnitId = "unexpected_platform";
        }

        return adUnitId;
    }
}