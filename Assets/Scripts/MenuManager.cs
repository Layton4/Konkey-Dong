using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MenuManager: MonoBehaviour
{
    public GameObject[] panels;
    public Button[] AutoSelectButtons;
    public GameObject[] SelectionBarrels;

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

    void Start()
    {
        OpenPanel(0); //When we arrive to the menu we make sure to activate only the MenuPanel, the first element of the array
        UpdateHighScoreBoard();
    }
    void Update()
    {
        
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

    public void ExitButton()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void Save()
    {
        DataPersistance.SaveForFutureGames();
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

        DataPersistance.MusicToggle = intToggleMusic;
        DataPersistance.SoundToggle = intToggleSound;

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
        scoreRanks[0].text = DataPersistance.score1.ToString();
        playerNames[0].text = DataPersistance.name1;

        scoreRanks[1].text = DataPersistance.score2.ToString();
        playerNames[1].text = DataPersistance.name2;


        scoreRanks[2].text = DataPersistance.score3.ToString();
        playerNames[2].text = DataPersistance.name3;


        scoreRanks[3].text = DataPersistance.score4.ToString();
        playerNames[3].text = DataPersistance.name4;


        scoreRanks[4].text = DataPersistance.score5.ToString();
        playerNames[4].text = DataPersistance.name5;
    }

    public void TestSound()
    {
        audioManagerAudioSource.Play();
    }
}
