using UnityEngine;
using System.Collections;

public class Cubo : MonoBehaviour {

	public InputManager InputManager;

	// Use this for initialization
	void Awake () {
		InputManager.OnClick += OnBoxClick;
	}
	
	// Update is called once per frame
	void OnBoxClick (GameObject go) {
		if(go == gameObject){
			GetComponent<Renderer> ().material.color = Color.green;
		}
	}
}
