﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject notEnoughFuel;
    [SerializeField] private EnergyManager energyManager;
    [SerializeField] private AudioClip tap;
   

    // Start is called before the first frame update
    private void Start() {
        Application.targetFrameRate = 60;
    }

    public void PlayGame()
    {
        if(PlayerPrefs.GetInt("totalEnergy") > 0) {
            SoundManager.instance.PlaySound(tap);
            energyManager.UseEnergy();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else {
            notEnoughFuel.SetActive(true);
        }
    }

    public void LoadShop() {
        SoundManager.instance.PlaySound(tap);
        SceneManager.LoadScene("Shop");
    }

    public void LoadStorage() {
        SoundManager.instance.PlaySound(tap);
        SceneManager.LoadScene("Storage");
    }

    public void LoadHangar() {
        SoundManager.instance.PlaySound(tap);
        SceneManager.LoadScene("Hangar");
    }

    public void LoadMyFarm() {
        SoundManager.instance.PlaySound(tap);
        SceneManager.LoadScene("MyFarm");
    }

    public void Quit()
    {
        SoundManager.instance.PlaySound(tap);
        Application.Quit();
    }

    public void PlayTap() {
        SoundManager.instance.PlaySound(tap);
    }

    public void ResetBtn() {
        PlayerPrefs.DeleteAll();
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