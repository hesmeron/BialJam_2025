using UnityEngine;

public class GrabbingDog : MonoBehaviour
{
    [SerializeField]
    private PlayerConroller _player;
    [SerializeField]
    private DogController _dogControler;
    [SerializeField] 
    private Transform _target;
    [SerializeField]
    private GameEndManager _gameEndManager;
    [SerializeField]
    private float _timeToPassOut =  15f;
    [SerializeField] 
    private float velocityApplied = 0f;
    [SerializeField] 
    private float _velocityLetLooseThreshold = 25f;
    [SerializeField]
    private float _timePassed = 0f;
    [SerializeField]
    private bool _thresholdSurpassed = false;
    [SerializeField]
    public float _fluFailureThreshold = 50f;

    private bool _isGrabbing = true;
    private bool _isFirstFrame = true;
    private Vector2 _translation;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        Debug.Log("Initialize");
        _isGrabbing = true;
        _isFirstFrame = true;
        _timePassed = 0f;
        transform.position = _target.position;
        transform.up = _target.right;
    }

    public void Update()
    {
        if (_isGrabbing)
        {
            TryLetLoose();
        }
        else
        {
            transform.position += (Vector3) _translation * 1f;
        }

    }

    public void TryLetLoose()
    {
        _timePassed += Time.deltaTime;
        if (_timePassed >= _timeToPassOut)
        {
            _gameEndManager.EndGame(GameEndScenario.Injured);
        }
        _translation= _target.position - transform.position;
        transform.up = _target.right;
        if (_translation.magnitude > 0.05f)
        {
            float distance  = _translation.magnitude / Time.deltaTime;
            velocityApplied = distance;
            _thresholdSurpassed = velocityApplied > _velocityLetLooseThreshold;
            Debug.Log("Let loose " + velocityApplied);
            if (_thresholdSurpassed)
            {
                if (_isFirstFrame)
                {
                    _isFirstFrame = false;
                }
                else
                {
                    _isGrabbing = false;
                    Debug.Log("Actually loose " + velocityApplied);
                    if (velocityApplied > _fluFailureThreshold)
                    {
                        if (_dogControler.Fattness > 0.65)
                        {
                            _gameEndManager.EndGame(GameEndScenario.DemolitionBall);
                        }
                        else
                        {
                            _gameEndManager.EndGame(GameEndScenario.SpaceDog);
                        }
                    }
                    else
                    {
                        _player.ChangeMode(PlayerConroller.GameMode.Up);
                    }
                }

            }
            else
            {
                _isFirstFrame = true;
            }
            transform.position = _target.position;
        }

    }
}
