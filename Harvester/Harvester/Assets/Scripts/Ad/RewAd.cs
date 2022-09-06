using UnityEngine;
using GoogleMobileAds.Api;
using System;
using Firebase.Analytics;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;

public class RewAd : MonoBehaviour, IRewardedVideoAdListener {
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

    private string adUnitId;

    private int rewardIndex;

    private void Start() {
        Appodeal.SetRewardedVideoCallbacks(this);
        //if (adUnitId == null) {
        //    adUnitId = GetAdId();
        //}
        //RequesAndLoadCurrentAd();
    }

    private void OnLevelWasLoaded(int level) {
        //if (adUnitId == null) {
        //    adUnitId = GetAdId();
        //}
        //if (level == 0) {
        //    RequesAndLoadCurrentAd();
        //}
        //if(level == 1) {
        //    RequestAndLoadRespawnRewardedAd();
        //    RequestAndLoadDoubleScoreRewardedAd();
        //}

    }

    //Menu ad
    public void RequestAndLoadDollarRewardedAd() {
        //rewardedDollarsAd = new RewardedAd(adUnitId);
        //AdRequest adRequest = new AdRequest.Builder().Build();
        //rewardedDollarsAd.LoadAd(adRequest);

        //rewardedDollarsAd.OnUserEarnedReward += (sender, args) => {
        //    PlayerPrefs.SetInt("LastShowedAd", 0);
        //    PlayerPrefs.SetString("LastShowedTime", DateTime.Now.ToString());
        //    PlayerData.instance.ChangeDollarsAmount(2);
        //    statisticBar.UpdateStatisticBar();
        //    dollarsBtn.SetActive(false);
        //    FirebaseAnalytics.LogEvent("rew_dollar_showed");
        //    GAManager.instance.ADRewDollarShowed();
        //};
    }

    public void RequestAndLoadFuelRewardedAd() {
        //rewardedFuelAd = new RewardedAd(adUnitId);
        //AdRequest adRequest = new AdRequest.Builder().Build();
        //rewardedFuelAd.LoadAd(adRequest);

        //rewardedFuelAd.OnUserEarnedReward += (sender, args) => {
        //    PlayerPrefs.SetInt("LastShowedAd", 1);
        //    PlayerPrefs.SetString("LastShowedTime", DateTime.Now.ToString());
        //    energyManager.ChangeEnergyAmount(2);
        //    statisticBar.UpdateStatisticBar();
        //    fuelBtn.SetActive(false);
        //    FirebaseAnalytics.LogEvent("rew_fuel_showed");
        //    GAManager.instance.ADRewFuelShowed();
        //};
    }

    public void RequestAndLoadDashRewardedAd() {
        //rewardedDashAd = new RewardedAd(adUnitId);
        //AdRequest adRequest = new AdRequest.Builder().Build();
        //rewardedDashAd.LoadAd(adRequest);

        //rewardedDashAd.OnUserEarnedReward += (sender, args) => {
        //    PlayerPrefs.SetInt("LastShowedAd", 2);
        //    PlayerPrefs.SetString("LastShowedTime", DateTime.Now.ToString());
        //    PlayerData.instance.ChangeSpeedUpAmount(3);
        //    powerUpsAmount.UpdatePowerUpsAmount();
        //    dashBtn.SetActive(false);
        //    FirebaseAnalytics.LogEvent("rew_dash_showed");
        //    GAManager.instance.ADRewDashShowed();
        //};
    }

    public void RequestAndLoadShieldRewardedAd() {
        //rewardedShieldAd = new RewardedAd(adUnitId);
        //AdRequest adRequest = new AdRequest.Builder().Build();
        //rewardedShieldAd.LoadAd(adRequest);

        //rewardedShieldAd.OnUserEarnedReward += (sender, args) => {
        //    PlayerPrefs.SetInt("LastShowedAd", 3);
        //    PlayerPrefs.SetString("LastShowedTime", DateTime.Now.ToString());
        //    PlayerData.instance.ChangeShieldAmount(2);
        //    powerUpsAmount.UpdatePowerUpsAmount();
        //    shieldBtn.SetActive(false);
        //    FirebaseAnalytics.LogEvent("rew_shield_showed");
        //    GAManager.instance.ADRewShieldShowed();
        //};
    }

