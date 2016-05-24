using UnityEngine;
using System.Collections;

public class CubeBehavior : MonoBehaviour {
	public GameObject balaPrefab;
	public GameObject alvo;
	public Transform muzzle;
	// Use this for initialization
	void Start () {
		alvo = GameObject.FindGameObjectWithTag ("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerStay(Collider col) {
		int i = 0;
		gameObject.transform.LookAt (col.transform.position);
		if(col.gameObject.tag.Equals ("Enemy")){
			Atirar (col.gameObject);
			i++;
			print ("i: " + i);
		}
	}

	void Atirar(GameObject alvo){
		Bullet bala = Instantiate (balaPrefab,muzzle.position,Quaternion.identity) as Bullet;
		bala.alvo = alvo;
	}
}
