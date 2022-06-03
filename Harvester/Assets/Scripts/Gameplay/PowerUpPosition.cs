using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPosition : MonoBehaviour
{
    private GameObject model;
    private BoxCollider collider;
    private float colliderZSize;
    private Vector3 offset;  
   
    void Start()
    {
        //Get model holder
        model = GameObject.Find("HarvesterSelection").transform.GetChild(0).gameObject;
        //Get collider
        collider = model.GetComponent<BoxCollider>();
        //Get model size
        colliderZSize = collider.size.z;

        offset = new Vector3(transform.position.x, transform.position.y, transform.position.z + colliderZSize);
        gameObject.transform.position = offset;
    }

}
