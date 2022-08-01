using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class RewAd : MonoBehaviour {
    //Real id ca-app-pub-4018757636499144/4918268035
    //Test id ca-app-pub-3940256099942544/5224354917

    private RewardedAd rewardedDollarsAd;
    private RewardedAd rewardedFuelAd;
    private RewardedAd rewardedDashAd;
    private RewardedAd rewardedShieldAd;
    private RewardedAd rewardedCultivatorAd;
    private RewardedAd rewardedRespawnAd;
    private RewardedAd rewardedDoubleScore;


    [SerializeField] private EnergyManager energyManager;
    [SerializeField] private StatisticBar statisticBar;
    [SerializeField] private PowerUpsAmount powerUpsAmount;
    [SerializeField] private AdManager adManager;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject dollarsBtn;
    [SerializeField] private GameObject fuelBtn;
    [SerializeField] private GameObject dashBtn;
    [SerializeField] private GameObject shieldBtn;
    [SerializeField] private GameObject cultivatorBtn;



#if UNITY_EDITOR
    string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-4018757636499144/4918268035"; 
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
        if(level == 1) {
            RequestAndLoadRespawnRewardedAd();
            RequestAndLoadDoubleScoreRewardedAd();
        }
        
    }

    //Menu ad
    public void RequestAndLoadDollarRewardedAd() {
        rewardedDollarsAd = new RewardedAd(adUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedDollarsAd.LoadAd(adRequest);

        rewardedDollarsAd.OnUserEarnedReward += (sender, args) => {
            PlayerPrefs.SetInt("LastShowedAd", 0);
            PlayerPrefs.SetString("LastShowedTime", DateTime.Now.ToString());
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
            PlayerPrefs.SetInt("LastShowedAd", 1);
            PlayerPrefs.SetString("LastShowedTime", DateTime.Now.ToString());
            energyManager.ChangeEnergyAmount(2);
            statisticBar.UpdateStatisticBar();
            fuelBtn.SetActive(false);
        };
    }

    public void RequestAndLoadDashRewardedAd() {
        rewardedDashAd = new RewardedAd(adUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedDashAd.LoadAd(adRequest);

        rewardedDashAd.OnUserEarnedReward += (sender, args) => {
            PlayerPrefs.SetInt("LastShowedAd", 2);
            PlayerPrefs.SetString("LastShowedTime", DateTime.Now.ToString());
            PlayerData.instance.ChangeSpeedUpAmount(3);
            powerUpsAmount.UpdatePowerUpsAmount();
            dashBtn.SetActive(false);
        };
    }

    public void RequestAndLoadShieldRewardedAd() {
        rewardedShieldAd = new RewardedAd(adUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedShieldAd.LoadAd(adRequest);

        rewardedShieldAd.OnUserEarnedReward += (sender, args) => {
            PlayerPrefs.SetInt("LastShowedAd", 3);
            PlayerPrefs.SetString("LastShowedTime", DateTime.Now.ToString());
            PlayerData.instance.ChangeShieldAmount(2);
            powerUpsAmount.UpdatePowerUpsAmount();
            shieldBtn.SetActive(false);
        };
    }

    public void RequestAndLoadCultivatorRewardedAd() {
        rewardedCultivatorAd = new RewardedAd(adUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedCultivatorAd.LoadAd(adRequest);

        rewardedCultivatorAd.OnUserEarnedReward += (sender, args) => {
            PlayerPrefs.SetInt("LastShowedAd", 4);
            PlayerPrefs.SetString("LastShowedTime", DateTime.Now.ToString());
            PlayerData.instance.ChangeCultivatorAmount(1);
            powerUpsAmount.UpdatePowerUpsAmount();
            cultivatorBtn.SetActive(false);
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

    //Game ad
    public void RequestAndLoadRespawnRewardedAd() {
        rewardedRespawnAd = new RewardedAd(adUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedRespawnAd.LoadAd(adRequest);

        rewardedRespawnAd.OnUserEarnedReward += (sender, args) => {
            gameManager.Respawn();
        };
    }

    public void RequestAndLoadDoubleScoreRewardedAd() {
        rewardedDoubleScore = new RewardedAd(adUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedDoubleScore.LoadAd(adRequest);

        rewardedDoubleScore.OnUserEarnedReward += (sender, args) => {
            gameManager.DoubleReward();
            gameManager.BackToMenu();
        };
    }

    public void ShowRespawnRewardedAd() {
        if (!rewardedRespawnAd.IsLoaded()) {
            RequestAndLoadShieldRewardedAd();
            rewardedRespawnAd.Show();
        }
        else {
            rewardedRespawnAd.Show();
        }
    }

    public void ShowDoubleScoreRewardedAd() {
        if (!rewardedDoubleScore.IsLoaded()) {
            RequestAndLoadCultivatorRewardedAd();
            rewardedDoubleScore.Show();
        }
        else {
            rewardedDoubleScore.Show();
           
        }
    }
}
