using UnityEngine;
using System.Collections;

public class Wheat : MonoBehaviour {
    private GameManager gameManager;

    void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other) {
        gameManager.UpdateScore(1);
        gameManager.TapPopVibrate();
        //PlayerData.instance.ChangeWheatAmount(1);
        gameManager.wheatCollected += 1;
        Destroy(gameObject);
    }    
}

