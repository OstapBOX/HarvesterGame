using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private AudioClip collideSound;

    void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other) {
        PlayerData.instance.UpdateStatisticHealCollected();
        gameManager.UpdateStrenght(1);
        SoundManager.instance.PlaySound(collideSound);
        Destroy(this.gameObject);
    }
}
