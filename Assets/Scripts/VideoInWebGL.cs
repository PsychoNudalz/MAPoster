using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoInWebGL : MonoBehaviour
{
    [SerializeField]
    private string name = "";
    private void Awake()
    {
        VideoPlayer vp = GetComponent<VideoPlayer>();
        vp.url = System.IO.Path.Combine (Application.streamingAssetsPath,name+".mp4"); 

    }
}
