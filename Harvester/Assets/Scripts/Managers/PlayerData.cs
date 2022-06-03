using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
  public static PlayerData instance { get; private set; }

    //Statistic
    private void Start() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(instance != null && instance != this) {
            Destroy(this.gameObject);
        }
  
    }
    
    public void UpdateStatisticGamesPlayed() {
        PlayerPrefs.SetInt("GamesPlayed", PlayerPrefs.GetInt("GamesPlayed", 0) + 1);
    }

    public void UpdateStatisticHighestScore(int _score) {
        if (_score > PlayerPrefs.GetInt("HighScore", 0)) {
            PlayerPrefs.SetInt("HighScore", _score);
        }
    }

    public void UpdateStatisticWheatCollected(int _score) {
        PlayerPrefs.SetInt("WheatCollected", PlayerPrefs.GetInt("WheatCollected", 0) + _score);
    }

    public void UpdateStatisticCanisterCollected() {
        PlayerPrefs.SetInt("CanisterCollected", PlayerPrefs.GetInt("CanisterCollected", 0) + 1);
    }

    public void UpdateStatisticStoneCollected() {
        PlayerPrefs.SetInt("StoneCollected", PlayerPrefs.GetInt("StoneCollected", 0) + 1);
    }

    public void UpdateStatisticHealCollected() {
        PlayerPrefs.SetInt("HealCollected", PlayerPrefs.GetInt("HealCollected", 0) + 1);
    }

    //Storage 
    public void ChangeWheatAmount(int amount) {
        PlayerPrefs.SetInt("WheatAmount", PlayerPrefs.GetInt("WheatAmount", 0) + amount);
    }

    public void ChangeCarrotAmount(int amount) {
        PlayerPrefs.SetInt("CarrotAmount", PlayerPrefs.GetInt("CarrotAmount", 0) + amount);
    }

    public void ChangeCornAmount(int amount) {
        PlayerPrefs.SetInt("CornAmount", PlayerPrefs.GetInt("CornAmount", 0) + amount);
    }

    public void ChangeCottonAmount(int amount) {
        PlayerPrefs.SetInt("CottonAmount", PlayerPrefs.GetInt("CottonAmount", 0) + amount);
    }

    public void ChangePumpkinAmount(int amount) {
        PlayerPrefs.SetInt("PumpkinAmount", PlayerPrefs.GetInt("PumpkinAmount", 0) + amount);
    }

    public void ChangeSaladAmount(int amount) {
        PlayerPrefs.SetInt("SaladAmount", PlayerPrefs.GetInt("SaladAmount", 0) + amount);
    }

    public void ChangeSunflowerAmount(int amount) {
        PlayerPrefs.SetInt("SunflowerAmount", PlayerPrefs.GetInt("SunflowerAmount", 0) + amount);
    }

    public void UpdateStatisticTime(int _minutes, int _seconds) {
        if (_minutes >= PlayerPrefs.GetInt("Minutes")) {
            PlayerPrefs.SetInt("Minutes", _minutes);
            if (PlayerPrefs.GetInt("Seconds") == 59) {
                PlayerPrefs.SetInt("Seconds", 0);
            }
            if (_seconds >= PlayerPrefs.GetInt("Seconds")) {
                PlayerPrefs.SetInt("Seconds", _seconds);
            }
        }
    }

    //Currency
    public void ChangeCoinsAmount(int amount) {
        PlayerPrefs.SetInt("CoinsAmount", PlayerPrefs.GetInt("CoinsAmount", 0) + amount);
    }

    public void ChangeDollarsAmount(int amount) {
        PlayerPrefs.SetInt("DollarsAmount", PlayerPrefs.GetInt("DollarsAmount", 0) + amount);
    }

    public int GetCoinsAmount() {
        return PlayerPrefs.GetInt("CoinsAmount", 0);
    }

    public int GetDollarsAmount() {
        return PlayerPrefs.GetInt("DollarsAmount", 0);
    }

    //PowerUps


    public void ChangeSpeedUpAmount(int amount) {
        PlayerPrefs.SetInt("SpeedUpAmount", PlayerPrefs.GetInt("SpeedUpAmount", 0) + amount);
    }

    public void ChangeShieldAmount(int amount) {
        PlayerPrefs.SetInt("ShieldAmount", PlayerPrefs.GetInt("ShieldAmount", 0) + amount);
    }

    public void ChangeCultivatorAmount(int amount) {
        PlayerPrefs.SetInt("CultivatorAmount", PlayerPrefs.GetInt("CultivatorAmount", 0) + amount);
    }

    public int GetSpeedUpAmount() {
        return PlayerPrefs.GetInt("SpeedUpAmount", 0);
    }
   
    public int GetShieldAmount() {
        return PlayerPrefs.GetInt("ShieldAmount", 0);
    }
   
    public int GetCultivatorAmount() {
        return PlayerPrefs.GetInt("CultivatorAmount", 0);
    }

}
