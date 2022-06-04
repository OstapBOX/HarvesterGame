using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class СharacteristicsBar : MonoBehaviour
{
    private GameObject harvesterSelection;

    [SerializeField] private TextMeshProUGUI strenth;
    [SerializeField] private TextMeshProUGUI fuel;
    [SerializeField] private TextMeshProUGUI speedUp;
    [SerializeField] private TextMeshProUGUI shield;
    [SerializeField] private TextMeshProUGUI cultivator;

    // Start is called before the first frame update
    void Start()
    {
        harvesterSelection = GameObject.Find("HarvesterSelection");
    }

    public void UpdateCharacteristics() {
        for(int i = 0; i < harvesterSelection.transform.childCount; i++) {
            if(harvesterSelection.transform.GetChild(i).gameObject.activeSelf == true) {
                Harvester currentHarvester = harvesterSelection.transform.GetChild(i).GetComponent<Harvester>();
                strenth.text = currentHarvester.strength.ToString();
                fuel.text = currentHarvester.consumption.ToString();
                speedUp.text = currentHarvester.abilitiesDuration[0].ToString() + " s";
                shield.text = currentHarvester.abilitiesDuration[1].ToString() + " s";
                cultivator.text = currentHarvester.abilitiesDuration[2].ToString() + " s";
            }
        }
    }

}
