using System;
using TMPro;
using UnityEngine;

public class LegController : MonoBehaviour
{
    [SerializeField] 
    private bool _inverse = false;
    [SerializeField]
    float  _maxAngle = 60f;
    [SerializeField] 
    private ActionManager _actionManager;
    [SerializeField]
    private float _speed = 1f;
    [SerializeField] 
    private Transform _pivot;
    [SerializeField]
    private Animator _animator;

    private Grabable _grabbedItem;
    private bool _isGrabbing;
    private float _targetAngle;
    private float _currentAngle = 0f;


    void Update()
    {
        _currentAngle = Mathf.Lerp(_currentAngle, _targetAngle, _speed * Time.deltaTime);
        _pivot.localRotation = Quaternion.Euler(0, 0, _currentAngle);
    }

    public void ReceiveInput(Vector2 inputValue)
    {
        Vector2 direction = inputValue.normalized;
        if (_inverse)
        {
            direction = new Vector2(-direction.x, direction.y);
        }
        if (direction.magnitude <= 0.9f)
        {
            return;
        }
        else
        {
            float angle = Vector2.SignedAngle(Vector2.up, direction.normalized);
            angle = Mathf.Clamp(angle, -_maxAngle, _maxAngle);
            //Debug.Log("Left leg angle " + angle);
            _targetAngle = angle;
        }

    }
}