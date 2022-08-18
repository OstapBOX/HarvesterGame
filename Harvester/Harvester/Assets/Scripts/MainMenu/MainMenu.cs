using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Analytics;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject notEnoughFuel;
    [SerializeField] private EnergyManager energyManager;
    [SerializeField] private AudioClip tap;
    [SerializeField] private GameObject playForDollar;
    [SerializeField] private GameObject menuButtons;

    //Add
    //private InterAd interAd;

    private void Start() {
        Application.targetFrameRate = 60;
        //interAd = GetComponent<InterAd>();
        //interAd.RequestAndLoadInterstitialAd();
        //interAd.ShowAd();
    }

    public void PlayGame()
        {
        if(PlayerPrefs.GetInt("totalEnergy") > 0) {
            SoundManager.instance.PlaySound(tap);
            energyManager.UseEnergy();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else {
            if (PlayerData.instance.GetDollarsAmount() > 0) {
                playForDollar.SetActive(true);
            }
            else {
                notEnoughFuel.SetActive(true);
            }           
        }
    }

    public void ShowMenuButtons() {
        menuButtons.SetActive(true);
    }

    public void HideMenuButtons() {
        menuButtons.SetActive(false);
    }

    public void PlayForDollar() {
        PlayerData.instance.ChangeDollarsAmount(-1);
        SoundManager.instance.PlaySound(tap);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        FirebaseAnalytics.LogEvent("start_game_for_dollars");
    }

    public void LoadShop() {
        SoundManager.instance.PlaySound(tap);
        SceneManager.LoadScene("Shop");
        FirebaseAnalytics.LogEvent("player_entered_shop");
    }

    public void LoadStorage() {
        SoundManager.instance.PlaySound(tap);
        SceneManager.LoadScene("Storage");
        FirebaseAnalytics.LogEvent("player_entered_storage");
    }

    public void LoadHangar() {
        SoundManager.instance.PlaySound(tap);
        SceneManager.LoadScene("Hangar");
    }

    public void LoadMyFarm() {
        SoundManager.instance.PlaySound(tap);
        SceneManager.LoadScene("MyFarm");
    }

    public void Quit()    {
        SoundManager.instance.PlaySound(tap);
        Application.Quit();
    }

    public void PlayTap() {
        SoundManager.instance.PlaySound(tap);
    }

    public void ResetBtn() {
        PlayerPrefs.DeleteAll();
    }

    public void InterBtn() {
       //interAd.ShowAd();
    }

    public void GiveBonus() {
        PlayerData.instance.ChangeCoinsAmount(100000);
        PlayerData.instance.ChangeSpeedUpAmount(100);
        PlayerData.instance.ChangeShieldAmount(100);
        PlayerData.instance.ChangeCultivatorAmount(100);

    }
    public void GiveDollars() {
        PlayerData.instance.ChangeDollarsAmount(1);
    }

}
