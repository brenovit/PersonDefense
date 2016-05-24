using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

	public Wave[] orda;							//vetor que vai conter vario objetos do tipo onda
	public int timeBetweenWaves = 5;			//tempo entre cada onda

	private GameManagerBehaviour gameManager;	//gerenciador do jogo

	private float lastSpawnTime;				//tempo do ultimo spawn
	private int enemiesSpawned = 0;				//quantidade de inimigos spawnados

	public GameObject[] waypoints;				//vetor que vai conter todos os wayspoints do mapa			
	public bool	iniciouGame = false;

	void Start () {
		lastSpawnTime = Time.time;				//tempo para o ultimo
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManagerBehaviour> ();	
	}
	
	// Update is called once per frame
	void Update (){
		if (iniciouGame) {
			StartSpawn ();
		}
	}

	private void StartSpawn(){
		int currentWave = gameManager.Orda;									//vai receber o numero da onda atual
		if(currentWave < orda.Length){										//verifica se o numero da onda atual é menor do que o tamanho total de ondas
			float timeInterval = Time.time - lastSpawnTime;					//variavel que vai receber um valor em segundos e subtrair este valor pelo tempo do ultimo spawn.
			float spawnInterval = orda [currentWave].spawnInterval;			//variavel que vai receber o intervalo entre os spawns da onda atual.
			if(((enemiesSpawned == 0 && 									//se a quantidade de inimigos spawnados for igual a 0 e 
			timeInterval > timeBetweenWaves) || 							//o intervalo de tempo for maior que o tempo entre cada onda, ou
			timeInterval > spawnInterval) && 								//o intevavlo de tempo for maior que o intervalo entre os spawn e
			enemiesSpawned < orda[currentWave].maxEnemies){					//a quantidade de inimigos spawned dor menor do que a quantidade maxima de inimigos que tem de ser spawnada na onda atual
				lastSpawnTime = Time.time;									//o tempo do utlimo spawn recebe um sistema de tempo em segundos.
				GameObject newEnemy = 
				(GameObject)Instantiate (orda [currentWave].enemyPrefab);	//Cria-se um novo game object recebendo a instancia  do prefab de inimigo definido na onda.
				newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;	//este gameobject vai preencher seu vetor de wayspoints com os waispoints definidos nesta classe.
				enemiesSpawned++;											//a quantidade de inimigos spawnados auamenta
			}
			if(enemiesSpawned == orda[currentWave].maxEnemies 				//se a quantidaede de inimigos spawnados for igual a quantidade maxima de inimigos que tem de ser śpawnados na onda e
			&& GameObject.FindGameObjectWithTag ("Enemy") == null){			//não tiver mais nenhum gameobject no game com a tag Enemy
				gameManager.Orda++;											//avança para proxima onda
				gameManager.Tropas = Mathf.RoundToInt (gameManager.Tropas * 1.1f);		//a quantidade de dinheiro do jogador vai ser uma quantidade aproximada do dinheiro que o jogador tiver vezes 1.1f;
				enemiesSpawned = 0;											//a variavel que conta os inimigos spawnados vai ser igual a 0
				lastSpawnTime = Time.time;									//o tempo do ultimo spawn ser igual ao presente momento.
				iniciouGame = false;
			}
		} else {															//senão
			gameManager.gameOver = true;									//ativa o game over do sistema
			GameObject gameOverText = GameObject.FindGameObjectWithTag ("GameWon");		//procura o objeto com a tag GamoWon
			gameOverText.GetComponent<Animator> ().SetBool ("gameOver",true);			//inicia a animação do texto de game over
		}
	}

	public void StartWave(){
		iniciouGame = true;
	}
}
