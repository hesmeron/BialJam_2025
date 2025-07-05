using System;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum GameEndScenario
{
    Success,
    Disabled,
    TimeOut,
    DogOverfed,
    DogStarved,
    DogPoisoned
}
public class GameEndManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _mockPanel;
    [SerializeField] 
    private TMP_Text _mockText;

    private void Awake()
    {
        _mockPanel.SetActive(false);
    }

    public void EndGame(GameEndScenario scenario)
    {
        _mockPanel.SetActive(true);
        _mockText.text = scenario.ToString();
        switch (scenario)
        {
            case GameEndScenario.Success:
                _mockText.text = "You win!";
                break;
            case GameEndScenario.Disabled:
                
                break;
            case GameEndScenario.TimeOut:
                break;
            case GameEndScenario.DogOverfed:
                break;
            case GameEndScenario.DogStarved:
                break;
            case GameEndScenario.DogPoisoned:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(scenario), scenario, null);
        }
    }
}
