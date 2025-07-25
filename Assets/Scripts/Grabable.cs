using System.Collections;
using UnityEngine;

public class Grabable : MonoBehaviour
{
    [SerializeField] 
    private bool _isLove;
    [SerializeField] 
    private bool _edibleByDog = true;
    [SerializeField]
    private Bounds _bounds;
    [SerializeField]
    private Rigidbody2D _rigidbody;
    
    public bool EdibleByDog => _edibleByDog;
    public bool IsLove => _isLove;


    public void Initialize(Bounds bounds)
    {
        _bounds = bounds;
    }
    
    public void DropAndDisable()
    {
        
        StartCoroutine(DropCoroutine());
    }

    IEnumerator DropCoroutine()
    {
        Vector2 velocity = Vector2.zero;

        while (_bounds.Contains(transform.position))
        {
            velocity -= Vector2.up * Time.deltaTime;
            transform.position += new Vector3(velocity.x, velocity.y, 0);
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
