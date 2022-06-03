using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeSystem : MonoBehaviour {

    [SerializeField] private GameObject[] upgradeLevels;
    [SerializeField] private int[] levelsPrices;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject notEnoughCoins;
    [SerializeField] private StatisticBar statisticBar;

    [SerializeField] private AudioClip upgrade;
    [SerializeField] private AudioClip reject;


    private int currentLevel;

    void Start() {
        Load();
        UpdateScene(currentLevel);
    }

    public void UpdateScene(int _level) {
        for (int i = 0; i <= _level; i++) {
            upgradeLevels[i].SetActive(true);
        }
        priceText.text = levelsPrices[_level].ToString() + "$";
        levelText.text = "Level " + _level.ToString();
        statisticBar.UpdateStatisticBar();
    }

    public void Upgrade() {
        if (PlayerData.instance.GetCoinsAmount() >= levelsPrices[currentLevel]) {
            SoundManager.instance.PlaySound(upgrade);
            PlayerData.instance.ChangeCoinsAmount(-levelsPrices[currentLevel]);
            currentLevel++;
            UpdateScene(currentLevel);
            Save();
        }
        else {
            SoundManager.instance.PlaySound(reject);
            notEnoughCoins.SetActive(true);
        }
    }

    public void Save() {
        PlayerPrefs.SetInt("FarmLevel", currentLevel);
    }

    public void Load() {
        currentLevel = PlayerPrefs.GetInt("FarmLevel", 0);
    }

}
