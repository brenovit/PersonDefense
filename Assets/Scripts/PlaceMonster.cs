using UnityEngine;
using System.Collections;

public class PlaceMonster : MonoBehaviour {

	public GameObject torrePrefab;
	private GameObject torre;
	private GameManagerBehaviour gameManager;

	void Start() {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManagerBehaviour> ();			//o gamemanager vai procurar o gameobject que tiver o componente gamemanagerbehaiour
	}

	void OnMouseUp(){																					//Quando o mouse clicar no espaço
		BotarTorre ();
	}

	private void BotarTorre(){
		if(canPlaceMonster ()) {																		//se puder botar monstro
			torre = (GameObject)Instantiate (torrePrefab, transform.position, Quaternion.identity); //instancia o monstro definido no mosterprefab, no pasição e rotação do gameobject atual(slot)
			AudioSource audiosource = gameObject.GetComponent<AudioSource> (); 							//define uma variavel para o audio
			audiosource.PlayOneShot (audiosource.clip);													//toca o som de botar item
			gameManager.Gold -= torre.GetComponent <TowerData> ().CurrentLevel.custo;				//acessa a carteira do jogador e reduz o seu dinheiro, de acordo com o custo de compra do monstro.
		} else if(canUpgradeMosnter ()) {																//se ja tiver mosntro, verifica se pode evoluir
			torre.GetComponent <TowerData>().increaseLevel ();										//para o monstro atual, execulta o procedimento de incrementar o level
			AudioSource audiosource = gameObject.GetComponent<AudioSource> (); 							//define uma variavel para tratar o audio
			audiosource.PlayOneShot (audiosource.clip);													//toca o som de botar item
			gameManager.Gold -= torre.GetComponent <TowerData> ().CurrentLevel.custo;				//acessa a carteira do jogador e reduz o seu dinheiro, de acordo com o custo de melhoria do monstro.
		}
	}

	private bool canPlaceMonster(){																		//procedimento que vai informar se pode ou não colocar monstro
		int cost = torrePrefab.GetComponent <TowerData> ().levels [0].custo;							//define o custo, vai até o prefab do monstro acessa a lista de levels dele, na posição 0, retornando o custo
		return torre == null && gameManager.Gold >= cost;												//retorna true, se monstro igual a null, isto é, o lugar esta vazio, e o dinheiro atual for maior que o custo

	}

	private bool canUpgradeMosnter() {																	//procedimento que vai informar se pode evoluir um monstro;
		if(torre != null){																			//se o local tiver algum monstro
			TowerData monsterData = torre.GetComponent <TowerData> ();							//cria uma variavel do tipo dados de monstro, que vai receber o monstro que estiver no slot
			TowerLevel nextLevel = monsterData.getNextLevel ();										//cria uma variavel do tipo de level do monstro, que vai execultar o procedimento de pegar o level atual
			if(nextLevel != null){																		//se existir um proximo level
				return gameManager.Gold >= nextLevel.custo;												//vai retornar verdadeiro se o dinheiro atual for maior que o custo de evoluir para o proximo level.
			}
		}
		return false;																					//retorna falso, informando que não pode evoluir.
	}


}
