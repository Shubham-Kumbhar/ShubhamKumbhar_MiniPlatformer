using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMeueManager : MonoBehaviour
{
    [SerializeField] Slider  SFX , music;

    private void Start() {
        //play music
        SoundManager.instance.PlayMusic(GameManager.instance.dataManager.MainMenuMusic);
        //Volune Chnaged for Audio Source
        SFXSliderValueChanged();
        musicSliderValueChanged();
        
    }
    //Change Scene After Wait of X sec 
    public void SceneChanger(int i)
    {
        GameManager.instance.SceneChanger(i);
    }
    // call Reset function from Game Manager
    public void ResetScore()
    {
        GameManager.instance.dataManager.resetScore();
    }
    //Call SFX from Sound Manager 
    public void PlaySFX(AudioClip a)
    {
        SoundManager.instance.PlaySFX(a);
    }
    //set Volume of audio source to Slider Value 
    public void SFXSliderValueChanged()
    {
        GameManager.instance.dataManager.SFXVolume=SFX.value; 
        SoundManager.instance.SetSFXVolume(SFX.value);
    }
    //set Volume of audio source to Slider Value 
    public void musicSliderValueChanged()
    {
        GameManager.instance.dataManager.MusicVolume=music.value; 
        SoundManager.instance.SetMusicVolume(music.value);
    }
    //Set SFX Volume from Sound Manager 
    public void SetSFXVolume(float a)
    {
        SoundManager.instance.SetSFXVolume(a);
    }
    //Set music Volume from Sound Manager 
    public void SetMusicVolume(float a)
    {
        SoundManager.instance.SetMusicVolume(a);
    }
    // Quit Application
    public void Quit() {
        Application.Quit();   
    }


}
