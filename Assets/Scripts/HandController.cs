using System;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] 
    private PlayAreaBounds _bounds;
    [SerializeField] 
    private float _speed = 1f;
    [SerializeField]
    private Vector2 _target;

    void Awake()
    {
        _target = transform.position;
    }

    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, _target, _speed * Time.deltaTime);
    }

    public void ReceiveInput(Vector2 inputValue)
    {
        Vector2 translation = inputValue.normalized * (Time.deltaTime * inputValue.magnitude * _speed);
        Vector2 newTarget = _bounds.ClampPoint(_target + translation);
        _target = newTarget;
    }

    public void GrabEvent()
    {
        Debug.Log("Grab ");
    }    
    
    public void LetGoEvent()
    {
        Debug.Log("Let go");
    }
}