    public void RequestAndLoadCultivatorRewardedAd() {
        //rewardedCultivatorAd = new RewardedAd(adUnitId);
        //AdRequest adRequest = new AdRequest.Builder().Build();
        //rewardedCultivatorAd.LoadAd(adRequest);

        //rewardedCultivatorAd.OnUserEarnedReward += (sender, args) => {
        //    PlayerPrefs.SetInt("LastShowedAd", 4);
        //    PlayerPrefs.SetString("LastShowedTime", DateTime.Now.ToString());
        //    PlayerData.instance.ChangeCultivatorAmount(1);
        //    powerUpsAmount.UpdatePowerUpsAmount();
        //    cultivatorBtn.SetActive(false);
        //    FirebaseAnalytics.LogEvent("rew_cultivator_showed");
        //    GAManager.instance.ADRewCultivatorShowed();
        //};
    }

    public void ShowDollarRewardedAd() {
        if (Appodeal.CanShow(AppodealAdType.RewardedVideo, "dollarAd")) {
            Appodeal.Show(AppodealShowStyle.RewardedVideo, "dollarAd");
            rewardIndex = 1;
        }

        //if (!rewardedDollarsAd.IsLoaded()) {
        //    RequestAndLoadDollarRewardedAd();
        //    rewardedDollarsAd.Show();
        //}
        //else {
        //    rewardedDollarsAd.Show();
        //}
    }

    public void ShowFuelRewardedAd() {
        if (Appodeal.CanShow(AppodealAdType.RewardedVideo, "fuelAd")) {
            Appodeal.Show(AppodealShowStyle.RewardedVideo, "fuelAd");
            rewardIndex = 2;
        }

        //if (!rewardedFuelAd.IsLoaded()) {
        //    RequestAndLoadFuelRewardedAd();
        //    rewardedFuelAd.Show();
        //}
        //else {
        //    rewardedFuelAd.Show();
        //}
    }

    public void ShowDashRewardedAd() {
        if (Appodeal.CanShow(AppodealAdType.RewardedVideo, "dashAd")) {
            Appodeal.Show(AppodealShowStyle.RewardedVideo, "dashAd");
            rewardIndex = 3;
        }

        //if (!rewardedDashAd.IsLoaded()) {
        //    RequestAndLoadDashRewardedAd();
        //    rewardedDashAd.Show();
        //}
        //else {
        //    rewardedDashAd.Show();
        //}
    }

    public void ShowShieldRewardedAd() {
        if (Appodeal.CanShow(AppodealAdType.RewardedVideo, "shieldAd")) {
            Appodeal.Show(AppodealShowStyle.RewardedVideo, "shieldAd");
            rewardIndex = 4;
        }

        //if (!rewardedShieldAd.IsLoaded()) {
        //    RequestAndLoadShieldRewardedAd();
        //    rewardedShieldAd.Show();
        //}
        //else {
        //    rewardedShieldAd.Show();
        //}
    }

    public void ShowCultivatorRewardedAd() {

        if (Appodeal.CanShow(AppodealAdType.RewardedVideo, "cultivatorAd")) {
            Appodeal.Show(AppodealShowStyle.RewardedVideo, "cultivatorAd");
            rewardIndex = 5;
        }

        //if (!rewardedCultivatorAd.IsLoaded()) {
        //    RequestAndLoadCultivatorRewardedAd();
        //    rewardedCultivatorAd.Show();
        //}
        //else {
        //    rewardedCultivatorAd.Show();
        //}
    }

