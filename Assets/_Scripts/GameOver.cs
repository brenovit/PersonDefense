using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace InGame{
	public class GameOver : MonoBehaviour {

		// Use this for initialization
		void RestartLevel ()
		{
			if (SceneManager.GetActiveScene ().buildIndex <= 3 && GameManagerBehaviour.gameState == 2) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);	
			}else{
				SceneManager.LoadScene ("Menu");
			}
		}
	}
}
