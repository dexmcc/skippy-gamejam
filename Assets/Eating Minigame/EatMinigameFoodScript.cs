using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatMinigameFoodScript : MonoBehaviour
{

    public EatMinigameGameManager gameManager; 

    public void GivePointAndDestroy()
    {
        gameManager.OnFoodDestroy();
        Destroy(gameObject);
    }
}
