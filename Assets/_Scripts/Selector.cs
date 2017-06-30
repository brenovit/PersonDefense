using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {


	public void Start(){
		Hide ();
	}

	public void MoveTo(Vector3 position){
		this.gameObject.transform.position = position;
	}

	public void Hide(){
		this.gameObject.SetActive (false);
	}

	public void Show(){
		this.gameObject.SetActive (true);
	}

	public void ShowOn(Vector3 position){
		Show ();
		MoveTo (position);
	}
}
