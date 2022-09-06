using UnityEngine;
using GameAnalyticsSDK;
using System;

public class GAManager : MonoBehaviour {
    public static GAManager instance { get; private set; }

    void Start() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        GameAnalytics.Initialize();
    }

    //Gameplay
    public void PlayerUsedDash() {
        GameAnalytics.NewDesignEvent("player_used_dash");
    }
    public void PlayerUsedShield() {
        GameAnalytics.NewDesignEvent("player_used_shield");
    }
    public void PlayerUsedCultivator() {
        GameAnalytics.NewDesignEvent("player_used_cultivator");
    }
    public void DollarRespawn() {
        GameAnalytics.NewDesignEvent("game_dollar_respawn");
    }
    public void RestartGame() {
        GameAnalytics.NewDesignEvent("game_restart");
    }

    //Tutorial
    public void TutorialSwipeRight() {
        GameAnalytics.NewDesignEvent("tutorial_swipe_right");
    }
    public void TutorialSwipeLeft() {
        GameAnalytics.NewDesignEvent("tutorial_swipe_left");
    }
    public void TutorialUsedDash() {
        GameAnalytics.NewDesignEvent("tutorial_used_dash");
    }
    public void TutorialUsedShield() {
        GameAnalytics.NewDesignEvent("tutorial_used_shield");
    }
    public void TutorialUsedCultivator() {
        GameAnalytics.NewDesignEvent("tutorial_used_cultivator");
    }
    public void TutorialBoughtWheat() {
        GameAnalytics.NewDesignEvent("tutorial_bought_wheat");
    }
    public void TutorialSelledWheat() {
        GameAnalytics.NewDesignEvent("tutorial_selled_wheat");
    }
    public void TutorialFuelpPointerShowed() {
        GameAnalytics.NewDesignEvent("fuel_pointer_showed");
    }
    public void TutorialEnergyPointerShowed() {
        GameAnalytics.NewDesignEvent("energy_pointer_showed");
    }
    public void TutorialCompleate() {
        GameAnalytics.NewDesignEvent("tutorial_compleate");
    }
    public void TutorialSkip() {
        GameAnalytics.NewDesignEvent("tutorial_skip");
    }

    //Transactions
    public void CoinsSpent(float amount) {
        GameAnalytics.NewDesignEvent("coins_spent", amount);
    }
    public void DollarsSpent(float amount) {
        GameAnalytics.NewDesignEvent("dollars_spent", amount);
    }

    //Ad
    public void ADRewDollarShowed() {
        GameAnalytics.NewDesignEvent("rew_dollar_showed");
    }
    public void ADRewFuelShowed() {
        GameAnalytics.NewDesignEvent("rew_fuel_showed");
    }
    public void ADRewDashShowed() {
        GameAnalytics.NewDesignEvent("rew_dash_showed");
    }
    public void ADRewShieldShowed() {
        GameAnalytics.NewDesignEvent("rew_shield_showed");
    }
    public void ADRewCultivatorShowed() {
        GameAnalytics.NewDesignEvent("rew_cultivator_showed");
    }
    public void ADRewRespawnShowed() {
        GameAnalytics.NewDesignEvent("rew_respawn_showed");
    }
    public void ADRewDoubleRewShowed() {
        GameAnalytics.NewDesignEvent("rew_double_showed");
    }
    public void ADInterShowed() {
        GameAnalytics.NewDesignEvent("inter_add_showed");
    }
}
