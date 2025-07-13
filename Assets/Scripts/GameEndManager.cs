using System;
using System.Collections;
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
    DogPoisoned,
    DemolitionBall,
    SpaceDog,
    Injured,
    Love
}
public class GameEndManager : MonoBehaviour
{
    private static readonly int Ended = Animator.StringToHash("Ended");

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private PlayerConroller _player;
    [SerializeField] 
    private GameObject _mockPanel;
    [SerializeField] 
    private TMP_Text _mockText;

    public bool GameEnded = false;

    private void Awake()
    {
        _mockPanel.SetActive(false);
    }

    public void EndGame(GameEndScenario scenario)
    {
        StartCoroutine(EndScenario(scenario));
    }

    IEnumerator EndScenario(GameEndScenario scenario)
    {
        yield return new WaitForSeconds(0.7f);
        _mockPanel.SetActive(true);
        _mockText.text = scenario.ToString();
        Debug.Log("Scenario" + scenario);
        _animator.SetTrigger(scenario.ToString());
        _animator.SetBool(Ended, true);
        StartCoroutine(Coroutine());
        /*
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
        }
        */
    }

    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(3f);
        GameEnded = true;
    }
}
