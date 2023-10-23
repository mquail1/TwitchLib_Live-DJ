using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    // External References
    [Header("Main Camera References")]
    [SerializeField] private GameObject frontSweepRLCam;
    [SerializeField] private GameObject underneathSweepUpCam;
    [SerializeField] private GameObject frontZoomCam;
    [SerializeField] private GameObject faceCam;
    [SerializeField] private GameObject overShoulderCam;
    [SerializeField] private GameObject audienceSweepCam;
    [SerializeField] private GameObject audienceBackCam;
    [SerializeField] private GameObject roundRobinCam;
    [SerializeField] private GameObject audienceAboveCam;
    
    [Header("Boss Camera References")]
    [SerializeField] private GameObject upCam;
    [SerializeField] private GameObject lowAudienceCam;
    [SerializeField] private GameObject behindDJCam;
    [SerializeField] private GameObject bossCloseCam;
    [SerializeField] private GameObject bossBehindCam;

    // GUI Button Settings
    [Header("Settings")]
    [SerializeField] private int GUIButtonWidth;
    [SerializeField] private int GUIButtonHeight;
    [SerializeField] private int xOffset;
    [SerializeField] private int yOffset;

    // Start is called before the first frame update
    void Start(){}

    void OnGUI()
    {
        if( GUI.Button(new Rect(0*xOffset,0*yOffset,GUIButtonWidth,GUIButtonHeight), "sweepRL") )
            setCams(frontSweepRLCam);
        if( GUI.Button(new Rect(1*xOffset,1*yOffset,GUIButtonWidth,GUIButtonHeight), "sweepUp") )
            setCams(underneathSweepUpCam);
        if( GUI.Button(new Rect(2*xOffset,2*yOffset,GUIButtonWidth,GUIButtonHeight), "fZoom") )
            setCams(frontZoomCam);
        if( GUI.Button(new Rect(3*xOffset,3*yOffset,GUIButtonWidth,GUIButtonHeight), "face") )
            setCams(faceCam);
        if( GUI.Button(new Rect(4*xOffset,4*yOffset,GUIButtonWidth,GUIButtonHeight), "shoulder") )
            setCams(overShoulderCam);
        if( GUI.Button(new Rect(5*xOffset,5*yOffset,GUIButtonWidth,GUIButtonHeight), "audSweep") )
            setCams(audienceSweepCam);
        if( GUI.Button(new Rect(6*xOffset,6*yOffset,GUIButtonWidth,GUIButtonHeight), "audBack") )
            setCams(audienceBackCam);
        if( GUI.Button(new Rect(7*xOffset,7*yOffset,GUIButtonWidth,GUIButtonHeight), "rRobin") )
            setCams(roundRobinCam);
        if( GUI.Button(new Rect(8*xOffset,8*yOffset,GUIButtonWidth,GUIButtonHeight), "aAbove") )
            setCams(audienceAboveCam);

        if( GUI.Button(new Rect(9*xOffset,9*yOffset,GUIButtonWidth,GUIButtonHeight), "up") )
            setCams(upCam);
        if( GUI.Button(new Rect(10*xOffset,10*yOffset,GUIButtonWidth,GUIButtonHeight), "low") )
            setCams(lowAudienceCam);
        if( GUI.Button(new Rect(11*xOffset,11*yOffset,GUIButtonWidth,GUIButtonHeight), "bhdDJ") )
            setCams(behindDJCam);
        if( GUI.Button(new Rect(12*xOffset,12*yOffset,GUIButtonWidth,GUIButtonHeight), "close") )
            setCams(bossCloseCam);
        if( GUI.Button(new Rect(13*xOffset,13*yOffset,GUIButtonWidth,GUIButtonHeight), "bhdSKTH") )
            setCams(bossBehindCam);
    }

    public void setCams(GameObject currentCam)
    {
        // turn all other cams off while current cam remains on
        underneathSweepUpCam.SetActive(false);
        frontSweepRLCam.SetActive(false);
        frontZoomCam.SetActive(false);
        overShoulderCam.SetActive(false);
        faceCam.SetActive(false);
        audienceSweepCam.SetActive(false);
        audienceBackCam.SetActive(false);
        roundRobinCam.SetActive(false);
        audienceAboveCam.SetActive(false);

        upCam.SetActive(false);
        lowAudienceCam.SetActive(false);
        behindDJCam.SetActive(false);
        bossCloseCam.SetActive(false);
        bossBehindCam.SetActive(false);

        currentCam.SetActive(true);
    }

    // Update is called once per frame
    void Update(){}
}
