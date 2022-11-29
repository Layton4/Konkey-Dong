using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class JBP_AudioManager : MonoBehaviour
{
    public static JBP_AudioManager Instance;

    public AudioMixer Mixer;

    public const string MusicKey = "MusicVolume";
    public const string SFXKey = "SFXVolume";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadVolume();
    }

    void LoadVolume() //volume saved on VolumeSettings
    {
        float musicVolume = PlayerPrefs.GetFloat(MusicKey, 0.5f); //We remember the last setting of the Music volume and we want to put it on the audioMixer Musicvolume
        float sfxVolume = PlayerPrefs.GetFloat(SFXKey, 0.5f); //We remember the last setting of the SFX volume and we want to put it on the audioMixer SFXvolume

        //I have created first the variables musicVolume and sfxVolume and then introduced them into the Audiomixer volume setting because i already put it directly it bring some error we don't have this way.
        Mixer.SetFloat(JBP_VolumeSettings.MixerMusic, Mathf.Log10(musicVolume) * 20); 
        Mixer.SetFloat(JBP_VolumeSettings.MixerSFX, Mathf.Log10(sfxVolume) * 20);
    }
}
