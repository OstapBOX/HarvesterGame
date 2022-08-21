using UnityEngine;

public class LineSpawn : MonoBehaviour {
    public GameObject[] obstaclePrefabObjects;
    public GameObject lineSpawnIndicator, gardenRotation;

    [SerializeField] private GameObject canister;
    [SerializeField] private GameObject wrench;
    [SerializeField] private GameObject[] stones;
    [SerializeField] private GameObject[] plants;
    [SerializeField] private GameObject[] gardenPrefabObjects;
    [SerializeField] private GameObject[] housePrefabObjects;
    [SerializeField] private GameObject[] signs;


    public float zDistanceBetweenLines = 6.0f,
                 xDistanceBetweenBlocks = 6.0f;


    private int roadBlocksOffset = 0,
                blocksAmount = 5,
                obstacleRotationIndex,
                plantsType;

    private const float gardenCornerLengthOffset = 2.0f,
                        gardenCornerWidthOffset = 4.1f,
                        zBlocksSpawnPosition = 70.0f,
    zHousesSpawnOffset = 10.0f;

    private float[] anglesRotation = { 90f, 180f, 270f };

    private Vector3 indicatorReset = new Vector3(0, 0, 0);
    Vector3[] gardenCornersOffset = {new Vector3(gardenCornerLengthOffset, 0, -gardenCornerWidthOffset),
                                     new Vector3(-gardenCornerWidthOffset, 0, -gardenCornerLengthOffset),
                                     new Vector3(-gardenCornerLengthOffset, 0, -gardenCornerWidthOffset),
                                     new Vector3(gardenCornerWidthOffset, 0, -gardenCornerLengthOffset) };

    private int spawnStoneBlock,
                randomCanisterBlock,
                randomHealBlock,
                stoneSpawnFrequence = 18,
                canisterSpawnFrequence = 120,
                healSpawnFrequence = 135,
                gardenObjectsInLine = 2,
                housesObjectsInLine = 2,
                randomGardenObject,
                minFrequenceHouseSpawn = 9,
                maxFrequenceHouseSpawn = 14,
                minFrequenceBlocksAmount = 5,
                maxFrequenceBlocksAmount = 15,
                blocksAmountPrev = 0,
                linesSpawned = 0,
                leftHouseSpawned = 0,
                rightHouseSpawned = 0,
                houseSpawnAmountPeriod = 0,
                linesSpawnAmountPeriod = 0;




    private bool canisterSpawned = true,
                 stoneSpawned = true,
                 healSpawned = true,
                 incrementalBlocksAmount = false,
                 decrementalBlocksAmount = false,
                 incrementalDual = false,
                 decrementalDual = false,
                 incrementalSpawned = true,
                 leftHouseOffset = true;



    // Start is called before the first frame update
    void Start() {        
       blocksAmountPrev = blocksAmount;
        Application.targetFrameRate = 60;
        plantsType = PlantsType();
    }

    // Update is called once per frame
    void Update() {
        if (changeBlockAmount()) {
            blockAmount();
            linesSpawned = 0;
        }


        if (lineSpawnIndicator.transform.position.z < -zDistanceBetweenLines) {
            if (blocksAmount > blocksAmountPrev) {
                incrementalBlocksAmount = true;
                if (blocksAmount - blocksAmountPrev == 2) {
                    incrementalDual = true;
                }
            }
            else if (blocksAmount < blocksAmountPrev) {
                decrementalBlocksAmount = true;
                if (blocksAmount - blocksAmountPrev == -2) {
                    decrementalDual = true;
                }
            }

            lineSpawn();

            linesSpawned++;

            lineSpawnIndicator.transform.position = indicatorReset;
        }

    }

