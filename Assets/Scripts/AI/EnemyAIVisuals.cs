using UnityEngine;

public class EnemyAIVisuals : MonoBehaviour
{
    public float detectionRange = 10f;
    public float viewAngle = 45f;
    private Vector3 lastKnownPlayerPosition;

    void OnDrawGizmos()
    {
        DrawLineOfSight();
        DrawFieldOfView();
        DrawLastKnownPlayerPosition();
    }

    // Draw line of sight
    void DrawLineOfSight()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * detectionRange);
    }

    // Draw field of view angle
    void DrawFieldOfView()
    {
        Gizmos.color = Color.green;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-viewAngle * 0.5f, transform.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(viewAngle * 0.5f, transform.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + leftRayDirection * detectionRange);
        Gizmos.DrawLine(transform.position, transform.position + rightRayDirection * detectionRange);
    }

    // Draw line to indicate the player's last known position within LOS
    void DrawLastKnownPlayerPosition()
    {
        if (lastKnownPlayerPosition != Vector3.zero)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, lastKnownPlayerPosition);
        }
    }
}
