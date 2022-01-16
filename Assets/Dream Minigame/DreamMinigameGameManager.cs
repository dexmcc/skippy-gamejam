using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamMinigameGameManager : MonoBehaviour
{

    public GameObject platformPrefab;
    public GameObject playerPrefab;
    public GameObject dropEnemyPrefab;
    public GameObject walkEnemyPrefab;
    

    public float minYOffset = 3f;
    public float maxYOffset = 5f;
    public float xOffset = 20f;

    public float minDropEnemyTime = 2.0f;
    public float dropEnemyTimeOffset = 1.0f;
    public int walkEnemyChance = 40;

    public float downSpeed = 1.5f;

    public int sleepStatAdd = 5;

    [HideInInspector] public GameObject player;
    DreamMinigamePlayerScript playerScript;

    int platformNum = 10;

    Queue<GameObject> platforms = new Queue<GameObject>();
    GameObject highestPlatform = null;

    List<GameObject> allObjects;

    float middleX;

    int score = 0;

    float dropEnemyTime = 3.0f;

    [HideInInspector] public bool paused = false;

    Animation animation; 
    

    
    


    Camera main;
    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
        allObjects = new List<GameObject>();

        middleX = main.ViewportToWorldPoint(new Vector3(.5f, 0, 0)).x;

        animation = GetComponent<Animation>();

        initializePlatforms();
    }

    void TogglePaused()
    {
        paused = !paused; 

        foreach (GameObject ob in allObjects)
        {

            Rigidbody2D rb;
            if (ob != null)
            {
                ob.TryGetComponent<Rigidbody2D>(out rb);

                if (rb != null)
                {
                    rb.simulated = !rb.simulated;
                }
            }

        }
    }

    private void FixedUpdate()
    {
        if (!paused)
        {
            MoveAllObjects();
            UpdatePlatforms();
            CheckDropEnemy();
        }
    }

    public void GameOver()
    {
        TogglePaused();
        animation.Play();
    }

    public void GotoHub()
    {
        Global.getInstance().gotoHubArea();
    }



    void initializePlatforms()
    {

        for (int i = 0; i < platformNum; i++)
        {

            
            GameObject platform = Instantiate(platformPrefab);

            ResetPlatform(platform);

            allObjects.Add(platform);

            platforms.Enqueue(platform);
        }

        player = Instantiate(playerPrefab);
        allObjects.Add(player);
        player.transform.position = platforms.Peek().transform.position;

        playerScript = player.GetComponent<DreamMinigamePlayerScript>();
        playerScript.gameManager = this; 

        player.transform.Translate(new Vector3(0, 2, 0));

        
    }

    public void AddScore()
    {
        score++;
    }


    void ResetPlatform(GameObject platform)
    {

        DreamMinigamePlatformLogic script = platform.GetComponent<DreamMinigamePlatformLogic>();
        script.jumpedOn = false; 
        
        if (highestPlatform != null)
        {

            DreamMinigamePlatformLogic highestScript = highestPlatform.GetComponent<DreamMinigamePlatformLogic>();
            int posNum = 0; 

            do
            {
                posNum = Random.Range(0, 3);
            } while (posNum == highestScript.posNum);


            script.posNum = posNum;

            float xCamPos = 0; 

            // Left
            if (posNum == 0)
            {
                xCamPos = .1f; 
            }
            else if (posNum == 1)
            {
                xCamPos = .5f; 
            }
            else
            {
                xCamPos = .9f; 
            }

            float newX = main.ViewportToWorldPoint(new Vector3(xCamPos, 0, 0)).x;


            //float rawXOffset = Random.Range(2f, xOffset);
            //float xSign = Mathf.Sign(Random.Range(-1, 1));

            //platform.transform.position = new Vector3(highestPlatform.transform.position.x + (rawXOffset * xSign), highestPlatform.transform.position.y + Random.Range(minYOffset, maxYOffset), 0);

            platform.transform.position = new Vector3(Mathf.Clamp(newX, highestPlatform.transform.position.x - xOffset, highestPlatform.transform.position.x + xOffset), highestPlatform.transform.position.y + Random.Range(minYOffset, maxYOffset), 0);

            Vector3 platformCameraPos = main.WorldToViewportPoint(platform.transform.position);
            platformCameraPos.x = Mathf.Clamp(platformCameraPos.x, 0, 1);
            platform.transform.position = main.ViewportToWorldPoint(platformCameraPos);


            int ran = Random.Range(0, 100);

            if (ran <= walkEnemyChance)
            {
                SpawnWalkEnemy(new Vector3(platform.transform.position.x, platform.transform.position.y + 1f, 0));
            }

        }
        else
        {
            script.posNum = 1; 
            platform.transform.position = new Vector3(middleX, -3.0f, 0);
        }

        highestPlatform = platform;
    }


    void SpawnWalkEnemy(Vector3 position)
    {
        GameObject enemy = Instantiate(walkEnemyPrefab);

        enemy.transform.position = position;

        allObjects.Add(enemy);
    }

    void CheckDropEnemy()
    {
        if (dropEnemyTime > 0)
        {
            dropEnemyTime -= Time.fixedDeltaTime; 
        }
        else
        {
            GameObject enemy = Instantiate(dropEnemyPrefab);

            enemy.GetComponent<DreamMinigameDropEnemyScript>().gameManager = this; 

            Vector3 newPos = main.ViewportToWorldPoint(new Vector3(Random.value, 1.5f, 0));
            newPos.z = 0; 
            enemy.transform.position = newPos;

            allObjects.Add(enemy);

            dropEnemyTime = minDropEnemyTime + Random.Range(0, dropEnemyTimeOffset);
        }
    }

    void UpdatePlatforms()
    {

        // Position of lowest platform from camera's perspective 
        Vector3 viewPoint = main.WorldToViewportPoint(platforms.Peek().transform.position);

        if (viewPoint.y < -.2)
        {
            GameObject lowPlatform = platforms.Dequeue();

            ResetPlatform(lowPlatform);

            platforms.Enqueue(highestPlatform);

        }
    }


    // Moves everything down 
    void MoveAllObjects()
    {
        for (int i = 0; i < allObjects.Count; i++)
        {
            if (allObjects[i] != null)
            {
                allObjects[i].transform.Translate(new Vector3(0, -downSpeed * Time.fixedDeltaTime, 0));
            }
        }
    }

    public void SpeedUp()
    {
        playerScript.horizontalSpeed *= 1.02f;
        downSpeed *= 1.03f; 

    }

    public void OnPlayerJumpedOnPlatform()
    {
        AddScore();
        SpeedUp();
        minDropEnemyTime *= .9f;
        Global.getInstance().addSleepStat(sleepStatAdd);

    }

    

    
}
