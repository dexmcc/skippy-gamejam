using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class EndingManager : MonoBehaviour
{

    public VideoPlayer endingCutscene;
    public void FixedUpdate()
    {
        if ((endingCutscene.frame) > 0 && ((endingCutscene.isPlaying == false) || ((long)endingCutscene.frame == (long)(endingCutscene.frameCount - ((long)1)))))
        {
            SceneManager.LoadScene("MainMenu");
        }

    }
}
