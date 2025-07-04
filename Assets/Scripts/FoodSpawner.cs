using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField]
    private Grabable[] _grabablePrefabs;
    
    public void SpawnFoodInHand(HandController handController)
    {
        Grabable foodItem = Instantiate(_grabablePrefabs[Random.Range(0, _grabablePrefabs.Length)]);
        handController.Grab(foodItem);
    }
}
