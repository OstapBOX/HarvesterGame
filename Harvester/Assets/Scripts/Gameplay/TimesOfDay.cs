using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimesOfDay : MonoBehaviour {
    private Animator lightsGroupAC;
    private Animator mainLightAC;
    private Animator moonLightAC;

    private bool isDay = true;

    private float minSkyBoxIntencity = 0.3f;
    private float maxSkyBoxIntencity = 1.0f;

    private float minVolume = 0.05f;
    private float maxVolume = 0.2f;

    [SerializeField] private Skybox environmentMaterial;
    private AudioSource musicAudioSource;

    float fadeDurationInSeconds = 2.0f;
    float timeout = 0.05f;

    private void Start() {
        lightsGroupAC = this.gameObject.GetComponent<Animator>();
        mainLightAC = this.gameObject.transform.GetChild(0).GetComponent<Animator>();
        moonLightAC = this.gameObject.transform.GetChild(1).GetComponent<Animator>();
        musicAudioSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();
    }

    private void SetDay() {
        lightsGroupAC.SetTrigger("SetDay");
        mainLightAC.SetTrigger("SetDay");
        moonLightAC.SetTrigger("SetDay");
        StartCoroutine(FadeInGlobalIntencity());
        StartCoroutine(FadeInMusicVolume());
    }

    private void SetNight() {
        lightsGroupAC.SetTrigger("SetNight");
        mainLightAC.SetTrigger("SetNight");
        moonLightAC.SetTrigger("SetNight");
        StartCoroutine(FadeOutGlobalIntencity());
        StartCoroutine(FadeOutMusicVolume());
    }

    public void ChangeTime() {
        if (isDay) {
            SetNight();
            isDay = !isDay;
        }
        else {
            SetDay();
            isDay = !isDay;
        }
    }

    private IEnumerator FadeInGlobalIntencity() {
        float fadeAmount = 1 / (fadeDurationInSeconds / timeout);

        for (float f = minSkyBoxIntencity; f <= maxSkyBoxIntencity; f += fadeAmount) {
            RenderSettings.ambientIntensity = f;
            yield return new WaitForSeconds(timeout);
        }
    }

    private IEnumerator FadeOutGlobalIntencity() {
        float fadeAmount = 1 / (fadeDurationInSeconds / timeout);

        for (float f = maxSkyBoxIntencity; f >= minSkyBoxIntencity; f -= fadeAmount) {
            RenderSettings.ambientIntensity = f;
            yield return new WaitForSeconds(timeout);
        }
    }

    private IEnumerator FadeInMusicVolume() {
        float volumeAmount = 1 / (fadeDurationInSeconds / timeout);

        for (float f = minVolume; f <= maxVolume; f += volumeAmount) {
            musicAudioSource.volume = f;
            yield return new WaitForSeconds(timeout);
        }
    }

    private IEnumerator FadeOutMusicVolume() {
        float volumeAmount = 1 / (fadeDurationInSeconds / timeout);

        for (float f = maxVolume; f >= minVolume; f -= volumeAmount) {
            musicAudioSource.volume = f;
            yield return new WaitForSeconds(timeout);
        }
    }

}
