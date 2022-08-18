using UnityEngine;
using TMPro;

public class Stone : MonoBehaviour {
    private GameManager gameManager;

    [SerializeField] public int damage;
    [SerializeField] private AudioClip collideSound;

    [Header("Camers Shake Properties")]
    [SerializeField] public float shakeDuration;
    [SerializeField] public float shakeStrength;


    private GameObject wrenchAnim;
    private Animator animator; 

    void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Harvester")) {            
            if (!gameManager.isInvulnerable) {
                gameManager.UpdateStrenght(-damage);
                CameraShake.instance.ShakeCamera(shakeDuration, shakeStrength);
            }
        }
        PlayerData.instance.UpdateStatisticStoneCollected();
        SoundManager.instance.PlaySound(collideSound);
        gameManager.TapPeekVibrate();
        Destroy(this.gameObject);
    }
}
