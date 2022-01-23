using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatMinigamePlayerScript : MonoBehaviour
{

    public float speed = 1;
    public LayerMask enemyLayer;

    private Vector2 move = new Vector2(0, 0);

    private Camera main;
    private Rigidbody2D rb;
    private AudioSource audioSource; 

    [HideInInspector] public EatMinigameGameManager gameManager;
    [HideInInspector] public bool paused = false;

    bool killFlag;
    bool flashedFlag;
    SpriteRenderer skippySprite;

    Vector2 lastPos;

    public AudioSource gameOverClip;

    // Start is called before the first frame update
    void Start()
    {
        killFlag = false;
        flashedFlag = false;
        main = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        skippySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (killFlag)
        {
            if (flashedFlag)
            {
                gameManager.GameOver();
            } else
            {
                if (skippySprite.color.g <= 0.3f)
                {
                    flashedFlag = true;
                } else
                {
                    skippySprite.color = new Color (skippySprite.color.r - 0.01f, skippySprite.color.g - 0.01f, skippySprite.color.b);
                }
            }
        }
        else
        {
            if (!paused)
            {
                // Input! Hard coded right now :P

                if (Input.GetKey(KeyCode.A))
                {
                    move.x = -1;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    move.x = 1;
                }
                else
                {
                    move.x = 0;
                }


                if (Input.GetKey(KeyCode.W))
                {
                    move.y = 1;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    move.y = -1;
                }
                else
                {
                    move.y = 0;
                }
            } else
            {
                move.x = 0;
                move.y = 0;
            }
        }
       
    }

    // Handles Movement
    private void FixedUpdate()
    {

        if (!paused)
        {

            if (move != Vector2.zero)
            {
                move = move.normalized;

                float angle = Vector2.SignedAngle(Vector2.right, move);

                transform.rotation = Quaternion.Euler(0, 0, angle - 90);


                //transform.forward = move; 

                Vector2 newPos = rb.position + (move * speed * Time.fixedDeltaTime);

                // Check if the new position is within the bounds of the camera
                Vector3 viewPoint = main.WorldToViewportPoint(newPos);

                // Clamp the position in the camera
                viewPoint.x = Mathf.Clamp(viewPoint.x, 0, 1);
                viewPoint.y = Mathf.Clamp(viewPoint.y, 0, 1);

                newPos = main.ViewportToWorldPoint(viewPoint);

                rb.MovePosition(newPos);

                lastPos = rb.position;
            }

            
            
        } else
        {
            rb.MovePosition(lastPos);
        }
    }


    public void Kill()
    {

        if (!paused)
        {
            killFlag = true;
            move.x = 0;
            move.y = 0;
            rb.MovePosition(lastPos);
            // Here logic will go to take to main menu n such
            // TODO, something like an animation instead of just going straight to the hub
            //Global.getInstance().gotoHubArea();
            //Destroy(gameObject);
        }
    }

    // Only gameobject with trigger is food, this checks if the player is interacing with food
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject ob = collision.gameObject;
        EatMinigameFoodScript script = null; 

        ob.TryGetComponent<EatMinigameFoodScript>(out script);



        if (script != null)
        {
            //audioSource.Play();
            script.GivePointAndDestroy();
        }


        print(ob.layer.ToString() + " " + enemyLayer.ToString());
        // Hardcoded to detect enemy layer b/c wasn't working before
        if (ob.layer == 7)
        {
            gameOverClip.Play();
            Kill();
        }
    }
}
