using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatMinigamePlayerScript : MonoBehaviour
{

    public float speed = 1;

    private Vector2 move = new Vector2(0, 0);

    private Camera main;
    private Rigidbody2D rb; 

    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //Vector2 move = new Vector2(0, 0);
        
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
    }

    private void FixedUpdate()
    {

        Vector2 newPos = rb.position + (move * speed * Time.fixedDeltaTime); 
        Vector3 viewPoint = main.WorldToViewportPoint(newPos);

        viewPoint.x = Mathf.Clamp(viewPoint.x, 0, 1);
        viewPoint.y = Mathf.Clamp(viewPoint.y, 0, 1);

        newPos = main.ViewportToWorldPoint(viewPoint);

        rb.MovePosition(newPos);
    }


    public void Kill()
    {
        // Here logic will go to take to main menu n such

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject ob = collision.gameObject;
        EatMinigameFoodScript script = null; 

        ob.TryGetComponent<EatMinigameFoodScript>(out script);

        if (script != null)
        {
            script.GivePointAndDestroy();
        }
    }
}
