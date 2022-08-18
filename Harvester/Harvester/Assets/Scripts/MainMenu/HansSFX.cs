using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HansSFX : MonoBehaviour
{
    private AudioSource hansAudioSourse;

    private void OnLevelWasLoaded(int level) {
        hansAudioSourse = GetComponent<AudioSource>();
        if(PlayerPrefs.GetInt("EffectsMuted", 0) == 0) {
            if (level == 1) {
                hansAudioSourse.mute = true;
            }
            else {
                hansAudioSourse.mute = false;
            }
        }
     
    }
}
