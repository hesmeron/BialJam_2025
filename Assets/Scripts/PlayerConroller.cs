using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConroller : MonoBehaviour
{
    [SerializeField]
    private HandController _lHand;
    [SerializeField]
    private HandController _rHand;

    private bool _isLeftGrabbed = false;
    private bool _isRightGrabbed = false;
    
    public void OnLeftHandMove(InputValue inputValue)
    {
        Vector2 value = inputValue.Get<Vector2>();
        Debug.Log("Value L: " + value);
        _lHand.ReceiveInput(value);
    }    
    
    public void OnRightHandMove(InputValue inputValue)
    {
        Vector2 value = inputValue.Get<Vector2>();
       Debug.Log("Value R: " + value);
        _rHand.ReceiveInput(value);
    }    
    
    public void OnLeftGrab(InputValue inputValue)
    {
        float value = inputValue.Get<float>();
        //Debug.Log("Left grab " + value);
        if (value > 0.5f && !_isLeftGrabbed)
        {
            _isLeftGrabbed = true;
            _lHand.GrabEvent();
        }
        else if (value < Single.Epsilon && _isLeftGrabbed)
        {
            _isLeftGrabbed = false;
            _lHand.LetGoEvent();
        }
    }    
    
    public void OnRightGrab(InputValue inputValue)
    {
        float value = inputValue.Get<float>();
        if (value > 0.5f && !_isRightGrabbed)
        {
            _isRightGrabbed = true;
            _rHand.GrabEvent();
        }
        else if (value < Single.Epsilon && _isRightGrabbed)
        {
            _isRightGrabbed = false;
            _rHand.LetGoEvent();
        }
    }
}
