using System;
using UnityEngine;
using UnityEngine.Events;

public class ActionSpace : MonoBehaviour
{
    enum BoundsType
    {
        Sphere, 
        Box2D
    }
    [SerializeField]
    private BoundsType _mode = BoundsType.Sphere;
    [SerializeField] 
    private Bounds _bounds;
    [SerializeField] 
    private UnityEvent<HandController> OnGrab;    
    [SerializeField] 
    private UnityEvent<HandController> OnEnter;    
    [SerializeField] 
    private UnityEvent<Grabable> OnDrop;
    [SerializeField] 
    private float _radius;

    private void OnDrawGizmos()
    {
        switch (_mode)
        {
            case BoundsType.Sphere:
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(transform.position, _radius);
                Gizmos.color = new Color(0, 0, 1, 0.25f);
                Gizmos.DrawSphere(transform.position, _radius);
                break;
            case BoundsType.Box2D:
                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(_bounds.center + transform.position, _bounds.size);                
                Gizmos.color = new Color(0, 0, 1, 0.25f);
                Gizmos.DrawCube(_bounds.center+ transform.position, _bounds.size);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

    }

    public bool IsWithinBounds(Vector2 position)
    {
        switch (_mode)
        {
            case BoundsType.Sphere:
                return (Vector2.Distance(transform.position, position) < _radius);
            case BoundsType.Box2D:
                return _bounds.Contains(position - (Vector2)transform.position);
            default:
                throw new ArgumentOutOfRangeException();
        }

    }

    public void Grab(HandController controller)
    {
        Debug.Log("Use action space");
        OnGrab.Invoke(controller);
    }    
    public void Drop(Grabable item)
    {
        Debug.Log("Use action space");
        OnDrop.Invoke(item);
    }    
    
    public void Enter(HandController controller)
    {
        Debug.Log("Use action space");
        OnEnter.Invoke(controller);
    }
}
