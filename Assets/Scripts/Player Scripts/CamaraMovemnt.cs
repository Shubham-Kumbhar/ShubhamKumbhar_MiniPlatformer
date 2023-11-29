using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMovemnt : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float smoothSpeed = 5.0f;
    public float yOffset,xOffset;
    
    void Start()
    {
        // Set the Postion to player postion
        transform.position = new Vector3(playerTransform.position.x + xOffset, playerTransform.position.y + yOffset, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //Smothly transions to whereever Player is moving 
        if (playerTransform != null){
            Vector3 targetPosition = new Vector3(playerTransform.position.x + xOffset, playerTransform.position.y + yOffset, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
        else
        {
            // Finds the player Transforms if player not Found 
            try{
                playerTransform = GameObject.FindObjectOfType<PlayerController>().gameObject.transform;
            }
            catch{
                Debug.Log("game object not found");
            }
            
        }
    }
}
