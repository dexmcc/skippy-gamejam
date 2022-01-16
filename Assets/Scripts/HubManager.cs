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


    public void ExploreButton()
    {

        explore.SetActive(true);
        exploreManager.StartExplore();
        
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
