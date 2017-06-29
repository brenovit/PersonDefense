using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{	
	public string cenaUI;
	public int tropas;
	public int vidas;

	private UIManager uiManager;

	// Use this for initialization
	void Awake ()
	{	
		if (!PlayerPrefs.HasKey ("pontos")) {
			PlayerPrefs.SetInt ("pontos", tropas);
			tropas = 200;
		} else {
			tropas = PlayerPrefs.GetInt ("pontos");
		}
		SceneManager.LoadScene (cenaUI, LoadSceneMode.Additive);
	}

	void Start ()
	{
		uiManager = FindObjectOfType<UIManager> ();
		uiManager.SetTropas (tropas);
		//PlayerPrefs.Save ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
