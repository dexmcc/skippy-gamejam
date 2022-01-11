using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://www.youtube.com/watch?v=dwcT-Dch0bA
// thanks brackeys, and rest in piece ;)
public class DreamMinigamePlayerScript : MonoBehaviour
{

    public DreamMinigameCharacterController controller; 

    Rigidbody2D rb;
    int moveX = 0;
    float jumpForce = 10f;
    public float horizontalSpeed = 1f;

    bool jump = false; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        //rb.MovePosition(rb.position + new Vector2(moveX * horizontalSpeed * Time.fixedDeltaTime, 0));
    }
}
