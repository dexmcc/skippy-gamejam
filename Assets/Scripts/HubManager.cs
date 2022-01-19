using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HubManager : MonoBehaviour
{

    

    public Slider sleepBar;
    public Slider hungerBar;
    public Slider explorationBar;
    public EndingSequence ending;

    bool endingFlag;

    //holding the gameobjects;
    public GameObject[] explorationObjects1 = new GameObject[4];
    public GameObject[] explorationObjects2 = new GameObject[4];

    GameObject[] toRender;


    public void Start()
    {

        RenderExplorationObjects();

        UpdateSliders();
    }



    


    public void UpdateSliders()
    {
        sleepBar.value = (Global.getInstance().sleepStat / 100.0f);
        hungerBar.value = (Global.getInstance().foodStat / 100.0f);
        explorationBar.value = (Global.getInstance().explorationStat / 100.0f);
    }

    public void RenderExplorationObjects()
    {
        toRender = new GameObject[Global.getInstance().maxExplore];
        for (int i = 0; i < 4; i++)
        {
            int objectTracker = Global.getInstance().explorationChoices[i];

            if (objectTracker == 1)
            {
                toRender[i] = explorationObjects1[i];
            }
            else if (objectTracker == 2)
            {
                toRender[i] = explorationObjects2[i];
            }

        }

        for (int i = 0; i < 4; i++)
        {
            if (toRender[i] != null)
            {
                toRender[i].SetActive(true);
            }
        }

    }

    public GameObject[] GetObjectsForEnd()
    {
        return toRender;
    }

    public void FeedButton()
    {
        SceneManager.LoadScene("EatingMinigame");
    }

    public void SleepButton()
    {
        SceneManager.LoadScene("DreamMinigame");
    }


    public void ExploreButton()
    {

        // Change this to goto explore area
        SceneManager.LoadScene("ExploreHub");
        //explore.SetActive(true);
        //exploreManager.StartExplore();

    }

    public void StartEnding()
    {
        ending.StartEndSequence();
    }
    
}
