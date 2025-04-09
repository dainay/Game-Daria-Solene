using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{
    public string videoFileName = "intro.mp4";  
    public string nextSceneName = "Game scene";  
    void Start()
    {
        // code from the unity community.
        // it is written that very often video is not worjing in build if it is not in streaming assets
        //it must be attached by code additionally and not only in Unity settings
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
         
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = Application.streamingAssetsPath + "/" + videoFileName;
        videoPlayer.loopPointReached += OnVideoEnd;
         
        videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {   
        SceneManager.LoadScene(nextSceneName);
    }
}
