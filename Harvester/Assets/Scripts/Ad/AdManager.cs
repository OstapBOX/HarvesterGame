using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour {

    private int adInterval = 30;
    [SerializeField] private GameObject dollarsBtn;

    private void Start() {
        CheckDollarsButton();
    }

    private void OnLevelWasLoaded(int level) {
        CheckDollarsButton();
    }

    private void CheckDollarsButton() {
        DateTime lastCoinsShowedTime = StringToDate(PlayerPrefs.GetString("LastCoinsShowedTime", null));
        TimeSpan interval = DateTime.Now - lastCoinsShowedTime;
        Debug.Log("Interval: " + interval.Seconds);
        if (interval.Seconds > adInterval) {
            dollarsBtn.SetActive(true);
        }
        else {
            Debug.Log("Wait more");
        }
    }

    private DateTime StringToDate(string date) {
        if (String.IsNullOrEmpty(date)) {
            return DateTime.Now;
        }

        return DateTime.Parse(date);
    }
}



