using UnityEngine;

public class CodeLineController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField] 
    private Transform _pivot;
    [SerializeField]
    private Color _inProgressColor = Color.red;
    [SerializeField]
    private Color _completedColor = Color.green;

    private float _intendedLength;
    private float _progressAllocated;
    private float _currentProgress;
    private float _currentLength;

    public void Initialize(float intendedLength, float progressAllocated)
    {
        _intendedLength = intendedLength;
        _progressAllocated = progressAllocated;
        _currentProgress = 0;
        _spriteRenderer.color = _inProgressColor;
        _pivot.localScale = new Vector3(0, 1, 1);
    }

    public bool TryComplete(float progress)
    {
        _currentProgress += progress;
        _pivot.localScale = Vector3.Lerp(new Vector3(0,1,1), new Vector3(_intendedLength, 1,1), _currentProgress/_progressAllocated);
        bool success = _currentProgress >= _progressAllocated;
        if (success)
        {
            _spriteRenderer.color = _completedColor;
        }
        return success;
    }
}
