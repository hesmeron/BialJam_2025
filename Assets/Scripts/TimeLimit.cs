using TMPro;
using UnityEngine;

public class TimeLimit : MonoBehaviour
{
    [SerializeField]
    private GameEndManager _gameEndManager;
    [SerializeField]
    private TMP_Text _text;
    [SerializeField] 
    private int finishHour = 13;
    [SerializeField] 
    private float _gameTimeLimitInSeconds = 120;

    private float _currentTimePassed = 0f;

    void Update()
    {
        _currentTimePassed += Time.deltaTime;
        float completion = _currentTimePassed / _gameTimeLimitInSeconds;
        int hours = Mathf.FloorToInt(completion * finishHour);
        int minutes = Mathf.FloorToInt(((completion * finishHour) - hours)*60);
        _text.text = $"{hours}:{minutes.ToString("00")}";

        if (_currentTimePassed >= _gameTimeLimitInSeconds)
        {
            _gameEndManager.EndGame(GameEndScenario.TimeOut);
        }
    }
}
