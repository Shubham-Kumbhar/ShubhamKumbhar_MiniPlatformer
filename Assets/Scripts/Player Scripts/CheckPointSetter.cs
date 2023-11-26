using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSetter : MonoBehaviour
{

    [Header("checkpoint")]
    [SerializeField] private bool  _setChekpoint =true; 


    [Header("Cam pos")]
    [SerializeField] private bool _overrideCamSetting;
    CamaraMovemnt cam;
    [SerializeField] private float Y,X;

    //Set he Spawn point and Change Offset of Camera
    private void OnTriggerEnter2D(Collider2D other) {
        //check if Player is collieded or not 
        if(other.gameObject.CompareTag("Player"))
        {
            //set the CheckPoint 
            if(_setChekpoint) GameManager.instance.dataManager.CheckPointLocation = transform;

            //set the Camara offset
            if(_overrideCamSetting)
            {
                cam= FindAnyObjectByType<CamaraMovemnt>();
                cam.xOffset =X;
                cam.yOffset =Y;
            }

        }
    }
}
