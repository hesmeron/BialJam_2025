using System;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField]
    private ActionManager _actionManager;
    [SerializeField] 
    private PlayAreaBounds _bounds;
    [SerializeField] 
    private float _speed = 1f;
    [SerializeField]
    private Vector2 _target;

    private Grabable grabbedItem;
    private bool _isGrabbing;
    private Vector3 _velocity;

    void Awake()
    {
        _target = transform.position;
    }

    private void Update()
    {
        Vector2 translation = _velocity.normalized * (Time.deltaTime * _velocity.magnitude * _speed);
        Vector2 newTarget = _bounds.ClampPoint(_target + translation);
        _target = newTarget;
        transform.position = Vector2.Lerp(transform.position, _target, _speed * Time.deltaTime);
    }

    public void ReceiveInput(Vector2 inputValue)
    {
        _velocity = inputValue;
    }

    public void GrabEvent()
    {
        Debug.Log("Grab ");
        if (_actionManager.GetActionAtPoint(transform.position, out ActionSpace actionspace))
        {
            actionspace.Grab(this);
        }
    }    
    
    public void LetGoEvent()
    {
        Debug.Log("Let go");
    }

    public void Grab(Grabable grabable)
    {
        grabable.transform.SetParent(transform);
        grabable.transform.localPosition = Vector3.zero;
    }
}
