using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSwitch : MonoBehaviour
{
    [Header("Environment References")]
    [SerializeField] private GameObject Environment1;
    [SerializeField] private GameObject Environment2;

    [Header("Audio References")]
    [SerializeField] private GameObject mixAudio;
    [SerializeField] private GameObject encoreAudio;

    [Header("UI References")]
    [SerializeField] private GameObject fadeToBlack;
    [SerializeField] private GameObject fadeToAlpha;
    [SerializeField] private GameObject cutSceneUI;
    [SerializeField] private GameObject bossPrompt; // Canvas displaying "Use channel points to attack the boss!"

    [Header("Video References")]
    [SerializeField] private VideoController videoController;

    [Header("Settings")]
    [SerializeField] private float fadeOutTime;
    [SerializeField] private float fadeInTime;
    [SerializeField] private float cutsceneTime;
    
    // GUI Button Settings
    [Header("GUI Settings")]
    [SerializeField] private int GUIButtonWidth;
    [SerializeField] private int GUIButtonHeight;
    [SerializeField] private int xOffset;
    [SerializeField] private int yOffset;

    void OnGUI()
    {
        if( GUI.Button(new Rect(8*xOffset,8*yOffset,GUIButtonWidth,GUIButtonHeight), "->") )
        {
            StartCoroutine(transitionScenes());
        }
    }

    private IEnumerator transitionScenes()
    {
        // Fade screen to black
        fadeToBlack.SetActive(true);

        // Wait for fade out
        yield return new WaitForSeconds(fadeOutTime);

        // Turn off Environment 1
        Environment1.SetActive(false);

        // Turn off mix audio
        mixAudio.SetActive(false);

        // Turn on Environment 2
        Environment2.SetActive(true);

        // Turn off fade out
        fadeToBlack.SetActive(false);

        // Turn on Cutscene UI
        cutSceneUI.SetActive(true);

        // Turn off fade out
        fadeToBlack.SetActive(false);

        // Wait for cutscene to play
        yield return new WaitForSeconds(cutsceneTime);

        // Turn on Fade in
        fadeToAlpha.SetActive(true);
    
        // Turn off Cutscene UI
        cutSceneUI.SetActive(false);

        // Wait for In
        // yield return new WaitForSeconds(fadeInTime);

        // start boss battle prompt
        bossPrompt.SetActive(true);

        // Begin Encore Audio
        encoreAudio.SetActive(true);

        // Play final video
        videoController.finalVideo();

        // Wait for text prompt
        yield return new WaitForSeconds(8);

        // disable the boss prompt
        bossPrompt.SetActive(false);

        // End Coroutine
        yield break;
    }
}