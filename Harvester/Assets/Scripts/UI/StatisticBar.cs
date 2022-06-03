using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StatisticBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI dollarsText;
    [SerializeField] private AudioClip tap;

    private void Start() {
        UpdateStatisticBar();
    }

    public void UpdateStatisticBar() {
        coinsText.text = PlayerPrefs.GetInt("CoinsAmount", 0).ToString();
        dollarsText.text = PlayerPrefs.GetInt("DollarsAmount", 0).ToString();
    }

    public void LoadShop() {
        SceneManager.LoadScene("Shop");
        SoundManager.instance.PlaySound(tap);
    }


}
