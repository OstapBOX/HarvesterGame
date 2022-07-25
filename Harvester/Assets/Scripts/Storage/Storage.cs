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


    [Header("Buy Screens")]
    [SerializeField] private GameObject wheatScreen;
    [SerializeField] private GameObject cornScreen;
    [SerializeField] private GameObject saladScreen;
    [SerializeField] private GameObject carrotScreen;
    [SerializeField] private GameObject cottonScreen;
    [SerializeField] private GameObject sunflowerScreen;
    [SerializeField] private GameObject pumpkinScreen;

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
    [SerializeField] private GameObject farmLevelRequired;
    [SerializeField] private TextMeshProUGUI farmlevel;

    private int cornLevel = 3;
    private int saladLevel = 6;
    private int carrotLevel = 9;
    private int cottonLevel = 12;
    private int sunflowerLevel = 15;
    private int pumpkinLevel = 18;

    private int cornPrice = 500;
    private int saladPrice = 2500;
    private int carrotPrice = 4000;
    private int cottonPrice = 6000;
    private int sunflowerPrice = 9000;
    private int pumpkinPrice = 13000;

    //Add
    //private InterAd interAd;

    private void Start() {
        //interAd = GetComponent<InterAd>();
        //interAd.RequestAndLoadInterstitialAd();
        //interAd.ShowAd();

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
        if (PlayerData.instance.GetCoinsAmount() >= cornPrice) {
            SoundManager.instance.PlaySound(buy);
            PlayerData.instance.ChangeCoinsAmount(-cornPrice);
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

        if (PlayerData.instance.GetCoinsAmount() >= saladPrice) {
            SoundManager.instance.PlaySound(buy);
            PlayerData.instance.ChangeCoinsAmount(-saladPrice);
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

        if (PlayerData.instance.GetCoinsAmount() >= carrotPrice) {
            SoundManager.instance.PlaySound(buy);
            PlayerData.instance.ChangeCoinsAmount(-carrotPrice);
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

        if (PlayerData.instance.GetCoinsAmount() >= cottonPrice) {
            SoundManager.instance.PlaySound(buy);
            PlayerData.instance.ChangeCoinsAmount(-cottonPrice);
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
        if (PlayerData.instance.GetCoinsAmount() >= sunflowerPrice) {
            SoundManager.instance.PlaySound(buy);
            PlayerData.instance.ChangeCoinsAmount(-sunflowerPrice);
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
        if (PlayerData.instance.GetCoinsAmount() >= pumpkinPrice) {
            SoundManager.instance.PlaySound(buy);
            PlayerData.instance.ChangeCoinsAmount(-pumpkinPrice);
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

    public void SellPlant(string plantPrefbName) {
        if (PlayerPrefs.GetInt(plantPrefbName, 0) > 0) {
            float onePlantPrice = 0.0f;
            int plantAmount = PlayerPrefs.GetInt(plantPrefbName, 0);
            SoundManager.instance.PlaySound(sell);            

            if (plantPrefbName == "WheatAmount") {
                PlayerData.instance.ChangeWheatAmount(-plantAmount);
                UpdateWheatAmount();
                onePlantPrice = 0.1f;
            }
            else if (plantPrefbName == "CornAmount") {
                PlayerData.instance.ChangeCornAmount(-plantAmount);
                UpdateCornAmount();
                onePlantPrice = 0.2f;
            }
            else if (plantPrefbName == "SaladAmount") {
                PlayerData.instance.ChangeSaladAmount(-plantAmount);
                UpdateSaladAmount();
                onePlantPrice = 0.5f;
            }
            else if (plantPrefbName == "CarrotAmount") {
                PlayerData.instance.ChangeCarrotAmount(-plantAmount);
                UpdateCarrotAmount();
                onePlantPrice = 0.7f;
            }
            else if (plantPrefbName == "CottonAmount") {
                PlayerData.instance.ChangeCottonAmount(-plantAmount);
                UpdateCottonAmount();
                onePlantPrice = 0.9f;
            }
            else if (plantPrefbName == "SunflowerAmount") {
                PlayerData.instance.ChangeSunflowerAmount(-plantAmount);
                UpdateSunflowerAmount();
                onePlantPrice = 1.2f;
            }
            else if (plantPrefbName == "PumpkinAmount") {
                PlayerData.instance.ChangePumpkinAmount(-plantAmount);
                UpdatePumpkinAmount();
                onePlantPrice = 1.5f;
            }
            else {
                Debug.Log("This plant dosen't exist");
            }

            float moneyAmount = onePlantPrice * plantAmount;
            PlayerData.instance.ChangeCoinsAmount(Mathf.RoundToInt(moneyAmount));

            statisticBar.UpdateStatisticBar();
        }
        else {
            SoundManager.instance.PlaySound(reject);
        }
    }



    public void CheckLevelCorn() {
        if (PlayerPrefs.GetInt("FarmLevel", 0) >= cornLevel) {
            SoundManager.instance.PlaySound(tap);
            cornScreen.SetActive(true);
        }
        else {
            SoundManager.instance.PlaySound(reject);
            farmLevelRequired.SetActive(true);
            farmlevel.text = cornLevel.ToString();
        }
    }

    public void CheckLevelSalad() {
        if (PlayerPrefs.GetInt("FarmLevel", 0) >= saladLevel) {
            SoundManager.instance.PlaySound(tap);
            saladScreen.SetActive(true);
        }
        else {
            SoundManager.instance.PlaySound(reject);
            farmLevelRequired.SetActive(true);
            farmlevel.text = saladLevel.ToString();
        }
    }

    public void CheckLevelCarrot() {
        if (PlayerPrefs.GetInt("FarmLevel", 0) >= carrotLevel) {
            SoundManager.instance.PlaySound(tap);
            carrotScreen.SetActive(true);
        }
        else {
            SoundManager.instance.PlaySound(reject);
            farmLevelRequired.SetActive(true);
            farmlevel.text = carrotLevel.ToString();
        }
    }

    public void CheckLevelCotton() {
        if (PlayerPrefs.GetInt("FarmLevel", 0) >= cottonLevel) {
            SoundManager.instance.PlaySound(tap);
            cottonScreen.SetActive(true);
        }
        else {
            SoundManager.instance.PlaySound(reject);
            farmLevelRequired.SetActive(true);
            farmlevel.text = cottonLevel.ToString();
        }
    }

    public void CheckLevelSunflower() {
        if (PlayerPrefs.GetInt("FarmLevel", 0) >= sunflowerLevel) {
            SoundManager.instance.PlaySound(tap);
            sunflowerScreen.SetActive(true);
        }
        else {
            SoundManager.instance.PlaySound(reject);
            farmLevelRequired.SetActive(true);
            farmlevel.text = sunflowerLevel.ToString();
        }
    }

    public void CheckLevelPumpkin() {
        if (PlayerPrefs.GetInt("FarmLevel", 0) >= pumpkinLevel) {
            SoundManager.instance.PlaySound(tap);
            pumpkinScreen.SetActive(true);
        }
        else {
            SoundManager.instance.PlaySound(reject);
            farmLevelRequired.SetActive(true);
            farmlevel.text = pumpkinLevel.ToString();
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

    public void LoadMyFarm() {
        SoundManager.instance.PlaySound(tap);
        SceneManager.LoadScene("MyFarm");
    }
}
