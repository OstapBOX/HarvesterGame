using UnityEngine;

public class HarvesterModel : MonoBehaviour
{
    [SerializeField] private GameObject[] harvesterModels;
    private void Awake() {
        ChooseHarvesterModel(CurrentHarvester());
    }

    private void ChooseHarvesterModel(int _index) {
        Instantiate(harvesterModels[_index], transform.position, transform.rotation, transform);
    }

    private int CurrentHarvester() {
        if (!SaveManager.instance.harvestersUnlocked[SaveManager.instance.currentHarvester]) {
            return 0;
        }
        else {
            return SaveManager.instance.currentHarvester;
        }
    }
}
