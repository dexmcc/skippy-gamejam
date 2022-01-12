using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamMinigameGameManager : MonoBehaviour
{

    Queue<GameObject> platforms = new Queue<GameObject>(); 
    GameObject highestPlatform = null;

    List<GameObject> allObjects;

    public GameObject platformPrefab;

    public GameObject player;
    public GameObject playerPrefab; 

    public float minYOffset = 3f;
    public float maxYOffset = 5f;
    public float xOffset = 20f; 

    int platformNum = 10;

    public float downSpeed = 1f;


    float middleX; 


    Camera main;
    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
        allObjects = new List<GameObject>();

        middleX = main.ViewportToWorldPoint(new Vector3(.5f, 0, 0)).x; 

        initializePlatforms();
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

        player.transform.Translate(new Vector3(0, 2, 0));
        
    }


    void ResetPlatform(GameObject platform)
    {
        
        if (highestPlatform != null)
        {

            float rawXOffset = Random.Range(2f, xOffset);
            float xSign = Mathf.Sign(Random.Range(-1, 1));

            platform.transform.position = new Vector3(highestPlatform.transform.position.x + (rawXOffset * xSign), highestPlatform.transform.position.y + Random.Range(minYOffset, maxYOffset), 0);

            Vector3 platformCameraPos = main.WorldToViewportPoint(platform.transform.position);
            platformCameraPos.x = Mathf.Clamp(platformCameraPos.x, 0, 1);
            platform.transform.position = main.ViewportToWorldPoint(platformCameraPos);

            highestPlatform = platform;
        }
        else
        {
            platform.transform.position = new Vector3(middleX, 0, 0);
            highestPlatform = platform;
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
            allObjects[i].transform.Translate(new Vector3(0, -downSpeed * Time.fixedDeltaTime, 0));
        }
    }

    private void FixedUpdate()
    {
        MoveAllObjects();
        UpdatePlatforms();
    }
}
