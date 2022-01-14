using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Global : MonoBehaviour
{
    private static Global _instance = null;

    public static Global getInstance()
    {
        return _instance;
    }

    // Game related variables

    public float explorationStat = 0;
    public float foodStat = 0;
    public float sleepStat = 0;
    public string hubScene; 

    


    void Awake()
    {

        // If there's already a global instance, destroy this one
        // Allows us to put a Global object in every room without issues
        if (_instance == null)
        {
            _instance = this; 
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }


    public void gotoHubArea()
    {
        SceneManager.LoadScene(hubScene);
    }


    public void addSleepStat(int amount)
    {
        sleepStat += amount; 
    }
}