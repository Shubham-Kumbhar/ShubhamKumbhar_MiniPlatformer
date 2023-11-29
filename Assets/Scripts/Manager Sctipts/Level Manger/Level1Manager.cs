using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;


public class Level1Manager : MonoBehaviour
{
    public GameObject startCanvas;
    public AnimationClip start, exit;
    float LevelCompletionTime;
    private bool _timeStrted = true;
    public TextMeshProUGUI Timer;
    public PlayerController Player;
    [SerializeField] float respawnTime=2f;

    private void Awake() {
        Player =GameObject.FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        //update the Timer 
        if (_timeStrted)
        LevelCompletionTime += Time.deltaTime;
        Timer.text = " Timer :" + LevelCompletionTime.ToString("0.00");
    }
    private void Start()
    {
        //Play the Fade in Animation
        startCanvas.GetComponent<Animation>().clip = start;
        startCanvas.GetComponent<Animation>().Play();
        SoundManager.instance.PlayMusic(GameManager.instance.dataManager.Level1Music);
    }

    //subscribe  Events 
    private void OnEnable() {
        ActionManager.Death+= Death;
    }
    //Unsubscribe Events 
    private void OnDisable() {
        ActionManager.Death-= Death;
    }

    //Change Scene After Wait of X sec 
    IEnumerator SceneChanger(int i, float time)
    {
        yield return new WaitForSeconds(time);
        GameManager.instance.SceneChanger(i);
    }

    //fade Screen in and change Scene
    public void LevelEnd()
    {
        startCanvas.GetComponent<Animation>().clip = exit;
        startCanvas.GetComponent<Animation>().Play();
        StartCoroutine(SceneChanger(2, 1.5f));
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
        //set player to Active again
        Player.gameObject.SetActive(true);
    }

    //check for level completions 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _timeStrted = false;
            GameManager.instance.dataManager.Level1Time = LevelCompletionTime;
            LevelEnd();

        }
    }

    //Play SFX from Sound Manager 
    public void PlaySFX(AudioClip a)
    {
        SoundManager.instance.PlaySFX(a);
    }

    //Change State from InstrucionState to Idle State
    public void ChangeStateFromInstruion()
    {
        PlayerStateMachine.instance.ChangeState(PlayerStateMachine.PlayerState.Idle);
    }

}
