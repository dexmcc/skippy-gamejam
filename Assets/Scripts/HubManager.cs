using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class HubManager : MonoBehaviour
{

    

    GameObject exploreScript;
    ExploreManager exploreManager;
    GameObject explore;
    GameObject ending;
    GameObject hub;

    GameObject buttons;

    public Slider healthBar;
    //float maxHunger = 10f;
    //float currentHunger = 0f;
    public Image hungerFillImage;

    public Slider exploreBar;
    public Image exploreFillImage;

    int maxExplore = 4;
    int currentExplore = 0;

    //0 will say not explored yet, 1 and 2 show which of two options is chosen
    int[] explorations;

    //holding the gameobjects;
    public GameObject[] explorationObjects1 = new GameObject[4];
    public GameObject[] explorationObjects2 = new GameObject[4];

    public VideoPlayer endingCutscene;


    public Slider sleepBar;
    public Slider hungerBar;
    public Slider explorationBar;

    bool endingFlag;

    public void Start()
    {
        endingFlag = false;
        hub = GameObject.Find("MainHub");
        exploreScript = GameObject.Find("ExploreManager");
        explore = GameObject.Find("Exploring");
        ending = GameObject.Find("Ending");
        buttons = GameObject.Find("Buttons");

        explore.SetActive(false);
        ending.SetActive(false);

        exploreManager = exploreScript.GetComponent<ExploreManager>();

        //initialising array of objects
        explorations = new int[(int) maxExplore];
        for (int i = 0; i < maxExplore; i++) 
        {
            explorations[i] = 0;
        }

        UpdateSliders();
    }

    public void FixedUpdate()
    {

        if (endingFlag)
        {
            if ((endingCutscene.frame) > 0 && ((endingCutscene.isPlaying == false) || ((long) endingCutscene.frame == (long) (endingCutscene.frameCount - ((long) 1)))))
            { 
                endingFlag = false;
                SceneManager.LoadScene("MainMenu");
            }
        }

    }


    public void UpdateSliders()
    {
        sleepBar.value = (Global.getInstance().sleepStat / 100.0f);
        hungerBar.value = (Global.getInstance().foodStat / 100.0f);
        explorationBar.value = (Global.getInstance().explorationStat / 100.0f);
    }


    public void FeedButton()
    {
        SceneManager.LoadScene("EatingMinigame");
    }

    public void SleepButton()
    {
        SceneManager.LoadScene("DreamMinigame");
    }

    /*public void CleanButton()
    {
        SceneManager.LoadScene("CleaningMinigame");
        //SceneManager.LoadScene("SampleScene");
        //Debug.Log("clean");
    }*/

    public void ExploreButton()
    {

        explore.SetActive(true);
        exploreManager.StartExplore();
        
    }

    public void RenderExplorationObjects()
    {
        GameObject[] toRender = new GameObject[(int) maxExplore]; 
        for (int i = 0; i < 4; i++)
        {
            int objectTracker = explorations[i];

            if (objectTracker == 1)
            {
                toRender[i] = explorationObjects1[i];
            } else if (objectTracker == 2)
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

    public int getExploreCount()
    {
        return (int) currentExplore;
    }

    public void ExploreUpdate()
    {
        currentExplore = currentExplore + 1;
    }

    public void SetObject(int choice)
    {
        explorations[currentExplore] = choice;
    }

    public void StartEnding()
    {
        endingFlag = true;

        buttons.SetActive(false);
        explore.SetActive(false);
        hub.SetActive(false);
        ending.SetActive(true);

        endingCutscene.Play();
    }
    
}
