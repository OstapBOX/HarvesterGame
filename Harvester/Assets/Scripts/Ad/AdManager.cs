using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour {

    private int adInterval = 180;

    [SerializeField] private GameObject dollarsBtn;
    [SerializeField] private GameObject fuelBtn;
    [SerializeField] private GameObject dashBtn;
    [SerializeField] private GameObject shieldBtn;
    [SerializeField] private GameObject cultivatorBtn;

    private void Start() {
        CheckAdsToShow();
    }

    private void OnLevelWasLoaded(int level) {
        if (level == 0) {
            CheckAdsToShow();
        }
    }

    public void CheckAdsToShow() {
        DateTime lastShowedTime = StringToDate(PlayerPrefs.GetString("LastShowedTime"));
        if (DateTime.Now.AddSeconds(-adInterval) > lastShowedTime) {
            //Debug.Log("Interval");
            int lastShowedAd = PlayerPrefs.GetInt("LastShowedAd", 4);
            if (lastShowedAd == 0) {
                fuelBtn.SetActive(true);
            }
            else if (lastShowedAd == 1) {
                dashBtn.SetActive(true);
            }
            else if (lastShowedAd == 2) {
                shieldBtn.SetActive(true);
            }
            else if (lastShowedAd == 3) {
                cultivatorBtn.SetActive(true);
            }
            else {
                dollarsBtn.SetActive(true);
            }
        }
        else {
            Debug.Log("Not interval");
        }
    }

    private DateTime StringToDate(string date) {
        if (String.IsNullOrEmpty(date)) {
            return DateTime.Now;
        }
        return DateTime.Parse(date);
    }
}



