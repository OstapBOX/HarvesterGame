using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Storage : MonoBehaviour {
    //public static Storage instance { get; private set; }

    [Header("Amount Text")]
    [SerializeField] private StatisticBar statisticBar;
    [SerializeField] private TextMeshProUGUI wheatAmount;
    [SerializeField] private TextMeshProUGUI cornAmount;
    [SerializeField] private TextMeshProUGUI saladAmount;
    [SerializeField] private TextMeshProUGUI carrotAmount;
    [SerializeField] private TextMeshProUGUI cottonAmount;
    [SerializeField] private TextMeshProUGUI sunflowerAmount;
    [SerializeField] private TextMeshProUGUI pumpkinAmount;

    [Header("Buy Buttons")]
    [SerializeField] private GameObject wheatButton;
    [SerializeField] private GameObject cornButton;
    [SerializeField] private GameObject saladButton;
    [SerializeField] private GameObject carrotButton;
    [SerializeField] private GameObject cottonButton;
    [SerializeField] private GameObject sunflowerButton;
    [SerializeField] private GameObject pumpkinButton;

    [Header("Locks")]
    [SerializeField] private GameObject wheatLock;
    [SerializeField] private GameObject cornLock;
    [SerializeField] private GameObject saladLock;
    [SerializeField] private GameObject carrotLock;
    [SerializeField] private GameObject cottonLock;
    [SerializeField] private GameObject sunflowerLock;
    [SerializeField] private GameObject pumpkinLock;

    [Header("Locks")]
    [SerializeField] private AudioClip buy;
    [SerializeField] private AudioClip reject;
    [SerializeField] private AudioClip tap;
    [SerializeField] private AudioClip sell;



    [SerializeField] private GameObject notEnoughMoneyScreen;

    private void Start() {
        statisticBar = GameObject.Find("StatisticBarCanvas").GetComponent<StatisticBar>();
        UpdateWheatAmount();
        UpdateCornAmount();
        UpdateSaladAmount();
        UpdateCarrotAmount();
        UpdateCottonAmount();
        UpdateSunflowerAmount();
        UpdatePumpkinAmount();
        statisticBar.UpdateStatisticBar();
    }

    public void BuyWheat() {
        SoundManager.instance.PlaySound(buy);
        PlayerPrefs.SetInt("WheatBought", 1);
        Destroy(wheatButton);
        Destroy(wheatLock);
        cornButton.GetComponent<Button>().interactable = true;
    }


    public void BuyCorn() {
        SoundManager.instance.PlaySound(tap);
        if (PlayerData.instance.GetCoinsAmount() >= 100) {
            SoundManager.instance.PlaySound(buy);
            PlayerData.instance.ChangeCoinsAmount(-100);
            statisticBar.UpdateStatisticBar();

            PlayerPrefs.SetInt("CornBought", 1);
            Destroy(cornButton);
            Destroy(cornLock);
            cornAmount.enabled = true;
            saladButton.GetComponent<Button>().interactable = true;
        }
        else {
            SoundManager.instance.PlaySound(reject);
            notEnoughMoneyScreen.SetActive(true);
        }

    }

    public void BuySalad() {
        SoundManager.instance.PlaySound(tap);
        if (PlayerData.instance.GetCoinsAmount() >= 500) {
            SoundManager.instance.PlaySound(buy);
            PlayerData.instance.ChangeCoinsAmount(-500);
            statisticBar.UpdateStatisticBar();

            PlayerPrefs.SetInt("SaladBought", 1);
            Destroy(saladButton);
            Destroy(saladLock);
            saladAmount.enabled = true;
            carrotButton.GetComponent<Button>().interactable = true;
        }
        else {
            SoundManager.instance.PlaySound(reject);
            notEnoughMoneyScreen.SetActive(true);
        }
    }

    public void BuyCarrot() {
        SoundManager.instance.PlaySound(tap);
        if (PlayerData.instance.GetCoinsAmount() >= 1000) {
            SoundManager.instance.PlaySound(buy);
            PlayerData.instance.ChangeCoinsAmount(-1000);
            statisticBar.UpdateStatisticBar();

            PlayerPrefs.SetInt("CarrotBought", 1);
            Destroy(carrotButton);
            Destroy(carrotLock);
            carrotAmount.enabled = true;
            cottonButton.GetComponent<Button>().interactable = true;
        }
        else {
            SoundManager.instance.PlaySound(reject);
            notEnoughMoneyScreen.SetActive(true);
        }
    }

    public void BuyCotton() {
        SoundManager.instance.PlaySound(tap);
        if (PlayerData.instance.GetCoinsAmount() >= 2500) {
            SoundManager.instance.PlaySound(buy);
            PlayerData.instance.ChangeCoinsAmount(-2500);
            statisticBar.UpdateStatisticBar();

            PlayerPrefs.SetInt("CottonBought", 1);
            Destroy(cottonButton);
            Destroy(cottonLock);
            cottonAmount.enabled = true;
            sunflowerButton.GetComponent<Button>().interactable = true;
        }
        else {
            SoundManager.instance.PlaySound(reject);
            notEnoughMoneyScreen.SetActive(true);
        }
    }
    public void BuySunflower() {
        SoundManager.instance.PlaySound(tap);
        if (PlayerData.instance.GetCoinsAmount() >= 5000) {
            SoundManager.instance.PlaySound(buy);
            PlayerData.instance.ChangeCoinsAmount(-5000);
            statisticBar.UpdateStatisticBar();

            PlayerPrefs.SetInt("SunflowerBought", 1);
            Destroy(sunflowerButton);
            Destroy(sunflowerLock);
            sunflowerAmount.enabled = true;
            pumpkinButton.GetComponent<Button>().interactable = true;
        }
        else {
            SoundManager.instance.PlaySound(reject);
            notEnoughMoneyScreen.SetActive(true);
        }
    }
    public void BuyPumpkin() {
        SoundManager.instance.PlaySound(tap);
        if (PlayerData.instance.GetCoinsAmount() >= 10000) {
            SoundManager.instance.PlaySound(buy);
            PlayerData.instance.ChangeCoinsAmount(-10000);
            statisticBar.UpdateStatisticBar();

            PlayerPrefs.SetInt("PumpkinBought", 1);
            Destroy(pumpkinButton);
            Destroy(pumpkinLock);
            pumpkinAmount.enabled = true;
        }
        else {
            SoundManager.instance.PlaySound(reject);
            notEnoughMoneyScreen.SetActive(true);
        }
    }


    public void SellWheat() {
        if (PlayerPrefs.GetInt("WheatAmount") >= 100) {
            SoundManager.instance.PlaySound(sell);
            PlayerData.instance.ChangeWheatAmount(-100);
            PlayerData.instance.ChangeCoinsAmount(10);
            UpdateWheatAmount();
            statisticBar.UpdateStatisticBar();
        }
        else {
            SoundManager.instance.PlaySound(reject);
        }
    }
    public void SellCorn() {       
        if (PlayerPrefs.GetInt("CornAmount") >= 100) {
            SoundManager.instance.PlaySound(sell);
            PlayerData.instance.ChangeCornAmount(-100);
            PlayerData.instance.ChangeCoinsAmount(20);
            UpdateCornAmount();
            statisticBar.UpdateStatisticBar();
        }
        else {
            SoundManager.instance.PlaySound(reject);
        }
    }
    public void SellSalad() {
        if (PlayerPrefs.GetInt("SaladAmount") >= 100) {
            SoundManager.instance.PlaySound(sell);
            PlayerData.instance.ChangeSaladAmount(-100);
            PlayerData.instance.ChangeCoinsAmount(50);
            UpdateSaladAmount();
            statisticBar.UpdateStatisticBar();
        }
        else {
            SoundManager.instance.PlaySound(reject);
        }
    }
    public void SellCarrot() {
        if (PlayerPrefs.GetInt("CarrotAmount") >= 100) {
            SoundManager.instance.PlaySound(sell);
            PlayerData.instance.ChangeCarrotAmount(-100);
            PlayerData.instance.ChangeCoinsAmount(70);
            UpdateCarrotAmount();
            statisticBar.UpdateStatisticBar();
        }
        else {
            SoundManager.instance.PlaySound(reject);
        }
    }
    public void SellCotton() {
       
        if (PlayerPrefs.GetInt("CottonAmount") >= 100) {
            SoundManager.instance.PlaySound(sell);
            PlayerData.instance.ChangeCottonAmount(-100);
            PlayerData.instance.ChangeCoinsAmount(90);
            UpdateCottonAmount();
            statisticBar.UpdateStatisticBar();
        }
        else {
            SoundManager.instance.PlaySound(reject);
        }
    }
    public void SellSunflower() {
       
        if (PlayerPrefs.GetInt("SunflowerAmount") >= 100) {
            SoundManager.instance.PlaySound(sell);
            PlayerData.instance.ChangeSunflowerAmount(-100);
            PlayerData.instance.ChangeCoinsAmount(120);
            UpdateSunflowerAmount();
            statisticBar.UpdateStatisticBar();
        }
        else {
            SoundManager.instance.PlaySound(reject);
        }
    }
    public void SellPumpkin() {
        if (PlayerPrefs.GetInt("PumpkinAmount") >= 100) {
            SoundManager.instance.PlaySound(sell);
            PlayerData.instance.ChangePumpkinAmount(-100);
            PlayerData.instance.ChangeCoinsAmount(150);
            UpdatePumpkinAmount();
            statisticBar.UpdateStatisticBar();
        }
        else {
            SoundManager.instance.PlaySound(reject);
        }
    }

    private void UpdateWheatAmount() {
        wheatAmount.text = PlayerPrefs.GetInt("WheatAmount").ToString();
    }
    private void UpdateCornAmount() {
        cornAmount.text = PlayerPrefs.GetInt("CornAmount").ToString();
    }
    private void UpdateSaladAmount() {
        saladAmount.text = PlayerPrefs.GetInt("SaladAmount").ToString();
    }
    private void UpdateCarrotAmount() {
        carrotAmount.text = PlayerPrefs.GetInt("CarrotAmount").ToString();
    }
    private void UpdateCottonAmount() {
        cottonAmount.text = PlayerPrefs.GetInt("CottonAmount").ToString();
    }
    private void UpdateSunflowerAmount() {
        sunflowerAmount.text = PlayerPrefs.GetInt("SunflowerAmount").ToString();
    }
    private void UpdatePumpkinAmount() {
        pumpkinAmount.text = PlayerPrefs.GetInt("PumpkinAmount").ToString();
    }

    public void LoadMenu() {
        SoundManager.instance.PlaySound(tap);
        SceneManager.LoadScene("Menu");
    }
    public void LoadStore() {
        SoundManager.instance.PlaySound(tap);
        SceneManager.LoadScene("Shop");
    }
}
