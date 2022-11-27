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

    public string[] variablesNames;

    public string newUserName;

    private void Awake()
    {
        
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
            PlayerPrefs.SetString(variablesNames[JBP_DataPersistence.scoreBeated], newUserName);

            userNamePanel.SetActive(false);
            HighScorePanel.SetActive(true);
            UpdateHighScore();
        }
    }

    public void UpdateHighScore()
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
