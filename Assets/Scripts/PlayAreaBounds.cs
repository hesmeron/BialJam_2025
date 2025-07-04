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
    
    public Vector2 ClampPoint(Vector2 value)
    {
        float x = Mathf.Clamp(value.x, -_extents.x /2, _extents.x/2);
        float y = Mathf.Clamp(value.y, -_extents.y/2, _extents.y/2);
        return new Vector2(x, y);
    }
}
