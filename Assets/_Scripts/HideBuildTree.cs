using UnityEngine;
using System.Collections;

public class HideBuildTree : MonoBehaviour {
	
	public void Hide(){						//quando clicar no collider		
		BuildTree buildTree = FindObjectOfType<BuildTree>();
		if (buildTree != null) {
			Destroy (buildTree.gameObject);
		}
	}
}
