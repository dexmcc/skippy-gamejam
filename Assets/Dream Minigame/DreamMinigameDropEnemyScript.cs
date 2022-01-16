using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamMinigameDropEnemyScript : MonoBehaviour
{
    public float dropSpeed = 1.0f;
    public DreamMinigameGameManager gameManager;

    Camera main; 

    private void Start()
    {
        main = Camera.main;
    }


    private void FixedUpdate()
    {
        if (!gameManager.paused)
        {
            transform.Translate(new Vector3(0, -dropSpeed * Time.fixedDeltaTime, 0));

            if (main.WorldToViewportPoint(transform.position).y < -.3f)
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

}
