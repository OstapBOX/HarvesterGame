using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundColor : MonoBehaviour
{
    [SerializeField] private Material[] groundMaterials;
    
    void Start()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = groundMaterials[Random.Range(0, groundMaterials.Length)];
    }
}
