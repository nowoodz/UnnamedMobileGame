using UnityEngine;
using TMPro; // Import TextMesh Pro namespace

public class FPSCounter : MonoBehaviour
{
    public TMP_Text fpsText; // Assign this in the inspector
    private float deltaTime;
    private float fps;
    private float smoothedFPS; // Variable to hold smoothed FPS
    public float smoothingFactor = 0.1f; // Adjust this value for smoothing

    void Update()
    {
        // Calculate the time between frames
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // Calculate current FPS
        fps = 1.0f / deltaTime;

        // Apply exponential smoothing
        smoothedFPS = Mathf.Lerp(smoothedFPS, fps, smoothingFactor);

        // Display the smoothed FPS in the UI
        fpsText.text = Mathf.Ceil(smoothedFPS).ToString();
    }
}