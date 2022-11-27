using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SceneManagement;

public class JBP_GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public ParticleSystem grabTimeParticles;

    private float timeLeft = 150;
    private float timeBonus = 25;

    public bool isGameover;
    public GameObject JBP_postProcesing;


    public float score = 0;
    public TextMeshProUGUI JBP_ScoreText;

    private JBP_SpawnManager JBP_SpawnManagerScript;

    private void Awake()
    {
        JBP_SpawnManagerScript = GameObject.Find("JBP_SpawnManager").GetComponent<JBP_SpawnManager>();
    }
    private void Start()
    {
        Time.timeScale = 1;
        timerText.text = $"Time: {Mathf.Round(timeLeft)}";

        score = 0;

    }
    private void Update()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = $"Time: {Mathf.Round(timeLeft)}";

        score += Time.deltaTime;
        JBP_ScoreText.text = $"Score: {Mathf.Round(score)}";
        

        if(isGameover)
        {
            deadPlayer();
        }
    }

    public void WinTime()
    {
        timeLeft += timeBonus;
    }

    public void deadPlayer()
    {
        JBP_postProcesing.SetActive(true);
        foreach(GameObject Obstacle in JBP_SpawnManagerScript.JBP_barrelsOnScene)
        {
            Destroy(Obstacle);  
        }

        Time.timeScale = 0;
    }

    public IEnumerator JBP_deadPlayer()
    {
        JBP_postProcesing.SetActive(true);
        foreach (GameObject Obstacle in JBP_SpawnManagerScript.JBP_barrelsOnScene)
        {
            Destroy(Obstacle);
        }
        
        /*foreach(Animator monkeyAnim in JBP_SpawnManagerScript.JBP_DonkeyKongs)
        {
            monkeyAnim.SetBool("attack", false);
        }*/

        yield return new WaitForSeconds(4f);

        
    }

}
