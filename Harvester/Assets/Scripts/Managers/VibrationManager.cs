////////////////////////////////////////////////////////////////////////////////
//  
// @author Benoît Freslon @benoitfreslon
// https://github.com/BenoitFreslon/Vibration
// https://benoitfreslon.com
//
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VibrationManager : MonoBehaviour {

    // Use this for initialization
    void Start() {
        DontDestroyOnLoad(this.gameObject);
        Vibration.Init();
        Debug.Log("Application.isMobilePlatform: " + Application.isMobilePlatform);

    }

    public void TapVibrate() {
        Vibration.Vibrate();
    }

    public void TapCancelVibrate() {
        Vibration.Cancel();
    }

    public void TapPopVibrate() {
        if (PlayerPrefs.GetInt("VibrationsMuted", 0) == 0) {
            Vibration.VibratePop();
        }
    }

    public void TapPeekVibrate() {
        if (PlayerPrefs.GetInt("VibrationsMuted", 0) == 0) {
            Vibration.VibratePeek();
        }
    }

    public void TapNopeVibrate() {
        Vibration.VibrateNope();
    }
}
