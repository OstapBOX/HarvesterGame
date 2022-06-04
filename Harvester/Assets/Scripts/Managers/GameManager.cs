using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {
    public TextMeshProUGUI scoreText, strengthText, fuelText, timeText, recordText;
    [SerializeField] private GameObject gameOverTable, recordPointer, continueScreen, notEnoughDollarsScreen, shield, collector;
    [SerializeField] private AudioClip tap;
    private int score, fuel, strength, maxHeals;
    private int reachRecord;
    private float startTime, currentTime;
    private int minutes, seconds;
    public bool isGameActive, isInvulnerable;
    private bool firstDeath = true;
    public float gameSpeed = 20.0f, maxGameSpeed = 100,
                 harvesterRotationSpeed = 20.0f,
                 harvesterRotationForce;

    
    private GameObject harvesterModels;
    private Harvester harvester;


    [SerializeField] private BoxCollider cultivatorCollider;
    [SerializeField] private BoxCollider rotatorCollider;

    [SerializeField] private GameObject notEnoughFuel;

    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private ParticleSystem gameOverExplosion;
    [SerializeField] private GameObject plantsParticle;
    [SerializeField] private GameObject dirtParticle;

    [Header("Invulnerability")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;

    [Header("PowerUpTime")]
    [SerializeField] private float shieldDuration;
    [SerializeField] private float collectorDuration;
  

    private Animator shieldAnimator;
    private Animator collectorAnimator;

    // Start is called before the first frame update
    void Start() {
        Time.timeScale = 1f;
        isGameActive = true;
        reachRecord = PlayerPrefs.GetInt("HighScore", 0);
        harvesterModels = GameObject.Find("HarvesterSelection");
        harvester = harvesterModels.transform.GetChild(0).GetComponent<Harvester>();
        shieldAnimator = shield.GetComponent<Animator>();
        collectorAnimator = collector.GetComponent<Animator>();
        recordText.text = reachRecord.ToString();
        startTime = Time.time;
        score = 0;
        fuel = 100;
        strength = harvester.strength;
        maxHeals = harvester.strength;
        gameSpeed = 20.0f;
        harvesterRotationSpeed = 20.0f;
        StartCoroutine(FuelIndicator());
        StartCoroutine(PlantsParticle());
        strengthText.text = strength.ToString();
    }

    // Update is called once per frame
    void Update() {
        if (isGameActive) {
            if (gameSpeed < maxGameSpeed) {
                gameSpeed += 0.5f * Time.deltaTime;
            }
            UpdateTime();
            PlayerData.instance.UpdateStatisticTime(minutes, seconds);
        }
    }

    public void UpdateScore(int addScore) {
        score += addScore;
        scoreText.text = score.ToString();
        if (reachRecord > 0) {
            reachRecord -= addScore;
            recordText.text = reachRecord.ToString();
        }
        else {
            recordPointer.SetActive(false);
        }
    }

    public void UpdateTime() {
        currentTime = Time.time - startTime;
        minutes = ((int)currentTime / 60);
        seconds = ((int)currentTime % 60);

        timeText.text = minutes + ":" + seconds;
    }

    public void UpdateStrenght(int addStrength) {
        strength += addStrength;
        if (strength <= 0) {
            strength = 0;
            ContinueGame();
        }
        else if (strength >= maxHeals) {
            strength = maxHeals;
        }
        strengthText.text = strength.ToString();
    }

    IEnumerator FuelIndicator() {
        while (fuel > 0) {
            yield return new WaitForSeconds(harvester.consumption);
            if (isGameActive) {
                UpdateFuel(-1);
            }
        }
        ContinueGame();
    }


    public int getStrenght() {
        return strength;
    }

    public void UpdateFuel(int addFuel) {
        fuel += addFuel;
        if (fuel > 100)
            fuel = 100;

        fuelText.text = fuel + "%";
    }

    public void ContinueGame() {
        if (firstDeath) {
            isGameActive = false;
            Time.timeScale = 0f;
            Debug.Log("firstDeath");
            continueScreen.SetActive(true);
        }
        else {
            isGameActive = false;
            gameOverTable.SetActive(true);
            StopAllCoroutines();
        }

        plantsParticle.SetActive(true);
        dirtParticle.SetActive(true);

        firstDeath = false;
    }

    public void GameOver() {
        if (isGameActive) {
            SoundManager.instance.PlaySound(gameOverSound);
            gameOverExplosion.Play();
            plantsParticle.SetActive(false);
            dirtParticle.SetActive(false);
            gameOverTable.SetActive(true);
            StopAllCoroutines();
        }
        isGameActive = false;       
    }

    public void DollarRespawn() {
        if (PlayerData.instance.GetDollarsAmount() > 0) {
            PlayerData.instance.ChangeDollarsAmount(-1);
            Respawn();
        }
        else {
            notEnoughDollarsScreen.SetActive(true);
        }
    }

    public void Respawn() {

        Time.timeScale = 1f;
        Debug.Log("Inv");
        StartCoroutine(Invulnerability());
        isGameActive = true;
        UpdateStrenght(maxHeals/2);
        fuel = 100;
        Debug.Log("Respawn");
    }

  

    public void UpdateStatistic() {
        PlayerData.instance.UpdateStatisticHighestScore(score);
        PlayerData.instance.UpdateStatisticWheatCollected(score);
        PlayerData.instance.UpdateStatisticGamesPlayed();
        PlayerData.instance.UpdateStatisticTime(minutes, seconds);
    }

    public void RestartGame() {
        if (PlayerPrefs.GetInt("totalEnergy") > 0) {
            Time.timeScale = 1f;
            UpdateStatistic();
            PlayerPrefs.SetInt("totalEnergy", PlayerPrefs.GetInt("totalEnergy") - 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else {
            notEnoughFuel.SetActive(true);
        }
    }

    public void BackToMenu() {
        SoundManager.instance.PlaySound(tap);
        UpdateStatistic();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void LoadShop() {
        SoundManager.instance.PlaySound(tap);
        UpdateStatistic();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Shop");
    }

    //Vibrations
    public void TapPopVibrate() {
        if (PlayerPrefs.GetInt("Vibrations", 0) == 0) {
            Vibration.VibratePop();
        }
    }

    public void TapPeekVibrate() {
        if (PlayerPrefs.GetInt("Vibrations", 0) == 0) {
            Vibration.VibratePeek();
        }
    }

    private IEnumerator Invulnerability() {
        isInvulnerable = true;
        for (int i = 0; i < numberOfFlashes; i++) {
            harvesterModels.SetActive(false);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            harvesterModels.SetActive(true);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        isInvulnerable = false;
    }

    public void UseShield() {
        StartCoroutine(Shield());
    }

    public void UseCollector() {
        StartCoroutine(Collector());
    }

    private IEnumerator Collector() {
        collectorAnimator.SetTrigger("CollectorIncrease");
        cultivatorCollider.enabled = true;
        rotatorCollider.enabled = false;
        StartCoroutine(HideCollector());
        yield return new WaitForSeconds(collectorDuration);
    }

    private IEnumerator Shield() {
        shield.SetActive(true);
        shieldAnimator.SetTrigger("ShieldAppear");
        StartCoroutine(HideShield());
        yield return new WaitForSeconds(shieldDuration);
        shield.SetActive(false);
    }

    private IEnumerator HideCollector() {
        float hideAnimationDuration = 0.5f;
        yield return new WaitForSeconds(shieldDuration - hideAnimationDuration);
        collectorAnimator.SetTrigger("CollectorDecrease");
        yield return new WaitForSeconds(hideAnimationDuration);
        cultivatorCollider.enabled = false;
        rotatorCollider.enabled = true;
    }


    private IEnumerator HideShield() {
        float hideAnimationDuration = 0.4f;
        yield return new WaitForSeconds(shieldDuration - hideAnimationDuration);
        shieldAnimator.SetTrigger("ShieldDisappear");
    }

    private IEnumerator PlantsParticle() {
        yield return new WaitForSeconds(4.5f);
        plantsParticle.SetActive(true);
    }
}
