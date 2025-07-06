using System;
using UnityEngine;

public class DogController : MonoBehaviour
{
    private static readonly int HungerID = Animator.StringToHash("hunger");
    private static readonly int IsVisible = Animator.StringToHash("isVisible");
    private static readonly int FattnessId = Animator.StringToHash("Fattness");
    
    [SerializeField]
    private Animator _animator;
    [SerializeField] 
    private GameEndManager _gameEndManager;
    [SerializeField]
    private KonradController _konradController;
    [SerializeField]
    private EyeController _leftEye;   
    [SerializeField]
    private EyeController _rightEye;
    [SerializeField] 
    [Range(0, 1f)]
    private float hunger;
    [SerializeField] 
    [Range(0, 1f)] 
    private float _fatness = 0;
    [SerializeField]
    public float _timeToStarvation = 15f;
    [SerializeField] 
    public float _timeToLoseAllFat = 5f;

    public float Fattness => _fatness;
    public float Hunger => hunger;


    void Update()
    {
        hunger += Time.deltaTime / _timeToStarvation;
        if (_fatness > Single.Epsilon)
        {
            _fatness -= Time.deltaTime /_timeToLoseAllFat;
            _fatness = Mathf.Clamp(_fatness, 0, 1);
        }

        _animator.SetFloat(FattnessId, _fatness);
        _leftEye.SetHunger(hunger);
        _rightEye.SetHunger(hunger);
        if (hunger > 1)
        {
            _gameEndManager.EndGame(GameEndScenario.DogStarved);
        }
        hunger = Mathf.Clamp(hunger, 0, 1);
        _animator.SetFloat(HungerID, hunger);
    }

    public void Disable()
    {
        _animator.SetBool(IsVisible, false);
        hunger = 0;
        this.enabled = false;
    }
    public void Reenable()
    {
        _animator.SetBool(IsVisible, true);
        this.enabled = true;
    }
    
    public void Feed(Grabable grabable, HandController handController)
    {
        if (!grabable.EdibleByDog)
        {
            _gameEndManager.EndGame(GameEndScenario.DogPoisoned);
        }
        Debug.Log("Consume food");
        Destroy(grabable.gameObject);
        
        hunger = 0;
        _fatness += 0.3f;
        if (_fatness >= 1)
        {
            _gameEndManager.EndGame(GameEndScenario.DogOverfed);
        }
        
        _konradController.KondzioCheck(handController);
    }

}
