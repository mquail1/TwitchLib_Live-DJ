using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: right and left channel sampling? needed?

public class audioVisualizer : MonoBehaviour
{
    // External References
    [Header("Audio References")]
    [SerializeField] private AudioSource audioSource;

    // Settings
    [Header("Frequency Band Settings")]
    // array of 512 floats (sampling at 512)
    public static float[] samples = new float[512];
    [SerializeField] public static float[] samplesLeft = new float[512]; // left channel samples
    [SerializeField] public static float[] samplesRight = new float[512]; // right channel samples

    [SerializeField] public static float[] freqBand = new float[8];
    [SerializeField] public static float[] bandBuffer = new float[8];
    [SerializeField] private float[] bufferDecrease = new float[8];

    [Header("Audio Buffer Settings")]
    [SerializeField] public float[] freqBandHighest = new float[8];
    [SerializeField] public static float[] audioBand = new float[8];
    [SerializeField] public static float[] audioBandBuffer = new float[8];

    [Header("Amplitude Settings")]
    [SerializeField] public static float amplitude;
    [SerializeField] public static float amplitudeBuffer;
    [SerializeField] public float audioProfile;
    float amplitudeHighest;
    

    // Start is called before the first frame update
    void Start()
    {
        // audioSource = GetComponent<AudioSource>();

        AudioProfile(audioProfile);
    }

    // Update is called once per frame
    void Update()
    {
        // listens to audio source every frame
        // collects samples via GetSpectrumData in GetSpectrumAudioSource()
        GetSpectrumAudioSource();

        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();
    }

    void GetSpectrumAudioSource()
    {
        // 0 is left channel, 1 is right channel
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
        audioSource.GetSpectrumData(samplesLeft, 0, FFTWindow.Blackman);
        audioSource.GetSpectrumData(samplesRight, 1, FFTWindow.Blackman);
    }

    void MakeFrequencyBands()
    {
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;

            int sampleCount = (int)Mathf.Pow(2,i) * 2;

            if (i == 7)
                sampleCount +=2;

            for (int j = 0; j < sampleCount; j++)
            {
                // for left channel sampling only
                average += samples[count] * (count + 1);
                // for stereo (both channel) sampling
                average += samplesLeft[count] + samplesRight[count] * (count + 1);
                count++;
            }

            average /= count;

            freqBand[i] = average * 10;

        }
    }

    void BandBuffer()
    {
        for (int g = 0; g < 8; ++g)
        {
            if (freqBand[g] > bandBuffer[g])
            {
                bandBuffer[g] = freqBand[g];
                bufferDecrease[g] = 0.005f;
            }

            if (freqBand[g] < bandBuffer[g])
            {
                bandBuffer[g] -= bufferDecrease[g];
                bufferDecrease[g] *= 1.2f;
            }
        }
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (freqBand[i] > freqBandHighest[i])
                freqBandHighest[i] = freqBand[i];

            audioBand[i] = (freqBand[i] / freqBandHighest[i]);
            audioBandBuffer[i] = (bandBuffer[i] / freqBandHighest[i]);
        }
    }

    void GetAmplitude()
    {
        float currentAmplitude = 0;
        float currentAmplitudeBuffer = 0;

        for (int i = 0; i < 8; i++)
        {
            currentAmplitude += audioBand[i];
            currentAmplitudeBuffer += audioBandBuffer[i];
        }

        if (currentAmplitude > amplitudeHighest)
            amplitudeHighest = currentAmplitude;

        amplitude = currentAmplitude / amplitudeHighest;
        amplitudeBuffer = currentAmplitudeBuffer / amplitudeHighest;
    }

    void AudioProfile(float aProfile)
    {
        for (int i = 0; i < 8; i++)
        {
            freqBandHighest[i] = aProfile;
        }
    }
}
