using UnityEngine;

public class charachter_behavior : MonoBehaviour
{
    public Transform player;
    public Transform focusPoint;
    public lookatplayer lookScript;

    public float presenceDistance = 2.2f;

    public float initialWait = 2.5f;
    public float minWait = 0.6f;
    public float waitDecay = 0.4f;

    public float focusLowerAmount = 0.15f;

    float timer = 0f;
    float currentWait;
    bool acknowledged = false;

    Vector3 focusStartPos;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (lookScript == null)
            lookScript = GetComponentInChildren<lookatplayer>();

        if (focusPoint != null)
            focusStartPos = focusPoint.localPosition;

        // Load memory
        currentWait = PlayerPrefs.GetFloat("FigureWait", initialWait);
    }

    void Update()
    {
        if (player == null || focusPoint == null) return;

        float d = Vector3.Distance(transform.position, player.position);

        if (d < presenceDistance)
        {
            timer += Time.deltaTime;

            if (timer >= currentWait && !acknowledged)
            {
                acknowledged = true;

                // Learning
                currentWait = Mathf.Max(minWait, currentWait - waitDecay);
                PlayerPrefs.SetFloat("FigureWait", currentWait);

                // Stop gaze at full trust
                if (currentWait <= minWait + 0.05f && lookScript != null)
                    lookScript.allowLooking = false;
            }
        }
        else
        {
            timer = 0f;
            acknowledged = false;
        }

        Vector3 targetPos = acknowledged
            ? focusStartPos + Vector3.down * focusLowerAmount
            : focusStartPos;

        focusPoint.localPosition = Vector3.Lerp(
            focusPoint.localPosition,
            targetPos,
            Time.deltaTime * 2f
        );
    }
}
