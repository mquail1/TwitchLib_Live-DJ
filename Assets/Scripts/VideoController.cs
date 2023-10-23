using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

// Script to control the background video playing in the scene

public class VideoController : MonoBehaviour
{
    // External References
    [Header("Video Clip Array")]
    [SerializeField] private VideoClip[] vidArray = new VideoClip[23]; // currently 26 vids
    [SerializeField] private VideoClip finalVid;

    [SerializeField] private VideoPlayer videoPlayer1;
    [SerializeField] private VideoPlayer videoPlayer2;
    [SerializeField] private VideoPlayer videoPlayer3;
    [SerializeField] private VideoPlayer videoPlayer4;

    private int vidArrayIndex = 0;

    // GUI Button Settings
    [Header("GUI Settings")]
    [SerializeField] private int GUIButtonWidth;
    [SerializeField] private int GUIButtonHeight;
    [SerializeField] private int xOffset;
    [SerializeField] private int yOffset;

    void OnGUI()
    {
        if (GUI.Button(new Rect(5 * xOffset, 5 * yOffset, GUIButtonWidth, GUIButtonHeight), ">>"))
        {
            nextVideo();
        }

        if (GUI.Button(new Rect(6 * xOffset, 6 * yOffset, GUIButtonWidth, GUIButtonHeight), "<<"))
        {
            previousVideo();
        }

        if (GUI.Button(new Rect(7 * xOffset, 7 * yOffset, GUIButtonWidth, GUIButtonHeight), "FNL"))
        {
            finalVideo();
        }

    }

    void nextVideo()
    {
        // insert next clip into video player
        videoPlayer1.clip = vidArray[vidArrayIndex];
        videoPlayer2.clip = vidArray[vidArrayIndex];
        videoPlayer3.clip = vidArray[vidArrayIndex];
        videoPlayer4.clip = vidArray[vidArrayIndex];

        videoPlayer1.Play();
        videoPlayer2.Play();
        videoPlayer3.Play();
        videoPlayer4.Play();

        // increment index counter
        vidArrayIndex++;

        if (vidArrayIndex == 23)
        {
            vidArrayIndex = 0;
        }
    }

    void previousVideo()
    {
        // insert next clip into video player
        videoPlayer1.clip = vidArray[vidArrayIndex - 1];
        videoPlayer2.clip = vidArray[vidArrayIndex - 1];
        videoPlayer3.clip = vidArray[vidArrayIndex - 1];
        videoPlayer4.clip = vidArray[vidArrayIndex - 1];

        videoPlayer1.Play();
        videoPlayer2.Play();
        videoPlayer3.Play();
        videoPlayer4.Play();

        // increment index counter
        vidArrayIndex--;

        if (vidArrayIndex < 0)
        {
            vidArrayIndex = 0;
        }
    }

    public void finalVideo()
    {
        videoPlayer1.clip = finalVid;
        videoPlayer2.clip = finalVid;
        videoPlayer3.clip = finalVid;
        videoPlayer4.clip = finalVid;

        videoPlayer1.Play();
        videoPlayer2.Play();
        videoPlayer3.Play();
        videoPlayer4.Play();
    }
}
