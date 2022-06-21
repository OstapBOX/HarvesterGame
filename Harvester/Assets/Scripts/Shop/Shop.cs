using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    [SerializeField] private AudioClip tap;
    //private InterAd interAd;

    private void Start() {
        //interAd = GetComponent<InterAd>();
        //interAd.RequestAndLoadInterstitialAd();
        //interAd.ShowAd();
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
