using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleOnAmplitude : MonoBehaviour
{
    // [Header("Material Reference")]
    private Material material;
    
    [Header("Settings")]
    [SerializeField] private float startScale;
    [SerializeField] private float maxScale;
    [SerializeField] private bool useBuffer;
    [SerializeField] private float red;
    [SerializeField] private float green;
    [SerializeField] private float blue;

    // Start is called before the first frame update
    void Start()
    {
        // use this to get the material this script is placed on
        // otherwise, will use the custom material referenced in Inspector
        // material = GetComponent<MeshRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!useBuffer)
        {
            transform.localScale = new Vector3 ( (audioVisualizer.amplitude * maxScale) + startScale, (audioVisualizer.amplitude * maxScale) + startScale, (audioVisualizer.amplitude * maxScale) + startScale);
            // Color color = new Color (red * audioVisualizer.amplitude, green * audioVisualizer.amplitude, blue * audioVisualizer.amplitude);
            // material.SetColor("_EmissionColor", color);
        }

        if (useBuffer && audioVisualizer.amplitude > 0)
        {
            transform.localScale = new Vector3 ( (audioVisualizer.amplitudeBuffer * maxScale) + startScale, (audioVisualizer.amplitudeBuffer * maxScale) + startScale, (audioVisualizer.amplitudeBuffer * maxScale) + startScale);
            // Color color = new Color (red * audioVisualizer.amplitudeBuffer, green * audioVisualizer.amplitudeBuffer, blue * audioVisualizer.amplitudeBuffer);
            // material.SetColor("_EmissionColor", color);
        }       
    }
}
