using System;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    private static readonly int IsGrabbing = Animator.StringToHash("isGrabbing");

    [SerializeField]
    private ActionManager _actionManager;
    [SerializeField] 
    private PlayAreaBounds _bounds;
    [SerializeField] 
    private float _speed = 1f;
    [SerializeField]
    private Vector2 _target;
    [SerializeField]
    private Animator _animator;

    private Grabable grabbedItem;
    private bool _isGrabbing;
    private Vector3 _velocity;
    private List<ActionSpace> _enteredActionSpaces;

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

        var results = _actionManager.GetAllActionsAtPoint(transform.position);

        foreach (var actionSpace in results)
        {
            if (!_enteredActionSpaces.Contains(actionSpace))
            {
                actionSpace.Enter(this);
            }
        }
        
        _enteredActionSpaces = results;
    }

    public void ReceiveInput(Vector2 inputValue)
    {
        _velocity = inputValue;
    }

    public void GrabEvent()
    {
        Debug.Log("Grab ");
        _animator.SetBool(IsGrabbing, true);
        if (_actionManager.GetActionAtPoint(transform.position, out ActionSpace actionspace))
        {
            actionspace.Grab(this);
        }
    }    
    
    public void LetGoEvent()
    {
        Debug.Log("Let go");
        _animator.SetBool(IsGrabbing, false);
    }

    public void Grab(Grabable grabable)
    {
        grabable.transform.SetParent(transform);
        grabable.transform.localPosition = Vector3.zero;
    }

    public bool TryConsumeFood()
    {
        if (grabbedItem != null)
        {
            Destroy(grabbedItem);
            grabbedItem = null;
            return true;
        }

        return false;
    }
}
