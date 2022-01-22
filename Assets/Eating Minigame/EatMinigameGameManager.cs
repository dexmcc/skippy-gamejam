using System.Collections.Generic;
using UnityEngine;

public class EatMinigameGameManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject player;
    public GameObject foodPrefab;

    public Camera main; 

    public int score;

    public int foodStatAdd = 6;
    public int foodStatAddOffset = 1;

    public AudioClip gameOverClip;




    [HideInInspector] public bool paused = false;
    List<GameObject> enemies = new List<GameObject>();
    Animation animation;
    AudioSource eatSound;

    
    public bool initPaused = true;
    public GameObject instructions;


    private void Update()
    {
        if (initPaused)
        {
            if (Input.anyKey)
            {
                //Unpause, it will be paused initially
                TogglePaused();
                initPaused = false;
                Destroy(instructions);
            }
        }
    }


    private void Start()
    {
        main = Camera.main;

        SpawnFood();

        animation = GetComponent<Animation>();
        eatSound = GetComponent<AudioSource>();

        // Center the player on the screen to start
        player.transform.position = main.ViewportToWorldPoint(new Vector2(.5f, .5f));
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);

        player.GetComponent<EatMinigamePlayerScript>().gameManager = this;


        // Start paused initially for instructions
        TogglePaused();
    }


    public void OnFoodDestroy()
    {
        AddScore();
        SpawnEnemy();
        SpawnFood();

        Global.getInstance().addFoodStat(foodStatAdd + Random.Range(-foodStatAddOffset, foodStatAddOffset));

        eatSound.Play();
    }


    public void AddScore()
    {
        score++; 
    }

    public void GameOver()
    {
        TogglePaused();
        eatSound.clip = gameOverClip;
        eatSound.Play();
        animation.Play();
    }

    public void GotoHub()
    {
        Global.getInstance().gotoHubArea();
    }


    void TogglePaused()
    {
        paused = !paused;

        player.GetComponent<EatMinigamePlayerScript>().paused = paused; 

        foreach (GameObject ob in enemies)
        {
            if (ob != null) {

                EatMinigameEnemyScript es;
                es = ob.GetComponent<EatMinigameEnemyScript>();

                if (es != null)
                {
                    es.paused = paused; 
                }
            }

        }
    }



    // Food are spawned like normal objects, then given a gamemanager (this instance of this class)
    public GameObject SpawnFood()
    {
        GameObject food = SpawnObject(foodPrefab);
        food.GetComponent<EatMinigameFoodScript>().gameManager = this;
        return food; 
    }


    // Enemies are spawned like normal objects, then given a target (the player)
    public GameObject SpawnEnemy()
    {
        GameObject enemy = SpawnObject(enemyPrefab);
        EatMinigameEnemyScript script = enemy.GetComponent<EatMinigameEnemyScript>();
        script.target = player.GetComponent<EatMinigamePlayerScript>();

        enemies.Add(enemy);
        

        return enemy;
    }


    // All objects are spawned in a random area within the camera bounds
    public GameObject SpawnObject(GameObject prefab)
    {
        GameObject ob = Instantiate(prefab);

        Vector2 ranPos = new Vector2(Random.Range(.1f, .9f), Random.Range(.1f, .9f));

        ob.transform.position = main.ViewportToWorldPoint(ranPos);

        ob.transform.position = new Vector3(ob.transform.position.x, ob.transform.position.y, 0);

        return ob; 
    }


}
