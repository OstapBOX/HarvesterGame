using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    private GameManager gameManager;

    void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other) {
        gameManager.UpdateScore(1);
        gameManager.TapPopVibrate();
        //PlayerData.instance.ChangeCarrotAmount(1);
        gameManager.carrotCollected += 1;
        Destroy(gameObject);
    }
}
