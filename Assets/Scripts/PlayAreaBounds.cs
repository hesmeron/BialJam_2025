using UnityEngine;

public class PlayAreaBounds : MonoBehaviour
{
    [SerializeField] private Vector2 _extents;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, _extents);
        Gizmos.color = new Color(0, 1,  0, 0.25f);
        Gizmos.DrawCube(transform.position, _extents);
    }
}
