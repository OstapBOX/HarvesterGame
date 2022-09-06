using UnityEngine;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using System.Collections.Generic;
using System;
using Firebase.Analytics;
using TMPro;


public class AppodealAdManager : MonoBehaviour, IAppodealInitializationListener {

    private string appKey = "a59208440d8e60453cfc5fda5f3c81583248fb01f37e6741";

    [SerializeField] private TextMeshProUGUI rewText;

    public void OnInitializationFinished(List<string> errors) {
        Debug.Log("Initialized");
    }

    // Start is called before the first frame update
    void Start() {
        Initialize();
    }

    private void Initialize() {
        Appodeal.MuteVideosIfCallsMuted(true);
        int adTypes = AppodealAdType.Interstitial | AppodealAdType.Banner | AppodealAdType.RewardedVideo;
        Appodeal.Initialize(appKey, adTypes, this);        
    }    
}
