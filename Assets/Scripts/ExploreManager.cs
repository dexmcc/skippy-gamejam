using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ExploreManager : MonoBehaviour
{

    
    HubManager hubManager;

    GameObject explore;
    GameObject hub;
    GameObject choices;

    public GameObject[] exploreFilms = new GameObject[4];
    public long[] pauseFrames = new long[4];
    bool pauseFlag;
    public GameObject[] choiceSet = new GameObject[4];

    VideoPlayer currentVideo;

    int nextExplore;
    bool exploreActive;

    Global global;

   

    //holding the gameobjects;
    public GameObject[] explorationObjects1 = new GameObject[4];
    public GameObject[] explorationObjects2 = new GameObject[4];

    public void Start()
    {
        exploreActive = false;
        GameObject hubScript = GameObject.Find("HubManager");
        hubManager = hubScript.GetComponent<HubManager>();
        hub = GameObject.Find("MainHub");

        global = Global.getInstance();

        choices = choiceSet[0];

        
    }


    public void RenderExplorationObjects()
    {
        GameObject[] toRender = new GameObject[Global.getInstance().maxExplore];
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


    public void SetChoice(int choice)
    {
        Global.getInstance().explorationChoices[Global.getInstance().currentExplore] = choice;
    }

    public void StartExplore()
    {
        exploreActive = true;
        pauseFlag = false;
        nextExplore = global.getExploreCount();
        choices = choiceSet[nextExplore];
        hub.SetActive(false);
        explore = GameObject.Find("Exploring");
        exploreFilms[nextExplore].SetActive(true);
        currentVideo = exploreFilms[nextExplore].GetComponent<VideoPlayer>();

        for (int i = 0; i < 4; i++)
        {
            choiceSet[i].SetActive(false);
        }
    }

    public void EndExplore()
    {
        pauseFlag = true;
        exploreActive = false;

        nextExplore = global.getExploreCount();
        hub.SetActive(true);

        exploreFilms[nextExplore].SetActive(false);
        explore.SetActive(false);

        global.ExploreUpdate();
        choices.SetActive(false);
        RenderExplorationObjects();

        if (nextExplore == 3)
        {
            hubManager.StartEnding();
        }
    }

    public void Update()
    {
        if (exploreActive)
        {
            if ((pauseFlag == false) && (currentVideo.frame >= pauseFrames[nextExplore]))
            {
                //video is paused for choices
                pauseFlag = true;
                currentVideo.Pause();
                OpenChoices();
            }
        }
    }

    public void PlayVideo()
    {
        currentVideo.Play();
    }

    public void OpenChoices()
    {
        choices.SetActive(true);
    }

    

}
