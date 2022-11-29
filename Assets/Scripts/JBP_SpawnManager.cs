using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JBP_SpawnManager : MonoBehaviour
{

    public GameObject prefabBarrel;

    public Animator[] JBP_DonkeyKongs;

    public GameObject JBP_clockPrefab;
    private float JBP_timeBetweenClocks = 20f; //time to spawn a new clock on scene

    public int randomIndex;

    public Transform[] spawnPositionClocks;

    public GameObject player;
    public Transform[] spawnPositionsBarrels;

    //Scripts
    private JBP_GameManager JBP_GameManagerScript;

    public List<GameObject> JBP_barrelsOnScene = new List<GameObject>();

    private void Awake()
    {
        JBP_GameManagerScript = FindObjectOfType<JBP_GameManager>();
        StartCoroutine(ActivateMonkey()); //the enemy start throwing barrels to the player
    }

    private void Start()
    {
        //InvokeRepeating("BarrelSpawn", 1f, Random.Range(minTime, maxTime));
        InvokeRepeating("ClockSpawn", JBP_timeBetweenClocks, JBP_timeBetweenClocks); //Each 20 seconds a clock is spawned on scene to let the player win time
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
        Vector3 barrelPos = spawnPositionsBarrels[indx].transform.position; //the position where the barrel will spawn

        GameObject barrel = Instantiate(prefabBarrel, barrelPos, prefabBarrel.transform.rotation); //we instantiate the barrel on the position barrelPos
        //JBP_barrelsOnScene.Add(barrel);
    }

    public IEnumerator ActivateMonkey()
    {
        while(!JBP_GameManagerScript.isGameover) //if the game is still on, so we didn't lose, the enemie will continue attacking us
        {

        int monkeyindx = GetRandomIndex(spawnPositionsBarrels); //we generate a random index
        yield return new WaitForSeconds(1f);

            JBP_DonkeyKongs[monkeyindx].SetBool("attack", true); //we activate the attack animation of the proper random Monkey

        yield return new WaitForSeconds(1.75f); //we wait until in the animation the monkey throw a barrel

        BarrelSpawn(monkeyindx); //And we spawn a barrel just in time

        yield return new WaitForSeconds(1.25f); //we let the animation end to let go the enemie out of the screen

        JBP_DonkeyKongs[monkeyindx].SetBool("attack", false); //and return it to his idle state until is called again to attack us


        }
    }

    public void ClockSpawn()
    {
        Vector3 clockPos = GetRandomPosition(spawnPositionClocks);
        Instantiate(JBP_clockPrefab, clockPos, JBP_clockPrefab.transform.rotation);
    }
}
