using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using URPGlitch.Runtime.AnalogGlitch;
using URPGlitch.Runtime.DigitalGlitch;


public class GlitchController : MonoBehaviour
{
    [Header("External References")]
    [SerializeField] private Volume volume;
    [SerializeField] private GameObject blackUI;
    private AnalogGlitchVolume analogGlitchVolume;
    private DigitalGlitchVolume digitalGlitchVolume;
    private bool glitchOn;
    private bool majorGlitchOn;

    [Header("External References")]
    [SerializeField] private float scanLineJitterLower;
    [SerializeField] private float scanLineJitterUpper;
    [SerializeField] private float verticalJumpLower;
    [SerializeField] private float verticalJumpUpper;
    [SerializeField] private float horizontalShakeLower;
    [SerializeField] private float horizontalShakeUpper;
    [SerializeField] private float colorDriftLower;
    [SerializeField] private float colorDriftUpper;
    [SerializeField] private float intensityLower;
    [SerializeField] private float intensityUpper;

    

    // GUI Button Settings
    [Header("GUI Settings")]
    [SerializeField] private int GUIButtonWidth;
    [SerializeField] private int GUIButtonHeight;
    [SerializeField] private int xOffset;
    [SerializeField] private int yOffset;

    private void Start()
    {
        volume.profile.TryGet<AnalogGlitchVolume>(out analogGlitchVolume);
        volume.profile.TryGet<DigitalGlitchVolume>(out digitalGlitchVolume);
        glitchOn = false;
        majorGlitchOn = false;
    }

    void OnGUI()
    {
        if( GUI.Button(new Rect(10*xOffset,10*yOffset,GUIButtonWidth,GUIButtonHeight), "sG") )
        {
            if (glitchOn)
            {
                glitchOn = false;
            }

            else if (!glitchOn)
            {
                glitchOn = true;
            }
            glitch();
        }

        if( GUI.Button(new Rect(11*xOffset,11*yOffset,GUIButtonWidth,GUIButtonHeight), "mG") )
        {
            if (majorGlitchOn)
            {
                majorGlitchOn = false;
            }

            else if (!majorGlitchOn)
            {
                majorGlitchOn = true;
            }
            majorGlitch();
        }

        if( GUI.Button(new Rect(12*xOffset,12*yOffset,GUIButtonWidth,GUIButtonHeight), "rG") )
        {
            randomGlitch();
        }

        if( GUI.Button(new Rect(13*xOffset,13*yOffset,GUIButtonWidth,GUIButtonHeight), "0") )
        {
            blackScreen();
        }
    }

    void glitch()
    {
        if (!glitchOn)
        {
            analogGlitchVolume.scanLineJitter.value = 0.056f;
            analogGlitchVolume.verticalJump.value = 0.056f;
            analogGlitchVolume.horizontalShake.value = 0.04f;
            analogGlitchVolume.colorDrift.value = 0.206f;
            digitalGlitchVolume.intensity.value = 0.094f;
        }

        if (glitchOn)
        {
            analogGlitchVolume.scanLineJitter.value = 0.0f;
            analogGlitchVolume.verticalJump.value = 0.0f;
            analogGlitchVolume.horizontalShake.value = 0.0f;
            analogGlitchVolume.colorDrift.value = 0.0f;
            digitalGlitchVolume.intensity.value = 0.0f;
        }
    }

    void majorGlitch()
    {
        if (!majorGlitchOn)
        {
            analogGlitchVolume.scanLineJitter.value = 0.5f;
            analogGlitchVolume.verticalJump.value = 0.11f;
            analogGlitchVolume.horizontalShake.value = 0.573f;
            analogGlitchVolume.colorDrift.value = 0.568f;
            digitalGlitchVolume.intensity.value = 0.500f;
        }

        if (majorGlitchOn)
        {
            analogGlitchVolume.scanLineJitter.value = 0.0f;
            analogGlitchVolume.verticalJump.value = 0.0f;
            analogGlitchVolume.horizontalShake.value = 0.0f;
            analogGlitchVolume.colorDrift.value = 0.0f;
            digitalGlitchVolume.intensity.value = 0.0f;
        }
    }

    void randomGlitch()
    {
        analogGlitchVolume.scanLineJitter.value = Random.Range(scanLineJitterLower, scanLineJitterUpper);
        analogGlitchVolume.verticalJump.value = Random.Range(verticalJumpLower, verticalJumpUpper);
        analogGlitchVolume.horizontalShake.value = Random.Range(horizontalShakeLower, horizontalShakeUpper);
        analogGlitchVolume.colorDrift.value = Random.Range(colorDriftLower, colorDriftUpper);
        digitalGlitchVolume.intensity.value = Random.Range(intensityLower, intensityUpper);
    }

    void blackScreen()
    {
        if (blackUI.activeSelf)
        {
            blackUI.SetActive(false);
        }

        else if (!blackUI.activeSelf)
        {
            blackUI.SetActive(true);
        }
    }
    
}
