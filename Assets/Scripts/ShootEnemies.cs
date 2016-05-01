using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootEnemies : MonoBehaviour {

	public List<GameObject> enemiesInRange;										//lista contendo os inimigos no perimetro

	void Start () {
		enemiesInRange = new List<GameObject> ();								//instancia a lista
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
}
