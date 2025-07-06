using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerConroller : MonoBehaviour
{
    private static readonly int IsVisible = Animator.StringToHash("isVisible");

    public enum GameMode
    {
        Up, 
        Down
    }
    [SerializeField]
    private GrabbingDog _grabbingDog;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private DogController _dogController;
    [SerializeField]
    private GameMode _gameMode = GameMode.Up;
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

    private void Awake()
    {
        ChangeMode(GameMode.Down);
    }

    IEnumerator ChangeGameModeDown()
    {
        yield return new WaitForSeconds(Random.Range(15, 30f));
        while (_dogController.Hunger < 0.15f)
        {
            yield return null;
        }
        _dogController.Disable();
        yield return new WaitForSeconds(0.15f);
        _animator.SetBool(IsVisible, false);
        yield return new WaitForSeconds(2f);
        _grabbingDog.gameObject.SetActive(true);
        _grabbingDog.Initialize();
        _cameraController.MoveDown();
        yield return new WaitForSeconds(2f);
        _gameMode = GameMode.Down;

    }
    
    IEnumerator ChangeGameModeUp()
    {
        _lHand.Reset();
        _rHand.Reset();
        _cameraController.MoveUp();
        yield return new WaitForSeconds(0.7f);
        _dogController.Reenable();
        _animator.SetBool(IsVisible, true);
        yield return new WaitForSeconds(2f);
        _gameMode = GameMode.Up;
        _grabbingDog.gameObject.SetActive(false);
        ChangeMode(GameMode.Down);
    }

    public void ChangeMode(GameMode mode)
    {
        switch (mode)
        {
            case GameMode.Up:
                StartCoroutine(ChangeGameModeUp());
                break;
            case GameMode.Down:
                StartCoroutine(ChangeGameModeDown());
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
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
        switch (_gameMode)
        {
            case GameMode.Up:
                Debug.Log("Value L: " + value);
                _lHand.ReceiveInput(value);
                break;
            case GameMode.Down:
                _lLeg.ReceiveInput(value);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }    
    
    public void OnRightHandMove(InputValue inputValue)
    {
        Vector2 value = inputValue.Get<Vector2>();
        switch (_gameMode)
        {
            case GameMode.Up:

                Debug.Log("Value R: " + value);
                _rHand.ReceiveInput(value);
                break;
            case GameMode.Down:
                _rLeg.ReceiveInput(value);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

    }    
    
    public void OnLeftGrab(InputValue inputValue)
    {
        switch (_gameMode)
        {
            case GameMode.Up:
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
            case GameMode.Down:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

    }    
    
    public void OnRightGrab(InputValue inputValue)
    {
        switch (_gameMode)
        {
            case GameMode.Up:
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
            case GameMode.Down:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

    }
}
