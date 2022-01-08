using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HubManager : MonoBehaviour
{

    public Slider healthBar;
    float maxHunger = 10f;
    public float currentHunger = 5f;
    public Image hungerFillImage;

    public Slider exploreBar;
    float maxExplore = 10f;
    public float currentExplore = 5f;
    public Image exploreFillImage;


    public void awake()
    {

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
        //fill when doing exploration stuff
        Debug.Log("explore");
    }


}
