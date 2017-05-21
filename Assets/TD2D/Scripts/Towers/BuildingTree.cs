using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Building tree.
/// </summary>
public class BuildingTree : MonoBehaviour
{
    /// <summary>
    /// Tower that open this building tree.
    /// </summary>
    [HideInInspector]
    public Tower myTower;

    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start()
    {
        Debug.Assert(myTower, "Wrong initial parameters");
    }

    /// <summary>
    /// Build the tower.
    /// </summary>
    /// <param name="prefab">Prefab.</param>
    public void Build(GameObject prefab)
    {
        myTower.BuildTower(prefab);
    }
}
