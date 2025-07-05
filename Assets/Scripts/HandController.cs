using System;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    private static readonly int IsGrabbing = Animator.StringToHash("isGrabbing");
    private static readonly int IsDisabledID = Animator.StringToHash("isDisabled");

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

    private Grabable _grabbedItem;
    private bool _isGrabbing;
    private Vector3 _velocity;
    private List<ActionSpace> _enteredActionSpaces;
    private bool _isDisabled = false;
    
    public bool IsDisabled => _isDisabled;

    void Start()
    {
        _target = transform.position;
        _enteredActionSpaces = _actionManager.GetAllActionsAtPoint(transform.position);
    }

    private void Update()
    {
        Vector2 translation = _velocity.normalized * (Time.deltaTime * _velocity.magnitude * _speed);
        Vector2 newTarget = _bounds.ClampPoint(_target + translation);
        _target = newTarget;
        transform.position = Vector2.Lerp(transform.position, _target, _speed * Time.deltaTime);

        if (!_isDisabled)
        {
            HandleEnterEvents();
        }
    }

    private void HandleEnterEvents()
    {
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
        if (!_isDisabled)
        {
            Debug.Log("Grab ");
            _animator.SetBool(IsGrabbing, true);
            if (_actionManager.GetActionAtPoint(transform.position, out ActionSpace actionspace))
            {
                actionspace.Grab(this);
            }
        }
    }    
    
    public void LetGoEvent()
    {
        if (!_isDisabled)
        {
            Debug.Log("Let go");
            _animator.SetBool(IsGrabbing, false);
            if (_grabbedItem != null)
            {
                _grabbedItem.transform.SetParent(null);
                if (_actionManager.GetActionAtPoint(transform.position, out ActionSpace actionspace))
                {
                    actionspace.Drop(_grabbedItem, this);
                }
                else
                {
                    _grabbedItem.DropAndDisable();
                }
                _grabbedItem = null;
            }
        }
    }

    public void Grab(Grabable grabable)
    {
        if (!_isDisabled)
        {
            grabable.transform.SetParent(transform);
            grabable.transform.localPosition = Vector3.zero;
            _grabbedItem = grabable;
        }
    }

    public void DisableHand()
    {
        _isDisabled = true;
        _animator.SetBool(IsDisabledID, true);
    }
}
