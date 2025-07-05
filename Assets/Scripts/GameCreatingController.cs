using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameCreatingController : MonoBehaviour
{
    [SerializeField]
    private GameEndManager _gameEndManager;
    [SerializeField]
    private Transform _spawnPivot;
    [SerializeField] 
    private List<Color> colors;
    [SerializeField]
    private CodeLineController _codeLinePrefab;
    [SerializeField] 
    private float _startSpawnHeight = 0f;    
    [SerializeField] 
    private float _despawnHeight = 0f;
    [SerializeField]
    private float _scrollThreshold = 0.5f;
    [SerializeField] 
    private float _lineSpacing  = 0.25f;
    
    private List<CodeLineController> _codeLinesWritten = new List<CodeLineController>();
    [SerializeField]
    private float _progressNeeded = 1000;
    [SerializeField]
    private float _progress = 0;
    private float _spawnHeight = 0f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.matrix = _spawnPivot.localToWorldMatrix;
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(new Vector3(0, _startSpawnHeight, 0), new Vector3(10, _lineSpacing, 10));        
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(0, _despawnHeight, 0), new Vector3(10, 0.1f, 10));
    }

    private void Awake()
    {
        _spawnHeight = _startSpawnHeight;
    }

    private void Update()
    {
        if (_startSpawnHeight - _spawnHeight > _scrollThreshold)
        {
            float newSpawnHeight = Mathf.Lerp(_spawnHeight, _startSpawnHeight, Time.deltaTime);
            float delta = newSpawnHeight - _spawnHeight;
            List<CodeLineController> codeLinesToRemove = new List<CodeLineController>();
            
            foreach (CodeLineController codeLine in _codeLinesWritten)
            {
                codeLine.transform.localPosition += Vector3.up * delta;

                if (codeLine.transform.localPosition.y > _despawnHeight)
                {
                    codeLinesToRemove.Add(codeLine);
                }
            }
            
            foreach (CodeLineController codeLineController in codeLinesToRemove)
            {
                    
                _codeLinesWritten.Remove(codeLineController);
                Destroy(codeLineController.gameObject);
            }
            _spawnHeight = newSpawnHeight;
        }
    }

    private void CreateLineOfCode()
    {
        CodeLineController codeLineController = Instantiate(_codeLinePrefab, _spawnPivot);
        codeLineController.transform.localPosition = new Vector3(0,_spawnHeight, 0);
        float intendedLength = Random.Range(0.12f, 0.75f);
        float allocatedProgress = intendedLength * 5f;
        codeLineController.Initialize(intendedLength, allocatedProgress);
        _codeLinesWritten.Add(codeLineController);
        _spawnHeight -= _lineSpacing;
    }

    public void Progress()
    {
        Debug.Log("Progress");
        
        _progress++;
        if (_progress >= _progressNeeded)
        {
            _gameEndManager.EndGame(GameEndScenario.Success);
        }

        if (_codeLinesWritten.Count == 0)
        {
            CreateLineOfCode();
        }

        if (_codeLinesWritten[_codeLinesWritten.Count-1].TryComplete(1f))
        {
            CreateLineOfCode();
        }
        
    }
}
