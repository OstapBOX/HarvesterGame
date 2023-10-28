using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class BannerAd : MonoBehaviour {

    //private BannerView bannerView;

    private void Start() {
        RequestBannerAd();
    }

    private void OnLevelWasLoaded(int level) {
        RequestBannerAd();
    }

    public void RequestBannerAd() {
        if (PlayerData.instance.GetRemoveAdsStatus()) {
            ShowBanner();
        }
    }

    private void ShowBanner() {
        if (Appodeal.isLoaded(Appodeal.BANNER)) {
            Appodeal.show(Appodeal.BANNER_BOTTOM);
        }
    }
}
