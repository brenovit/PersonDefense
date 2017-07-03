using UnityEngine;
using System.Collections;

public class HideBuildTree : MonoBehaviour {
	
	void OnMouseUp(){						//quando clicar no collider		
		BuildTree buildTree = FindObjectOfType<BuildTree>();
		if (buildTree != null) {
			Destroy (buildTree.gameObject);
		}
	}
}
