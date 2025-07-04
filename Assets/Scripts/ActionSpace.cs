using System;
using UnityEngine;
using UnityEngine.Events;

public class ActionSpace : MonoBehaviour
{
    [SerializeField] 
    private UnityEvent<HandController> OnGrab;
    [SerializeField] 
    private float _radius;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _radius);
        Gizmos.color = new Color(0, 0, 1, 0.25f);
        Gizmos.DrawSphere(transform.position, _radius);
    }

    public bool IsWithinBounds(Vector2 position)
    {
        return (Vector2.Distance(transform.position, position) < _radius);
    }

    public void Grab(HandController controller)
    {
        Debug.Log("Use action space");
        OnGrab.Invoke(controller);
    }
}
