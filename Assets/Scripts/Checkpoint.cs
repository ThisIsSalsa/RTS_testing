using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private float gizmoRadius = 0.5f; // Radius of the gizmo sphere

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, gizmoRadius);
    }
}
