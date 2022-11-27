using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    public List<int> currentHighScores = new List<int>();
    public List<string> currentNames = new List<string>();

    private void Awake()
    {
        JBP_SpawnManagerScript = GameObject.Find("JBP_SpawnManager").GetComponent<JBP_SpawnManager>();

        currentHighScores.Add(PlayerPrefs.GetInt("score1")); //top1
        currentHighScores.Add(PlayerPrefs.GetInt("score2")); //top2
        currentHighScores.Add(PlayerPrefs.GetInt("score3")); //top3
        currentHighScores.Add(PlayerPrefs.GetInt("score4")); //top4
        currentHighScores.Add(PlayerPrefs.GetInt("score5")); //top5

        currentNames.Add(PlayerPrefs.GetString("name1"));
        currentNames.Add(PlayerPrefs.GetString("name2"));
        currentNames.Add(PlayerPrefs.GetString("name3"));
        currentNames.Add(PlayerPrefs.GetString("name4"));
        currentNames.Add(PlayerPrefs.GetString("name5"));
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
            StartCoroutine(JBP_deadPlayer());
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
        
        for(int i = 0; i < 5; i++)
        {
            if(Mathf.Round(score) > currentHighScores[i])
            {
                JBP_DataPersistence.scoreBeated = i;
                UpdateScores();
                JBP_DataPersistence.SaveForFutureGames();

                //yield return new WaitForSeconds(4f);

                SceneManager.LoadScene("JBP_HighScore");
            }
        }

        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("JBP_Menu");


    }

    private void UpdateScores()
    {
        int scoreInt = (int)Mathf.Round(score);
        if(JBP_DataPersistence.scoreBeated == 0)
        {
            JBP_DataPersistence.score1 = scoreInt;
        }

        else if (JBP_DataPersistence.scoreBeated == 1)
        {
            JBP_DataPersistence.score2 = scoreInt;
        }

        else if (JBP_DataPersistence.scoreBeated == 2)
        {
            JBP_DataPersistence.score3 = scoreInt;
        }

        else if (JBP_DataPersistence.scoreBeated == 3)
        {
            JBP_DataPersistence.score4 = scoreInt;
        }

        else if (JBP_DataPersistence.scoreBeated == 4)
        {
            JBP_DataPersistence.score5 = scoreInt;
        }

    }


    public void searchUserRank(int userScore)
    {
        for (int i = 0; i < 5; i++)
        {
            if (Mathf.Round(userScore) > currentHighScores[i])
            {
                JBP_DataPersistence.scoreBeated = i;
                //UpdateScores();
                //JBP_DataPersistence.SaveForFutureGames();
            }
            break;
        }
    }
}
