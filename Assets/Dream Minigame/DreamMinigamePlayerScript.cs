using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://www.youtube.com/watch?v=dwcT-Dch0bA
// thanks brackeys, and rest in piece ;)
public class DreamMinigamePlayerScript : MonoBehaviour
{

    public DreamMinigameCharacterController controller;

    Camera main; 
    Rigidbody2D rb;

    public float horizontalSpeed = 30f;
    int moveX = 0;
    bool jump = false; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        main = Camera.main; 
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1; 
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = 1;
        }
        else
        {
            moveX = 0; 
        }

        


        if (Input.GetKey(KeyCode.Space))
        {
            jump = true; 
        }

    }

    private void FixedUpdate()
    {
        controller.Move(moveX * Time.fixedDeltaTime * horizontalSpeed, false, jump);
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
        // Game over logic will go here
        Destroy(gameObject);
    }
}
