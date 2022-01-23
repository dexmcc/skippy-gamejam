using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamMinigamePlatformLogic : MonoBehaviour
{
    public bool jumpedOn = false;
    public int posNum;
    public AudioSource landSound; 

    void Start()
    {
        landSound.volume = 0.2f;
    }

}
