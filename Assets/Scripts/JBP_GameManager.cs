using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JBP_GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public ParticleSystem grabTimeParticles;

    private float timeLeft = 200;
    private float timeBonus = 30;

    public bool isGameover;
    public GameObject JBP_postProcesing;

    private Animator JBP_marioAnimator;

    private void Start()
    {
        Time.timeScale = 1;
        timerText.text = $"Time: {Mathf.Round(timeLeft)}";

        JBP_marioAnimator = GameObject.Find("Mario").GetComponent<Animator>();
    }
    private void Update()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = $"Time: {Mathf.Round(timeLeft)}";

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
        

        Time.timeScale = 0;
    }
}
