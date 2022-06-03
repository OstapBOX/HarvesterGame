using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawn : MonoBehaviour
{
    public GameObject ground;
    public GameObject groundSpawnIndicator;
    private float destroyBorder = 199;
    private Vector3 groundIndicatorReset = new Vector3(0, 0, 0);
    private Vector3 spawnPos = new Vector3(0, 0, 200);
    private Vector3 startPos = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        groundSpawn(startPos);
        groundSpawn(spawnPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (groundSpawnIndicator.transform.position.z < -destroyBorder)
        {
            groundSpawn(spawnPos);
            groundSpawnIndicator.transform.position = groundIndicatorReset;
        }
    }

    void groundSpawn(Vector3 spawnPos)
    {
        Instantiate(ground, spawnPos, ground.transform.rotation);
    }

}
