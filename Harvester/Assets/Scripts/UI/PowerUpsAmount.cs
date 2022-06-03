using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUpsAmount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedUp;
    [SerializeField] private TextMeshProUGUI shield;
    [SerializeField] private TextMeshProUGUI cultivator;

    private void Start() {
        speedUp.text = PlayerPrefs.GetInt("SpeedUpAmount", 0).ToString();
        shield.text = PlayerPrefs.GetInt("ShieldAmount", 0).ToString();
        cultivator.text = PlayerPrefs.GetInt("CultivatorAmount", 0).ToString();
    }


    public void UpdatePowerUpsAmount() {
        speedUp.text = PlayerData.instance.GetSpeedUpAmount().ToString();
        shield.text = PlayerData.instance.GetShieldAmount().ToString();
        cultivator.text = PlayerData.instance.GetCultivatorAmount().ToString();
    } 
}
