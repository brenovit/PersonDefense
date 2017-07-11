using UnityEngine;
using System.Collections;

public class MoveEnemy : MonoBehaviour {
	[HideInInspector]						//faz com que esta variavel fique oculta no inspetor
	public GameObject[] waypoints;			//armazena uma copia dos waypoints em um array
	private int currentWaypoint = 0;		//armazena o waypoint que o inimigo estiver passando
	private float lastWaypointSwitchTime;	//armazena o tempo que o inimigo passou por esse waypoint
	private float speed = 0;				//velocidade do inimigo
	private GameManagerBehaviour gameManager;
	//public SpriteRenderer sprite;

	public float Speed{
		get {return speed;}
		set {speed = value;}
	}

	void Start () {
		gameManager = FindObjectOfType<GameManagerBehaviour> ();

		lastWaypointSwitchTime = Time.time;	//define que esta variavel vai recerbe um tempo.
		if (speed == 0)
			speed = 2;
	}

	void Update () {		
		Vector3 startPosition = waypoints [currentWaypoint].transform.position;				//defini a posição inicial do inimigo de acordo com a posição do WP que estiver na primeira posição do vetor 
		Vector3 endPosition = waypoints [currentWaypoint + 1].transform.position;			//defini a posição final do inimigo de acordo com a posição do WP que estiver na proxima posição posição do vetor 

		//sprite.sortingOrder = (int)Camera.main.WorldToScreenPoint (spriteEnemy.bounds.min).y * -1;

		float pathLength = Vector3.Distance (startPosition, endPosition);					//armazena a distancia etnre os dois pontos atuais, usando um procediemtno do vector3, chamado distance
		float totalTimeForPath = pathLength / speed;										//armazena o tempo total para fazer o percurso, usando a formula, tempo = distancia / velocida
		float currentTimeOnPath = Time.time - lastWaypointSwitchTime;						//armazena o tempo atual no caminho, sendo este tempo a diferença entre o tempo e tempo passoado pelo ultimo waypoint
		gameObject.transform.position = Vector3.Lerp (startPosition, endPosition,			//a posição do inimigo será alterada de acordo com posição do primeiro waypoint e o proximo
		currentTimeOnPath / totalTimeForPath);                                          	//esta posição será media com algumas interrupções de tempo entre esses pontos, usando o Vector3.Lerp(pos1,pos2,temp);

		if(gameObject.transform.position.Equals (endPosition)){								//se a posição atual do inimigo for igual a ultima posição do waypoint
			if (currentWaypoint < waypoints.Length - 2){									//verifica se o waypoint atual é menor que a qauntidade de wayspoints -2
				//NovaRotacao ();
				currentWaypoint++;															//se for verdade a posição do waypoint atual é incrementada
				lastWaypointSwitchTime = Time.time;											//o tempo do ultimo way point é alterado de acordo com o tempo atual
			} else {																		//se não for
				Destroy (gameObject);														//destroi o game object
				AudioSource audioSource = gameObject.GetComponent <AudioSource> ();			//toca um som
				AudioSource.PlayClipAtPoint (audioSource.clip, transform.position);
				gameManager.Vida -= 1;
			}
		}
	}

	private void NovaRotacao(){
		GameObject sprite = (GameObject) gameObject.transform.Find ("Sprite").gameObject;	//pega o filho do gameobject do inimigo, com o nome de sprite

		//Estas Variaveis receberão a Escala da Sprite nos respectivos eixos
		float inverter = sprite.transform.localScale.x;
		float y = sprite.transform.localScale.y;
		float z = sprite.transform.localScale.z;

		//Estas variaveis recebrão a posição no eixo X do waypoint atual e do proximo
		float currentWaypointX = waypoints[currentWaypoint].transform.position.x;
		float nextWaypointX = waypoints[currentWaypoint+1].transform.position.x;

		if(nextWaypointX < currentWaypointX){												//se a posição em X do proximo waypoint dor menor do que a posição em X do waypoint atual			
			inverter *= -1;																	//inverte a escala em x(passando a impressão de estar girando)
		}
		sprite.transform.localScale = new Vector3(inverter,y,z);							//altera a escala do Sprite
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