using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PLayVid : MonoBehaviour
{
    UnityEngine.Video.VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Maze Loop.mp4");
        videoPlayer.Play();
    }
}
