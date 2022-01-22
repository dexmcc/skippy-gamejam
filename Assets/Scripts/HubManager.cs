using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HubManager : MonoBehaviour
{

    public EndingSequence ending;

    public TextMeshProUGUI sleepTrack;
    public TextMeshProUGUI hungerTrack;
    public TextMeshProUGUI explorationTrack;

    bool endFlag = false;
    float timer = 0;


    //holding the gameobjects;
    public GameObject[] explorationObjects1 = new GameObject[4];
    public GameObject[] explorationObjects2 = new GameObject[4];

    public GameObject mainUI;

    GameObject[] toRender;

    public void Start()
    {

        RenderExplorationObjects();

        UpdateSliders();

        if (Global.getInstance().currentExplore == 4)
        {
            endFlag = true;
            mainUI.SetActive(false);
        }


    }



    public void FixedUpdate()
    {
        if (endFlag)
        {
            timer = timer + 1f;

            if (timer >= 50f)
            {
                StartEnding();
            }
            
        }
    }


    public void UpdateSliders()
    {
        sleepTrack.text = Global.getInstance().sleepStat.ToString("F0") + "%";
        hungerTrack.text = Global.getInstance().foodStat.ToString("F0") + "%";
        explorationTrack.text = Global.getInstance().explorationStat.ToString("F0") + "%";
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
        SceneManager.LoadScene("ExploreHub");

    }

    public void StartEnding()
    {
        ending.StartEndSequence();
    }
    
}
