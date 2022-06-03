using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsMovement : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();  
    }

    void Update()
    {
        if (gameManager.isGameActive)
        {
            transform.Translate(Vector3.back * Time.deltaTime * gameManager.gameSpeed, Space.World);            
        }           
    }
}
