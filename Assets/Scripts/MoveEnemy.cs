using UnityEngine;
using System.Collections;

public class MoveEnemy : MonoBehaviour {
	[HideInInspector]						//faz com que esta variavel fique oculta no inspetor
	public GameObject[] waypoints;			//armazena uma copia dos waypoints em um array
	private int currentWaypoint = 0;		//armazena o waypoint que o inimigo estiver passando
	private float lastWaypointSwitchTime;	//armazena o tempo que o inimigo passou por esse waypoint
	public float speed = 1.0f;				//velocidade do inimigo

	void Start () {
		lastWaypointSwitchTime = Time.time;		//define que esta variavel vai recerbe um tempo.
	}

	void Update () {
		Vector3 startPosition = waypoints [currentWaypoint].transform.position;				//defini a posição inicial do inimigo de acordo com a posição do WP que estiver na primeira posição do vetor 
		Vector3 endPosition = waypoints [currentWaypoint + 1].transform.position;			//defini a posição final do inimigo de acordo com a posição do WP que estiver na proxima posição posição do vetor 

		float pathLength = Vector3.Distance (startPosition, endPosition);					//armazena a distancia etnre os dois pontos atuais, usando um procediemtno do vector3, chamado distance
		float totalTimeForPath = pathLength / speed;										//armazena o tempo total para fazer o percurso, usando a formula, tempo = distancia / velocida
		float currentTimeOnPath = Time.time - lastWaypointSwitchTime;						//armazena o tempo atual no caminho, sendo este tempo a diferença entre o tempo e tempo passoado pelo ultimo waypoint
		gameObject.transform.position = Vector3.Lerp (startPosition, endPosition,			//a posição do inimigo será alterada de acordo com posição do primeiro waypoint e o proximo
		currentTimeOnPath / totalTimeForPath);                                          	//esta posição será media com algumas interrupções de tempo entre esses pontos, usando o Vector3.Lerp(pos1,pos2,temp);

		if(gameObject.transform.position.Equals (endPosition)){								//se a posição atual do inimigo for igual a ultima posição do waypoint
			if (currentWaypoint < waypoints.Length - 2){									//verifica se o waypoint atual é menor que a qauntidade de wayspoints -2
				currentWaypoint++;															//se for verdade a posição do waypoint atual é incrementada
				lastWaypointSwitchTime = Time.time;											//o tempo do ultimo way point é alterado de acordo com o tempo atual
				RotateIntoMoveDirection ();
			} else {																		//se não for
				Destroy (gameObject);														//destroi o game object
				AudioSource audioSource = gameObject.GetComponent <AudioSource> ();			//toca um som
				AudioSource.PlayClipAtPoint (audioSource.clip, transform.position);
				GameManagerBehaviour gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehaviour> ();	//procura o Objeto no jogo que referencia o GameManager
				gameManager.Health -= 1;
			}
		}
	}

	private void RotateIntoMoveDirection(){
		//Esta parte vai calcular a direção de movimentação atual  do inimigo, subtraindo a posição do way point atual pela posição do proximo waypoint
		Vector3 newStartPosition = waypoints[currentWaypoint].transform.position;
		Vector3 newEndPosition = waypoints[currentWaypoint+1].transform.position;
		Vector3 newDirection = (newEndPosition - newStartPosition);

		float x = newDirection.x;																//salva a nova posição em x
		float y = newDirection.y;																//salva a nova posição em y
		float rotationAngle = Mathf.Atan2 (y,x) * 180 / Mathf.PI;								//o angulo de rotação, usando uma base matematica onde tem-se o valor do radiano, vezes 180 dividido por PI
																								//radiano(razão em o comprimento de um arco e seu raio)
		GameObject sprite = (GameObject) gameObject.transform.FindChild ("Sprite").gameObject;	//pega o filho do gameobject do inimigo, com o nome de sprite
		sprite.transform.rotation = Quaternion.AngleAxis (rotationAngle, Vector3.forward);		//rotaciona esse game object de acordo com com o angulo definido anteriormente.

	}

	public float distanceToGoal(){											//este método calcula a distancia do inimigo até o final							
		float distance = 0;													//variavel float que vai receber a distancia
		distance += Vector3.Distance (										//a variavel distance vai receber a distancia 
		gameObject.transform.position, 										//entre a posição atual do gameObject e 
		waypoints [currentWaypoint + 1].transform.position);				//o proximo waypoint
		for(int i = currentWaypoint +1; i < waypoints.Length - 1; i++){		//fazer um laço que vai começar da posição do proximo waypoint enquanto ela for menor do que o tamanho maximo do waypoint
			Vector3 startPosition = waypoints[i].transform.position;		//vetor3D, recebendo a posição do proximo waypoint
			Vector3 endPosition = waypoints[i + 1].transform.position;		//vetor3D, recebendo a posição do waypoint depois do proximo waypoint
			distance += Vector3.Distance (startPosition, endPosition);		//a variavel distance vai acumalar a distancia entre todos estes vetores
		}
		return distance;
	}
}