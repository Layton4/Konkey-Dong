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
}