using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour {
    private void Start() {
        Debug.Log(1);
        int id = PlayerPrefs.GetInt("LocaleKey", 0);
        ChangeLocale(id);
    }

    private bool active = false;
    public void ChangeLocale(int localeID) {
        if (active == true) {
            return;
        }
        StartCoroutine(SetLocale(localeID));
        Debug.Log(5);
    }

    IEnumerator SetLocale(int _localeID) {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        PlayerPrefs.SetInt("LocaleKey", _localeID);
        active = false;
    }
}
