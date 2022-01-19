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


    bool pauseFlag;

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
