using System;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;

public class PlayerConroller : MonoBehaviour
{
    private enum GameMod
    {
        Up, 
        Down
    }
    [SerializeField]
    private GameMod _gameMod = GameMod.Up;
    [SerializeField]
    private CameraController _cameraController;
    [SerializeField]
    private GameEndManager _gameEndManager;
    [SerializeField]
    private HandController _lHand;
    [SerializeField]
    private HandController _rHand;    
    [SerializeField]
    private LegController _lLeg;
    [SerializeField]
    private LegController _rLeg;

    private bool _isLeftGrabbed = false;
    private bool _isRightGrabbed = false;

    private void OnDrawGizmosSelected()
    {
        //Gizmos
    }

    void Update()
    {
        if (_lHand.IsDisabled && _rHand.IsDisabled)
        {
            _gameEndManager.EndGame(GameEndScenario.Disabled);
        }
    }
    
    public void OnLeftHandMove(InputValue inputValue)
    {
        Vector2 value = inputValue.Get<Vector2>();
        switch (_gameMod)
        {
            case GameMod.Up:
                Debug.Log("Value L: " + value);
                _lHand.ReceiveInput(value);
                break;
            case GameMod.Down:
                _lLeg.ReceiveInput(value);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }    
    
    public void OnRightHandMove(InputValue inputValue)
    {
        Vector2 value = inputValue.Get<Vector2>();
        switch (_gameMod)
        {
            case GameMod.Up:

                Debug.Log("Value R: " + value);
                _rHand.ReceiveInput(value);
                break;
            case GameMod.Down:
                _rLeg.ReceiveInput(value);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

    }    
    
    public void OnLeftGrab(InputValue inputValue)
    {
        switch (_gameMod)
        {
            case GameMod.Up:
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
                break;
            case GameMod.Down:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

    }    
    
    public void OnRightGrab(InputValue inputValue)
    {
        switch (_gameMod)
        {
            case GameMod.Up:
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
                break;
            case GameMod.Down:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

    }
}
