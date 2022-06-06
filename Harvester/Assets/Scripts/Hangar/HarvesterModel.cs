using UnityEngine;

public class HarvesterModel : MonoBehaviour
{
    [SerializeField] private GameObject[] harvesterModels;
    private void Awake() {
        ChooseHarvesterModel(SaveManager.instance.currentHarvester);
    }

    private void ChooseHarvesterModel(int _index) {
        Instantiate(harvesterModels[_index], transform.position, transform.rotation, transform);
    }
}