    //private void RequesAndLoadCurrentAd() {
    //    if (PlayerPrefs.GetInt("LastShowedAd", 4) == 0 && rewardedFuelAd == null) {
    //        RequestAndLoadFuelRewardedAd();
    //    }
    //    else if (PlayerPrefs.GetInt("LastShowedAd", 4) == 1 && rewardedDashAd == null) {
    //        RequestAndLoadDashRewardedAd();
    //    }
    //    else if (PlayerPrefs.GetInt("LastShowedAd", 4) == 2 && rewardedShieldAd == null) {
    //        RequestAndLoadShieldRewardedAd();
    //    }
    //    else if (PlayerPrefs.GetInt("LastShowedAd", 4) == 3 && rewardedCultivatorAd == null) {
    //        RequestAndLoadCultivatorRewardedAd();
    //    }
    //    else if (PlayerPrefs.GetInt("LastShowedAd", 4) == 4 && rewardedDollarsAd == null) {
    //        RequestAndLoadDollarRewardedAd();
    //    }
    //}

    //Game ad
    public void RequestAndLoadRespawnRewardedAd() {

        //rewardedRespawnAd = new RewardedAd(adUnitId);
        //AdRequest adRequest = new AdRequest.Builder().Build();
        //rewardedRespawnAd.LoadAd(adRequest);

        //rewardedRespawnAd.OnUserEarnedReward += (sender, args) => {
        //    gameManager.Respawn();
        //    FirebaseAnalytics.LogEvent("player_video_respawn");
        //    GAManager.instance.ADRewRespawnShowed();
        //};
    }

    public void RequestAndLoadDoubleScoreRewardedAd() {


        //rewardedDoubleScore = new RewardedAd(adUnitId);
        //AdRequest adRequest = new AdRequest.Builder().Build();
        //rewardedDoubleScore.LoadAd(adRequest);

        //rewardedDoubleScore.OnUserEarnedReward += (sender, args) => {
        //    gameManager.DoubleReward();
        //    gameManager.BackToMenu();
        //    FirebaseAnalytics.LogEvent("rew_double_reward");
        //    GAManager.instance.ADRewDoubleRewShowed();
        //};
    }

    public void ShowRespawnRewardedAd() {
        if (Appodeal.CanShow(AppodealAdType.RewardedVideo, "respawnAd")) {
            Appodeal.Show(AppodealShowStyle.RewardedVideo, "respawnAd");
            rewardIndex = 6;
        }
        //if (!rewardedRespawnAd.IsLoaded()) {
        //    RequestAndLoadShieldRewardedAd();
        //    rewardedRespawnAd.Show();
        //}
        //else {
        //    rewardedRespawnAd.Show();
        //}
    }

    public void ShowDoubleScoreRewardedAd() {
        if (Appodeal.CanShow(AppodealAdType.RewardedVideo, "doubleReward")) {
            Appodeal.Show(AppodealShowStyle.RewardedVideo, "doubleReward");
            rewardIndex = 7;
        }
        //if (!rewardedDoubleScore.IsLoaded()) {
        //    RequestAndLoadCultivatorRewardedAd();
        //    rewardedDoubleScore.Show();
        //}
        //else {
        //    rewardedDoubleScore.Show();

        //}
    }

    //public string GetAdId() {
    //    string adUnitId;
    //    if (Application.platform == RuntimePlatform.Android) {
    //        if (Debug.isDebugBuild) {
    //            adUnitId = "ca-app-pub-3940256099942544/5224354917";
    //        }
    //        else {
    //            adUnitId = "ca-app-pub-4018757636499144/4918268035";
    //        }
    //    }
    //    else if (Application.platform == RuntimePlatform.IPhonePlayer) {
    //        if (Debug.isDebugBuild) {
    //            adUnitId = "ca-app-pub-3940256099942544/1712485313";
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

    public void OnRewardedVideoLoaded(bool isPrecache) {
        Debug.Log("On Rewarded Video Loaded");
    }

