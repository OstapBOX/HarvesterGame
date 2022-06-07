using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour {

    [SerializeField] private Slider slider;

    [SerializeField] private Button music;
    [SerializeField] private Button effects;
    [SerializeField] private Button vibrations;

    [SerializeField] private Sprite musicOn, musicOff, effectsOn, effectsOff, vibrationsOn, vibrationsOff;


    void Start() {
        slider.value = PlayerPrefs.GetFloat("musicVolume", 1);
        SoundManager.instance.ChangeMasterVolume(PlayerPrefs.GetFloat("musicVolume", 1));
        slider.onValueChanged.AddListener(val => SoundManager.instance.ChangeMasterVolume(val));
        SetSettingsIcons();
    }

    public void SwitchMusic() {
        PlayerPrefs.SetInt("MusicMuted", PlayerPrefs.GetInt("MusicMuted", 0) == 0 ? 1 : 0);
        music.image.sprite = PlayerPrefs.GetInt("MusicMuted", 0) == 0 ? musicOff : musicOn;
        SoundManager.instance.ApplyMusicStatus();
    }

    public void SwitchEffects() {
        PlayerPrefs.SetInt("EffectsMuted", PlayerPrefs.GetInt("EffectsMuted", 0) == 0 ? 1 : 0);
        effects.image.sprite = PlayerPrefs.GetInt("EffectsMuted", 0) == 0 ? effectsOff : effectsOn;
        SoundManager.instance.ApplyEffectsStatus();
    }

    public void SwitchVibrations() {
        PlayerPrefs.SetInt("VibrationsMuted", PlayerPrefs.GetInt("VibrationsMuted", 0) == 0 ? 1 : 0);
        vibrations.image.sprite = PlayerPrefs.GetInt("VibrationsMuted", 0) == 0 ? vibrationsOff : vibrationsOn;
    }    

    private void SetSettingsIcons() {
        music.image.sprite = PlayerPrefs.GetInt("MusicMuted", 0) == 0 ? musicOff : musicOn;
        effects.image.sprite = PlayerPrefs.GetInt("EffectsMuted", 0) == 0 ? effectsOff : effectsOn;
        vibrations.image.sprite = PlayerPrefs.GetInt("VibrationsMuted", 0) == 0 ? vibrationsOff : vibrationsOn;
    }
}
