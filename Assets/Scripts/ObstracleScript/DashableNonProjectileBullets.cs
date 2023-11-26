using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashableNonProjectileBullets : Obstracle
{
    
    private Rigidbody2D _rb;
    [SerializeField] float Speed;
    [Range(-1,1)]public int _direction; 
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //move bullet 
        _rb.velocity= _direction*transform.up*Speed;
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
            Destroy(gameObject);

        }
    }
}
