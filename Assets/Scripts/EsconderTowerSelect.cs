﻿using UnityEngine;
using System.Collections;

public class EsconderTowerSelect : MonoBehaviour {

	public GameObject panel;

	void OnMouseUp(){
		panel.SetActive (false);
		GameObject selectorAux = GameObject.FindGameObjectWithTag ("Selector");
		Destroy (selectorAux);
	}
}
