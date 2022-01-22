using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ExploreManager : MonoBehaviour
{

    public GameObject[] exploreFilms = new GameObject[4];
    public long[] pauseFrames = new long[4];
    public GameObject[] choiceSet = new GameObject[4];
    public int[] sleepRemove = new int[3];
    public int[] hungerRemove = new int[3];


    bool pauseFlag;
    bool endFlag;

    VideoPlayer currentVideo;

    int nextExplore;

    Global global;

    GameObject choices;





    public void Start()
    {

        global = Global.getInstance();

        choices = choiceSet[0];

        StartExplore();

    }


    


    public void SetChoice(int choice)
    {
        Global.getInstance().explorationChoices[Global.getInstance().currentExplore] = choice;
    }

    public void StartExplore()
    {
        pauseFlag = false;
        endFlag = false;

        nextExplore = global.getExploreCount();
        choices = choiceSet[nextExplore];

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

        if (nextExplore < 2)
        {
            Global.getInstance().sleepStat -= sleepRemove[nextExplore];
            Global.getInstance().foodStat -= hungerRemove[nextExplore];
        }


        
        nextExplore = global.getExploreCount();

        exploreFilms[nextExplore].SetActive(false);

        global.ExploreUpdate();

        SceneManager.LoadScene("MainHub");
    }

    public void Update()
    {
        if ((pauseFlag == false) && (currentVideo.frame >= pauseFrames[nextExplore]))
        {
            //video is paused for choices
            pauseFlag = true;
            currentVideo.Pause();
            OpenChoices();
        }
        
        if (endFlag && ((currentVideo.frame) > 0 && ((currentVideo.isPlaying == false) || ((long)currentVideo.frame == (long)(currentVideo.frameCount - ((long)1)))))) {
            EndExplore();
        }

    }

    public void PlayVideo()
    {
        currentVideo.Play();
        endFlag = true;
    }

    public void OpenChoices()
    {
        choices.SetActive(true);
    }

    

}
