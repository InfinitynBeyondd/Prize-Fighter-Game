using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class OutroVideoScript : MonoBehaviour
{

    public GameObject videoImage;
    public VideoPlayer introVideoPlayer;

    public delegate void VideoPlayerDelegate(VideoPlayer videoPlayer);
    //public static event VideoPlayerDelegate loopPointReached;

    private void OnEnable()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (introVideoPlayer.time > 11f && (introVideoPlayer.time < 11.5))
        {
            TurnOffCutscene();
        }
    }

    public void TurnOffCutscene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(8);
        Cursor.lockState = CursorLockMode.Confined; // Unlock the cursor
        Cursor.visible = false; // Make the cursor visible
    }

    
}
