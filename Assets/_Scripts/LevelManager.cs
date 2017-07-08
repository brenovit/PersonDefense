using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{	
	public string cenaUI;

	private UIManager uiManager;

	// Use this for initialization
	void Awake ()
	{	
		SceneManager.LoadScene (cenaUI, LoadSceneMode.Additive);
	}
}
