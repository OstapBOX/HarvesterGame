using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class RewAd : MonoBehaviour {

    private RewardedAd rewardedDollarsAd;
    private RewardedAd rewardedFuelAd;


    [SerializeField] private EnergyManager energyManager;
    [SerializeField] private StatisticBar statisticBar;

    [SerializeField] private GameObject dollarsBtn;
    [SerializeField] private GameObject fuelBtn;


#if UNITY_EDITOR
    string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        string adUnitId = "unexpected_platform";
#endif

    private void Start() {
        RequestAndLoadDollarRewardedAd();
        RequestAndLoadFuelRewardedAd();
    }

    public void RequestAndLoadDollarRewardedAd() {
        rewardedDollarsAd = new RewardedAd(adUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedDollarsAd.LoadAd(adRequest);

        rewardedDollarsAd.OnUserEarnedReward += (sender, args) => {
            PlayerData.instance.ChangeDollarsAmount(2);
            statisticBar.UpdateStatisticBar();
            dollarsBtn.SetActive(false);
        };
    }

    public void RequestAndLoadFuelRewardedAd() {
        rewardedFuelAd = new RewardedAd(adUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedFuelAd.LoadAd(adRequest);

        rewardedFuelAd.OnUserEarnedReward += (sender, args) => {
            energyManager.ChangeEnergyAmount(2);
            statisticBar.UpdateStatisticBar();
            fuelBtn.SetActive(false);
        };
    }

    public void ShowDollarRewardedAd() {
        if (rewardedDollarsAd == null) {
            RequestAndLoadDollarRewardedAd();
        }
        if (rewardedDollarsAd != null) {
            rewardedDollarsAd.Show();
            RequestAndLoadDollarRewardedAd();
        }
        else {
            Debug.Log("Rewarded ad is not ready yet.");
        }
    }

    public void ShowFuelRewardedAd() {
        if (rewardedFuelAd == null) {
            RequestAndLoadFuelRewardedAd();
        }
        if (rewardedFuelAd != null) {
            rewardedFuelAd.Show();
            RequestAndLoadFuelRewardedAd();
        }
        else {
            Debug.Log("Rewarded ad is not ready yet.");
        }
    }
}
