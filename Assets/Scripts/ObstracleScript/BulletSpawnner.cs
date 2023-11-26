using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnner : MonoBehaviour
{

    [SerializeField] GameObject _typeofbullet;
    [SerializeField] Transform _spanPoint;
    [SerializeField] float _spawnTime;
    [SerializeField] private bool _isRight;
    void Start()
    {
       StartCoroutine(Spawn());
    }
    private void Update() {
        
    }
    //Spawn bullet after Every X second
    IEnumerator  Spawn()
    {
        //wait for Spawn time 
        yield return new WaitForSeconds(_spawnTime);
        //spawn bullet 
        GameObject go =Instantiate(_typeofbullet, _spanPoint);
        //check if type of bullet 
        if(go.GetComponent<DashableNonProjectileBullets>() != null)
        {
            //set Direciton of Bullet 
            if(_isRight)go.GetComponent<DashableNonProjectileBullets>()._direction =1;
            else go.GetComponent<DashableNonProjectileBullets>()._direction =-1;
        }
        go.transform.SetParent(null);
        StartCoroutine(Spawn());
    }

}
