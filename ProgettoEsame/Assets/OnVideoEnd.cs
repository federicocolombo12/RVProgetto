using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class OnVideoEnd : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer; // Assign the VideoPlayer component in the Inspector

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished; // Subscribe to the event
        }
        else
        {
            Debug.LogError("VideoPlayer component not assigned!");
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Video has ended!");
        MySceneManager.instance.ResetSystem();
    }

    
}
