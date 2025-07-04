using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    public void ReceiveInput(Vector2 inputValue)
    {
        Vector2 translation = inputValue.normalized * (Time.deltaTime * inputValue.magnitude * _speed);
        transform.position += new Vector3(translation.x, translation.y, 0);
    }
}
