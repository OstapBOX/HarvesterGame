using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RewardedButton : MonoBehaviour {
    public string prefabName;
    public DateTime lastShowedTime;


    public RewardedButton() {
        lastShowedTime = StringToDate(PlayerPrefs.GetString(prefabName, DateTime.Now.ToString()));
    }

    private DateTime StringToDate(string date) {
        if (String.IsNullOrEmpty(date)) {
            return DateTime.Now;
        }

        return DateTime.Parse(date);
    }


}

