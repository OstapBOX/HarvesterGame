using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantInStorage : MonoBehaviour {
    [SerializeField] private string thisPlantName;
    [SerializeField] private TextMeshProUGUI thisAmountText;
    [SerializeField] private Button nextPlantButton;
    [SerializeField] private GameObject sellLock;


    void Start() {
        if (PlayerPrefs.GetInt(thisPlantName + "Bought", 0) == 1) {
            if (nextPlantButton != null) {
                nextPlantButton.interactable = true;
            }

            thisAmountText.enabled = true;

            Destroy(this.gameObject);

            if (sellLock != null) {
                Destroy(sellLock);

            }
        }
    }
}
