using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }

        source = GetComponent<AudioSource>();    
    }

    public void PlaySound(AudioClip _sound) {
        source.PlayOneShot(_sound);
    }

}
