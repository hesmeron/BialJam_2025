using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static readonly int IsUp = Animator.StringToHash("isUp");

    [SerializeField]
    private Animator _animator;

    public void MoveUp()
    {
        _animator.SetBool(IsUp, true);
    }   
    
    public void MoveDown()
    {
        _animator.SetBool(IsUp, false);
    }
}
