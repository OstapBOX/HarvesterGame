using UnityEngine;
//using GoogleMobileAds.Api;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;

public class BannerAd : MonoBehaviour {

    //Real id ca-app-pub-4018757636499144/8885433204
    //Test id ca-app-pub-3940256099942544/6300978111

    //private BannerView bannerView;

    private void Start() {
        RequestBannerAd();
    }

    private void OnLevelWasLoaded(int level) {
        RequestBannerAd();
    }

    public void RequestBannerAd() {
        //if (PlayerPrefs.GetInt("TutorialShowed") == 0) {
        //    return;
        //}

        // These ad units are configured to always serve test ads.
        //string adUnitId = GetAdId();

        if (PlayerData.instance.GetRemoveAdsStatus()) {
            //if (bannerView != null) {
            //    bannerView.Destroy();
            //}

            // Create a 320x50 banner at top of the screen
            //bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

            //AdRequest request = new AdRequest.Builder().Build();

            //bannerView.LoadAd(request);
            ShowBanner();
        }
    }

    //public string GetAdId() {
    //    string adUnitId;
    //    if (Application.platform == RuntimePlatform.Android) {
    //        if (Debug.isDebugBuild) {
    //            adUnitId = "ca-app-pub-3940256099942544/6300978111";
    //        }
    //        else {
    //            adUnitId = "ca-app-pub-4018757636499144/8885433204";
    //        }
    //    }
    //    else if (Application.platform == RuntimePlatform.IPhonePlayer) {
    //        if (Debug.isDebugBuild) {
    //            adUnitId = "ca-app-pub-3940256099942544/2934735716";
    //        }
    //        else {
    //            adUnitId = "real-id-ios";
    //        }
    //    }
    //    else {
    //        adUnitId = "unexpected_platform";
    //    }

    //    return adUnitId;
    //}

    private void ShowBanner() {
        if (Appodeal.IsLoaded(AppodealAdType.Banner)) {
            Appodeal.Show(AppodealShowStyle.BannerBottom);
        }
    }
}
