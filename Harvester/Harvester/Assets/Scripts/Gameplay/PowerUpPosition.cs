using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPosition : MonoBehaviour
{
    [SerializeField] private GameObject vehicleLight;
    private GameObject vehicle;
    
    private BoxCollider vehicleCollider;

    private float vehicleZSize;


    private Vector3 offset;
   
    void Start()
    {
 
        vehicle = GameObject.Find("HarvesterSelection").transform.GetChild(0).gameObject;
 
        vehicleCollider = vehicle.GetComponent<BoxCollider>();
    
        vehicleZSize = vehicleCollider.size.z;

        offset = new Vector3(transform.position.x, transform.position.y, transform.position.z + vehicleZSize);

        gameObject.transform.position = offset;

    }

}
