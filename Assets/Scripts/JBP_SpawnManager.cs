using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JBP_SpawnManager : MonoBehaviour
{
    public GameObject prefabBarrel;
    private float minTime = 2.5f;
    private float maxTime = 5f;

    public GameObject player;
    public Transform[] spawnPositions;

    private void Start()
    {
        InvokeRepeating("BarrelSpawn", 1f, Random.Range(minTime, maxTime));
    }
    public Vector3 GetRandomPosition()
    {
        int randomIndex = Random.Range(0, spawnPositions.Length);

        if(spawnPositions[randomIndex].position.y < player.transform.position.y)
        {
            randomIndex = Random.Range(0, spawnPositions.Length);
        }

        return spawnPositions[randomIndex].transform.position;
    }

    public void BarrelSpawn()
    {

        Instantiate(prefabBarrel, GetRandomPosition(), prefabBarrel.transform.rotation);
    }
}
