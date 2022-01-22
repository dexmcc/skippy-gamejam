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

    TextMeshProUGUI sleepSprite;
    TextMeshProUGUI hungerSprite;


    bool endFlag = false;
    float timer = 0;

    public GameObject exploreDeny;
    public GameObject exploreConfirm;


    //holding the gameobjects;
    public GameObject[] explorationObjects1 = new GameObject[4];
    public GameObject[] explorationObjects2 = new GameObject[4];

    public GameObject mainUI;

    GameObject[] toRender;

    public void Start()
    {
        sleepSprite = sleepTrack.GetComponent<TextMeshProUGUI>();
        hungerSprite = hungerTrack.GetComponent<TextMeshProUGUI>();


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
        if (Global.getInstance().sleepStat < 80)
        {
            sleepSprite.color = new Color(91 / 255, 226 / 255, 106 / 255, 203 / 255);
        }

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
        if ((Global.getInstance().sleepStat < 80) || (Global.getInstance().foodStat < 80))
        {
            exploreConfirm.SetActive(false);
            exploreDeny.SetActive(true);
        } else
        {
            SceneManager.LoadScene("ExploreHub");   
        }



    }

    public void StartEnding()
    {
        ending.StartEndSequence();
    }
    
}
