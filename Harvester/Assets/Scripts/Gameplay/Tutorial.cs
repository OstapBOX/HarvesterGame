using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class Tutorial : MonoBehaviour {
    public static Tutorial instance { get; private set; }

    private Storage storage;
    private SwipeManager swipeManager;
    private GameManager gameManager;
    private HarvesterControll harvesterControll;
    private UpgradeSystem upgradeSystem;
    private EnergyManager energyManager;
    private Button pauseButton;

    private int storageLoaded = 0;
    private int menuLoaded = 0;
    private int pointerNumb = 0;
    private int wheatSelled = 0;
    private int pointerMenuNumb = 0;

    private bool swipedRight, swipedLeft;
    private bool showPowerUps, showedDash, showedShield, showedCultivator;
  
    [SerializeField] private AudioClip tap;

    [SerializeField] private GameObject enterStorageGroup;
    [SerializeField] private GameObject buyWheatNullGroup;
    [SerializeField] private GameObject gameplayGroup;
    [SerializeField] private GameObject swipeAndPowerUp;
    [SerializeField] private GameObject swipeRight;
    [SerializeField] private GameObject swipeLeft;

    [SerializeField] private GameObject interfaceGroup;
    [SerializeField] private GameObject strenthPointer;
    [SerializeField] private GameObject fuelPointer;
    [SerializeField] private GameObject scorePointer;

    [SerializeField] private GameObject powerUpsGroup;
    [SerializeField] private GameObject dashGroup;
    [SerializeField] private GameObject shieldGroup;
    [SerializeField] private GameObject cultivatorGroup;
    [SerializeField] private GameObject endPowerUp;

    [SerializeField] private GameObject sellWheat;
    [SerializeField] private GameObject backToMenuButton;
    [SerializeField] private GameObject wheatFrame;

    [SerializeField] private GameObject myFarmGroup;
    [SerializeField] private GameObject upgradeFarm;
    [SerializeField] private GameObject farmBackToMenu;
    [SerializeField] private TextMeshProUGUI upgradePrice;

    [SerializeField] private GameObject menuInterfaceGroup;
    [SerializeField] private GameObject abilitiesHolder;
    [SerializeField] private GameObject coinsHolder;
    [SerializeField] private GameObject energyHolder;
    [SerializeField] private GameObject dollarsHolder;
    [SerializeField] private TextMeshProUGUI finishButton;

    [SerializeField] private GameObject blackPanel;
    [SerializeField] private GameObject invisiblePanel;

    void Start() {
        if (PlayerPrefs.GetInt("TutorialShowed") != 0) {
            Destroy(this.gameObject);
        }
        else {
            PlayerPrefs.SetString("LastShowedTime", DateTime.Now.ToString());
        }

        if (instance == null) {
            instance = this;
            energyManager = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
            DontDestroyOnLoad(this.gameObject);  
        }
        else if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
    }

    private void Update() {
        if (SceneManager.GetActiveScene().name == "Harvester") {
            gameManager.UpdateFuel(1);
            if (!swipedRight) {
                if (harvesterControll.currentLine == -1) {
                    gameManager.gameSpeed = 25;
                    blackPanel.SetActive(false);
                    invisiblePanel.SetActive(true);
                    swipeAndPowerUp.SetActive(false);
                    swipeLeft.SetActive(true);
                    swipeRight.SetActive(false);
                    swipedRight = true;
                    StartCoroutine(LeftSwipe(1));
                }
                else if (harvesterControll.currentLine == 1) {
                    harvesterControll.ChangeLine(-1);
                }
            }
            if(!swipedLeft && swipedRight){
                if (harvesterControll.currentLine == 0) {
                    gameManager.gameSpeed = 25;
                    blackPanel.SetActive(false);
                    invisiblePanel.SetActive(true);
                    swipeAndPowerUp.SetActive(false);
                    swipeLeft.SetActive(false);
                    swipedLeft = true;
                    StartCoroutine(Interface(1));
                }
                else if (harvesterControll.currentLine == -2) {
                    harvesterControll.ChangeLine(1);
                }
            }

            if (showPowerUps && !showedDash) {
                if (PlayerData.instance.GetSpeedUpAmount() == 0) {
                    gameManager.gameSpeed = 25;
                    blackPanel.SetActive(false);
                    invisiblePanel.SetActive(true);
                    dashGroup.SetActive(false);
                    showedDash = true;
                    showPowerUps = false;
                    StartCoroutine(ShieldPowerUp(3));
                }
            }

            if (swipeManager.tap && !showPowerUps && showedDash && !showedShield) {
                showPowerUps = true;
            }

            if (showPowerUps && showedDash && !showedShield) {
                if (PlayerData.instance.GetShieldAmount() == 0) {
                    gameManager.gameSpeed = 25;
                    blackPanel.SetActive(false);
                    invisiblePanel.SetActive(true);
                    shieldGroup.SetActive(false);
                    showedShield = true;
                    showPowerUps = false;
                    StartCoroutine(CultivatorPowerUp(7));
                }
            }

            if (swipeManager.doubleTap && !showPowerUps && showedDash && showedShield && !showedCultivator) {
                showPowerUps = true;
            }

            if (showPowerUps && showedDash && showedShield && !showedCultivator) {

                if (PlayerData.instance.GetCultivatorAmount() == 0) {
                    gameManager.gameSpeed = 25;
                    blackPanel.SetActive(false);
                    invisiblePanel.SetActive(true);
                    cultivatorGroup.SetActive(false);
                    showedCultivator = true;
                    showPowerUps = false;
                    StartCoroutine(LeaveGame(7));
                }
                
            }

            if(swipedLeft && swipedRight) {
                if (harvesterControll.currentLine == 1) {
                    harvesterControll.ChangeLine(-1);
                }
                else if (harvesterControll.currentLine == -1) {
                    harvesterControll.ChangeLine(1);
                }
            }

        }
    }

    private void OnLevelWasLoaded(int level) {
        if(level == 4) {
            storage = GameObject.Find("Canvas").GetComponent<Storage>();
        }
        if(level == 1) {
            StartCoroutine(StartGame(1));          
            swipeManager = GameObject.Find("SwipeManager").GetComponent<SwipeManager>();
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            harvesterControll = GameObject.Find("Harvester").GetComponent<HarvesterControll>();
            pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
            pauseButton.interactable = false;
            gameManager.isInvulnerable = true;        
        }if(level == 5) {
            upgradeSystem = GameObject.Find("FarmObjects").GetComponent<UpgradeSystem>();
        }
    }

    public void BuyWheat() {
        if(storage != null) {
            storage.BuyWheat();
        }      
    }

    public void SellWheat() {
        if(wheatSelled < 5) {
            storage.SellWheat();
            wheatSelled++;
        }
        else {
            wheatFrame.SetActive(false);
            backToMenuButton.SetActive(true);
        }
    }

    public void LoadStorage() {
        if(storageLoaded == 0) {
            enterStorageGroup.SetActive(false);
            buyWheatNullGroup.SetActive(true);
        }
        if(storageLoaded == 1) {
            sellWheat.SetActive(true);
            enterStorageGroup.SetActive(false);

        }
        SceneManager.LoadScene("Storage");
        storageLoaded++;
    }

    public void LoadMenu() {
        TapSound();
        if (menuLoaded == 0) {
            gameplayGroup.SetActive(true);
        }if(menuLoaded == 1) {
            enterStorageGroup.SetActive(true);
        }
        if(menuLoaded == 2) {
            myFarmGroup.SetActive(true);
        }
        if(menuLoaded == 3) {
          menuInterfaceGroup.SetActive(true);
        }
        SceneManager.LoadScene("Menu");
        menuLoaded++;
    }

    public void LoadMyFarm() {
        TapSound();     
        SceneManager.LoadScene("MyFarm");
    }

    public void LoadGame() {
        TapSound();
        SceneManager.LoadScene("Harvester");
    }

    public void UpgradeFarm() {
        if(PlayerPrefs.GetInt("FarmLevel", 0) == 0) {
            upgradeSystem.Upgrade();
            upgradePrice.text = "35$";            
        }
        else if (PlayerPrefs.GetInt("FarmLevel", 0) == 1) {
            upgradeSystem.Upgrade();
            upgradePrice.text = "GOT IT";
        }
        else {
            upgradeFarm.SetActive(false);
            farmBackToMenu.SetActive(true);              
        }
       
    }

    private IEnumerator StartGame(float delayTime) {
        yield return new WaitForSeconds(delayTime);
        gameManager.gameSpeed = 0;
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Image>().raycastTarget = false;
        swipeAndPowerUp.SetActive(true);
        invisiblePanel.SetActive(false);        
    }

    private IEnumerator LeftSwipe(float delayTime) {
        yield return new WaitForSeconds(delayTime);
        gameManager.gameSpeed = 0;
        swipeAndPowerUp.SetActive(true);
        blackPanel.SetActive(true);
        invisiblePanel.SetActive(false);
    }

    private IEnumerator Interface(float delayTime) {
        yield return new WaitForSeconds(delayTime);
        swipeAndPowerUp.SetActive(false);
        gameManager.gameSpeed = 0;        
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Image>().raycastTarget = true;
        interfaceGroup.SetActive(true);
        
    }

    private IEnumerator PowerUp(float delayTime) {
        yield return new WaitForSeconds(delayTime);
        gameManager.gameSpeed = 0;
        blackPanel.SetActive(true);
        invisiblePanel.SetActive(false);
        powerUpsGroup.SetActive(true);
    }

    private IEnumerator ShieldPowerUp(float delayTime) {
        yield return new WaitForSeconds(delayTime);
        PlayerData.instance.ChangeShieldAmount(1);
        gameManager.gameSpeed = 0;
        blackPanel.SetActive(true);
        invisiblePanel.SetActive(false);
        shieldGroup.SetActive(true);
    }

    private IEnumerator CultivatorPowerUp(float delayTime) {
        yield return new WaitForSeconds(delayTime);
        PlayerData.instance.ChangeCultivatorAmount(1);
        gameManager.gameSpeed = 0;
        blackPanel.SetActive(true);
        invisiblePanel.SetActive(false);
        cultivatorGroup.SetActive(true);
    }

    private IEnumerator LeaveGame(float delayTime) {
        yield return new WaitForSeconds(delayTime/2);
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Image>().raycastTarget = true;
        endPowerUp.SetActive(true);
        yield return new WaitForSeconds(delayTime/2);
        if (PlayerPrefs.GetInt("ResoursesGot") == 0){
            energyManager.ChangeEnergyAmount(10);
            PlayerData.instance.ChangeWheatAmount(566);
            PlayerData.instance.ChangeSpeedUpAmount(5);
            PlayerData.instance.ChangeCultivatorAmount(5);
            PlayerData.instance.ChangeShieldAmount(5);
        }
        PlayerPrefs.SetInt("ResoursesGot", 1);

        Destroy(endPowerUp);
        LoadMenu();
    }

    public void ChangePointer() {
        TapSound();
        if (pointerNumb == 0) {
            Destroy(strenthPointer);
            fuelPointer.SetActive(true);
            pointerNumb++;
        }
        else if(pointerNumb == 1) {
            Destroy(fuelPointer);
            scorePointer.SetActive(true);
            pointerNumb++;
        }
        else {
            Destroy(interfaceGroup);
            gameManager.gameSpeed = 25;
            invisiblePanel.SetActive(true);
            blackPanel.SetActive(false);
            StartCoroutine(PowerUp(1));
        }
    }

    public void ChangePointerMenu() {
        TapSound();
        if (pointerMenuNumb == 0) {
            Destroy(abilitiesHolder);
            coinsHolder.SetActive(true);
            pointerMenuNumb++;
        }
        else if (pointerMenuNumb == 1) {
            Destroy(coinsHolder);
            energyHolder.SetActive(true);
            pointerMenuNumb++;
        }
        else if (pointerMenuNumb == 2) {
            Destroy(energyHolder);
            finishButton.text = "Finish";
            dollarsHolder.SetActive(true);
            pointerMenuNumb++;
        }
        else {
            Destroy(this.gameObject);
            PlayerPrefs.SetInt("TutorialShowed", 1);
        }
    }

    public void PowerUpButton() {
        PlayerData.instance.ChangeSpeedUpAmount(1);
        showPowerUps = true;
        blackPanel.GetComponent<Image>().raycastTarget = false;
        dashGroup.SetActive(true);
    }

    public void TapSound() {
        SoundManager.instance.PlaySound(tap);
    }
    
}
