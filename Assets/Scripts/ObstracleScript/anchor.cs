using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anchor : Obstracle
{
    [SerializeField] float _timeToRespawn;
    public override void OnUsed()
    {
        //call base class OnUsed function
        base.OnUsed();
        //Start coroutine to respan the Anchor
        StartCoroutine(respawn(_timeToRespawn));
        //make Anchor invisable and non Interactive  
        GetComponent<SpriteRenderer>().enabled =false;
        GetComponent<Collider2D>().enabled =false;
       
    }
    // reSpawn the anchor After awating X seconds 
    IEnumerator respawn(float a)
    {
        yield return new WaitForSeconds(a);
        GetComponent<SpriteRenderer>().enabled =true;
        GetComponent<Collider2D>().enabled = true;

    }
}
