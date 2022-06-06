using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour {

    [SerializeField] private Slider slider;

    void Start() {
        slider.value = PlayerPrefs.GetFloat("musicVolume", 1);
        SoundManager.instance.ChangeMasterVolume(PlayerPrefs.GetFloat("musicVolume", 1));
        slider.onValueChanged.AddListener(val => SoundManager.instance.ChangeMasterVolume(val));
    }

    public void SwitchMusic() {
        PlayerPrefs.SetInt("MusicMuted", PlayerPrefs.GetInt("MusicMuted", 0) == 0 ? 1 : 0);
        SoundManager.instance.ApplyMusicStatus();
    }

    public void SwitchEffects() {
        PlayerPrefs.SetInt("EffectsMuted", PlayerPrefs.GetInt("EffectsMuted", 0) == 0 ? 1 : 0);
        SoundManager.instance.ApplyEffectsStatus();
    }
}
