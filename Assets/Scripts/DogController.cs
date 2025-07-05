using System;
using UnityEngine;

public class DogController : MonoBehaviour
{
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

    void Update()
    {
        hunger += Time.deltaTime / _timeToStarvation;
        if (_fatness > Single.Epsilon)
        {
            _fatness -= Time.deltaTime /_timeToLoseAllFat;
            _fatness = Mathf.Clamp(_fatness, 0, 1);
        }

        _leftEye.SetHunger(hunger);
        _rightEye.SetHunger(hunger);
        if (hunger > 1)
        {
            _gameEndManager.EndGame(GameEndScenario.DogStarved);
        }
        hunger = Mathf.Clamp(hunger, 0, 1);
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
        if (_konradController.IsKonradLooking)
        {
            handController.DisableHand();
        }
    }

}
