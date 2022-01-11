using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamMinigameGameManager : MonoBehaviour
{

    Queue<GameObject> platforms = new Queue<GameObject>(); 
    GameObject highestPlatform = null;

    public GameObject platformPrefab;

    public GameObject player;
    public GameObject playerPrefab; 

    public float minYOffset = 3f;
    public float maxYOffset = 5f;
    public float xOffset = 10f; 

    int platformNum = 10;

    Camera main;
    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
        initializePlatforms();
    }

    void initializePlatforms()
    {
        // Check if the new position is within the bounds of the camera
        /*Vector3 viewPoint = main.WorldToViewportPoint(newPos);

        // Clamp the position in the camera
        viewPoint.x = Mathf.Clamp(viewPoint.x, 0, 1);
        viewPoint.y = Mathf.Clamp(viewPoint.y, 0, 1);

        newPos = main.ViewportToWorldPoint(viewPoint);*/


        Vector2 startPos = main.ViewportToWorldPoint(new Vector3(.5f, 0, 0));

        for (int i = 0; i < platformNum; i++)
        {

            if (highestPlatform != null)
            {
                startPos.y = highestPlatform.transform.position.y + Random.Range(minYOffset, maxYOffset);
                startPos.x += Random.Range(-xOffset, xOffset);
            }

            GameObject platform = Instantiate(platformPrefab);

            platform.transform.position = startPos;

            highestPlatform = platform; 

            platforms.Enqueue(platform);
        }

        player = Instantiate(playerPrefab);
        player.transform.position = platforms.Peek().transform.position;

        player.transform.Translate(new Vector3(0, 2, 0));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
