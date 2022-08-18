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

    [SerializeField] private GameObject upgradeButton;


    private int currentLevel;

    //Add
   
    void Start() {
        Load();
        UpdateScene(currentLevel);
        if(currentLevel == levelsPrices.Length) {
            upgradeButton.SetActive(false);
        }
    }

    public void UpdateScene(int _level) {
        for (int i = 0; i <= _level; i++) {
            upgradeLevels[i].SetActive(true);
        }
        if(_level != levelsPrices.Length - 1) {
            priceText.text = levelsPrices[_level].ToString() + "$";
        }
        else {
            upgradeButton.SetActive(false);
        }
        levelText.text = _level.ToString();
        statisticBar.UpdateStatisticBar();
    }

    public void Upgrade() {
        if(currentLevel < levelsPrices.Length - 1) {
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
    }

    public void Save() {
        PlayerPrefs.SetInt("FarmLevel", currentLevel);
    }

    public void Load() {
        currentLevel = PlayerPrefs.GetInt("FarmLevel", 0);
    }

}
