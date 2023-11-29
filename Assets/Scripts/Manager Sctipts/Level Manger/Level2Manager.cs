using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Level2Manager : MonoBehaviour
{
    public GameObject startCanvas, ResultCanvas;
    public AnimationClip start, exit;
    public TextMeshProUGUI Timer;
    public PlayerController Player;
    [SerializeField] float respawnTime=2f;
    float LevelCompletionTime;

    private bool _timeStrted = true;

    
    private void Awake() {
        Player =GameObject.FindObjectOfType<PlayerController>();
    }
    private void Start()
    {
        //Play the Fade in Animation
        startCanvas.GetComponent<Animation>().clip = start;
        startCanvas.GetComponent<Animation>().Play();
        SoundManager.instance.PlayMusic(GameManager.instance.dataManager.Level2Music);
    }
    private void Update()
    {
        //update the Timer 
        if (_timeStrted)
        LevelCompletionTime += Time.deltaTime;
        Timer.text = " Timer :" + LevelCompletionTime.ToString("0.00");
    }

    //subscribe  Events 
    private void OnEnable() {
        ActionManager.Death+= Death;
    }
    //Unsubscribe Events 
    private void OnDisable() {
        ActionManager.Death-= Death;
    }
    //Chnage Scene After Wait of X sec 
    public void SceneChanger(int i)
    {
        GameManager.instance.SceneChanger(i);
    }
    //Play SFX from Sound Manager 
    public void PlaySFX(AudioClip a)
    {
        SoundManager.instance.PlaySFX(a);
    }

   //Call Respawn Coroutine
    void Death()
    {
        StartCoroutine(CheckPointRespawn());
    }
    IEnumerator CheckPointRespawn()
    {
        // Wiats for x amout od time 
        yield return new WaitForSeconds(respawnTime);
        //Changes the position of the player to perviosly stored Check Point 
        Player.transform.position = GameManager.instance.dataManager.CheckPointLocation.position;
        //player to Active again
        Player.gameObject.SetActive(true);
    }
    
    public void LevelEnd()
    {
        //fade Screen in
        startCanvas.GetComponent<Animation>().clip = exit;
        startCanvas.GetComponent<Animation>().Play();
        //show result
        ResultCanvas.SetActive(true);
        //stop music
        SoundManager.instance.StopMusic();
    }
    
    //check for level completions 
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            _timeStrted = false;
            GameManager.instance.dataManager.Level2Time = LevelCompletionTime;
            LevelEnd();
        }
    }

}
