using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamMinigameDropEnemyScript : MonoBehaviour
{
    public float dropSpeed = 1.0f;
    public DreamMinigameGameManager gameManager;

    Camera main;

    bool soundPlayed = false;

    AudioSource audioSource; 

    private void Start()
    {
        main = Camera.main;

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.3f;
    }


    

    private void FixedUpdate()
    {
        if (!gameManager.paused)
        {
            transform.Translate(new Vector3(0, -dropSpeed * Time.fixedDeltaTime, 0));

            float viewY = main.WorldToViewportPoint(transform.position).y;

            if (viewY < -.3f)
            {
                Kill();
            }

            if (viewY < 1.02f && !soundPlayed)
            {
                PlaySound();
            }
        }
    }

    void PlaySound()
    {

        audioSource.pitch = Random.Range(.5f, 1.5f);

        audioSource.Play();

        soundPlayed = true;

        //curGrowlSoundTime = defaultGrowlSoundTime + Random.Range(-growlSoundTimeOffset, growlSoundTimeOffset);
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

}
