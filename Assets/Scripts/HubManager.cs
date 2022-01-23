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

        float energyAlpha = Mathf.Clamp((Global.getInstance().sleepStat / 100), 0.5f, 1f);
        if (Global.getInstance().sleepStat >= 80)
        {
            sleepSprite.color = new Color(0.356f , 0.886f, 0.415f, energyAlpha);
        } else
        {
            sleepSprite.color = new Color(0.529f, 0.141f, 0.141f, energyAlpha);
        }

        float foodAlpha = Mathf.Clamp((Global.getInstance().foodStat / 100), 0.5f, 1f);
        if (Global.getInstance().foodStat >= 80)
        {
            hungerSprite.color = new Color(0.356f, 0.886f, 0.415f, foodAlpha);
        }
        else
        {
            hungerSprite.color = new Color(0.529f, 0.141f, 0.141f, foodAlpha);
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
