using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashableProjectileBullets : Obstracle
{
    
    private Rigidbody2D _rb;
    [SerializeField] float Speed;
    void Start()
    {
        // Add force to bullet 
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(transform.up*Speed,ForceMode2D.Impulse);
    }

    public override void OnUsed()
    {
        //called the Base Class
        base.OnUsed();   
    }
    private void OnTriggerEnter2D(Collider2D other) {
        //check if collided object is Player Or not if plyaer the n it will kill player else destroy itself
        if(other.gameObject.CompareTag("Player"))
        {
            ActionManager.Death.Invoke();
        }
        else
        {
            //partilce

            Destroy(gameObject);

        }
    }

}
