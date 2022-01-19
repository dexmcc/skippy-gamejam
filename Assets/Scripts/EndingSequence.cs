using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class EndingSequence : MonoBehaviour
{
    bool colorCheck;
    bool endFlag;

    GameObject[] endingObjects;
    Transform[] endingTransforms;
    SpriteRenderer[] endingSprites;

    public GameObject skippy;

    Transform skippyTransform;
    SpriteRenderer skippySprite;

    int endCheck = 0;

    public HubManager hubManager;

    public GameObject fader;
    Image fadeWhite;

    Vector3 directionChange;

    public GameObject skippyText;

    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        endFlag = false;

        colorCheck = false;

        skippyTransform = skippy.GetComponent<Transform>();
        skippySprite = skippy.GetComponent<SpriteRenderer>();

        fadeWhite = fader.GetComponent<Image>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (endFlag)
        {
            //objects glowing
            ObjectGlow();

            //objects absorbing into skippy
            for (int i = 0; i < 4; i++)
            {
                if (endingObjects[i] != null)
                {
                    directionChange = (skippyTransform.position - endingTransforms[i].position).normalized;
                    endingTransforms[i].Translate(directionChange * Time.deltaTime);
                    endingTransforms[i].localScale = endingTransforms[i].localScale - (endingTransforms[i].localScale * 0.01f);

                }
            }

            if (endCheck >= 4)
            {
                fadeWhite.color = new Color(fadeWhite.color.r, fadeWhite.color.g, fadeWhite.color.b, fadeWhite.color.a + 0.01f);
                if (fadeWhite.color.a >= 0.99f)
                {
                    SceneManager.LoadScene("Ending");
                }
            }

            //text popping up and stuff
            timer = timer + 1f;
            if (timer == 135f)
            {
                skippyText.SetActive(true);
            } else if (timer == 270f)
            {
                skippyText.SetActive(false);
            }

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.SetActive(false);

        endCheck = endCheck + 1;
    }

    public void GetActiveObjects()
    {
        endingObjects = hubManager.GetObjectsForEnd();

        endingSprites = new SpriteRenderer[4];
        endingTransforms = new Transform[4];

        for (int i = 0; i < 4; i++)
        {
            if (endingObjects[i] != null)
            {
                endingSprites[i] = endingObjects[i].GetComponentInChildren<SpriteRenderer>();
                endingTransforms[i] = endingObjects[i].GetComponent<Transform>();
            }
        }
    }

    public void StartEndSequence()
    {
        fader.SetActive(true);
        GetActiveObjects();
        endFlag = true;
    }

    public void ObjectGlow()
    {
        for (int i = 0; i < 4; i++)
        {
            if (endingObjects[i] != null)
            {
                float colorChange = 1;
                if (colorCheck)
                {
                    colorChange = 1;
                }
                else
                {
                    colorChange = -1;
                }

                float colorTest = (endingSprites[i].color.a * 255) + (colorChange * 15);

                if ((colorTest >= 0f) && (colorTest <= 255f))
                {
                    endingSprites[i].color = new Color(endingSprites[i].color.r, endingSprites[i].color.g, endingSprites[i].color.b, (colorTest / 255f));
                    skippySprite.color = new Color(endingSprites[i].color.r, endingSprites[i].color.g, endingSprites[i].color.b, (colorTest / 255f));
                }
                else
                {
                    if (colorCheck)
                    {
                        colorCheck = false;
                    }
                    else
                    {
                        colorCheck = true;
                    }
                }
            }
        }
    }

    // Go to ending room 
    //SceneManager.LoadScene("Ending");
}
