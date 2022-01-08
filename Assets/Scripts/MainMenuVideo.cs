using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MainMenuVideo : MonoBehaviour
{
    VideoPlayer BackgroundVideo;
    public GameObject Note;

    // Start is called before the first frame update
    void Start()
    {
        BackgroundVideo = gameObject.GetComponent<VideoPlayer>();
        BackgroundVideo.Pause();
        
    }

    void FixedUpdate()
    {
        if (BackgroundVideo.clockTime >= 31.5)
        {
            BackgroundVideo.Pause();
            OpenNote();
        }
    }

    //for when play button is pressed
    public void PlayButton()
    {
        BackgroundVideo.Play();
    }

    public void QuitButton()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    public void OpenNote()
    {
        Note.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainHub");
    }

//skips to 8 seconds, 480 frames
//fps is 60
}
