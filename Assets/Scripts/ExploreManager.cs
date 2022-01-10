using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ExploreManager : MonoBehaviour
{

    GameObject hubScript;
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

    public void Start()
    {
        exploreActive = false;
        hubScript = GameObject.Find("HubManager");
        hubManager = hubScript.GetComponent<HubManager>();
        hub = GameObject.Find("MainHub");

        choices = choiceSet[0];
    }

    public void StartExplore()
    {
        exploreActive = true;
        pauseFlag = false;
        nextExplore = hubManager.getExploreCount();
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
        nextExplore = hubManager.getExploreCount();
        hub.SetActive(true);
        exploreFilms[nextExplore].SetActive(false);
        explore.SetActive(false);
        hubManager.ExploreUpdate();
        choices.SetActive(false);
        hubManager.RenderExplorationObjects();
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

    public void SetChoice(int choice)
    {
        hubManager.SetObject(choice);
    }

}
