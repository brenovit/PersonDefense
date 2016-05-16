using UnityEngine;
using System.Collections;

public class CubeBehavior : MonoBehaviour {
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerStay(Collider col) {
		gameObject.transform.LookAt (col.transform.position);
		if(col.gameObject.tag.Equals ("Enemy")){
			Atirar (col.gameObject);
		}
	}

	void Atirar(GameObject alvo){
		GunBehavior[] arma = gameObject.GetComponentsInChildren<GunBehavior>();
			for(int i = 0; i < arma.Length; i++){
				arma[i].Atira (alvo);
		}		
	}
}
