using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "DataManager", menuName = "2D plat former/DataManager", order = 0)]
public class DataManager : ScriptableObject {

    //Contains All Data

    //mucic Data
    [Header("Music")]
    public AudioClip Level1Music;
    public AudioClip Level2Music;
    public AudioClip MainMenuMusic;

    //Sound of all sound efects  Data
    [Header("SFX")]
    public AudioClip SFXJump;
    public AudioClip SFXDash;
    public AudioClip SFXDeath;
    public AudioClip SFXBulletUsed;

    //LeveL detials
    [Header("Level 1")]
    public float Level1Time;
    public int Level1TotalJumps;
    public int Level1TotalDash;
    public int Level1NoOfDeaths;
    
    [Header("Level 2")]
    public float Level2Time;
    public int Level2TotalJumps;
    public int Level2TotalDash;
    public int Level2NoOfDeaths;

    //audio
    [Header("Sound")]
    [Range(0,1)]
    public float SFXVolume;
    [Range(0,1)]
    public float MusicVolume;

    //check point data
    [Header("CheckPioint")]
    public Transform CheckPointLocation;
    

    //Reset all score and deta requred;
    public void resetScore()
    {
        Level1Time =0;
        Level1TotalJumps=0;
        Level1TotalDash=0;
        Level1NoOfDeaths=0;

        Level2Time = 0;
        Level2TotalJumps=0;
        Level2TotalDash=0;
        Level2NoOfDeaths=0;

        CheckPointLocation= null;
    }

    //Increacase if Player jump 
    public void JumpIncremnt()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Level1TotalJumps+=1;
        }
        else{
            Level2TotalJumps+=1;
        }
    }

    //Increacase if Player Dies 
    public void DeathIncremnt()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Level1NoOfDeaths+=1;
        }
        else{
            Level2NoOfDeaths+=1;
        }
    }
    //Increacase if Player Dashs
    public void DashIncremnt()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Level1TotalDash+=1;
        }
        else{
            Level2TotalDash+=1;
        }
    }
}

