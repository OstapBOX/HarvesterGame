using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class EnergyManager : MonoBehaviour {


    [SerializeField]private TextMeshProUGUI textEnergy;
    [SerializeField]private TextMeshProUGUI textTimer;

    private int maxEnergy = 10;
    private int totalEnergy = 0;

    private DateTime nextEnergyTime;
    private DateTime lastAddedTime;

    private int restoreDuration = 600;
    private bool restoring = false;

    void Awake() {
        Load();
        StartCoroutine(RestoreRoutine());
    }

    public void UseEnergy() {
        if (totalEnergy == 0) {
            return;
        }
        totalEnergy--;
        UpdateEnergy();

        if (!restoring) {
            if (totalEnergy + 1 == maxEnergy) {
                //if energy is full just now
                nextEnergyTime = AddDuration(DateTime.Now, restoreDuration);
            }
            StartCoroutine(RestoreRoutine());
        }
    }

    private IEnumerator RestoreRoutine() {
        UpdateEnergy();
        UpdateTimer();
        restoring = true;
        while (totalEnergy < maxEnergy) {
            DateTime currentTime = DateTime.Now;
            DateTime counter = nextEnergyTime;
            bool isAdding = false;
            while (currentTime > counter) {
                if (totalEnergy < maxEnergy) {
                    isAdding = true;
                    totalEnergy++;
                    DateTime timeToAdd = lastAddedTime > counter ? lastAddedTime : counter;
                    counter = AddDuration(timeToAdd, restoreDuration);
                }
                else break;
            }

            if (isAdding) {
                lastAddedTime = DateTime.Now;
                nextEnergyTime = counter;
            }
            UpdateEnergy();
            UpdateTimer();
            Save();
            yield return null;
        }
        restoring = false;
    }

    private void UpdateTimer() {
        if (totalEnergy >= maxEnergy) {
            textTimer.text = "Full";
            return;
        }
        TimeSpan t = nextEnergyTime - DateTime.Now;
        string value = String.Format("{0}:{1}", t.Minutes.ToString("00"), t.Seconds.ToString("00"));

        textTimer.text = value;
    }

    private void UpdateEnergy() {
        Save();
        textEnergy.text = totalEnergy.ToString();
    }

    private DateTime AddDuration(DateTime time, int duration) {
        return time.AddSeconds(duration);
    }

    private void Load() {
        totalEnergy = PlayerPrefs.GetInt("totalEnergy");
        nextEnergyTime = StringToDate(PlayerPrefs.GetString("nextEnergyTime"));
        lastAddedTime = StringToDate(PlayerPrefs.GetString("lastAddedTime"));
    }

    private void Save() {
        PlayerPrefs.SetInt("totalEnergy", totalEnergy);
        PlayerPrefs.SetString("nextEnergyTime", nextEnergyTime.ToString());
        PlayerPrefs.SetString("lastAddedTime", lastAddedTime.ToString());
    }

    public void ChangeEnergyAmount(int amount) {
        PlayerPrefs.SetInt("totalEnergy", PlayerPrefs.GetInt("totalEnergy") + amount);
        Load();
        UpdateEnergy();
    }

    private DateTime StringToDate(string date) {
        if (String.IsNullOrEmpty(date)) {
            return DateTime.Now;
        }

        return DateTime.Parse(date);
    }

}
