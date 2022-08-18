using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvester : MonoBehaviour {
    public string[] vehicleName;
    public int price;
    public int strength;
    public int farmLevel;
    public float consumption;
    public float[] abilitiesDuration = new float[3];

    [SerializeField] private GameObject redLights;
    private bool lightsState;

    public void SwitchLights() {
        if (lightsState) {
            TurnOffLights();
            lightsState = !lightsState;
        }
        else {
            TurnOnLights();
            lightsState = !lightsState;
        }
    }

    public void TurnOnLights() {
        redLights.SetActive(true);
    }

    public void TurnOffLights() {
        redLights.SetActive(false);
    }
}

