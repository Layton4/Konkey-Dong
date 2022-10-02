using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer Mixer;

    //Sliders
    public Slider MusicSlider;
    public Slider SFXSlider;

    //Name of the exposed parameter of the volume in the audiomixer
    public const string MixerMusic = "MusicVolume";
    public const string MixerSFX = "SFXVolume";

    private void Awake()
    {
        //AddListener add one function/method to the Slider, on this case a method that is used when their value is changed.

        MusicSlider.onValueChanged.AddListener(SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);

    }
    private void Start()
    {
        //Load the value of the playerprefs on the sliders
        MusicSlider.value = PlayerPrefs.GetFloat(AudioManager.MusicKey, 0.5f); 
        SFXSlider.value = PlayerPrefs.GetFloat(AudioManager.SFXKey, 0.5f);
    }
    private void OnDisable() //When we are done in the Menu we save the values in PlayerPrefs
    {
        PlayerPrefs.SetFloat(AudioManager.MusicKey, MusicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFXKey, SFXSlider.value);
    }
    void SetMusicVolume(float value)
    {
        Mixer.SetFloat(MixerMusic, Mathf.Log10(value) * 20); //Introduce the value of the slider into de audiomixer
        DataPersistance.MusicVolume = MusicSlider.value; //Then save in Datapersistance the value of the slider
    }

    void SetSFXVolume(float value)
    {
        Mixer.SetFloat(MixerSFX, Mathf.Log10(value) * 20);
        DataPersistance.SoundVolume = SFXSlider.value;
    }

}
