using UnityEngine;

public class TrustAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public float minVolume = 0f;
    public float maxVolume = 0.35f;

    void Start()
    {
        if (audioSource != null)
            audioSource.Play();
    }

    void Update()
    {
        if (audioSource == null) return;

        float wait = PlayerPrefs.GetFloat("FigureWait", 2.5f);

        // Stabilize at full trust
        if (wait <= 0.65f)
        {
            audioSource.volume = maxVolume;
            return;
        }

        float t = Mathf.InverseLerp(2.5f, 0.6f, wait);
        audioSource.volume = Mathf.Lerp(minVolume, maxVolume, t);
    }
}
