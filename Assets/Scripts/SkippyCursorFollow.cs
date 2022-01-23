using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkippyCursorFollow : MonoBehaviour
{

    Vector3 mousePosition;
    Vector3 directionChange;

    Transform SkippyTransform;
    GameObject skippy;
    SpriteRenderer skippySprite;
    public HubManager manager;

    AudioSource skippyNoise;
    
    public EndingSequence endingSequence;
    bool ending;

    public GameObject hearts;
    public GameObject cough;
    public GameObject skippySmall;


    GameObject currentObject;
    float clickCount;

    float timer;
    bool heartFlag;

    public GameObject[] skippyAngles = new GameObject[8];

    bool endingPause;

    public GameObject hand;
    Animator handAnim;

    // Start is called before the first frame update
    void Start()
    {
        clickCount = 0;
        heartFlag = false;
        ending = false;
        skippy = GameObject.Find("Skippy");
        SkippyTransform = skippy.GetComponent<Transform>();
        skippyNoise = skippy.GetComponent<AudioSource>();
        skippySprite = skippy.GetComponent<SpriteRenderer>();
        endingPause = manager.IsEnding();
        handAnim = hand.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!endingPause)
        {
            if (!(endingSequence.IsEnding()))
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;

                directionChange = (mousePosition - SkippyTransform.position).normalized * 0.7f;

                float angle = Mathf.Atan2((mousePosition - SkippyTransform.position).x, (mousePosition - SkippyTransform.position).y) * Mathf.Rad2Deg;

                //Debug.Log(angle);

                AimTowards(angle);

                SkippyTransform.Translate(directionChange * Time.deltaTime);



                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                    RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                    if (hit.collider != null)
                    {
                        DoSomethingCute();
                    }
                }
            }

            if (heartFlag)
            {
                timer = timer + 1;

                if (timer < 400f)
                {
                    hand.SetActive(true);
                    currentObject.SetActive(true);
                }
                else
                {
                    hand.SetActive(false);
                    currentObject.SetActive(false);
                    heartFlag = false;
                }

            }
            else
            {
                timer = 0;
            }
        } else
        {
            endingPause = manager.IsEnding();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        endingSequence.ColTrigger(col);
    }

    void DoSomethingCute()
    {
        if (!(heartFlag))
        {
            skippyNoise.Play();
            heartFlag = true;
            if (clickCount == 2)
            {
                clickCount = 0;
            }
            else
            {
                clickCount++;
            }
            if (clickCount == 0)
            {
                currentObject = hearts;
            }
            else if (clickCount == 1)
            {
                currentObject = cough;
            }
            else
            {
                currentObject = skippySmall;
            }
        }
    }

    void AimTowards(float x)
    {

        float toShow = 0;

        if (Vector3.Distance(new Vector3(mousePosition.x, mousePosition.y, 0), new Vector3(SkippyTransform.position.x, SkippyTransform.position.y, 0)) <= 1f)
        {

            skippySprite.enabled = true;

            for (int i = 0; i < 8; i++)
            {
                skippyAngles[i].SetActive(false);
            }
        } else
        {
            skippySprite.enabled = false;

            if ((x <= -22.5f) && (x > -67.5f))
            {
                toShow = 0;
            }
            else if (((x <= 22.5f) && (x >= 0f)) || ((x <= 0) && (x >= -22.5f)))
            {
                toShow = 1;
            }
            else if ((x <= 67.5f) && (x > 22.5f))
            {
                toShow = 2;
            }
            else if ((x <= -67.5f) && (x > -112.5f))
            {
                toShow = 3;
            }
            else if ((x <= 112.5f) && (x > 67.5f))
            {
                toShow = 4;
            }
            else if ((x <= -112.5f) && (x > -157.5f))
            {
                toShow = 5;
            }
            else if (((x <= -157.5f) && (x >= -180f)) || ((x <= 180f) && (x >= 157.5f)))
            {
                toShow = 6;
            }
            else if ((x <= 157.5f) && (x > 112.5f))
            {
                toShow = 7;
            }

            for (int i = 0; i < 8; i++)
            {
                if (i == toShow)
                {
                    skippyAngles[i].SetActive(true);
                }
                else
                {
                    skippyAngles[i].SetActive(false);
                }
            }

        }

    }
}
