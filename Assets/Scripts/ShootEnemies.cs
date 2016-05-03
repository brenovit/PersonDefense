using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootEnemies : MonoBehaviour {
	public float lastShotTime;
	private MonsterData monsterData;

	public List<GameObject> enemiesInRange;										//lista contendo os inimigos no perimetro

	void Start () {
		enemiesInRange = new List<GameObject> ();								//instancia a lista
		lastShotTime = Time.time;
		monsterData = gameObject.GetComponentInChildren<MonsterData> ();
	}

	void Update(){
		GameObject target = null;
		float minimalEnemyDistance = float.MaxValue;
		foreach(GameObject enemy in enemiesInRange){
			float distanceToGoal = enemy.GetComponent<MoveEnemy> ().distanceToGoal ();
			if(distanceToGoal < minimalEnemyDistance){
				target = enemy;
				minimalEnemyDistance = distanceToGoal;
			}
		}

		if(target != null){
			if(Time.time - lastShotTime > monsterData.CurrentLevel.fireRate){
				Shoot (target.GetComponent<Collider2D> ());
				lastShotTime = Time.time;
			}

			Vector3 direction = gameObject.transform.position - target.transform.position;
			gameObject.transform.rotation = Quaternion.AngleAxis (
				Mathf.Atan2 (direction.y, direction.x) * 180 / Mathf.PI,
				new Vector3 (0, 0, 1));
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

	void Shoot (Collider2D target){
		GameObject bulletPrefab = monsterData.CurrentLevel.bullet;

		Vector3 startPosition = gameObject.transform.position;
		Vector3 targetPosition = target.transform.position;
		startPosition.z = bulletPrefab.transform.position.z;
		targetPosition.z = bulletPrefab.transform.position.z;

		GameObject newBullet = (GameObject)Instantiate (bulletPrefab);
		newBullet.transform.position = startPosition;
		BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior> ();
		bulletComp.target = target.gameObject;
		bulletComp.startPosition = startPosition;
		bulletComp.targetPosition = targetPosition;

		Animator animator = monsterData.CurrentLevel.visualizacao.GetComponent <Animator> ();
		animator.SetTrigger ("fireShot");
		AudioSource audioSource = gameObject.GetComponent<AudioSource> ();
		audioSource.PlayOneShot (audioSource.clip);

	}
}
