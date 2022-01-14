using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatMinigameEnemyScript : MonoBehaviour
{

    public EatMinigamePlayerScript target = null;

    public float baseSpeed = 3f;
    public float baseSpeedRandomOffset = 1f;
    public float attackRange = 1.2f;

    public float rangeSpeedOffset = 1.5f;
    public float rangeTargetOffset = 1f; 


    private bool attacking = false;

    private Rigidbody2D rb;
    private Animation animation;

    private Vector2 randomSpeedOffset = new Vector2(0, 0);
    private Vector2 randomTargetOffset = new Vector2(0, 0);
    private float curOffsetChangeTime = 1;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animation>();

        baseSpeed += Random.Range(-baseSpeedRandomOffset, baseSpeedRandomOffset);
    }

    // Enemy AI
    // Moves toward the player's position (with a random offset, at a randomly offseted speed) 
    // When close to the player, it stops moving, and plays an attack animation
    // In the attack animation two functions are called: 
    // Attack, which destroys the player if they're still near
    // and AttackEnd, which causes the Enemy to start moving again and it repeats the process
    // Meant to be sporadic and "nightmare" like
    private void FixedUpdate()
    {
        if (!attacking && target != null)
        {
            // Check if the random offsets should be changed, if not, decrement the time.
            if (curOffsetChangeTime > 0)
            {
                curOffsetChangeTime -= Time.fixedDeltaTime;
            }
            else
            {
                randomSpeedOffset = new Vector2(Random.Range(-rangeSpeedOffset, rangeSpeedOffset), 
                    Random.Range(-rangeSpeedOffset, rangeSpeedOffset));
                randomTargetOffset = new Vector2(Random.Range(-rangeTargetOffset, rangeTargetOffset),
                    Random.Range(-rangeTargetOffset, rangeTargetOffset));
                curOffsetChangeTime = Random.Range(.5f, 2);
            }

            Vector2 moveTo = ((target.transform.position + new Vector3(randomTargetOffset.x, randomTargetOffset.y, 0)) 
                - transform.position).normalized;

            rb.MovePosition(rb.position + ((moveTo + randomSpeedOffset) * baseSpeed * Time.fixedDeltaTime));


            if (Vector3.Distance(transform.position, target.transform.position) < attackRange)
            {
                AttackStart();
            }

        }
    }

    // Starts the animation
    void AttackStart()
    {
        // Play animation
        attacking = true;
        animation.Play();
    }


    // Called in the animation, checks if the player is in range then kills them
    void Attack()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < attackRange)
        {
            target.Kill();
            target = null;
        }
        
    }

    // Called at the end of the animation
    public void AttackEnd()
    {
        attacking = false;
        animation.Stop();
    }
}