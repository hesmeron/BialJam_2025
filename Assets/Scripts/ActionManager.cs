using System;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    private ActionSpace[] _actionSpaces = Array.Empty<ActionSpace>();

    private void Awake()
    {
        _actionSpaces = FindObjectsByType<ActionSpace>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    }

    public bool GetActionAtPoint(Vector2 position, out ActionSpace result)
    {
        foreach (ActionSpace actionSpace in _actionSpaces)         
        {
            if (actionSpace.IsWithinBounds(position))
            {
                result = actionSpace;
                return true;
                break;
            }
        }
        result = null;
        return false;
    }
}
