using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public Vector3 startPosition;		//posição inicial da bala
	public Vector3 targetPosition;		//posição do alva da bala

	public GameObject alvo;
	public float velocidade;
	public float tempo;
	public float distancia;

	// Use this for initialization

	void Start () {
	}

	void Update () {
		gameObject.transform.Translate (alvo.transform.position);
		Destroy (gameObject,2);
	}
}
