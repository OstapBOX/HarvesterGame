using UnityEngine;
using System;
using Firebase.Analytics;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class RewAd : MonoBehaviour, IRewardedVideoAdListener {

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
        Appodeal.setRewardedVideoCallbacks(this);        
    }
 

    public void ShowDollarRewardedAd() {
       
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO)) {
            Appodeal.show(Appodeal.REWARDED_VIDEO, "dollarAd");
            rewardIndex = 1;
        }

    }

    public void ShowFuelRewardedAd() {       

        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO)) {
            Appodeal.show(Appodeal.REWARDED_VIDEO, "fuelAd");
            rewardIndex = 2;
        }
    }

    public void ShowDashRewardedAd() {        

        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO)) {
            Appodeal.show(Appodeal.REWARDED_VIDEO, "dashAd");
            rewardIndex = 3;
        }

    }

    public void ShowShieldRewardedAd() {

        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO)) {
            Appodeal.show(Appodeal.REWARDED_VIDEO, "shieldAd");
            rewardIndex = 4;
        }

    }

    public void ShowCultivatorRewardedAd() {       

        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO)) {
            Appodeal.show(Appodeal.REWARDED_VIDEO, "cultivatorAd");
            rewardIndex = 5;
        }
    }

    //Game ad
    public void RequestAndLoadRespawnRewardedAd() {

    }

    public void RequestAndLoadDoubleScoreRewardedAd() {

    }

    public void ShowRespawnRewardedAd() {
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO)) {
            Appodeal.show(Appodeal.REWARDED_VIDEO, "respawnAd");
            rewardIndex = 6;
        }

    }

    public void ShowDoubleScoreRewardedAd() {
      
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO)) {
            Appodeal.show(Appodeal.REWARDED_VIDEO, "doubleReward");
            rewardIndex = 7;
        }
    }


    public void onRewardedVideoLoaded(bool precache) {
        throw new NotImplementedException();
    }

    public void onRewardedVideoFailedToLoad() {
        throw new NotImplementedException();
    }

    public void onRewardedVideoShowFailed() {
        throw new NotImplementedException();
    }

    public void onRewardedVideoShown() {
        throw new NotImplementedException();
    }

    public void onRewardedVideoFinished(double amount, string name) {
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

    public void onRewardedVideoClosed(bool finished) {
        throw new NotImplementedException();
    }

    public void onRewardedVideoExpired() {
        throw new NotImplementedException();
    }

    public void onRewardedVideoClicked() {
        throw new NotImplementedException();
    }
}
