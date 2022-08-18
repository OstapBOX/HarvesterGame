using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance { get; private set; }
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectsSource;
    [SerializeField] private AudioSource hansAudioSourse;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        ChangeMasterVolume(PlayerPrefs.GetFloat("musicVolume", 1));
        ApplyEffectsStatus();
        ApplyMusicStatus();
    }

    public void PlaySound(AudioClip _sound) {
        effectsSource.PlayOneShot(_sound);
    }

    public void ChangeMasterVolume(float value) {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("musicVolume", value);
    }

    public void ApplyEffectsStatus() {
        effectsSource.mute = PlayerPrefs.GetInt("EffectsMuted", 0) == 0 ?  false : true;
        hansAudioSourse.mute = PlayerPrefs.GetInt("EffectsMuted", 0) == 0 ? false : true;

    }
    public void ApplyMusicStatus() {
        musicSource.mute = PlayerPrefs.GetInt("MusicMuted", 0) == 0 ? false : true;
    }
}



