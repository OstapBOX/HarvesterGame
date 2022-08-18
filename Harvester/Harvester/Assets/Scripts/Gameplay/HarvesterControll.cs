using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HarvesterControll : MonoBehaviour {


    [SerializeField] private Vector3 harvesterOffset;
    [SerializeField] private PowerUpsAmount powerUpsAmount;

    [SerializeField] private AudioClip swipe;
    [SerializeField] private AudioClip dashActivation;
    [SerializeField] private AudioClip shieldActivation;
    [SerializeField] private AudioClip cultivatorActivation;
    [SerializeField] private AudioClip reject;

    private GameManager gameManager;
    private SwipeManager swipeManager;
    private Harvester harvester;
    

    public float currentLine;
    private float lineDistance = 6;

    private float speedUpZPosition = 20f;
    private float normalZPosition = -32f;


    private float slideAngle = 15f;
    private float changeLineTime = 0.3f;
    private float changeLineAngle = 10f;

    //Abilities
    [Header("Abilities")]

    [SerializeField] private Image[] abilitiesMasks = new Image[3];
    private float[] abilitiesDuration = new float[3];
    private bool[] abilitiesCooldown = new bool[3];

    void Start() {
        harvester = GameObject.Find("HarvesterSelection").transform.GetChild(0).GetComponent<Harvester>();

        abilitiesDuration = harvester.abilitiesDuration;

        //Set abilities icons to 0
        for (int i =0; i< abilitiesMasks.Length; i++) {
            abilitiesMasks[i].fillAmount = 0;
        }

        currentLine = 0;
        swipeManager = GameObject.Find("SwipeManager").GetComponent<SwipeManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


    }

    // Update is called once per frame
    void Update() {
        if (gameManager.isGameActive) {
            if (swipeManager.swipeRight) {
                swipeManager.swipeRight = false;
                ChangeLine(1);
            }

            if (swipeManager.swipeLeft) {
                swipeManager.swipeLeft = false;
                ChangeLine(-1);
            }

            if (swipeManager.swipeUp) {
                swipeManager.swipeUp = false;
                CastDash();                
            }

            if (swipeManager.tap) {
                swipeManager.tap = false;
                CastShield();
            }

            if (swipeManager.doubleTap) {
                swipeManager.doubleTap = false;
                CastCultivator();
               
            }

            AbilityInterface(0);
            AbilityInterface(1);
            AbilityInterface(2);
        }

    }


    public void ChangeLine(int _diection) {
        currentLine += _diection;
        var Sequence = DOTween.Sequence();
        Sequence.Append(transform.DOMoveX(currentLine * lineDistance + harvesterOffset.x, changeLineTime/2));
        Sequence.Join(transform.DORotate(new Vector3(0, changeLineAngle * _diection, 0), changeLineTime/2));
        Sequence.Append(transform.DORotate(Vector3.zero, changeLineTime));
        SoundManager.instance.PlaySound(swipe);
    }

    private void MoveForward() {
        gameManager.DashInvulnerability();
        float slideDirection = Random.Range(-1, 1);
        var Sequence = DOTween.Sequence();
        Sequence.Append(transform.DOMoveZ(speedUpZPosition, 1f));
        Sequence.Join(transform.DORotate(new Vector3(0, slideAngle * slideDirection, 0), 0.5f));
        Sequence.AppendInterval(0.5f);
        Sequence.Append(transform.DOMoveZ(normalZPosition, 1f));
        Sequence.Join(transform.DORotate(Vector3.zero, 0.5f));
    }

    public void CastDash() {
        if (abilitiesCooldown[0] == false) {
            if (PlayerData.instance.GetSpeedUpAmount() > 0) {
                SoundManager.instance.PlaySound(dashActivation);
                PlayerData.instance.ChangeSpeedUpAmount(-1);
                powerUpsAmount.UpdatePowerUpsAmount();
                abilitiesCooldown[0] = true;
                abilitiesMasks[0].fillAmount = 1;
                MoveForward();
            }
            else {
                SoundManager.instance.PlaySound(reject);
            }
        }
        else {
            SoundManager.instance.PlaySound(reject);
        }
    }
    public void CastShield() {
        if (abilitiesCooldown[1] == false) {
            if (PlayerData.instance.GetShieldAmount() > 0) {
                SoundManager.instance.PlaySound(shieldActivation);
                PlayerData.instance.ChangeShieldAmount(-1);
                powerUpsAmount.UpdatePowerUpsAmount();
                abilitiesCooldown[1] = true;
                abilitiesMasks[1].fillAmount = 1;
                gameManager.UseShield();
            }
            else {
                SoundManager.instance.PlaySound(reject);
            }
        }
        else {
            SoundManager.instance.PlaySound(reject);
        }
    }
    public void CastCultivator() {
        if (abilitiesCooldown[2] == false) {
            if (PlayerData.instance.GetCultivatorAmount() > 0) {
                SoundManager.instance.PlaySound(cultivatorActivation);
                PlayerData.instance.ChangeCultivatorAmount(-1);
                powerUpsAmount.UpdatePowerUpsAmount();
                abilitiesCooldown[2] = true;
                abilitiesMasks[2].fillAmount = 1;
                gameManager.UseCollector();
            }
            else {
                SoundManager.instance.PlaySound(reject);
            }
        }
        else {
            SoundManager.instance.PlaySound(reject);
        }
    }

    private void AbilityInterface(int _abilityIndex) {
        if (abilitiesCooldown[_abilityIndex]) {
            abilitiesMasks[_abilityIndex].fillAmount -= 1 / abilitiesDuration[_abilityIndex] * Time.deltaTime;
            if (abilitiesMasks[_abilityIndex].fillAmount <= 0) {
                abilitiesMasks[_abilityIndex].fillAmount = 0;
                abilitiesCooldown[_abilityIndex] = false;
            }
        }
    }

}



