using UnityEngine;
using System.Collections;

public class Tester : MonoBehaviour {
	public GameObject[] pontos;
	public GameObject target;
	private int pontoAtual = 0;
	public float velocidade = 2f;
	private float tempoAteUltimoWaypoint;

	void Start(){
		tempoAteUltimoWaypoint = Time.time;
	}


	// Update is called once per frame
	void Update () {
		if(pontoAtual < pontos.Length){
			Vector3 startPosition = pontos [pontoAtual].transform.position;
			Vector3 endPosition = pontos [pontoAtual+1].transform.position;

			float distancia = Vector3.Distance (startPosition, endPosition);
			float tempoParaChegarNoFinal = distancia / velocidade;
			float tempoAtualNoCaminho = Time.time - tempoAteUltimoWaypoint;

			gameObject.transform.LookAt (pontos[pontoAtual+1].transform.position);
			gameObject.transform.position = Vector3.Lerp (startPosition, endPosition, tempoAtualNoCaminho / tempoParaChegarNoFinal);

			if(gameObject.transform.position.Equals (endPosition)){
				pontoAtual++;
				tempoAteUltimoWaypoint = Time.time;
			}

		}
		if(pontoAtual == pontos.Length-1){
			Destroy (gameObject);
		}
	}
}
