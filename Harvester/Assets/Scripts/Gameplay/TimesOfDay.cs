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

    [SerializeField] private Skybox environmentMaterial;

    float fadeDurationInSeconds = 2.0f;
    float timeout = 0.05f;

    private void Start() {

        lightsGroupAC = this.gameObject.GetComponent<Animator>();
        mainLightAC = this.gameObject.transform.GetChild(0).GetComponent<Animator>();
        moonLightAC = this.gameObject.transform.GetChild(1).GetComponent<Animator>();
    }

    private void SetDay() {
        lightsGroupAC.SetTrigger("SetDay");
        mainLightAC.SetTrigger("SetDay");
        moonLightAC.SetTrigger("SetDay");
        StartCoroutine(FadeInGlobalIntencity());
    }

    private void SetNight() {
        lightsGroupAC.SetTrigger("SetNight");
        mainLightAC.SetTrigger("SetNight");
        moonLightAC.SetTrigger("SetNight");
        StartCoroutine(FadeOutGlobalIntencity());
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

}
