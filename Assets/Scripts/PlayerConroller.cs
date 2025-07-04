using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConroller : MonoBehaviour
{
    [SerializeField] private HandController _lHand;
    [SerializeField] private HandController _rHand;
    
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
}
