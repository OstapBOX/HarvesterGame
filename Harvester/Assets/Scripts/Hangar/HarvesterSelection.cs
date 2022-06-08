using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HarvesterSelection : MonoBehaviour
{
    [Header("Navigation Buttons")]
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    [Header("Play/Buy Buttons")]
    [SerializeField] private Button play;
    [SerializeField] private Button buy;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI nameText;
 

    [Header("Car Attributes")]
    [SerializeField] private int[] harvesterPrices;
    [SerializeField] private string[] harvesterName;
    [SerializeField] private GameObject carLockedScreen;
    private int currentHarvester;

    [Header("UI")]
    [SerializeField] private StatisticBar statisticBar;
    [SerializeField] private СharacteristicsBar сharacteristicsBar;
    [SerializeField] private AudioClip swipe;
    [SerializeField] private AudioClip pick;

    private void Start() {
        currentHarvester = CurrentHarvester();
        CheckArrowsStatus();
        SelectHarvester(currentHarvester);
        сharacteristicsBar.UpdateCharacteristics();
    }

    private void SelectHarvester(int _index) {
       
        for(int i =0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(i == _index);
        }

        UpdateUI();
    }

    private void UpdateUI() {
        if (SaveManager.instance.harvestersUnlocked[currentHarvester]) {
            play.gameObject.SetActive(true);
            buy.gameObject.SetActive(false);
            carLockedScreen.SetActive(false);
            nameText.text = harvesterName[currentHarvester].ToString();
        }
        else {
            nameText.text = harvesterName[currentHarvester].ToString();
            play.gameObject.SetActive(false);
            buy.gameObject.SetActive(true);
            carLockedScreen.SetActive(true);
            priceText.text = harvesterPrices[currentHarvester] + "$";
            buy.interactable = (PlayerData.instance.GetCoinsAmount() >= harvesterPrices[currentHarvester]);
        }
        statisticBar.UpdateStatisticBar();
    }


    private void Update() {
        if (buy.gameObject.activeInHierarchy) {
            buy.interactable = (PlayerData.instance.GetCoinsAmount() >= harvesterPrices[currentHarvester]);
        }
    }

    public void ChangeHarvester(int _change) {
        SoundManager.instance.PlaySound(swipe);
        currentHarvester += _change;
        previousButton.interactable = (currentHarvester != 0 );
        nextButton.interactable = (currentHarvester != transform.childCount - 1);
        SaveManager.instance.currentHarvester = currentHarvester;
        SaveManager.instance.Save();
        SelectHarvester(currentHarvester);
    }

    private void CheckArrowsStatus() {
        previousButton.interactable = (currentHarvester != 0);
        nextButton.interactable = (currentHarvester != transform.childCount - 1);
    }

    public void BuyHarvester() {
        SoundManager.instance.PlaySound(pick);
        PlayerData.instance.ChangeCoinsAmount(-harvesterPrices[currentHarvester]);
        SaveManager.instance.harvestersUnlocked[currentHarvester] = true;
        SaveManager.instance.Save();
        UpdateUI();
    }

    private int CurrentHarvester() {
        if (!SaveManager.instance.harvestersUnlocked[currentHarvester]) {
            return 0;
        }
        else {
            return SaveManager.instance.currentHarvester;
        }
    }
        

    public void LoadMenu() {
        SoundManager.instance.PlaySound(pick);
        SceneManager.LoadScene("Menu");
    }
}
