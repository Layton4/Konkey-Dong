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

    private float timeLeft = 45; //time you have at begining of the game
    private float timeBonus = 18; //time you win when you grab a clock

    public bool isGameover;
    public GameObject JBP_postProcesing;


    public float score = 0;
    public TextMeshProUGUI JBP_ScoreText;

    private JBP_SpawnManager JBP_SpawnManagerScript;

    public List<int> currentHighScores = new List<int>();
    public List<string> currentNames = new List<string>();

    
    private bool goToScoreBoard;
   

    [SerializeField] private LayerMask barrelLayerMask;

    public GameObject gameOverPanel;
    public GameObject timesUpMessage;
    public GameObject gamePanel;

    private JBP_PlayerController JBP_playerControllerScript;

    private AudioSource JBP_musicAudioSource;
    public AudioClip JBP_deadMusic;

    public GameObject[] laders;

    private float divider = 2f;


    private void Awake()
    {
        gamePanel.SetActive(true); //we make sure we start with the score and time active on screen
        gameOverPanel.SetActive(false); //and the gameOverPanel Disabled
        JBP_postProcesing.SetActive(false);
        goToScoreBoard = false;

        JBP_playerControllerScript = FindObjectOfType<JBP_PlayerController>();
        JBP_SpawnManagerScript = GameObject.Find("JBP_SpawnManager").GetComponent<JBP_SpawnManager>();

        JBP_musicAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        //we save the current scoreBoard on two lists that we will check and update when we die
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
        Time.timeScale = 1f;
        timerText.text = $"Time: {Mathf.Round(timeLeft)}"; //we show on screen how many time we have

        score = 0; //restart the scoreCounter on each game

    }
    private void Update()
    {
        if(!isGameover) //when we still alive each seconds is reduced to the timer and 1 points is added to the score,all showed on screen
        {
            timeLeft -= Time.deltaTime;
            timerText.text = $"Time: {Mathf.Round(timeLeft)}";

            score += Time.deltaTime / divider;
            JBP_ScoreText.text = $"Score: {Mathf.Round(score)}";
        }

        if(timeLeft <= 0) //when we go out of time we lose and activate the GameOver
        {
            if(!isGameover)
            {
                JBP_playerControllerScript.deadPosition = JBP_playerControllerScript.gameObject.transform.position; //we save our last position before we lose to stay freezed on it
                StopAllCoroutines(); //we stop the attack corroutine
            }
            showGameOverPanel(false);
            StartCoroutine(JBP_deadPlayer());
        }
   
    }

    public void WinTime() //when we grab a clock some time is added to the timer
    {
        timeLeft += timeBonus;
    }


    public IEnumerator JBP_deadPlayer() //when we die
    {
        if (!isGameover) //boolean to make sure we only do this corroutine once on each game
        {
            isGameover = true;
            JBP_postProcesing.SetActive(true); //activate the postprocesing, turning red all the screen

            SearchUserRank();
            RankingUpdate(); //we update the scoreBoard scores looking our new score

            JBP_musicAudioSource.Stop(); //the music of the game ended
            JBP_musicAudioSource.PlayOneShot(JBP_deadMusic, 1f); //we play the game Over Music
            yield return new WaitForSeconds(4.4f); //we wait a few seconds to let see the player the gameOver, the particles and the animation

            if (goToScoreBoard) //if we are inside the top5 scoreboard we need to go to introduce our name on it
            {
                SceneManager.LoadScene("JBP_HighScore");
            }

            else //if we did not arrive to the top 5 we dont need to introduce our name, so we will be send to the menu, where we can just see the scores on highscore board
            {
                SceneManager.LoadScene("JBP_Menu");
            }
        }
    }

    public void showGameOverPanel(bool wasaBarrel) //if we died by a barrel or by time we show one gameOverPanel or another
    {
        if(!wasaBarrel) //if we died by the time, a message of times up will apear and a big YOU LOSE
        {
            timesUpMessage.SetActive(true);
        }
        else //but if we died by just a barrel, we only active the message: YOU LOSE
        {
            timesUpMessage.SetActive(false);
        }
        gameOverPanel.SetActive(true);
       
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

        else //we didn't found our score in any of the top 5 score we don't need to do nothing
        {
            return;
        }
    }


    public void RankingUpdate() //we introduce our new top 5 scores inside the datapersistance
    {
        JBP_DataPersistence.score1 = currentHighScores[0];
        JBP_DataPersistence.score2 = currentHighScores[1];
        JBP_DataPersistence.score3 = currentHighScores[2];
        JBP_DataPersistence.score4 = currentHighScores[3];
        JBP_DataPersistence.score5 = currentHighScores[4];

        JBP_DataPersistence.SaveForFutureGames(); //we save our changes on PlayerPrefs
    }
    #endregion
}
