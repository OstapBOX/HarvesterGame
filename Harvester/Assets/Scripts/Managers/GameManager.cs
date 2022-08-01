using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

    public TextMeshProUGUI scoreText, strengthText, fuelText, timeText, recordText, energyLeft;
    [SerializeField] private GameObject gameOverTable, recordPointer, continueScreen, notEnoughDollarsScreen, shield, collector;

    [SerializeField] private AudioClip tap;
    [SerializeField] private AudioClip switchClip;

    private int score, fuel, strength, maxHeals;
    private int reachRecord;
    private float startTime, currentTime;
    private float cycleDuration = 150.0f;
    private int minutes, seconds;    
    public float gameSpeed = 25.0f, maxGameSpeed = 150.0f;

    public bool isGameActive, isInvulnerable;
    private bool firstDeath = true;

    private GameObject harvesterModels;
    private Harvester harvester;

    private AudioSource effectsAudioSource;


    [SerializeField] private BoxCollider cultivatorCollider;
    [SerializeField] private BoxCollider rotatorCollider;

    [SerializeField] private GameObject notEnoughFuel;
    [SerializeField] private GameObject doubleReward;


    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip engineStarts;
    [SerializeField] private ParticleSystem gameOverExplosion;
    [SerializeField] private GameObject plantsParticle;
    [SerializeField] private GameObject dirtParticle;
    [SerializeField] private TimesOfDay timesOfDay;
    private EnergyManager energyManager;

    [Header("Invulnerability")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;

    [Header("PowerUpTime")]
    [SerializeField] private float shieldDuration;
    [SerializeField] private float collectorDuration;


    private Animator shieldAnimator;
    private Animator collectorAnimator;

    public int wheatCollected = 0, cornCollected = 0, saladCollected = 0, carrotCollected = 0, sunflowerCollected = 0, cottonCollected = 0, pumpkinCollected = 0;

    //Add
    private InterAd interAd;

    // Start is called before the first frame update
    void Start() {
        Time.timeScale = 1f;
        isGameActive = true;
        reachRecord = PlayerPrefs.GetInt("HighScore", 0);
        harvesterModels = GameObject.Find("HarvesterSelection");
        interAd = GetComponent<InterAd>();
        harvester = harvesterModels.transform.GetChild(0).GetComponent<Harvester>();
        effectsAudioSource = GameObject.Find("EffectsSource").GetComponent<AudioSource>();
        energyManager = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
        shieldAnimator = shield.GetComponent<Animator>();
        collectorAnimator = collector.GetComponent<Animator>();
        recordText.text = reachRecord.ToString();
        startTime = Time.time;
        SoundManager.instance.PlaySound(engineStarts);
        score = 0;
        fuel = 100;
        strength = harvester.strength;
        maxHeals = harvester.strength;
        gameSpeed = 25.0f;
        StartCoroutine(FuelIndicator());
        StartCoroutine(PlantsParticle());
        StartCoroutine(ChangeTimeOfDay());
        strengthText.text = strength.ToString();

        if (PlayerData.instance.GetGamesAmount() % 2 == 0) {
            doubleReward.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        if (isGameActive) {
            if (gameSpeed < maxGameSpeed) {
                gameSpeed += 0.25f * Time.deltaTime;
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
        if (PlayerPrefs.GetInt("TutorialShowed") != 0) {
            strength += addStrength;
        }
        if (strength <= 0) {
            strength = 0;
            ContinueGame();
        }
        else if (strength >= maxHeals) {
            strength = maxHeals;
        }
        strengthText.text = strength.ToString();
    }

    private IEnumerator FuelIndicator() {
        while (fuel > 0) {
            yield return new WaitForSeconds(1 - harvester.consumption);
            if (isGameActive) {
                UpdateFuel(-1);
            }
        }
        ContinueGame();
    }



    public int GetStrenght() {
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
            continueScreen.SetActive(true);
        }
        else {
            isGameActive = false;
            ShowGameOverTable();
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
            ShowGameOverTable();
            StopAllCoroutines();
            interAd.ShowAdInGame();
        }
        isGameActive = false;
    }

    public void ShowGameOverTable() {
        energyLeft.text = PlayerPrefs.GetInt("totalEnergy").ToString();
        gameOverTable.SetActive(true);       
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
        fuel = 100;
        continueScreen.SetActive(false);
        gameOverTable.SetActive(false);
        StartCoroutine(Invulnerability());
        StartCoroutine(FuelIndicator());
        isGameActive = true;
        UpdateStrenght(maxHeals / 2);
    }

    private void UpdateCollected() {
        PlayerData.instance.ChangeWheatAmount(wheatCollected);
        PlayerData.instance.ChangeCornAmount(cornCollected);
        PlayerData.instance.ChangeSaladAmount(saladCollected);
        PlayerData.instance.ChangeCarrotAmount(carrotCollected);
        PlayerData.instance.ChangeSunflowerAmount(sunflowerCollected);
        PlayerData.instance.ChangeCottonAmount(cottonCollected);
        PlayerData.instance.ChangePumpkinAmount(pumpkinCollected);
    }

    private void UpdateStatistic() {
        PlayerData.instance.UpdateStatisticHighestScore(score);
        PlayerData.instance.UpdateStatisticWheatCollected(score);
        PlayerData.instance.UpdateStatisticGamesPlayed();
        PlayerData.instance.UpdateStatisticTime(minutes, seconds);
    }

    public void RestartGame() {
        if (PlayerPrefs.GetInt("totalEnergy") > 0) {
            Time.timeScale = 1f;
            UpdateStatistic();
            UpdateCollected();
            //PlayerPrefs.SetInt("totalEnergy", PlayerPrefs.GetInt("totalEnergy") - 1);
            energyManager.UseEnergy();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else {
            notEnoughFuel.SetActive(true);
        }
    }

    public void BackToMenu() {
        SoundManager.instance.PlaySound(tap);
        UpdateStatistic();
        UpdateCollected();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void LoadShop() {
        SoundManager.instance.PlaySound(tap);
        UpdateStatistic();
        UpdateCollected();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Shop");
    }

    public void DoubleReward() {
        doubleReward.SetActive(false);
        UpdateCollected();
    }

    //Vibrations
    public void TapPopVibrate() {
#if !UNITY_EDITOR
            Vibration.VibratePop();    
#endif
    }

    public void TapPeekVibrate() {
#if !UNITY_EDITOR
            Vibration.VibratePeek();
#endif
    }

    private void OnDestroy() {
        if(effectsAudioSource != null) {
            effectsAudioSource.Stop();
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

    private IEnumerator ChangeTimeOfDay() {
        yield return new WaitForSeconds(cycleDuration);
        timesOfDay.ChangeTime();
        StartCoroutine(ChangeTimeOfDay());
        yield return new WaitForSeconds(2.3f);       
        harvester.SwitchLights();
        SoundManager.instance.PlaySound(switchClip);
    }

    public void UseShield() {
        StartCoroutine(Shield());
    }

    public void UseCollector() {
        StartCoroutine(Collector());
    }

    public void DashInvulnerability() {
        StartCoroutine(DashInvulnerabilityCo());
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

    private IEnumerator DashInvulnerabilityCo() {
        isInvulnerable = true;
        yield return new WaitForSeconds(1.5f);
        isInvulnerable = false;
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
        yield return new WaitForSeconds(3.8f);
        plantsParticle.SetActive(true);
    }
}
