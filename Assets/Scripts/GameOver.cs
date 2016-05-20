using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void RestartLevel () {
		SceneManager.LoadScene ("Menu");
	}

}
