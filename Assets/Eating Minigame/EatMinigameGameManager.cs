using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatMinigameGameManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject player;
    public GameObject foodPrefab;

    public Camera main; 

    public int score;

    private void Start()
    {
        print("starting");
        main = Camera.main;

        SpawnFood();

        // Center the player on the screen to start
        player.transform.position = main.ViewportToWorldPoint(new Vector2(.5f, .5f));
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
    }


    public void OnFoodDestroy()
    {
        AddScore();
        SpawnEnemy();
        SpawnFood();
    }


    public void AddScore()
    {
        score++; 
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
