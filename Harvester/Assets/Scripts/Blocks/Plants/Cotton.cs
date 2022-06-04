using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cotton : MonoBehaviour
{
    private GameManager gameManager;

    void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other) {
        gameManager.UpdateScore(1);
        gameManager.TapPopVibrate();
        PlayerData.instance.ChangeCottonAmount(1);
        Destroy(gameObject);
    }
}
