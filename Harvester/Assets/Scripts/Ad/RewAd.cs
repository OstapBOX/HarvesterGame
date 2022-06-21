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
    private RewardedAd rewardedDashAd;
    private RewardedAd rewardedShieldAd;
    private RewardedAd rewardedCultivatorAd;

    [SerializeField] private EnergyManager energyManager;
    [SerializeField] private StatisticBar statisticBar;
    [SerializeField] private PowerUpsAmount powerUpsAmount;
    [SerializeField] private AdManager adManager;

    [SerializeField] private GameObject dollarsBtn;
    [SerializeField] private GameObject fuelBtn;
    [SerializeField] private GameObject dashBtn;
    [SerializeField] private GameObject shieldBtn;
    [SerializeField] private GameObject cultivatorBtn;



#if UNITY_EDITOR
    string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        string adUnitId = "unexpected_platform";
#endif

    private void Awake() {
        RequesAndLoadCurrentAd();
    }

    private void OnLevelWasLoaded(int level) {
        if(level == 0) {
            RequesAndLoadCurrentAd();
        }
        
    }

    public void RequestAndLoadDollarRewardedAd() {
        rewardedDollarsAd = new RewardedAd(adUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedDollarsAd.LoadAd(adRequest);

        rewardedDollarsAd.OnUserEarnedReward += (sender, args) => {
            PlayerPrefs.SetInt("LastShowedAd", 0);
            PlayerData.instance.ChangeDollarsAmount(2);
            statisticBar.UpdateStatisticBar();
            dollarsBtn.SetActive(false);
            if (PlayerPrefs.GetInt("AdsShowed", 0) > 1) {
                PlayerPrefs.SetInt("AdsShowed", 0);
                PlayerPrefs.SetString("LastShowedTime", DateTime.Now.ToString());
            }
            else {
                adManager.CheckAdsToShow();
                RequestAndLoadFuelRewardedAd();
            }
            PlayerPrefs.SetInt("AdsShowed", PlayerPrefs.GetInt("AdsShowed", 0) + 1);
        };
    }

    public void RequestAndLoadFuelRewardedAd() {
        rewardedFuelAd = new RewardedAd(adUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedFuelAd.LoadAd(adRequest);

        rewardedFuelAd.OnUserEarnedReward += (sender, args) => {
            PlayerPrefs.SetInt("LastShowedAd", 1);
            energyManager.ChangeEnergyAmount(2);
            statisticBar.UpdateStatisticBar();
            fuelBtn.SetActive(false);
            if (PlayerPrefs.GetInt("AdsShowed", 0) > 1) {
                PlayerPrefs.SetInt("AdsShowed", 0);
                PlayerPrefs.SetString("LastShowedTime", DateTime.Now.ToString());
            }
            else {
                adManager.CheckAdsToShow();
                RequestAndLoadDashRewardedAd();
            }
            PlayerPrefs.SetInt("AdsShowed", PlayerPrefs.GetInt("AdsShowed", 0) + 1);
        };
    }

    public void RequestAndLoadDashRewardedAd() {
        rewardedDashAd = new RewardedAd(adUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedDashAd.LoadAd(adRequest);

        rewardedDashAd.OnUserEarnedReward += (sender, args) => {
            PlayerPrefs.SetInt("LastShowedAd", 2);
            PlayerData.instance.ChangeSpeedUpAmount(3);
            powerUpsAmount.UpdatePowerUpsAmount();
            dashBtn.SetActive(false);
            if (PlayerPrefs.GetInt("AdsShowed", 0) > 1) {
                PlayerPrefs.SetInt("AdsShowed", 0);
                PlayerPrefs.SetString("LastShowedTime", DateTime.Now.ToString());
            }
            else {
                adManager.CheckAdsToShow();
                RequestAndLoadShieldRewardedAd();
            }
            PlayerPrefs.SetInt("AdsShowed", PlayerPrefs.GetInt("AdsShowed", 0) + 1);
        };
    }

    public void RequestAndLoadShieldRewardedAd() {
        rewardedShieldAd = new RewardedAd(adUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedShieldAd.LoadAd(adRequest);

        rewardedShieldAd.OnUserEarnedReward += (sender, args) => {
            PlayerPrefs.SetInt("LastShowedAd", 3);
            PlayerData.instance.ChangeShieldAmount(2);
            powerUpsAmount.UpdatePowerUpsAmount();
            shieldBtn.SetActive(false);
            if (PlayerPrefs.GetInt("AdsShowed", 0) > 1) {
                PlayerPrefs.SetInt("AdsShowed", 0);
                PlayerPrefs.SetString("LastShowedTime", DateTime.Now.ToString());
            }
            else {
                adManager.CheckAdsToShow();
                RequestAndLoadCultivatorRewardedAd();
            }
            PlayerPrefs.SetInt("AdsShowed", PlayerPrefs.GetInt("AdsShowed", 0) + 1);
        };
    }

    public void RequestAndLoadCultivatorRewardedAd() {
        rewardedCultivatorAd = new RewardedAd(adUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedCultivatorAd.LoadAd(adRequest);

        rewardedCultivatorAd.OnUserEarnedReward += (sender, args) => {
            PlayerPrefs.SetInt("LastShowedAd", 4);
            PlayerData.instance.ChangeCultivatorAmount(1);
            powerUpsAmount.UpdatePowerUpsAmount();
            cultivatorBtn.SetActive(false);
            if (PlayerPrefs.GetInt("AdsShowed", 0) > 1) {
                PlayerPrefs.SetInt("AdsShowed", 0);
                PlayerPrefs.SetString("LastShowedTime", DateTime.Now.ToString());
            }
            else {
                adManager.CheckAdsToShow();
                RequestAndLoadDollarRewardedAd();
            }
            PlayerPrefs.SetInt("AdsShowed", PlayerPrefs.GetInt("AdsShowed", 0) + 1);
        };
    }

    public void ShowDollarRewardedAd() {
        if (!rewardedDollarsAd.IsLoaded()) {
            RequestAndLoadDollarRewardedAd();
            rewardedDollarsAd.Show();
        }
        else {
            rewardedDollarsAd.Show();
        }
    }

    public void ShowFuelRewardedAd() {
        if (!rewardedFuelAd.IsLoaded()) {
            RequestAndLoadFuelRewardedAd();
            rewardedFuelAd.Show();
        }
        else {
            rewardedFuelAd.Show();
        }
    }

    public void ShowDashRewardedAd() {
        if (!rewardedDashAd.IsLoaded()) {
            RequestAndLoadDashRewardedAd();
            rewardedDashAd.Show();
        }
        else {
            rewardedDashAd.Show();
        }
    }

    public void ShowShieldRewardedAd() {
        if (!rewardedShieldAd.IsLoaded()) {
            RequestAndLoadShieldRewardedAd();
            rewardedShieldAd.Show();
        }
        else {
            rewardedShieldAd.Show();
        }
    }

    public void ShowCultivatorRewardedAd() {
        if (!rewardedCultivatorAd.IsLoaded()) {
            RequestAndLoadCultivatorRewardedAd();
            rewardedCultivatorAd.Show();
        }
        else {
            rewardedCultivatorAd.Show();
        }
    }

    private void RequesAndLoadCurrentAd() {
        if (PlayerPrefs.GetInt("LastShowedAd", 4) == 0 && rewardedFuelAd == null) {
            RequestAndLoadFuelRewardedAd();
        }
        else if (PlayerPrefs.GetInt("LastShowedAd", 4) == 1 && rewardedDashAd == null) {
            RequestAndLoadDashRewardedAd();
        }
        else if (PlayerPrefs.GetInt("LastShowedAd", 4) == 2 && rewardedShieldAd == null) {
            RequestAndLoadShieldRewardedAd();
        }
        else if (PlayerPrefs.GetInt("LastShowedAd", 4) == 3 && rewardedCultivatorAd == null) {
            RequestAndLoadCultivatorRewardedAd();
        }
        else if (PlayerPrefs.GetInt("LastShowedAd", 4) == 4 && rewardedDollarsAd == null) {
            RequestAndLoadDollarRewardedAd();
        }
    }
}
