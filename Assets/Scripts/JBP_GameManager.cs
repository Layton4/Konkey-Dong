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
    private float timeBonus;

    private void Start()
    {
        timerText.text = $"Time: {Mathf.Round(timeLeft)}";
    }
    private void Update()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = $"Time: {Mathf.Round(timeLeft)}";
    }

    public void WinTime()
    {
        timeLeft += timeBonus;
    }
}
