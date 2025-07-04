using UnityEngine;

public class DogController : MonoBehaviour
{
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
        _fatness += Time.deltaTime /_timeToLoseAllFat;
        _leftEye.SetHunger(hunger);
        _rightEye.SetHunger(hunger);
        if (hunger > 1)
        {
            Debug.Log("Lose game");
        }
        hunger = Mathf.Clamp(hunger, 0, 1);
        _fatness = Mathf.Clamp(_fatness, 0, 1);
    }

    public void Feed()
    {
        hunger = 0;
        _fatness += 0.1f;
        if (_fatness >= 1)
        {
            Debug.Log("Lose game FAT");
        }
    }

}
