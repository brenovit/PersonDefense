using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlaceTower : MonoBehaviour {
	
	public GameObject buildTreePrefab;
	public GameObject upgradeTreePrefab;

	private GameObject activeBuildTree;

	private GameObject tower;
	private Selector seletor;

	private GameManagerBehaviour gameManager;
	private Canvas canvas;

	private PlaceTower[] placesTower;

	[HideInInspector]
	public bool euChamei = false;

	/*void OnEnable(){
		EventManager.CriarEvento ("BuildTower", BuildTower);
		EventManager.CriarEvento ("UpgradeTower", UpgradeTower);
		EventManager.CriarEvento ("DestroyTower", DestroyTower);
	}

	void OnDisable(){
		EventManager.RemoverEvento ("BuildTower",BuildTower);
		EventManager.RemoverEvento ("UpgradeTower",UpgradeTower);
		EventManager.RemoverEvento ("DestroyTower", DestroyTower);
	}*/

	void Start(){
		placesTower = GameOver.FindObjectsOfType<PlaceTower> ();
		gameManager = FindObjectOfType<GameManagerBehaviour> (); //o gamemanager vai procurar o gameobject que tiver o componente gamemanagerbehaiour
		Canvas[] canvases = Resources.FindObjectsOfTypeAll<Canvas> ();
		foreach (Canvas canv in canvases) {
			if (canv.CompareTag ("LevelUI")) {
				canvas = canv;
			}
		}
	}

	void OnMouseUp ()	{	//Quando o mouse clicar no espaço		
		DisableAllTowers();
		if (tower == null) {
			ShowTowerBuildTree (buildTreePrefab);
		} else {
			ShowTowerUpgradeTree (upgradeTreePrefab);
		}
		//euChamei = true;

		//if (euChamei)
			//seletor.ShowOn (gameObject.transform.position);
			//Instantiate (selector, gameObject.transform.position, Quaternion.identity);
	}

	private bool canPlaceTower(TowerData towerData){														//procedimento que vai informar se pode ou não colocar monstro
		int cost = towerData.levels [0].tropas;									//define o custo, vai até o prefab do monstro acessa a lista de levels dele, na posição 0, retornando o custo
		return gameManager.Tropas >= cost;												//retorna true, se monstro igual a null, isto é, o lugar esta vazio, e o dinheiro atual for maior que o custo
	}

	private bool canUpgradeTower() {																	//procedimento que vai informar se pode evoluir um monstro;
		if(tower != null){																				//se o local tiver algum monstro
			TowerData td = tower.GetComponent <TowerData> ();									//cria uma variavel do tipo dados de monstro, que vai receber o monstro que estiver no slot
			TowerLevel nextLevel = td.getNextLevel ();											//cria uma variavel do tipo de level do monstro, que vai execultar o procedimento de pegar o level atual
			if(nextLevel != null){																		//se existir um proximo level
				return gameManager.Tropas >= nextLevel.tropas;											//vai retornar verdadeiro se o dinheiro atual for maior que o custo de evoluir para o proximo level.
			}
		}
		return false;																					//retorna falso, informando que não pode evoluir.
	}

	private void ShowTowerBuildTree(GameObject prefab){
		if (prefab != null) {
			activeBuildTree = Instantiate (prefab, canvas.transform);
			activeBuildTree.transform.position = Camera.main.WorldToScreenPoint (transform.position);
			activeBuildTree.GetComponent<BuildTree> ().SetPlaceTower(this);
		}
	}

	private void ShowTowerUpgradeTree(GameObject prefab){
		if (prefab != null) {			
			activeBuildTree = Instantiate (prefab, canvas.transform);
			activeBuildTree.transform.position = Camera.main.WorldToScreenPoint (transform.position);
			activeBuildTree.GetComponent<BuildTree> ().SetTower (tower);
			activeBuildTree.GetComponent<BuildTree> ().SetPlaceTower(this);
		}
	}


	private void CloseTowerBuildTree(){
		if (activeBuildTree != null) {
			Destroy (activeBuildTree);
		}
	}

	private void DisableAllTowers(){
		for (int i = 0; i < placesTower.Length; i++) {
			placesTower [i].CloseTowerBuildTree ();
		}
	}

	public void BuildTower(GameObject pTower){		
		if (pTower != null) {
			TowerData td = pTower.GetComponent<TowerData> ();
			if(td != null && canPlaceTower (td)) {																		//se puder botar monstro
				tower = (GameObject)Instantiate (pTower, transform.position, Quaternion.identity); 			//instancia o monstro definido no mosterprefab, no pasição e rotação do gameobject atual(slot)
				AudioSource audiosource = gameObject.GetComponent<AudioSource> (); 							//define uma variavel para o audio
				audiosource.PlayOneShot (audiosource.clip);													//toca o som de botar item
				gameManager.Tropas -= tower.GetComponent <TowerData> ().CurrentLevel.tropas;				//acessa a carteira do jogador e reduz o seu dinheiro, de acordo com o custo de compra do monstro.
			}
		}
		DisableAllTowers();
	}

	public void UpgradeTower(){
		if(canUpgradeTower ()) {																	//se ja tiver mosntro, verifica se pode evoluir
			tower.GetComponent <TowerData>().increaseLevel ();											//para o monstro atual, execulta o procedimento de incrementar o level
			AudioSource audiosource = gameObject.GetComponent<AudioSource> (); 							//define uma variavel para tratar o audio
			audiosource.PlayOneShot (audiosource.clip);													//toca o som de botar item
			gameManager.Tropas -= tower.GetComponent <TowerData> ().CurrentLevel.tropas;				//acessa a carteira do jogador e reduz o seu dinheiro, de acordo com o custo de melhoria do monstro.
		}
		DisableAllTowers();
	}

	public void DestroyTower(){
		if(tower != null){																				//se o local tiver algum monstro
			TowerData ta = tower.GetComponent <TowerData> ();											//cria uma variavel do tipo dados de monstro, que vai receber o monstro que estiver no slot
			int tropas = (int)ta.levels [ta.getCurrentLevel ()].tropas;
			gameManager.Tropas += (int)(tropas * 0.4);
			Destroy (this.tower.gameObject);
			this.tower = null;
			DisableAllTowers();
		}
	}
}
