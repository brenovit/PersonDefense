using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace InGame
{
	/// <summary>
	/// This class manages the tower construction, destruction and upgrade system. Be carefully.
	/// </summary>
	public class PlaceTower : MonoBehaviour
	{
		private GameManagerBehaviour gameManager;
		private Selector selector;

		void Awake ()
		{
			gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManagerBehaviour> ();			//o gamemanager vai procurar o gameobject que tiver o componente gamemanagerbehaiour
			selector = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Selector> ();			//o gamemanager vai procurar o gameobject que tiver o componente gamemanagerbehaiour

		}

		public void BuildTower (GameObject tower)
		{
			GameObject selectorAux = GameObject.FindGameObjectWithTag ("Selector");
			AudioSource audiosource = gameObject.GetComponent<AudioSource> (); 							//define uma variavel para o audio
			audiosource.PlayOneShot (audiosource.clip);													//toca o som de botar item
			gameManager.Tropas -= tower.GetComponent <TowerData> ().CurrentLevel.tropas;				//acessa a carteira do jogador e reduz o seu dinheiro, de acordo com o custo de compra do monstro.

		}

		public void DestroyTower ()
		{																			//se o local tiver algum monstro
			TowerData ta = selector.Tower.GetComponent <TowerData> ();											//cria uma variavel do tipo dados de monstro, que vai receber o monstro que estiver no slot
			int tropas = (int)ta.levels [ta.getCurrentLevel ()].tropas;
			gameManager.Tropas += (int)(tropas * 0.4);
			selector.DestroyAll ();
		}

		public void Upgrade ()
		{
			
		}

		private void BotarTorre (GameObject tower)
		{
			/*if (canPlaceTower (tower)) {																		//se puder botar monstro
				torre = (GameObject)Instantiate (tower, transform.position, Quaternion.identity); 			//instancia o monstro definido no mosterprefab, no pasição e rotação do gameobject atual(slot)
				AudioSource audiosource = gameObject.GetComponent<AudioSource> (); 							//define uma variavel para o audio
				audiosource.PlayOneShot (audiosource.clip);													//toca o som de botar item
				gameManager.Tropas -= torre.GetComponent <TowerData> ().CurrentLevel.tropas;				//acessa a carteira do jogador e reduz o seu dinheiro, de acordo com o custo de compra do monstro.
			} else if (canUpgradeTower ()) {																	//se ja tiver mosntro, verifica se pode evoluir
				torre.GetComponent <TowerData> ().increaseLevel ();											//para o monstro atual, execulta o procedimento de incrementar o level
				AudioSource audiosource = gameObject.GetComponent<AudioSource> (); 							//define uma variavel para tratar o audio
				audiosource.PlayOneShot (audiosource.clip);													//toca o som de botar item
				gameManager.Tropas -= torre.GetComponent <TowerData> ().CurrentLevel.tropas;				//acessa a carteira do jogador e reduz o seu dinheiro, de acordo com o custo de melhoria do monstro.
			}*/
		}

		private bool canPlaceTower (GameObject tower)
		{														//procedimento que vai informar se pode ou não colocar monstro
			int cost = tower.GetComponent <TowerData> ().levels [0].tropas;									//define o custo, vai até o prefab do monstro acessa a lista de levels dele, na posição 0, retornando o custo
			return gameManager.Tropas >= cost;												//retorna true, se monstro igual a null, isto é, o lugar esta vazio, e o dinheiro atual for maior que o custo
		}

		private bool canUpgradeTower ()
		{																	//procedimento que vai informar se pode evoluir um monstro;
			/*if (torre != null) {																				//se o local tiver algum monstro
				TowerData monsterData = torre.GetComponent <TowerData> ();									//cria uma variavel do tipo dados de monstro, que vai receber o monstro que estiver no slot
				TowerLevel nextLevel = monsterData.getNextLevel ();											//cria uma variavel do tipo de level do monstro, que vai execultar o procedimento de pegar o level atual
				if (nextLevel != null) {																		//se existir um proximo level
					return gameManager.Tropas >= nextLevel.tropas;											//vai retornar verdadeiro se o dinheiro atual for maior que o custo de evoluir para o proximo level.
				}
			}*/
			return false;																					//retorna falso, informando que não pode evoluir.
		}
	}
}