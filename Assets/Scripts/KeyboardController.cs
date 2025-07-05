using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    [SerializeField]
    private GameCreatingController _gameCreatingController;
    
    public void HitTheKeyboard(HandController handController)
    {
        _gameCreatingController.Progress();
    }
}
