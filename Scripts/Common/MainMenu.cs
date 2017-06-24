using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main menu operate.
/// </summary>
public class MainMenu : MonoBehaviour
{
	// Name of scene to start on click
    public string startSceneName;

	// Start new game
	public void NewGame()
	{
		SceneManager.LoadScene(startSceneName);
	}

	/// <summary>
	/// Close application.
	/// </summary>
	public void Quit()
	{
		Application.Quit();
	}
}
