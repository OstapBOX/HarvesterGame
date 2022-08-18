using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    [SerializeField] private AudioClip tap;
    [SerializeField] private AudioClip reject;
    [SerializeField] private AudioClip successes;

    [SerializeField] private PowerUpsAmount powerUpsAmount;
    [SerializeField] private GameObject removeAdsButton;
    //private InterAd interAd;

    private void Start() {
        if (!PlayerData.instance.GetRemoveAdsStatus()) {
            removeAdsButton.SetActive(false);
        }
    }

    public void BuyDashForDollars() {
        if(PlayerData.instance.GetDollarsAmount() >= 5) {
            PlayerData.instance.ChangeDollarsAmount(-5);
            PlayerData.instance.ChangeSpeedUpAmount(10);
            SoundManager.instance.PlaySound(successes);
            powerUpsAmount.UpdatePowerUpsAmount();
        }
        else {
            SoundManager.instance.PlaySound(reject);
        }
    }

    public void BuyShieldForDollars() {
        if (PlayerData.instance.GetDollarsAmount() >= 5) {
            PlayerData.instance.ChangeDollarsAmount(-5);
            PlayerData.instance.ChangeShieldAmount(7);
            SoundManager.instance.PlaySound(successes);
            powerUpsAmount.UpdatePowerUpsAmount();
        }
        else {
            SoundManager.instance.PlaySound(reject);
        }
    }

    public void BuyCultivatorForDollars() {
        if (PlayerData.instance.GetDollarsAmount() >= 5) {
            PlayerData.instance.ChangeDollarsAmount(-5);
            PlayerData.instance.ChangeCultivatorAmount(5);
            SoundManager.instance.PlaySound(successes);
            powerUpsAmount.UpdatePowerUpsAmount();
        }
        else {
            SoundManager.instance.PlaySound(reject);
        }
    }

    public void LoadHangar() {
        SoundManager.instance.PlaySound(tap);
        SceneManager.LoadScene("Hangar");
    }

    public void LoadMenu() {
        SoundManager.instance.PlaySound(tap);
        SceneManager.LoadScene("Menu");
    }
}
