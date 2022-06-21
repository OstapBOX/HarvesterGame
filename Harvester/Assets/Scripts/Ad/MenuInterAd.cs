using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInterAd : MonoBehaviour
{
    public static MenuInterAd instance { get; private set; }

    private InterAd interAd;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            interAd = GetComponent<InterAd>();
        }
        else if (instance != null && instance != this) {
            Destroy(this.gameObject);           
        }
    }

    private void OnLevelWasLoaded(int level) {
        if(level != 1) {
            interAd.ShowAd();
        }
    }


}
