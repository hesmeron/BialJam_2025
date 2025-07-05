using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class KonradController : MonoBehaviour
{
    [SerializeField] 
    private float _startingX;    
    [SerializeField] 
    private float _lookingThresholdXA = 0f;
    [SerializeField]
    private float _lookingThresholdXB = 0f;
    [SerializeField]
    private float _endingX;
    [SerializeField] 
    private Transform _characterPivot;
    [SerializeField]
    private float _minWaitTime = 7f;
    [SerializeField]
    private float _maxWaitTime = 7f;

    private float _currentX;
    [SerializeField]
    private bool _isKonradLooking = false;

    public bool IsKonradLooking => _isKonradLooking;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector3(_startingX, 0, 0), new Vector3(0.1f, 100, 0f));
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(_endingX, 0, 0), new Vector3(0.1f, 100, 0f));        
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(new Vector3(_lookingThresholdXA, 0, 0), new Vector3(0.1f, 100, 0f));
        Gizmos.DrawCube(new Vector3(_lookingThresholdXB, 0, 0), new Vector3(0.1f, 100, 0f));
    }

    void Start()
    {
        StartCoroutine(KonradLookingCoroutine());
    }

    IEnumerator KonradLookingCoroutine()
    {
        bool gameIsRunning = true;
        while (gameIsRunning)
        {
            yield return new WaitForSeconds(Random.Range(_minWaitTime, _maxWaitTime));
            SetCharacterX(_startingX);
            while (Mathf.Abs(_currentX - _endingX) > 0.1f)
            {
                SetCharacterX(Mathf.Lerp(_currentX, _endingX, Time.deltaTime / 4f));
                Debug.Log("Move konrad");
                //_isKonradLooking = (Mathf.Abs(_currentX - _endingX) < Mathf.Abs(_currentX - _lookingThresholdX));
                _isKonradLooking = ((_currentX - _lookingThresholdXA) *  (_currentX - _lookingThresholdXB)) < 0;
                yield return null;
            }
            
        }

        

    }

    private void SetCharacterX(float value)
    {
        _currentX = value;
        Vector3 currentPos = _characterPivot.position;
        _characterPivot.transform.position =new Vector3(value, currentPos.y, currentPos.z);
    }
}
