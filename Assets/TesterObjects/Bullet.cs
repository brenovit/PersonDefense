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
		tempo = Time.time;											//a variavel startTime vai receber o tempo de inicio da bala
		distancia = Vector3.Distance (startPosition, targetPosition);	//esta variavel vai receber a distancia entre a posição inicial da bala e do alvo
	}

	void Update () {
		float timeInterval = Time.time - tempo;						//instavelo de tempo para a proxima bala sair
		gameObject.transform.position = 								//a posição da bala vai ser alterada de acordo com a 
			Vector3.Lerp(startPosition, targetPosition, 				//interpolação linear entre dois pontosl evando em consideração o tempo, 
			timeInterval * velocidade / distancia);							//que neste caso traduz o intervalo que a bala sai, vezes a velocidade dividido pela distancia
		if (gameObject.transform.position.Equals(targetPosition)) {		//se a posição da bala for igual a posição do inimigo
			Destroy(gameObject);										//por fim detroi-se a bala
		}	
	}
}
