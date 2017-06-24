using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Position for defenders.
/// </summary>
public class DefendPoint : MonoBehaviour
{
    /// <summary>
    /// Gets the defend points list.
    /// </summary>
    /// <returns>The defend points.</returns>
    public List<Transform> GetDefendPoints()
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in transform)
        {
            children.Add(child);
        }
        return children;
    }
}
