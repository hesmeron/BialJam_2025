using System;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    private static readonly int IsGrabbing = Animator.StringToHash("isGrabbing");
    private static readonly int IsDisabledID = Animator.StringToHash("isDisabled");

    [SerializeField] 
    private Transform _pivot;
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
        _target = _pivot.transform.position;
        _enteredActionSpaces = _actionManager.GetAllActionsAtPoint(transform.position);
    }

    private void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        _pivot.localPosition = Vector3.zero;
        _animator.SetBool(IsDisabledID, _isDisabled);
        Freeze();
    }

    public void Freeze()
    {
        _target = _pivot.transform.position;
        _velocity = Vector3.zero;
    }


    public void UpdateHand()
    {
        Vector2 translation = _velocity.normalized * (Time.deltaTime * _velocity.magnitude * _speed);
        Vector2 newTarget = _bounds.ClampPoint(_target + translation);
        _target = newTarget;
        SetPosition(Vector2.Lerp(_pivot.position, _target, _speed * Time.deltaTime));

        if (!_isDisabled)
        {
            HandleEnterEvents();
        }
    }

    private void SetPosition(Vector2 position)
    {
        _pivot.position = position;
        //_pivot.transform.up = _pivot.localPosition.normalized;
    }

    private void HandleEnterEvents()
    {
        var results = _actionManager.GetAllActionsAtPoint(_pivot.position);

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
            if (_actionManager.GetActionAtPoint(_pivot.position, out ActionSpace actionspace))
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
                if (_actionManager.GetActionAtPoint(_pivot.position, out ActionSpace actionspace))
                {
                    actionspace.Drop(_grabbedItem, this);
                }

                if (_grabbedItem != null)
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
            grabable.transform.SetParent(_pivot);
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
