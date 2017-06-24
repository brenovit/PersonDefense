using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tower building and operation.
/// </summary>
public class Tower : MonoBehaviour
{
    // Prefab for building tree
    public GameObject buildingTreePrefab;
    // Visualisation of attack range for this tower
    public GameObject rangeImage;

    // User interface manager
    private UiManager uiManager;
    // Level UI canvas for building tree display
    private Canvas canvas;
    // Collider of this tower
    private Collider2D bodyCollider;
    // Displayed building tree
    private BuildingTree activeBuildingTree;

    /// <summary>
    /// Raises the enable event.
    /// </summary>
    void OnEnable()
    {
        EventManager.StartListening("GamePaused", GamePaused);
        EventManager.StartListening("UserClick", UserClick);
		EventManager.StartListening("UserUiClick", UserClick);
    }

    /// <summary>
    /// Raises the disable event.
    /// </summary>
    void OnDisable()
    {
        EventManager.StopListening("GamePaused", GamePaused);
        EventManager.StopListening("UserClick", UserClick);
		EventManager.StopListening("UserUiClick", UserClick);
    }

    /// <summary>
    /// Atart this instance.
    /// </summary>
    void Start()
    {
        uiManager = FindObjectOfType<UiManager>();
		// This canvas wiil use to place building tree UI
        Canvas[] canvases = Resources.FindObjectsOfTypeAll<Canvas>();
        foreach (Canvas canv in canvases)
        {
            if (canv.CompareTag("LevelUI"))
            {
                canvas = canv;
                break;
            }
        }
        bodyCollider = GetComponent<Collider2D>();
        Debug.Assert(uiManager && canvas && bodyCollider, "Wrong initial parameters");
    }

    /// <summary>
    /// Opens the building tree.
    /// </summary>
    private void OpenBuildingTree()
    {
        if (buildingTreePrefab != null)
        {
            // Create building tree
            activeBuildingTree = Instantiate<GameObject>(buildingTreePrefab, canvas.transform).GetComponent<BuildingTree>();
            // Set it over the tower
            activeBuildingTree.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            activeBuildingTree.myTower = this;
            // Disable tower raycast
            bodyCollider.enabled = false;
        }
    }

    /// <summary>
    /// Closes the building tree.
    /// </summary>
    private void CloseBuildingTree()
    {
        if (activeBuildingTree != null)
        {
            Destroy(activeBuildingTree.gameObject);
            // Enable tower raycast
            bodyCollider.enabled = true;
        }
    }

    /// <summary>
    /// Builds the tower.
    /// </summary>
    /// <param name="towerPrefab">Tower prefab.</param>
    public void BuildTower(GameObject towerPrefab)
    {
        // Close active building tree
        CloseBuildingTree();
        Price price = towerPrefab.GetComponent<Price>();
        // If anough gold
        if (uiManager.SpendGold(price.price) == true)
        {
            // Create new tower and place it on same position
            GameObject newTower = Instantiate<GameObject>(towerPrefab, transform.parent);
            newTower.transform.position = transform.position;
            newTower.transform.rotation = transform.rotation;
            // Destroy old tower
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Disable tower raycast and close building tree on game pause.
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <param name="param">Parameter.</param>
    private void GamePaused(GameObject obj, string param)
    {
        if (param == bool.TrueString) // Paused
        {
            CloseBuildingTree();
            bodyCollider.enabled = false;
        }
        else // Unpaused
        {
            bodyCollider.enabled = true;
        }
    }

    /// <summary>
    /// On user click.
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <param name="param">Parameter.</param>
    private void UserClick(GameObject obj, string param)
    {
        if (obj == gameObject) // This tower is clicked
        {
            // Show attack range
            ShowRange(true);
            if (activeBuildingTree == null)
            {
                // Open building tree if it is not
                OpenBuildingTree();
            }
        }
        else // Other click
        {
            // Hide attack range
            ShowRange(false);
            // Close active building tree
            CloseBuildingTree();
        }
    }

    /// <summary>
    /// Display tower's attack range.
    /// </summary>
    /// <param name="condition">If set to <c>true</c> condition.</param>
    private void ShowRange(bool condition)
    {
        if (rangeImage != null)
        {
            rangeImage.SetActive(condition);
        }
    }
}