    //Spawn a line with 6 elements
    private void lineSpawn() {
        canisterSpawned = false;
        stoneSpawned = false;
        healSpawned = false;

        int objectsInLine = gardenObjectsInLine + blocksAmount + housesObjectsInLine;

        Vector3[] blocksPosition = blockSpawnPosition(roadBlocksOffset, objectsInLine);

        int spawnedObjectId,
        gardenPrefabAmount = gardenPrefabObjects.Length,
        leftHousePosition = 0,
        rightHousePosition = objectsInLine - 1,
        leftGardenPosition = 1,
        rightGardenPosition = objectsInLine - 2;


        //Change position of spawned block.
        for (int blockNumber = 0; blockNumber < objectsInLine; blockNumber++) {

            if (blockNumber == leftHousePosition || blockNumber == rightHousePosition) {
                //if (spawnLeftHouse()) {
                //    spawnHouse(blocksPosition[leftHousePosition], blockNumber, leftHouseOffset);
                //    leftHouseSpawned = 0;
                //}
                //leftHouseSpawned++;

                //if (spawnRightHouse()) {
                //    spawnHouse(blocksPosition[rightHousePosition], blockNumber, !leftHouseOffset);
                //    rightHouseSpawned = 0;
                //}
                //rightHouseSpawned++;
            }

            else if (blockNumber == leftGardenPosition || blockNumber == rightGardenPosition) {

                //Choose random garden prefab;
                randomGardenObject = Random.Range(0, gardenPrefabAmount);

                if (blockNumber == leftGardenPosition) {
                    //incremental odd dual
                    if ((incrementalBlocksAmount) && blocksAmount % 2 == 0)//Incremental Left
                    {
                        Instantiate(gardenPrefabObjects[randomGardenObject], blocksPosition[leftGardenPosition] + gardenCornersOffset[0], gardenRotation.transform.rotation);
                        if (incrementalDual)//Incremental Right
                        {
                            Instantiate(gardenPrefabObjects[randomGardenObject], blocksPosition[rightGardenPosition] + gardenCornersOffset[2], gardenRotation.transform.rotation);
                        }
                    }
                    //decremental odd dual
                    if ((decrementalBlocksAmount) && blocksAmount % 2 == 1)//Decremental Left
                        {
                        int randomSign01 = Random.Range(0, 3);
                        Instantiate(signs[randomSign01], blocksPosition[leftGardenPosition] + gardenCornersOffset[1], signs[randomSign01].transform.rotation);
                        if (decrementalDual)//Decremental Right
                        {

                            int randomSign02 = Random.Range(0, 3);

                            Instantiate(signs[randomSign02], blocksPosition[rightGardenPosition] + gardenCornersOffset[3], signs[randomSign02].transform.rotation);
                        }
                    }
                }

                else if (blockNumber == rightGardenPosition) {
                    //incremental odd dual
                    if ((incrementalBlocksAmount) && blocksAmount % 2 == 1)//Incremental Right
                    {
                        Instantiate(gardenPrefabObjects[randomGardenObject], blocksPosition[rightGardenPosition] + gardenCornersOffset[2], gardenRotation.transform.rotation);
                        if (incrementalDual)//Incremental Left
                        {
                            Instantiate(gardenPrefabObjects[randomGardenObject], blocksPosition[leftGardenPosition] + gardenCornersOffset[0], gardenRotation.transform.rotation);
                        }
                    }
                    //decremental even dual
                    if ((decrementalBlocksAmount) && blocksAmount % 2 == 0)//Decremental Right
                    {
                        int randomSign01 = Random.Range(0, 3);
                        Instantiate(signs[randomSign01], blocksPosition[rightGardenPosition] + gardenCornersOffset[3], signs[randomSign01].transform.rotation);
                        if (decrementalDual)//Decremental Left
                        {
                            int randomSign02 = Random.Range(0, 3);
                            Instantiate(signs[randomSign02], blocksPosition[leftGardenPosition] + gardenCornersOffset[1], signs[randomSign02].transform.rotation);
                        }
                    }
                }
                Instantiate(gardenPrefabObjects[randomGardenObject], blocksPosition[blockNumber], gardenPrefabObjects[randomGardenObject].transform.rotation);
            }

            else {
                //If randomStoneBlock = 0, spawn an stone.
                spawnStoneBlock = Random.Range(0, stoneSpawnFrequence);

                //If randomCanisterBlock = 0, spawn an canister.
                randomCanisterBlock = Random.Range(0, canisterSpawnFrequence);

                randomHealBlock = Random.Range(0, healSpawnFrequence);

                //Spawn stone if it haven't been spawned in this line before.
                if (spawnStoneBlock == 0 && stoneSpawned == false) {
                    Instantiate(stones[Random.Range(0, stones.Length)], blocksPosition[blockNumber], Quaternion.Euler(0f, anglesRotation[obstacleRotationIndex], 0f));
                    stoneSpawned = true;
                }

                //Spawn canister if it haven't been spawned in this line before.
                else if (randomCanisterBlock == 0 && canisterSpawned == false) {

                    Instantiate(canister, blocksPosition[blockNumber], Quaternion.Euler(0f, anglesRotation[obstacleRotationIndex], 0f));
                    canisterSpawned = true;
                }

                else if (randomHealBlock == 0 && healSpawned == false) {
                    Instantiate(wrench, blocksPosition[blockNumber], Quaternion.Euler(0f, anglesRotation[obstacleRotationIndex], 0f));
                    healSpawned = true;
                }

                //Spawn an wheat block.
                else {
                    Instantiate(plants[Random.Range(0, plantsType)], blocksPosition[blockNumber], Quaternion.Euler(0f, anglesRotation[obstacleRotationIndex], 0f));
                }
                obstacleRotationIndex = Random.Range(0, 3);
            }
        }

        blocksAmountPrev = blocksAmount;
        incrementalBlocksAmount = false;
        decrementalBlocksAmount = false;
        incrementalDual = false;
        decrementalDual = false;
    }

