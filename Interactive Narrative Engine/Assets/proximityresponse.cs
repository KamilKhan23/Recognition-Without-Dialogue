using UnityEngine;

public class proximityresponse : MonoBehaviour
{
    public Transform player;

    public float noticeDistance = 3.0f;   // when it becomes aware
    public float boundaryDistance = 1.6f; // personal space
    public float retreatAmount = 0.25f;

    Vector3 startPos;
    bool boundarySet = false;

    void Start()
    {
        startPos = transform.position;
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        float d = Vector3.Distance(transform.position, player.position);

        // Player noticed, but no movement yet
        if (d < noticeDistance && d > boundaryDistance)
        {
            boundarySet = false;
            // do nothing on purpose — this pause is important
            return;
        }

        // Player crosses boundary
        if (d <= boundaryDistance && !boundarySet)
        {
            Vector3 dir = (transform.position - player.position).normalized;
            startPos = transform.position + dir * retreatAmount;
            boundarySet = true;
        }

        // Smoothly move to boundary position
        if (boundarySet)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                startPos,
                Time.deltaTime * 1.5f
            );
        }
    }
}
