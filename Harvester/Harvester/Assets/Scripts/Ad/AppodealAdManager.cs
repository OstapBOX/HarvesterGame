using UnityEngine;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
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
        Appodeal.MuteVideosIfCallsMuted(true);
        int adTypes = AppodealAdType.Interstitial | AppodealAdType.Banner | AppodealAdType.RewardedVideo;
        Appodeal.Initialize(appKey, adTypes, this);        
    }    
}
