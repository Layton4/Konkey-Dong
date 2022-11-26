using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JBP_SpawnManager : MonoBehaviour
{
    #region Barrels
    public GameObject prefabBarrel;
    private float minTime = 3f;
    private float maxTime = 6f;
    #endregion

    public Animator[] JBP_DonkeyKongs;

    public GameObject JBP_clockPrefab;
    private float JBP_timeBetweenClocks = 20f; //time to spawn a new clock on scene

    public int randomIndex;

    public Transform[] spawnPositionClocks;

    public GameObject player;
    public Transform[] spawnPositionsBarrels;

    private JBP_GameManager JBP_GameManagerScript;

    private void Awake()
    {
        JBP_GameManagerScript = FindObjectOfType<JBP_GameManager>();
        StartCoroutine(ActivateMonkey());
    }

    private void Start()
    {
        //InvokeRepeating("BarrelSpawn", 1f, Random.Range(minTime, maxTime));
        InvokeRepeating("ClockSpawn", JBP_timeBetweenClocks, JBP_timeBetweenClocks);
    }

    public Vector3 GetRandomPosition(Transform[] positionArray)
    {
        randomIndex = Random.Range(0, positionArray.Length);

        return positionArray[randomIndex].transform.position;
    }

    public int GetRandomIndex(Transform[] positionArray)
    {
        int randomIndex = Random.Range(0, positionArray.Length);

        return randomIndex;
      
    }

    public void BarrelSpawn(int indx)
    {
        //int idx = GetRandomIndex(spawnPositionsBarrels);

        Vector3 barrelPos = spawnPositionsBarrels[indx].transform.position;

        //ActivateMonkey(randomIndex);
        Instantiate(prefabBarrel, barrelPos, prefabBarrel.transform.rotation);
    }

    public IEnumerator ActivateMonkey()
    {
        while(!JBP_GameManagerScript.isGameover)
        {

        int monkeyindx = GetRandomIndex(spawnPositionsBarrels);
        yield return new WaitForSeconds(1f);

            JBP_DonkeyKongs[monkeyindx].SetBool("attack", true);

        yield return new WaitForSeconds(1.75f);

        BarrelSpawn(monkeyindx);

        yield return new WaitForSeconds(1.25f);

        JBP_DonkeyKongs[monkeyindx].SetBool("attack", false);


        }
    }

    public void ClockSpawn()
    {
        Vector3 clockPos = GetRandomPosition(spawnPositionClocks);
        Instantiate(JBP_clockPrefab, clockPos, JBP_clockPrefab.transform.rotation);
    }
}