    public void OnRewardedVideoFailedToLoad() {
        Debug.Log("On Rewarded Video Faild To Load");
    }

    public void OnRewardedVideoShowFailed() {
        Debug.Log("On Rewarded Video Show Failed");
    }

    public void OnRewardedVideoShown() {
        Debug.Log("On Rewarded Video Shown");
    }

    public void OnRewardedVideoFinished(double amount, string currency) {
        Debug.Log("VIDEO FINISHED");
        UnityMainThreadDispatcher.Instance().Enqueue(() => {
            if (rewardIndex == 1) {
                PlayerPrefs.SetInt("LastShowedAd", 0);
                PlayerPrefs.SetString("LastShowedTime", DateTime.Now.ToString());
                PlayerData.instance.ChangeDollarsAmount(2);
                statisticBar.UpdateStatisticBar();
                dollarsBtn.SetActive(false);
                FirebaseAnalytics.LogEvent("rew_dollar_showed");
                GAManager.instance.ADRewDollarShowed();
            }
            else if (rewardIndex == 2) {
                PlayerPrefs.SetInt("LastShowedAd", 1);
                PlayerPrefs.SetString("LastShowedTime", DateTime.Now.ToString());
                energyManager.ChangeEnergyAmount(2);
                statisticBar.UpdateStatisticBar();
                fuelBtn.SetActive(false);
                FirebaseAnalytics.LogEvent("rew_fuel_showed");
                GAManager.instance.ADRewFuelShowed();
            }
            else if (rewardIndex == 3) {
                PlayerPrefs.SetInt("LastShowedAd", 2);
                PlayerPrefs.SetString("LastShowedTime", DateTime.Now.ToString());
                PlayerData.instance.ChangeSpeedUpAmount(3);
                powerUpsAmount.UpdatePowerUpsAmount();
                dashBtn.SetActive(false);
                FirebaseAnalytics.LogEvent("rew_dash_showed");
                GAManager.instance.ADRewDashShowed();
            }
            else if (rewardIndex == 4) {
                PlayerPrefs.SetInt("LastShowedAd", 3);
                PlayerPrefs.SetString("LastShowedTime", DateTime.Now.ToString());
                PlayerData.instance.ChangeShieldAmount(2);
                powerUpsAmount.UpdatePowerUpsAmount();
                shieldBtn.SetActive(false);
                FirebaseAnalytics.LogEvent("rew_shield_showed");
                GAManager.instance.ADRewShieldShowed();
            }
            else if (rewardIndex == 5) {
                PlayerPrefs.SetInt("LastShowedAd", 4);
                PlayerPrefs.SetString("LastShowedTime", DateTime.Now.ToString());
                PlayerData.instance.ChangeCultivatorAmount(1);
                powerUpsAmount.UpdatePowerUpsAmount();
                cultivatorBtn.SetActive(false);
                FirebaseAnalytics.LogEvent("rew_cultivator_showed");
                GAManager.instance.ADRewCultivatorShowed();
            }
            else if (rewardIndex == 6) {
                gameManager.Respawn();
                FirebaseAnalytics.LogEvent("player_video_respawn");
                GAManager.instance.ADRewRespawnShowed();
            }
            else if (rewardIndex == 7) {
                gameManager.DoubleReward();
                gameManager.BackToMenu();
                FirebaseAnalytics.LogEvent("rew_double_reward");
                GAManager.instance.ADRewDoubleRewShowed();
            }
            else {
                Debug.Log("UNEXPECTED REWARD");
            }
        });       
    }

    public void OnRewardedVideoClosed(bool finished) {
        Debug.Log("On Rewarded Video Clodsed");
    }

    public void OnRewardedVideoExpired() {
        Debug.Log("On Rewarded Video Expired");
    }

    public void OnRewardedVideoClicked() {
        Debug.Log("On Rewarded Video ");
    }
}
