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


    public int maxExplore = 4;
    public int currentExplore = 0;


    // 0 will say not explored yet, 1 and 2 show which of two options is chosen
    public int[] explorationChoices;



    void Awake()
    {

        // If there's already a global instance, destroy this one
        // Allows us to put a Global object in every room without issues
        if (_instance == null)
        {
            _instance = this;

            initExplorationChoices(); 

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void initExplorationChoices()
    {
        //initialising array of objects
        explorationChoices = new int[maxExplore];
        for (int i = 0; i < maxExplore; i++)
        {
            explorationChoices[i] = 0;
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

    public int getExploreCount()
    {
        return currentExplore;
    }

    public void ExploreUpdate()
    {
        currentExplore++;
    }
}