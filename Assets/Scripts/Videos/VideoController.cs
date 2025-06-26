using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnLoopPointReached;
        videoPlayer.Play();
    }

    void OnLoopPointReached(VideoPlayer vp)
    {
        vp.frame = 0;
        vp.Play();
    }
}
