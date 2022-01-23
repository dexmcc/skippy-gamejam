using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://www.youtube.com/watch?v=dwcT-Dch0bA
// thanks brackeys, and rest in piece ;)
public class DreamMinigamePlayerScript : MonoBehaviour
{

    public DreamMinigameCharacterController controller;
    public DreamMinigameGameManager gameManager;


    private AudioSource jumpSound; 

    Camera main; 
    public Rigidbody2D rb;
    SpriteRenderer spriteRenderer; 

    public float horizontalSpeed = 30f;
    int moveX = 0;
    bool jump = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        main = Camera.main;
        jumpSound = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        jumpSound.volume = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z); 
            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = 1;

            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        }
        else
        {
            moveX = 0; 
        }


        print(spriteRenderer.flipX);


        if (Input.GetKey(KeyCode.Space))
        {
                
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Kill();
        }

    }

    private void FixedUpdate()
    {
        bool jumped = controller.Move(moveX * Time.fixedDeltaTime * horizontalSpeed, false, jump);

        if (jumped)
        {
            jumpSound.Play();
        }

        jump = false;

        CheckDead();
    }

    private void CheckDead()
    {
        Vector3 viewPoint = main.WorldToViewportPoint(transform.position);

        if (viewPoint.y < -.3)
        {
            Kill();
        }

    }

    public void Kill()
    {
        gameManager.GameOver(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            DreamMinigamePlatformLogic script = collision.gameObject.GetComponent<DreamMinigamePlatformLogic>();

            if (script != null)
            {

                script.landSound.Play();

                if (script.jumpedOn == false)
                {
                    gameManager.OnPlayerJumpedOnPlatform();
                    script.jumpedOn = true;
                }
            }
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Kill();
        }
    }
}
