using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Position for defenders.
/// </summary>
public class DefendPoint : MonoBehaviour
{
	// Prefab for defend point
	public GameObject defendPointPrefab;

	// List with defend places for this defend point
	private List<Transform> defendPlaces = new List<Transform>();

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		Debug.Assert(defendPointPrefab, "Wrong initial settings");
		// Get defend places from defend point prefab and place it on scene
		foreach (Transform defendPlace in defendPointPrefab.transform)
		{
			Instantiate(defendPlace.gameObject, transform);
		}
		// Create defend places list
		foreach (Transform child in transform)
		{
			defendPlaces.Add(child);
		}
	}

    /// <summary>
    /// Gets the defend points list.
    /// </summary>
    /// <returns>The defend points.</returns>
    public List<Transform> GetDefendPoints()
    {
		return defendPlaces;
    }
}
