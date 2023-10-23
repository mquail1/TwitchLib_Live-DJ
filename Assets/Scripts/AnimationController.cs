using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    // Instance variables
    [SerializeField] private Animator animator;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material[] visorMaterialArray;
    [SerializeField] private GameObject zoomCam;
    
    // Animation Bools
    private bool invisVisor;

    // GUI button settings
    [Header("Settings")]
    [SerializeField] private int GUIButtonWidth;
    [SerializeField] private int GUIButtonHeight;
    [SerializeField] private int xOffset;
    [SerializeField] private int yOffset;

    // Start is called before the first frame update
    void Start()
    {
        // get Animator component on start
        animator = GetComponent<Animator>();
        invisVisor = true;
    }

    void OnGUI()
    {
        if( GUI.Button(new Rect(10*xOffset,0*yOffset,GUIButtonWidth,GUIButtonHeight), "visorOn") )
            StartCoroutine(changeVisor());
        if( GUI.Button(new Rect(11*xOffset,1*yOffset,GUIButtonWidth,GUIButtonHeight), "handUp1") )
            animator.SetTrigger("handsOn");
        if( GUI.Button(new Rect(12*xOffset,2*yOffset,GUIButtonWidth,GUIButtonHeight), "handUp2") )
            animator.SetTrigger("hands2On");
        if( GUI.Button(new Rect(13*xOffset,3*yOffset,GUIButtonWidth,GUIButtonHeight), "bounce") )
            animator.SetTrigger("bounceOn");
        if( GUI.Button(new Rect(14*xOffset,4*yOffset,GUIButtonWidth,GUIButtonHeight), "armin") )
            animator.SetTrigger("arminOn");
        if( GUI.Button(new Rect(15*xOffset,4*yOffset,GUIButtonWidth,GUIButtonHeight), "pointCam") )
            animator.SetTrigger("camptOn"); 
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // 3 KEY PRESSED : CHANGE VISOR MATERIAL
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            animator.SetBool("visorOn", true);
            StartCoroutine(changeVisor());
        }

        else
        {
            animator.SetBool("visorOn", false);
        }
        */
    }

    private IEnumerator changeVisor()
    {
        animator.SetTrigger("visorOn");
        zoomCam.SetActive(true);
        // change to the 2nd position in array
        if (invisVisor)
        {
            yield return new WaitForSeconds(4);
            meshRenderer.material = visorMaterialArray[1];
            invisVisor = false;
            yield return new WaitForSeconds(7.5f);
            zoomCam.SetActive(false);
            yield break;
        }
        
        if (!invisVisor)
        {
            yield return new WaitForSeconds(4);
            meshRenderer.material = visorMaterialArray[0];
            invisVisor = true;
            yield return new WaitForSeconds(7.5f);
            zoomCam.SetActive(false);
            yield break;
        }

        yield break;
    }
}
