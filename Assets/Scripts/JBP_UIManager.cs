using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class JBP_UIManager : MonoBehaviour
{
    public TMP_InputField JBP_inputFieldUser;
    public TextMeshProUGUI[] userNameLeters;

    public GameObject userNamePanel;
    public GameObject HighScorePanel;

    public TextMeshProUGUI[] scoreRanks;
    public TextMeshProUGUI[] playerNames;

    public List<int> currentHighScores = new List<int>();
    public List<string> currentNames = new List<string>();

    public string[] variablesNames;

    public string newUserName;

    private void Awake()
    {
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
        HighScorePanel.SetActive(false);
        userNamePanel.SetActive(true);

        foreach(TextMeshProUGUI letter in userNameLeters)
        {
            letter.text = "-";
        }
    }
    public void UpdateUsername()
    {
        int nameLenght = JBP_inputFieldUser.text.Length;
        for(int i = 0; i < nameLenght; i++)
        {
            userNameLeters[i].text = JBP_inputFieldUser.text[i].ToString();
        }
    }

    public void ConfirmButton()
    {
        if(string.IsNullOrWhiteSpace(JBP_inputFieldUser.text) == false)
        {
            newUserName = JBP_inputFieldUser.text;

            currentNames.Insert(JBP_DataPersistence.scoreBeated, newUserName);
            currentNames.RemoveAt(5);
            NameUpdate();
            JBP_DataPersistence.SaveForFutureGames();

            userNamePanel.SetActive(false);
            HighScorePanel.SetActive(true);
            UpdateHighScorePanel();
        }
    }

    public void NameUpdate()
    {
        JBP_DataPersistence.name1 = currentNames[0];
        JBP_DataPersistence.name2 = currentNames[1];
        JBP_DataPersistence.name3 = currentNames[2];
        JBP_DataPersistence.name4 = currentNames[3];
        JBP_DataPersistence.name5 = currentNames[4];
    }
    public void UpdateHighScorePanel()
    {
        scoreRanks[0].text = JBP_DataPersistence.score1.ToString();
        playerNames[0].text = PlayerPrefs.GetString("name1", "-");


        scoreRanks[1].text = JBP_DataPersistence.score2.ToString();
        playerNames[1].text = PlayerPrefs.GetString("name2", "-");


        scoreRanks[2].text = JBP_DataPersistence.score3.ToString();
        playerNames[2].text = PlayerPrefs.GetString("name3", "-");


        scoreRanks[3].text = JBP_DataPersistence.score4.ToString();
        playerNames[3].text = PlayerPrefs.GetString("name4", "-");


        scoreRanks[4].text = JBP_DataPersistence.score5.ToString();
        playerNames[4].text = PlayerPrefs.GetString("name5", "-");
    }

    public void ReturnButton()
    {
        SceneManager.LoadScene("JBP_Menu");
    }
}
