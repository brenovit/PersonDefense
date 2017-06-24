using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI icon in towers building tree.
/// </summary>
public class BuildingIcon : MonoBehaviour
{
    // Tower prefab for this icon
    public GameObject towerPrefab;

    // Text field for tower price
    private Text price;
    // Parent building tree
    private BuildingTree myTree;

    /// <summary>
    /// Raises the enable event.
    /// </summary>
    void OnEnable()
    {
        EventManager.StartListening("UserUiClick", UserUiClick);
    }

    /// <summary>
    /// Raises the disable event.
    /// </summary>
    void OnDisable()
    {
		EventManager.StopListening("UserUiClick", UserUiClick);
    }

    /// <summary>
    /// Awake this instance.
    /// </summary>
    void Awake()
    {
        // Get building tree from parent object
        myTree = transform.GetComponentInParent<BuildingTree>();
        price = GetComponentInChildren<Text>();
        Debug.Assert(price && myTree, "Wrong initial parameters");
        if (towerPrefab == null)
        {
            // If this icon have no tower prefab - hide icon
            gameObject.SetActive(false);
        }
        else
        {
            // Display tower price
            price.text = towerPrefab.GetComponent<Price>().price.ToString();
        }
    }

    /// <summary>
    /// On user UI click.
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <param name="param">Parameter.</param>
	private void UserUiClick(GameObject obj, string param)
    {
        // If clicked on this icon
        if (obj == gameObject)
        {
            // Build the tower
            myTree.Build(towerPrefab);
        }
    }
}
