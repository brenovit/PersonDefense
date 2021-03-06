﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootEnemies : MonoBehaviour {
	private float lastShotTime;													//este atributo vai receber o tempo do ultimo tiro feito
	private TowerData towerData;												//este atributo vai receber os dados do monstro(torre)
	[SerializeField]
	private List<GameObject> enemiesInRange;				//lista contendo os inimigos no perimetro

	void Start () {
		enemiesInRange = new List<GameObject> ();								//instancia a lista
		lastShotTime = Time.time;												//o atributo de tempo receber o momento exato de inicio
		towerData = gameObject.GetComponentInChildren<TowerData> ();			//o objeto contendo os dados do monstro é instanciado recebendo componente do filho deste gameObject
	}

	void Update(){
		GameObject target = null;												//cria-se uma variavel que vai definir o alvo, começando como vazia
		float minimalEnemyDistance = float.MaxValue;							//esta variavel vair receber a distance minima do inimigo, que na verdade é o tamanho maximo de uma variavel float
		foreach(GameObject enemy in enemiesInRange){							//um laço que vai procurar todos os inimigos 
			float distanceToGoal = 												//esta variavel vai receber a distancia entre o inimigo e o final
				enemy.GetComponent<MoveEnemy> ().distanceToGoal ();				//recebendo o atributo do componente MoveEnemy presente no inimigo
			if(distanceToGoal < minimalEnemyDistance){							//checando se a distancia for menor que a distancia minima
				target = enemy;													//o alvo se torna o inimigo
				minimalEnemyDistance = distanceToGoal;							//e a distancia minima recebe a distancia até o final do inimigo
			}
		}

		if(target != null){														//se o alvo não for  nulo
			if(Time.time - lastShotTime > towerData.CurrentLevel.cadencia){		//se o tempo atual menos o tmepo do ultimo tiro for menor que a cadencia do monstro
				Shoot (target.GetComponent<Collider2D> ());						//executa o método de atirar, passando o collider do inimigo
				lastShotTime = Time.time;										//o tempo do ultimo tiro receber o segundo atual
			}

			Transform muzzle = towerData.CurrentLevel.muzzle;
			Vector3 direction = 												//cria-se um vector3d que vai passar a direção da bala
			muzzle.position - target.transform.position;						//recebendo a posição atual do monstro menos a posição do inimigo
			muzzle.rotation = Quaternion.AngleAxis (							//esta torre vai rotacionar num angulo fixo
			Mathf.Atan2 (direction.y, direction.x) * 180 / Mathf.PI,			//levando em conta o valor da tangente em considerção os eixos das ordenadas e das abscissas vezes 180 dividido por PI em graus graus 
			new Vector3 (0, 0, 1)); 											//em um ponto fixo
		}
	}

	void OnEnemyDestroy(GameObject enemy){										//este método remove um objeto da lista
		enemiesInRange.Remove (enemy);											//se o inimigo for detruido ele deleta da lista
	}

	void OnTriggerEnter2D(Collider2D other){									//este método detecta se algum objeto com corpo rigido colidiu com este gameObject
		if(other.gameObject.tag.Equals ("Enemy")){								//se o objeto que colidir com o collider da arma, tiver a tag de enemy
			enemiesInRange.Add (other.gameObject);								//o objeto é adicionado na lista
			EnemyDestructionDelegate del = 										//em seguida a variavel del que é um do tipo de um delegate recebe o  
				other.gameObject.GetComponent<EnemyDestructionDelegate> ();		//GameObject que tiver o componente EnemuDestructionDelegate
			del.enemyDelegate += OnEnemyDestroy;								//o del chama o ponteiro do delgate inserindo o metodo a qual ele referencia
			//SpriteRenderer spriteEnemy = other.gameObject.GetComponentInChildren<SpriteRenderer>();
			//spriteEnemy.sortingOrder = (int)Camera.main.WorldToScreenPoint (spriteEnemy.bounds.min).y * -1;
		
		}
	}

	void OnTriggerExit2D (Collider2D other) {									//este método detecta se algum objeto com corpo rigido colidiu com este gameObject
  		if (other.gameObject.tag.Equals("Enemy")) {                          	//se o objeto que colidir com o collider da arma, tiver a tag de enemy
    		enemiesInRange.Remove(other.gameObject);                        	//o objeto é adicionado na lista
    		EnemyDestructionDelegate del =                                  	//em seguida a variavel del que é um do tipo de um delegate recebe o  
       			other.gameObject.GetComponent<EnemyDestructionDelegate>();   	//GameObject que tiver o componente EnemuDestructionDelegate
    		del.enemyDelegate -= OnEnemyDestroy;                             	//o del chama o ponteiro do delgate inserindo o metodo a qual ele referencia
  		}
	}

	void Shoot (Collider2D target){												//este método vai ser responsavel por atirar no inimigo
		GameObject bulletPrefab = towerData.bala;								//variavel que vai receber o prefab da bala
		Vector3 startPosition = towerData.CurrentLevel.muzzle.position;			//variavel da posição de inicio recebendo a posição atual do monstro
		Vector3 targetPosition = target.transform.position;						//variavel da posição do alvo recebendo a posição do alvo
		startPosition.z = bulletPrefab.transform.position.z;					//a posição no eixo z da posição inicial vai ser igual a posição em z da bala
		targetPosition.z = bulletPrefab.transform.position.z;					//a posição no eixo Z do alvo vai receber a posição em z da bala

		GameObject newBullet = (GameObject)Instantiate (bulletPrefab, startPosition,Quaternion.identity);			//instanciando uma nova bala como um GameObject

		//newBullet.transform.position = startPosition;							//a posição da bala vai ser igual a posição inicial do monstro
		BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior> ();	//Cria-se um objeto do tipo comportamento da bala que vai receber a nova bala pegando o componente BulletBehavior
		bulletComp.alvo = target.gameObject;									//este objeto de comportamento da bala em seu atributo de alvo vai receber o gameObject target e
		bulletComp.posicaoInicial = startPosition;								//em seu atributo de posição inicial vai receber a variavel startPosition
		bulletComp.posicaoAlvo = targetPosition;								//e em seu atributo de posição do alvo vai receber a variavel targetPosition
		bulletComp.Dano = towerData.CurrentLevel.dano;

		//por fim executa uma animação que representa o monstro atirando
		/*Animator animator = 
			towerData.CurrentLevel.visualizacao.GetComponent <Animator> ();
		animator.SetTrigger ("fireShot");*/
		//e toca um som
		AudioSource audioSource = gameObject.GetComponent<AudioSource> ();
		audioSource.PlayOneShot (audioSource.clip);
	}
}
