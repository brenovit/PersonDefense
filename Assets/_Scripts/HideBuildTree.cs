using UnityEngine;
using System.Collections;

public class HideBuildTree : MonoBehaviour {
	private GameManagerBehaviour gameManager;

	void Start(){
		gameManager = FindObjectOfType<GameManagerBehaviour> ();
	}

	public void Hide(){						//quando clicar no collider		
		gameManager.DisableAllTowers();
	}
}
