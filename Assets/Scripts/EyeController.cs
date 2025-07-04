using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EyeController : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField] 
    [Range(0, 1)] 
    private float _hunger = 0f;
    [SerializeField]
    private Vector2 _targetPosition = new Vector2(0, 0);

    public void SetHunger(float hunger)
    {
        _hunger = hunger;
    }
    
    private void Update()
    {
        float size = 1 + _hunger;
        _target.localScale = new Vector3(size, size, size);
        float delta = _hunger * 0.1f;
        if (Vector2.Distance(_targetPosition, _target.localPosition) < delta * 0.05f)
        {
            float x = Random.Range(-1, 1f) * delta;
            float y = Random.Range(-1, 1f) * delta;
            _targetPosition = new Vector2(x, y);
        }
        _target.transform.localPosition = Vector2.Lerp( _target.transform.localPosition, _targetPosition, Mathf.Clamp01(Time.deltaTime * _hunger*70));
    }
}
