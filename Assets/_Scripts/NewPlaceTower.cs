using UnityEngine;
using System.Collections;

public class NewPlaceTower : MonoBehaviour {
	public GameObject torre;
	private Vector3 mousePosition;

	void OnMouseDown(){
		/*mousePosition = Input.mousePosition;
		mousePosition.z = -1;*/
		//mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = -1;
	}
	void OnMouseUp(){
		Instantiate (torre,mousePosition,Quaternion.identity);
		print ("Mouse saiu de mim");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
