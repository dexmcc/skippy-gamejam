using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamMinigameWalkEnemyScript : MonoBehaviour
{
    int direction = 0;
    float speed = 80.0f;

    public LayerMask hit; 

    Camera main;
    Rigidbody2D rb;
    AudioSource audioSource; 

    Vector3 zeroVel = Vector3.zero;

    public AudioClip[] growlSounds;
    bool growlPlayed = false; 



    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;

        direction = (int) Mathf.Sign(main.ScreenToWorldPoint(new Vector2(.5f, 0)).x - transform.position.x);
        
        if (direction == 0) { direction = 1; }

        rb = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();


    }

    private void FixedUpdate()
    {

        //rb.MovePosition(transform.position + new Vector3(direction * speed * Time.fixedDeltaTime, Physics.gravity.y * Time.fixedDeltaTime, 0));

        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(direction * speed * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = targetVelocity;
        // And then smoothing it out and applying it to the character
        //rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref zeroVel, .05f);

        Vector3 start = transform.position + new Vector3(direction, -.5f, 0);

        Debug.DrawLine(start + new Vector3(0, -1, 0), start + new Vector3(0, 1, 0));

        //!Physics2D.OverlapCircle(start, .5f, LayerMask.NameToLayer("OnlyHitGround"))

        
        if (!Physics2D.OverlapCircle(start, 0.5f, hit))
        {
            direction *= -1; 
        }

        if (main.WorldToViewportPoint(transform.position).y < -.3f)
        {
            Kill();
        }

        if (main.WorldToViewportPoint(transform.position).y < .85 && !growlPlayed)
        {
            PlayGrowl();
        }

    }

    void PlayGrowl()
    {
        audioSource.clip = growlSounds[Random.Range(0, growlSounds.Length - 1)];
        audioSource.pitch = Random.Range(.85f, 1.15f);

        audioSource.Play();

        growlPlayed = true;

        //curGrowlSoundTime = defaultGrowlSoundTime + Random.Range(-growlSoundTimeOffset, growlSoundTimeOffset);
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
