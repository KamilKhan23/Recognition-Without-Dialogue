using UnityEngine;

public class lookatplayer : MonoBehaviour
{
    public float turnSpeed = 1.5f;
    public bool allowLooking = true;

    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (!allowLooking || player == null) return;

        Vector3 dir = player.position - transform.position;
        dir.y = 0;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            Time.deltaTime * turnSpeed
        );
    }
}
