using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {
    public static Tutorial instance { get; private set; }

    private Storage storage;
    private SwipeManager swipeManager;
    private GameManager gameManager;
    private HarvesterControll harvesterControll;

    private int storageLoaded = 0;
    private int menuLoaded = 0;

    private bool swipedRight, swipedLeft;

    [SerializeField] private GameObject enterStorageGroup;
    [SerializeField] private GameObject buyWheatNullGroup;
    [SerializeField] private GameObject gameplayGroup;
    [SerializeField] private GameObject swipeAndPowerUp;
    [SerializeField] private GameObject swipeRight;
    [SerializeField] private GameObject swipeLeft;
    [SerializeField] private GameObject interfaceGroup;
 

    [SerializeField] private GameObject blackPanel;
    [SerializeField] private GameObject invisiblePanel;
   


    

    void Start() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
    }

    private void Update() {
      
        if(SceneManager.GetActiveScene().name == "Harvester") {
            if (!swipedRight) {
                if (harvesterControll.currentLine == -1) {
                    gameManager.gameSpeed = 25;
                    blackPanel.SetActive(false);
                    invisiblePanel.SetActive(true);
                    swipeAndPowerUp.SetActive(false);
                    swipeLeft.SetActive(true);
                    swipeRight.SetActive(false);
                    swipedRight = true;
                    StartCoroutine(LeftSwipe(1));
                }
                else if (harvesterControll.currentLine == 1) {
                    harvesterControll.ChangeLine(-1);
                }
            }
            if(!swipedLeft && swipedRight){
                if (harvesterControll.currentLine == 0) {
                    gameManager.gameSpeed = 25;
                    blackPanel.SetActive(false);
                    invisiblePanel.SetActive(true);
                    swipeAndPowerUp.SetActive(false);
                    swipeLeft.SetActive(false);
                    swipedLeft = true;
                }
                else if (harvesterControll.currentLine == -2) {
                    harvesterControll.ChangeLine(1);
                }
            }
            

        }
    }

    private void OnLevelWasLoaded(int level) {
        if(level == 4) {
            storage = GameObject.Find("Canvas").GetComponent<Storage>();
        }
        if(level == 1) {
            StartCoroutine(StartGame(1));
            swipeManager = GameObject.Find("SwipeManager").GetComponent<SwipeManager>();
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            harvesterControll = GameObject.Find("Harvester").GetComponent<HarvesterControll>();
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


    private IEnumerator StartGame(float delayTime) {
        yield return new WaitForSeconds(delayTime);
        gameManager.gameSpeed = 0;
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Image>().raycastTarget = false;
        swipeAndPowerUp.SetActive(true);
        invisiblePanel.SetActive(false);        
    }

    private IEnumerator LeftSwipe(float delayTime) {
        yield return new WaitForSeconds(delayTime);
        gameManager.gameSpeed = 0;
        swipeAndPowerUp.SetActive(true);
        blackPanel.SetActive(true);
        invisiblePanel.SetActive(false);
    }

    private IEnumerator Interface(float delayTime) {
        yield return new WaitForSeconds(delayTime);
        gameManager.gameSpeed = 0;
        blackPanel.SetActive(true);
        interfaceGroup.SetActive(true);
        invisiblePanel.SetActive(false);
    }



}
