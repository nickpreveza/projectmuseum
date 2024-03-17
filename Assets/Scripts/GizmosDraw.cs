using UnityEngine;

public class GizmosDraw : MonoBehaviour
{
    [SerializeField] Color gizmoColor; 
    [SerializeField] float gizmoWidth;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, gizmoWidth);
    }
}
