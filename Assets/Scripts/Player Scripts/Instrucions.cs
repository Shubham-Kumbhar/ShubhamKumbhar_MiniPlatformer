using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  TMPro;

public class Instrucions : MonoBehaviour
{

     [Header("Instrucion")]
    [SerializeField] private bool _showInstruciton= true;
    [SerializeField] private GameObject _InstrcionCanvas;
     [SerializeField] private TextMeshProUGUI _instrucitonTMP;
    [SerializeField] private string _instruciton ;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        //check if Collided Object is Player 

        if(_showInstruciton&&other.gameObject.CompareTag("Player"))
        {
            //change Player's State to Instuctions
            PlayerStateMachine.instance.ChangeState(PlayerStateMachine.PlayerState.Instrucion);
            //show the Instrucions 
            _InstrcionCanvas.gameObject.SetActive(true);
            //Set the Instrucion
            _instrucitonTMP.text = _instruciton;
            //make sure instrucion only Show Once 
            _showInstruciton=false;
        }
        
    }
}
