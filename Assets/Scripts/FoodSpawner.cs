using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField]
    private Bounds _bounds;
    [SerializeField]
    private Grabable[] _grabablePrefabs;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_bounds.center, _bounds.size);
    }

    public void SpawnFoodInHand(HandController handController)
    {
        Grabable foodItem = Instantiate(_grabablePrefabs[Random.Range(0, _grabablePrefabs.Length)]);
        foodItem.Initialize(_bounds);
        handController.Grab(foodItem);
    }
}
