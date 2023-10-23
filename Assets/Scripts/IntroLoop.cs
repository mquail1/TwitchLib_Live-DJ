using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that loops one render texture as a placeholder
// On prompt, the loop texture changes to the intro cutscene
// Cutscene plays out then transitions to ENV 1 (already pre-loaded, no need to turn on / off)
// Triggers the start of mix audio when all done

public class IntroLoop : MonoBehaviour
{
    // External References
    [Header("External References")]
    [SerializeField] private GameObject introLoop;
    [SerializeField] private GameObject introVideo;
    [SerializeField] private GameObject fadeToAlpha;
    [SerializeField] private GameObject mixAudio;
    [SerializeField] private GameObject encoreAudio;
    [SerializeField] private GameObject blackScreen;

    // Timing Settings
    [Header("Coroutine Settings")]
    [SerializeField] private float introVidTime;

    // GUI Button Settings
    [Header("GUI Settings")]
    [SerializeField] private int GUIButtonWidth;
    [SerializeField] private int GUIButtonHeight;
    [SerializeField] private int xOffset;
    [SerializeField] private int yOffset;

    void OnGUI()
    {
        if( GUI.Button(new Rect(9*xOffset,9*yOffset,GUIButtonWidth,GUIButtonHeight), "ST") )
        {
            StartCoroutine(startTheShow());
        }

        if( GUI.Button(new Rect(10*xOffset,10*yOffset,GUIButtonWidth,GUIButtonHeight), "ED") )
        {
            endTheShow();
        }
    }

    private IEnumerator startTheShow()
    {
        // change from loop texture to intro texture
        introLoop.SetActive(false);
        Debug.Log("Intro Loop Active: " + introLoop.activeSelf);
        
        introVideo.SetActive(true);
        Debug.Log("Intro Video Active: " + introVideo.activeSelf);
        
        // wait the specified time for the cutscene to play out
        yield return new WaitForSeconds(introVidTime);

        // set intro video to inactive
        introVideo.SetActive(false);

        // fade in
        fadeToAlpha.SetActive(true);

        // start audio
        mixAudio.SetActive(true);

        // wait a few seconds
        yield return new WaitForSeconds(5);

        // turn off fade to alpha
        fadeToAlpha.SetActive(false);
    }

    void endTheShow()
    {
        // turn off all music
        encoreAudio.SetActive(false);
        mixAudio.SetActive(false);

        // turn off black screen if it's on
        blackScreen.SetActive(false);

        // turn on looping video
        introLoop.SetActive(true);
    }
}
