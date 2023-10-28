using UnityEngine;
using Firebase.Analytics;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class InterAd : MonoBehaviour {



    private void Awake() {

    }

    private void OnLevelWasLoaded(int level) {
   
    }


    public void ShowAd() {
        if (PlayerData.instance.GetRemoveAdsStatus()) {
            PlayerPrefs.SetInt("InterstitialShowed", PlayerPrefs.GetInt("InterstitialShowed", 0) + 1);
            if (PlayerPrefs.GetInt("InterstitialShowed", 0) % 10 == 0 ){               
                Debug.Log("Interstitial Showed");
                ShowInterstitial();
                FirebaseAnalytics.LogEvent("inter_add_showed");
                GAManager.instance.ADInterShowed();
            }
        }

    }

    public void ShowAdInGame() {
        if (PlayerData.instance.GetRemoveAdsStatus()) {
            if (PlayerPrefs.GetInt("GamesPlayed", 0) % 2 == 0 && PlayerPrefs.GetInt("TutorialShowed") != 0) {
                ShowInterstitial();
            }
        }
    }

    private void ShowInterstitial() {
        if (Appodeal.isLoaded(Appodeal.INTERSTITIAL)) {
            Appodeal.show(Appodeal.INTERSTITIAL);
        }
    }
}