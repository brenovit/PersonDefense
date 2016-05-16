using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour {

	public float speed = 10;			//velocidade que a bala vai se movimentar
	public int damage;					//dano que a bala vai causar
	public GameObject target;			//alvo da bala
	public Vector3 startPosition;		//posição inicial da bala
	public Vector3 targetPosition;		//posição do alva da bala

	private float  distance;			//vai medir a distancia entre o inimigo e a torre
	private float startTime;			//tempo que ela vai iniciar
	
	private GameManagerBehaviour gameManager;		//este Objeto vai tratar de alterar os dados do player na classe gameObject

	void Start () {
		startTime = Time.time;											//a variavel startTime vai receber o tempo de inicio da bala
		distance = Vector3.Distance (startPosition, targetPosition);	//esta variavel vai receber a distancia entre a posição inicial da bala e do alvo
		GameObject gm = GameObject.Find("GameManager");					//estou criando uma variavel do tipo GameObject e mandando ela procurar em jogo o objeto que tiver o nome "GameManager"
		gameManager = gm.GetComponent<GameManagerBehaviour>();			//o objeto gameManager vai receber o GameObject gm, passando o seu componente GameManagerBehaviour
	}

	void Update () {
		float timeInterval = Time.time - startTime;						//instavelo de tempo para a proxima bala sair
		gameObject.transform.position = 								//a posição da bala vai ser alterada de acordo com a 
			Vector3.Lerp(startPosition, targetPosition, 				//interpolação linear entre dois pontosl evando em consideração o tempo, 
			timeInterval * speed / distance);							//que neste caso traduz o intervalo que a bala sai, vezes a velocidade dividido pela distancia
		if (gameObject.transform.position.Equals(targetPosition)) {		//se a posição da bala for igual a posição do inimigo
			if (target != null) {										//se o alvo não for nulo
				Mosquito inimigo = target.GetComponent<Mosquito> ();	//Cria-se um variavel do tipo mosquito recebendo o compoente Mosquito do alvo 
				inimigo.RecebeuDano (damage);							//essa variavel chama o metodo recebeuDano
			}
			Destroy(gameObject);										//por fim detroi-se a bala
		}	
	}
}
