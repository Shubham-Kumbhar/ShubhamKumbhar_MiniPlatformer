using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    [Header("Effects")]
    [SerializeField] private ParticleSystem _burst;
    [SerializeField] private GameObject _DeathPartilce;


    void Start()
    {
        _anim = GetComponent<Animator>();
        _burst.Stop();
    }
     //subscribe  Events 
    private void OnEnable()
    {
        ActionManager.Jump += Jump;
        ActionManager.DashStart += Dash;
        ActionManager.Death+=Death;

    }
    //Unsubscribe Events 
    private void OnDisable()
    {
        ActionManager.Jump -= Jump;
        ActionManager.DashStart -= Dash;
        ActionManager.Death-=Death;
    }
    //Play animation Sound Increce Jump Score
    void Jump()
    {
        _anim.SetTrigger("Jump");
        _burst.Play();
        GameManager.instance.dataManager.JumpIncremnt();
        SoundManager.instance.PlaySFX(GameManager.instance.dataManager.SFXJump);

    }
    //Play animation Sound Increce Dash Score
    void Dash()
    {
        _anim.SetTrigger("Dash");
        SoundManager.instance.PlaySFX(GameManager.instance.dataManager.SFXDash);
        GameManager.instance.dataManager.DashIncremnt();
        _burst.Play();

    }
    //Play animation Sound Increce number of time Player Died
    void Death()
    {
        GameObject go = Instantiate(_DeathPartilce,transform);
        go.transform.SetParent(null);
        go.GetComponent<ParticleSystem>().Play();
        GameManager.instance.dataManager.DeathIncremnt();
       SoundManager.instance.PlaySFX(GameManager.instance.dataManager.SFXDeath);
    }
}
