using UnityEngine;
using System.Collections;

public class Cubo : MonoBehaviour {
	public GameObject outroCubo;
	private Vector3 posicao;
	// Use this for initialization
	void Start () {
		posicao = gameObject.transform.position;
		gameObject.transform.position = outroCubo.transform.position;
		outroCubo.transform.position = posicao;
	}
}
