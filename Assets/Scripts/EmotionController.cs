using UnityEngine;
using UnityEngine.InputSystem;

public class EmotionController : MonoBehaviour
{
    [Header("Material Array References")]
    [SerializeField] private Material[] neutralEmotions;
    [SerializeField] private Material[] angryEmotions;
    [SerializeField] private Material[] happyEmotions;
    [SerializeField] private Material[] wideEmotions;

    [Header("Mesh Renderer Reference")]
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    private Material[] currentArray = new Material[6];

    [Header("GUI Button Settings")]
    [SerializeField] private int GUIButtonWidth;
    [SerializeField] private int GUIButtonHeight;
    [SerializeField] private int xOffset;
    [SerializeField] private int yOffset;

    // Instance Variables
    // private int index = 0;
    private string input;

    // Start is called before the first frame update
    void Start()
    {
        neutralEmotions.CopyTo(currentArray, 0);
        // Debug.Log(currentArray);
    }

    void OnGUI()
    {
        if( GUI.Button(new Rect(1*xOffset,1*yOffset,GUIButtonWidth,GUIButtonHeight), ":)") )
            neutralEmotions.CopyTo(currentArray,0);

        if( GUI.Button(new Rect(2*xOffset,2*yOffset,GUIButtonWidth,GUIButtonHeight), ">:(") )
            angryEmotions.CopyTo(currentArray,0);

        if( GUI.Button(new Rect(3*xOffset,3*yOffset,GUIButtonWidth,GUIButtonHeight), ":D") )
            happyEmotions.CopyTo(currentArray,0);

        if(GUI.Button(new Rect(4*xOffset,4*yOffset,GUIButtonWidth,GUIButtonHeight), "8|") )
            wideEmotions.CopyTo(currentArray,0);
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.inputString;

        // Debug
        /*
        if (!string.IsNullOrEmpty(input))
        {
            Debug.Log("Pressed: " + Input.inputString);
        }
        */

        switch(input)
        {
            case "x": // look forward
                meshRenderer.material = currentArray[0];
                break;     
            case "a": // look left
                meshRenderer.material = currentArray[1];
                break;
            case "d": // look right
                meshRenderer.material = currentArray[2];
                break;
            case "w": // look up
                meshRenderer.material = currentArray[3];
                break;
            case "s": // look down
                meshRenderer.material = currentArray[4];
                break;
            case "z": // blink
                meshRenderer.material = currentArray[5];
                break;   
        }
    }

    void swapArray(Material[] specifiedArray)
    {
        // swap to another emotion
        System.Array.Copy(specifiedArray, currentArray, specifiedArray.Length);
    }
}

// Keyboard.current.digit0Key.wasPressedThisFrame