﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlaceTower : MonoBehaviour {
	
	public GameObject buildTreePrefab;
	public GameObject upgradeTreePrefab;

	private GameObject activeBuildTree;

	private GameObject torre;
	private Selector seletor;

	private GameManagerBehaviour gameManager;
	private Canvas canvas;

	private PlaceTower[] placesTower;

	[HideInInspector]
	public bool euChamei = false;

	void Awake() {
		gameManager = FindObjectOfType<GameManagerBehaviour> (); //GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManagerBehaviour> ();			//o gamemanager vai procurar o gameobject que tiver o componente gamemanagerbehaiour
		seletor = FindObjectOfType<Selector>();
	}

	void Start(){
		Canvas[] canvases = Resources.FindObjectsOfTypeAll<Canvas> ();
		foreach (Canvas canv in canvases) {
			if (canv.CompareTag ("LevelUI")) {
				canvas = canv;
			}
		}
		placesTower = GameOver.FindObjectsOfType<PlaceTower> ();
	}

	void OnMouseUp ()	{	//Quando o mouse clicar no espaço
		
		//GameObject selectorAux = GameObject.FindGameObjectWithTag ("Selector");
		//Destroy (selectorAux);
		DisableAllTowers();
		if (torre == null) {
			ShowTowerBuildTree (buildTreePrefab);
			//painel.gameObject.GetComponent<TowerSelect> ().AttTorresDisponiveis (torrePrefab);
		} else {
			ShowTowerBuildTree (upgradeTreePrefab);
			//painel.gameObject.GetComponent<TowerSelect> ().MelhorarDestruirTorre (torre);
		}
		euChamei = true;

		if (euChamei)
			seletor.ShowOn (gameObject.transform.position);
			//Instantiate (selector, gameObject.transform.position, Quaternion.identity);
	}

	public void DesativaTudo(){
		euChamei = false;
	}

	public void ConstruirMelhorarTorre (GameObject tower){
		//GameObject selectorAux = GameObject.FindGameObjectWithTag ("Selector");
		//Destroy (selectorAux);
		seletor.ShowOn (gameObject.transform.position);
		DesativaTudo ();
		BotarTorre (tower);
	}

	public void Destruir(){
		if(torre != null){																				//se o local tiver algum monstro
			TowerData ta = torre.GetComponent <TowerData> ();											//cria uma variavel do tipo dados de monstro, que vai receber o monstro que estiver no slot
			int tropas = (int)ta.levels [ta.getCurrentLevel ()].tropas;
			gameManager.tropas += (int)(tropas * 0.4);
			Destroy (this.torre.gameObject);
			this.torre = null;
		}
		DesativaTudo ();
	}

	private void BotarTorre(GameObject tower){
		if(canPlaceTower (tower)) {																		//se puder botar monstro
			torre = (GameObject)Instantiate (tower, transform.position, Quaternion.identity); 			//instancia o monstro definido no mosterprefab, no pasição e rotação do gameobject atual(slot)
			AudioSource audiosource = gameObject.GetComponent<AudioSource> (); 							//define uma variavel para o audio
			audiosource.PlayOneShot (audiosource.clip);													//toca o som de botar item
			gameManager.tropas -= torre.GetComponent <TowerData> ().CurrentLevel.tropas;				//acessa a carteira do jogador e reduz o seu dinheiro, de acordo com o custo de compra do monstro.
		} else if(canUpgradeTower ()) {																	//se ja tiver mosntro, verifica se pode evoluir
			torre.GetComponent <TowerData>().increaseLevel ();											//para o monstro atual, execulta o procedimento de incrementar o level
			AudioSource audiosource = gameObject.GetComponent<AudioSource> (); 							//define uma variavel para tratar o audio
			audiosource.PlayOneShot (audiosource.clip);													//toca o som de botar item
			gameManager.tropas -= torre.GetComponent <TowerData> ().CurrentLevel.tropas;				//acessa a carteira do jogador e reduz o seu dinheiro, de acordo com o custo de melhoria do monstro.
		}
	}

	private bool canPlaceTower(GameObject tower){														//procedimento que vai informar se pode ou não colocar monstro
		int cost = tower.GetComponent <TowerData> ().levels [0].tropas;									//define o custo, vai até o prefab do monstro acessa a lista de levels dele, na posição 0, retornando o custo
		return torre == null && gameManager.tropas >= cost;												//retorna true, se monstro igual a null, isto é, o lugar esta vazio, e o dinheiro atual for maior que o custo
	}

	private bool canUpgradeTower() {																	//procedimento que vai informar se pode evoluir um monstro;
		if(torre != null){																				//se o local tiver algum monstro
			TowerData monsterData = torre.GetComponent <TowerData> ();									//cria uma variavel do tipo dados de monstro, que vai receber o monstro que estiver no slot
			TowerLevel nextLevel = monsterData.getNextLevel ();											//cria uma variavel do tipo de level do monstro, que vai execultar o procedimento de pegar o level atual
			if(nextLevel != null){																		//se existir um proximo level
				return gameManager.tropas >= nextLevel.tropas;											//vai retornar verdadeiro se o dinheiro atual for maior que o custo de evoluir para o proximo level.
			}
		}
		return false;																					//retorna falso, informando que não pode evoluir.
	}

	private void ShowTowerBuildTree(GameObject prefab){
		if (prefab != null) {
			activeBuildTree = Instantiate (prefab, canvas.transform);
			activeBuildTree.transform.position = Camera.main.WorldToScreenPoint (transform.position);
		}
	}


	private void CloseTowerBuildTree(){
		if (activeBuildTree != null) {
			Destroy (activeBuildTree);
		}
	}

	private void ShowUpgradeTower(){

	}


	private void DisableAllTowers(){
		for (int i = 0; i < placesTower.Length; i++) {
			placesTower [i].euChamei = false;
			placesTower [i].CloseTowerBuildTree ();
		}
	}
}
