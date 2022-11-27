using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JBP_DataPersistence
{
    //Music and Audio Values
    public static float SoundVolume;
    public static int SoundToggle;

    public static float MusicVolume;
    public static int MusicToggle;

    //HighScore data
    public static string name1 = "";
    public static string name2 = "";
    public static string name3 = "";
    public static string name4 = "";
    public static string name5 = "";

    public static int score1;
    public static int score2;
    public static int score3;
    public static int score4;
    public static int score5;

    public static int scoreBeated;


    public static void SaveForFutureGames()
    {
        //Music and SFX
        PlayerPrefs.SetFloat("Sound_Volume", SoundVolume);
        PlayerPrefs.SetInt("Sound_Toggle", SoundToggle);
        PlayerPrefs.SetFloat("Music_Volume", MusicVolume);
        PlayerPrefs.SetInt("Music_Toggle", MusicToggle);

        //HighScore values
        PlayerPrefs.SetInt("score1", score1);
        PlayerPrefs.SetString("name1", name1);

        PlayerPrefs.SetInt("score2", score2);
        PlayerPrefs.SetString("name2", name2);

        PlayerPrefs.SetInt("score3", score3);
        PlayerPrefs.SetString("name3", name3);

        PlayerPrefs.SetInt("score4", score4);
        PlayerPrefs.SetString("name4", name4);

        PlayerPrefs.SetInt("score5", score5);
        PlayerPrefs.SetString("name5", name5);
    }

    public static void LoadUserData()
    {
        JBP_DataPersistence.score1 = PlayerPrefs.GetInt("score1");
        JBP_DataPersistence.score2 = PlayerPrefs.GetInt("score2");
        JBP_DataPersistence.score3 = PlayerPrefs.GetInt("score3");
        JBP_DataPersistence.score4 = PlayerPrefs.GetInt("score4");
        JBP_DataPersistence.score5 = PlayerPrefs.GetInt("score5");

        JBP_DataPersistence.name1 = PlayerPrefs.GetString("name1");
        JBP_DataPersistence.name2 = PlayerPrefs.GetString("name2");
        JBP_DataPersistence.name3 = PlayerPrefs.GetString("name3");
        JBP_DataPersistence.name4 = PlayerPrefs.GetString("name4");
        JBP_DataPersistence.name5 = PlayerPrefs.GetString("name5");
    }
}