using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canister : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private int minFuelAmount;
    [SerializeField] private int maxFuelAmount;
    [SerializeField] private AudioClip collideSound;

    void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other) {
        int fuelAmount = Random.Range(minFuelAmount, maxFuelAmount);
        PlayerData.instance.UpdateStatisticCanisterCollected();
        gameManager.UpdateFuel(fuelAmount);
        SoundManager.instance.PlaySound(collideSound);
        Destroy(this.gameObject);
    }
}
