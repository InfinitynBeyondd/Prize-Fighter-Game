using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroVideoScript : MonoBehaviour
{

    public GameObject videoImage;
    public VideoPlayer introVideoPlayer;
    public GameObject gameMusic;
    public bool IsCutscenePlaying = true;
    public GameObject theProjector;


    public delegate void VideoPlayerDelegate(VideoPlayer videoPlayer);
    //public static event VideoPlayerDelegate loopPointReached;

    private void OnEnable()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (introVideoPlayer.time > 37f && (introVideoPlayer.time < 37.2f))
        {
            TurnOffCutscene();
        }

        if (Input.anyKeyDown && IsCutscenePlaying == true) 
        {
            skipCutscene();
            IsCutscenePlaying = false;
        }
    }

    public void TurnOffCutscene()
    {
        videoImage.SetActive(false);
    }

    public void skipCutscene()
    {
        transform.gameObject.SetActive(false);
        gameMusic.SetActive(false);
        theProjector.SetActive(false);
    }

    
}
