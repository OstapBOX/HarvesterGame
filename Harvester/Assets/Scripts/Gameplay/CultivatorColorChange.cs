using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultivatorColorChange : MonoBehaviour {
    [SerializeField] private Material[] materials;


    [SerializeField] private Renderer cultivatorRenderer;
    [SerializeField] private Renderer rotatorRenderer;
    [SerializeField] private Renderer shieldRenderer;

    void Start() {
        Material[] cultivatorMaterialsArray = cultivatorRenderer.materials;
        Material[] rotatorMaterialsArray = rotatorRenderer.materials;

        int firstColour = 0, secondColour = 0;

        if (SaveManager.instance.currentHarvester == 0) {
            //Passenger
            firstColour = 11;
            secondColour = 16;
        }
        else if (SaveManager.instance.currentHarvester == 1) {
            //Taxi
            firstColour = 11;
            secondColour = 23;
        }
        else if (SaveManager.instance.currentHarvester == 2) {
            //Police
            firstColour = 17;
            secondColour = 30;
        }
        else if (SaveManager.instance.currentHarvester == 3) {
            //Hippie
            firstColour = 35;
            secondColour = 33;
        }
        else if (SaveManager.instance.currentHarvester == 4) {
            //TowTruck
            firstColour = 31;
            secondColour = 17;
        }
        else if (SaveManager.instance.currentHarvester == 5) {
            //Jeep
            firstColour = 4;
            secondColour = 9;
        }
        else if (SaveManager.instance.currentHarvester == 6) {
            //BayWatch
            firstColour = 14;
            secondColour = 23;
        }
        else if (SaveManager.instance.currentHarvester == 7) {
            //ArmoredTruck
            firstColour = 11;
            secondColour = 13;
        }
        else if (SaveManager.instance.currentHarvester == 8) {
            //PassangerRace
            firstColour = 11;
            secondColour = 21;
        }
        else if (SaveManager.instance.currentHarvester == 9) {
            //Veteran
            firstColour = 14;
            secondColour = 37;
        }
        else if (SaveManager.instance.currentHarvester == 10) {
            //Firefighter
            firstColour = 19;
            secondColour = 17;
        }
        else if (SaveManager.instance.currentHarvester == 11) {
            //Ambulance
            firstColour = 32;
            secondColour = 17;
        }
        else if (SaveManager.instance.currentHarvester == 12) {
            //SchoolBus
            firstColour = 23;
            secondColour = 11;
        }
        else if (SaveManager.instance.currentHarvester == 13) {
            //TruckDump
            firstColour = 27;
            secondColour = 16;
        }
        else if (SaveManager.instance.currentHarvester == 14) {
            //TruckCement
            firstColour = 17;
            secondColour = 22;
        }
        else if (SaveManager.instance.currentHarvester == 15) {
            //Bulldozer
            firstColour = 11;
            secondColour = 22;
        }
        else if (SaveManager.instance.currentHarvester == 16) {
            //Formula
            firstColour = 19;
            secondColour = 17;
        }
        else {
            firstColour = 0;
            secondColour = 0;
        }



        cultivatorMaterialsArray[0] = materials[firstColour];
        cultivatorMaterialsArray[1] = materials[secondColour];
        rotatorMaterialsArray[0] = materials[firstColour];
        shieldRenderer.material = materials[secondColour];

        cultivatorRenderer.materials = cultivatorMaterialsArray;
        rotatorRenderer.materials = rotatorMaterialsArray;


    }


}
