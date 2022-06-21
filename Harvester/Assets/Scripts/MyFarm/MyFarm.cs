using UnityEngine;
using UnityEngine.SceneManagement;

public class MyFarm : MonoBehaviour
{
    [SerializeField] private AudioClip tap;
    //private InterAd interAd;

    private void Start() {
        //interAd = GetComponent<InterAd>();
        //interAd.RequestAndLoadInterstitialAd();
        //interAd.ShowAd();
    }

    public void LoadMenu() {
        SoundManager.instance.PlaySound(tap);
        
        SceneManager.LoadScene("Menu");
    }
}
