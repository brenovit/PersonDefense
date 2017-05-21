using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Level control script.
/// </summary>
public class LevelManager : MonoBehaviour
{
	// UI scene. Load on level start
	public string levelUiSceneName;
	// Gold amount for this level
	public int goldAmount = 20;
	// How many times enemies can reach capture point before defeat
	public int defeatCounter = 1;

    // User interface manager
    private UiManager uiManager;
    // Nymbers of enemy spawners in this level
    private int spawnNumbers;
	// Current loose counter
	private int looseCounter;

    /// <summary>
    /// Awake this instance.
    /// </summary>
    void Awake()
    {
		// Load UI scene
		SceneManager.LoadScene(levelUiSceneName, LoadSceneMode.Additive);
    }

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start()
	{
		uiManager = FindObjectOfType<UiManager>();
		spawnNumbers = FindObjectsOfType<SpawnPoint>().Length;
		if (spawnNumbers <= 0)
		{
			Debug.LogError("Have no spawners");
		}
		Debug.Assert(uiManager, "Wrong initial parameters");
		// Set gold amount for this level
		uiManager.SetGold(goldAmount);
		looseCounter = defeatCounter;
	}

    /// <summary>
    /// Raises the enable event.
    /// </summary>
    void OnEnable()
    {
        EventManager.StartListening("Captured", Captured);
        EventManager.StartListening("AllEnemiesAreDead", AllEnemiesAreDead);
    }

    /// <summary>
    /// Raises the disable event.
    /// </summary>
    void OnDisable()
    {
        EventManager.StopListening("Captured", Captured);
        EventManager.StopListening("AllEnemiesAreDead", AllEnemiesAreDead);
    }

    /// <summary>
    /// Determines if is collision valid for this scene.
    /// </summary>
    /// <returns><c>true</c> if is collision valid the specified myTag otherTag; otherwise, <c>false</c>.</returns>
    /// <param name="myTag">My tag.</param>
    /// <param name="otherTag">Other tag.</param>
    public static bool IsCollisionValid(string myTag, string otherTag)
    {
        bool res = false;
        switch (myTag)
        {
            case "Tower":
            case "Defender":
                switch (otherTag)
                {
                    case "Enemy":
                        res = true;
                        break;
                }
                break;
            case "Enemy":
                switch (otherTag)
                {
                    case "Defender":
                    case "Tower":
                        res = true;
                        break;
                }
                break;
            case "Bullet":
                switch (otherTag)
                {
                    case "Enemy":
                        res = true;
                        break;
                }
                break;
            case "CapturePoint":
                switch (otherTag)
                {
                    case "Enemy":
                        res = true;
                        break;
                }
                break;
            default:
                Debug.Log("Unknown collision tag => " + myTag + " - " + otherTag);
                break;
        }
        return res;
    }

    /// <summary>
    /// Enemy reached capture point.
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <param name="param">Parameter.</param>
    private void Captured(GameObject obj, string param)
    {
		if (looseCounter > 0)
		{
			looseCounter--;
			if (looseCounter <= 0)
			{
				// Defeat
				uiManager.GoToDefeatMenu();
			}
		}
    }

    /// <summary>
    /// All enemies are dead.
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <param name="param">Parameter.</param>
    private void AllEnemiesAreDead(GameObject obj, string param)
    {
        spawnNumbers--;
        // Enemies dead at all spawners
        if (spawnNumbers <= 0)
        {
            // Victory
            uiManager.GoToVictoryMenu();
        }
    }
}
