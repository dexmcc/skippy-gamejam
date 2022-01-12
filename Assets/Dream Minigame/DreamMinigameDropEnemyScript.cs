using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamMinigameDropEnemyScript : MonoBehaviour
{
    public float dropSpeed = 1.0f; 

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(0, -dropSpeed * Time.fixedDeltaTime, 0));
    }
}
