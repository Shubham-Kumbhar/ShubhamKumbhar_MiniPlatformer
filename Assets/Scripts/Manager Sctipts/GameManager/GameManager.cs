using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    //instance created to access this class from anywhere 
    public static GameManager instance;
    public DataManager dataManager;
    
    public GameObject Player;

    private void Awake() {
        // Ensure only one instance of GameManager exists
         if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        
    }

    //Change the Scene
    public void SceneChanger(int a)
    {
        SceneManager.LoadScene(a);
    }
    
    
}
