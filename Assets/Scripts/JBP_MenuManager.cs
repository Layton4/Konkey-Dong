using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class JBP_MenuManager: MonoBehaviour
{
    public GameObject[] panels;
    public Button[] AutoSelectButtons;
    public GameObject[] SelectionBarrels;
    private float alphaValue;

    //Audio
    public Toggle MusicToggle;
    public int intToggleMusic;

    public Toggle SoundToggle;
    public int intToggleSound;

    public AudioSource audioManagerAudioSource;
    public AudioClip testingSoundClip;

    //HighScores
    public TextMeshProUGUI[] scoreRanks;
    public TextMeshProUGUI[] playerNames;

    private void Awake()
    {
        JBP_DataPersistence.LoadUserData();
    }
    void Start()
    {
        //OpenPanel(0); //When we arrive to the menu we make sure to activate only the MenuPanel, the first element of the array
        StartCoroutine(InitialScreen());
        UpdateHighScoreBoard();
    }
    public void SelectButton(GameObject selectionImages)
    {
        selectionImages.SetActive(true);
    }
    public void DeselectButton(GameObject selectionImages)
    {
        selectionImages.SetActive(false);
    }

    #region MenuButton
    public void OpenPanel(int index)
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        panels[index].SetActive(true);

        /*EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(AutoSelectButtons[index]);
        */
        foreach (GameObject barrel in SelectionBarrels) //we turn off all the barrels of the UI to avoid keeping them when we return to that panel
        {
            DeselectButton(barrel);
        }
        EventSystem.current.SetSelectedGameObject(null);
        AutoSelectButtons[index].Select();
    }

    /*public void ExitButton()
    {
        Application.Quit();
    }*/

    public void StartButton()
    {
        SceneManager.LoadScene("JBP_Game");
    }

    public void Save()
    {
        JBP_DataPersistence.SaveForFutureGames();
    }
    #endregion

    #region Audio
    public void UpdateIntMusic_Sound()
    {
        /*
        intToggleMusic = BoolToInt(MusicToggle.GetComponent<Toggle>().isOn);
        intToggleSound = BoolToInt(SoundToggle.GetComponent<Toggle>().isOn);
        */
        intToggleMusic = BoolIntPrueba(MusicToggle.GetComponent<Toggle>().isOn);
        intToggleSound = BoolIntPrueba(SoundToggle.GetComponent<Toggle>().isOn);

        JBP_DataPersistence.MusicToggle = intToggleMusic;
        JBP_DataPersistence.SoundToggle = intToggleSound;

    }
    public int BoolIntPrueba(bool b)
    {
        if (b == false)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
    #endregion

    public void UpdateHighScoreBoard()
    {
        scoreRanks[0].text = JBP_DataPersistence.score1.ToString();
        playerNames[0].text = JBP_DataPersistence.name1;

        scoreRanks[1].text = JBP_DataPersistence.score2.ToString();
        playerNames[1].text = JBP_DataPersistence.name2;


        scoreRanks[2].text = JBP_DataPersistence.score3.ToString();
        playerNames[2].text = JBP_DataPersistence.name3;


        scoreRanks[3].text = JBP_DataPersistence.score4.ToString();
        playerNames[3].text = JBP_DataPersistence.name4;


        scoreRanks[4].text = JBP_DataPersistence.score5.ToString();
        playerNames[4].text = JBP_DataPersistence.name5;
    }

    public void TestSound()
    {
        audioManagerAudioSource.Play();
    }

    public IEnumerator InitialScreen()
    {
        alphaValue = 0;
        panels[4].SetActive(true);
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < 10; i += 1)
        {
            alphaValue += 0.1f;
            Color panelColor = panels[4].GetComponent<Image>().color;
            panelColor.a = alphaValue;

            panels[4].GetComponent<Image>().color = panelColor;

            yield return new WaitForSeconds(0.1f);
        }

        alphaValue = 1;

        for (int i = 0; i < 10; i+=1)
        {
            alphaValue -= 0.1f;
            Color panelColor = panels[4].GetComponent<Image>().color;
            panelColor.a = alphaValue;

            panels[4].GetComponent<Image>().color = panelColor;

            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.2f);
        OpenPanel(0);
    }

}
