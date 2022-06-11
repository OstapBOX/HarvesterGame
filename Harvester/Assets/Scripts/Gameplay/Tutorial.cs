using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {
    public static Tutorial instance { get; private set; }

    private Storage storage;
    private SwipeManager swipeManager;

    private int storageLoaded = 0;
    private int menuLoaded = 0;

    [SerializeField] private GameObject enterStorageGroup;
    [SerializeField] private GameObject buyWheatNullGroup;
    [SerializeField] private GameObject gameplayGroup;

    

    void Start() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
    }

    private void OnLevelWasLoaded(int level) {
        if(level == 4) {
            storage = GameObject.Find("Canvas").GetComponent<Storage>();
        }
        if(level == 1) {
            StartCoroutine(FreezeGame(1));
            swipeManager = GameObject.Find("SwipeManager").GetComponent<SwipeManager>();
        }
    }


    public void BuyWheat() {
        if(storage != null) {
            storage.BuyWheat();
        }      
    }

    public void LoadStorage() {
        if(storageLoaded == 0) {
            enterStorageGroup.SetActive(false);
            buyWheatNullGroup.SetActive(true);
        }
        SceneManager.LoadScene("Storage");
        storageLoaded++;
    }

    public void LoadMenu() {
        if(menuLoaded == 0) {
            gameplayGroup.SetActive(true);
        }
        SceneManager.LoadScene("Menu");
        menuLoaded++;
    }

    public void LoadGame() {
        SceneManager.LoadScene("Harvester");
    }

    private IEnumerator FreezeGame(float delayTime) {
        yield return new WaitForSeconds(delayTime);
        Time.timeScale = 0;
    }



}
