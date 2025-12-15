using UnityEngine;

public class TrustLight : MonoBehaviour
{
    public Light sceneLight;

    [Header("Intensity")]
    public float minIntensity = 0.4f;
    public float maxIntensity = 1.0f;

    [Header("Color")]
    public Color distrustColor = new Color(0.8f, 0.85f, 0.9f); // cool gray-blue
    public Color trustColor = new Color(1f, 0.95f, 0.85f);    // warm off-white

    void Update()
    {
        if (sceneLight == null) return;

        float wait = PlayerPrefs.GetFloat("FigureWait", 2.5f);

        // If trust is complete, stabilize the light
        if (wait <= 0.65f)
        {
            sceneLight.intensity = maxIntensity;
            sceneLight.color = trustColor;
            return;
        }

        // Map learning value to 0–1 range
        float t = Mathf.InverseLerp(2.5f, 0.6f, wait);

        // Apply emotional lighting
        sceneLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
        sceneLight.color = Color.Lerp(distrustColor, trustColor, t);
    }
}
