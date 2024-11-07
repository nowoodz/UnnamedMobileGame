using System.Net;
using UnityEngine;

public class SafeAreaScript : MonoBehaviour
{
    private RectTransform panelSafeArea;
    private Rect lastSafeArea = new Rect(0, 0, 0, 0);

    void Awake()
    {
        panelSafeArea = GetComponent<RectTransform>();
        ApplySafeArea();
    }

    void Update()
    {
        // Check if the safe area has changed
        if (lastSafeArea != Screen.safeArea)
        {
            ApplySafeArea();
        }
    }

    void ApplySafeArea()
    {
        Rect safeArea = Screen.safeArea;

        // Convert safe area Rect to anchor values for RectTransform
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        // Normalize the values by dividing by the screen dimensions
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        // Apply normalized anchors to RectTransform
        panelSafeArea.anchorMin = anchorMin;
        panelSafeArea.anchorMax = anchorMax;

        // Update the last known safe area
        lastSafeArea = safeArea;

    }
}
