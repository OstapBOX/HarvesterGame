using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using System.Collections.Generic;
using System;
using Firebase.Analytics;
using TMPro;


public class AppodealAdManager : MonoBehaviour, IAppodealInitializationListener {

    private string appKey = "9d29e61b4e3d409b1ebdf3c986854f792557c22652266c0e";

    [SerializeField] private TextMeshProUGUI rewText;

    public void OnInitializationFinished(List<string> errors) {
        Debug.Log("Initialized");
    }

    // Start is called before the first frame update
    void Start() {
        Initialize();
    }

    private void Initialize() {
        Appodeal.muteVideosIfCallsMuted(true);
        int adTypes = Appodeal.INTERSTITIAL | Appodeal.BANNER | Appodeal.REWARDED_VIDEO;
        Appodeal.initialize(appKey, adTypes, this);
    }

    public void onInitializationFinished(List<string> errors) {
        throw new NotImplementedException();
    }
}
