using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkippyCursorFollow : MonoBehaviour
{

    Vector3 mousePosition;
    Vector3 directionChange;

    Transform SkippyTransform;
    GameObject skippy;

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

    // Start is called before the first frame update
    void Start()
    {
        clickCount = 0;
        heartFlag = false;
        ending = false;
        skippy = GameObject.Find("Skippy");
        SkippyTransform = skippy.GetComponent<Transform>();
        skippyNoise = skippy.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(endingSequence.IsEnding()))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;

            directionChange = (mousePosition - SkippyTransform.position).normalized * 0.7f;
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

            if (timer < 300f)
            {
                
                currentObject.SetActive(true);
            } else
            {
                currentObject.SetActive(false);
                heartFlag = false;
            }

        } else
        {
            timer = 0;
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
}
