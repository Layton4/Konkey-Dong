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

    private float timeLeft = 100;
    private float timeBonus = 25;

    public bool isGameover;
    public GameObject JBP_postProcesing;


    public float score = 0;
    public TextMeshProUGUI JBP_ScoreText;

    private JBP_SpawnManager JBP_SpawnManagerScript;

    public List<int> currentHighScores = new List<int>();
    public List<string> currentNames = new List<string>();

    private bool corroutineDone;
    private bool goToScoreBoard;

    [SerializeField] private LayerMask barrelLayerMask;

    private void Awake()
    {
        corroutineDone = false;
        goToScoreBoard = false;

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
        if(!isGameover)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = $"Time: {Mathf.Round(timeLeft)}";

            score += Time.deltaTime;
            JBP_ScoreText.text = $"Score: {Mathf.Round(score)}";
        }
        
    }

    public void WinTime()
    {
        timeLeft += timeBonus;
    }


    public IEnumerator JBP_deadPlayer()
    {
        JBP_postProcesing.SetActive(true);
        foreach (GameObject Obstacle in JBP_SpawnManagerScript.JBP_barrelsOnScene)
        {
            Destroy(Obstacle);
        }

        SearchUserRank();
        RankingUpdate();
        corroutineDone = true;

        yield return new WaitForSeconds(4f);

        if(goToScoreBoard)
        {
            SceneManager.LoadScene("JBP_HighScore");
        }

        else
        {
            SceneManager.LoadScene("JBP_Menu");
        }
    }

    #region ScoreBoard
    public void SearchUserRank()
    {
        int scoreRank = 5; 
        for (int i = 0; i < 5; i++) //we search in what rankposition we are with our score
        {
            if (Mathf.Round(score) > currentHighScores[i]) //if we are higher than anyother scores of the top5
            {
                scoreRank = i;
                //JBP_DataPersistence.SaveForFutureGames();
                break; //we stop looking when we found it, so we exit the for
            }
            
        }

        if (scoreRank < 5) //if we had found the rank to our score
        {
            JBP_DataPersistence.scoreBeated = scoreRank; //we save the position
            int scoreInt = (int)Mathf.Round(score);

            goToScoreBoard = true;

            currentHighScores.Insert(scoreRank, scoreInt); //we insert the score in the list of scores and in the right position
            currentHighScores.RemoveAt(5); //we remove the top 6 score, because with our new score is out of the top5
        }

        else //we didn't found our score in any of the top 5 score
        {
            Debug.Log("Indice fuera de rango"); 
            return;
        }
    }


    public void RankingUpdate() //we introduce our new top 5 inside the datapersistance
    {
        JBP_DataPersistence.score1 = currentHighScores[0];
        JBP_DataPersistence.score2 = currentHighScores[1];
        JBP_DataPersistence.score3 = currentHighScores[2];
        JBP_DataPersistence.score4 = currentHighScores[3];
        JBP_DataPersistence.score5 = currentHighScores[4];

        JBP_DataPersistence.SaveForFutureGames();
    }
    #endregion
}
