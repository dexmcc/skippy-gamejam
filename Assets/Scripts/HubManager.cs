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
    float maxHunger = 10f;
    float currentHunger = 5f;
    public Image hungerFillImage;

    public Slider exploreBar;
    public Image exploreFillImage;

    float maxExplore = 4f;
    float currentExplore = 0f;

    //0 will say not explored yet, 1 and 2 show which of two options is chosen
    int[] explorations;

    //holding the gameobjects;
    public GameObject[] explorationObjects1 = new GameObject[4];
    public GameObject[] explorationObjects2 = new GameObject[4];

    public VideoPlayer endingCutscene;

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
    }

    public void FixedUpdate()
    {

        //disable hunger bar fill if empty and re-enable it if not
        if (healthBar.value <= healthBar.minValue)
        {
            hungerFillImage.enabled = false;
        }
        if (healthBar.value >= healthBar.minValue && !hungerFillImage.enabled)
        {
            hungerFillImage.enabled = true;
        }
        //update hunger bar
        float hungerFill = currentHunger / maxHunger;
        healthBar.value = hungerFill;

        //disable explore bar fill if empty and re-enable it if not
        if (exploreBar.value <= exploreBar.minValue)
        {
            exploreFillImage.enabled = false;
        }
        if (exploreBar.value >= exploreBar.minValue && !exploreFillImage.enabled)
        {
            exploreFillImage.enabled = true;
        }
        //update explore bar
        float exploreFill = currentExplore / maxExplore;
        exploreBar.value = exploreFill;

        if (endingFlag)
        {
            if ((endingCutscene.frame) > 0 && ((endingCutscene.isPlaying == false) || ((long) endingCutscene.frame == (long) (endingCutscene.frameCount - ((long) 1)))))
            { 
                endingFlag = false;
                SceneManager.LoadScene("MainMenu");
            }
        }

    }

    public void FeedButton()
    {
        //SceneManager.LoadScene("SampleScene");
        Debug.Log("feed");
    }

    public void CleanButton()
    {
        //SceneManager.LoadScene("SampleScene");
        Debug.Log("clean");
    }

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
        currentExplore = currentExplore + 1f;
    }

    public void SetObject(int choice)
    {
        explorations[(int)currentExplore] = choice;
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
