using UnityEngine;
using System.Collections;

public class InstanciarTorrer : MonoBehaviour {
	public GameObject efeito;

	void OnMouseUp(){
		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		Vector3 position = Input.mousePosition;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