    private Vector3[] blockSpawnPosition(float roadBlocksOffset, int blocksAmount) {
        Vector3[] blocksPosition = new Vector3[blocksAmount];

        float roadOffset = roadBlocksOffset * xDistanceBetweenBlocks;
        float blocksLeftPosition = (blocksAmount / 2) * xDistanceBetweenBlocks;
        float firstSpawnPosition = roadOffset - blocksLeftPosition + (xDistanceBetweenBlocks / 2);

        for (int i = 0; i < blocksAmount; i++) {
            blocksPosition[i] = new Vector3(firstSpawnPosition + i * xDistanceBetweenBlocks, obstaclePrefabObjects[0].transform.position.y, zBlocksSpawnPosition);
        }

        return blocksPosition;
    }

    private int blockAmount() {
        int minBlocksAmount = 3, maxBlocksAmount = 5;

        if (incrementalSpawned) {
            if (blocksAmount > minBlocksAmount) {
                blocksAmount -= Random.Range(1, 3);
            }
            incrementalSpawned = false;
        }
        else {
            if (blocksAmount < maxBlocksAmount) {
                blocksAmount += Random.Range(1, 3);

            }
            incrementalSpawned = true;
        }

        return blocksAmount;
    }

    private bool changeBlockAmount() {
        linesSpawnAmountPeriod = Random.Range(minFrequenceBlocksAmount, maxFrequenceBlocksAmount);
        if (linesSpawned > linesSpawnAmountPeriod) {
            return true;
        }
        return false;
    }

    //private bool spawnLeftHouse() {
    //    houseSpawnAmountPeriod = Random.Range(minFrequenceHouseSpawn, maxFrequenceHouseSpawn);
    //    if (leftHouseSpawned > houseSpawnAmountPeriod) {
    //        return true;
    //    }
    //    return false;
    //}

    //private bool spawnRightHouse() {
    //    houseSpawnAmountPeriod = Random.Range(minFrequenceHouseSpawn, maxFrequenceHouseSpawn);
    //    if (rightHouseSpawned > houseSpawnAmountPeriod) {
    //        return true;
    //    }
    //    return false;
    //}

    //private void spawnHouse(Vector3 blocksPosition, int blockNumber, bool leftHouseOffset) {
    //    int randomHouseObstacle = Random.Range(0, housePrefabObjects.Length);
    //    int randomRotation = Random.Range(0, anglesRotation.Length);
    //    float randomHouseOffsetX = Random.Range(10, 13);

    //    if (leftHouseOffset) {
    //        randomHouseOffsetX = -randomHouseOffsetX;
    //    }

    //    Instantiate(housePrefabObjects[randomHouseObstacle], blocksPosition + new Vector3(randomHouseOffsetX, (housePrefabObjects[randomHouseObstacle].transform.position.y), zHousesSpawnOffset), Quaternion.Euler(0, anglesRotation[randomRotation], 0));
    //}

    private int PlantsType() {
        return 1 + PlayerPrefs.GetInt("CornBought", 0) +
            PlayerPrefs.GetInt("SaladBought", 0) +
            PlayerPrefs.GetInt("CarrotBought", 0) +
            PlayerPrefs.GetInt("CottonBought", 0) +
            PlayerPrefs.GetInt("SunflowerBought", 0) +
            PlayerPrefs.GetInt("PumpkinBought", 0);
    }
}


