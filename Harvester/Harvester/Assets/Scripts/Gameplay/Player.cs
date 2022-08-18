using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public int wheatCollected;
    public int wheatAmount;

    public int dollarsAmount;
    // Start is called before the first frame update
    void Start()
    {
        wheatCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWheatAmount();
    }

    private void UpdateWheatAmount() {
        wheatAmount += wheatCollected;
    }


    public void WheatCollected(int _wheat) {
        wheatCollected += _wheat; 
    }
}
