using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : Obstracle
{
    // Kill's player if Collied 
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            ActionManager.Death.Invoke();
        }
    }
}
