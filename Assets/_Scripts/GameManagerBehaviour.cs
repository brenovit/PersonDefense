using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerBehaviour : MonoBehaviour {

	public UIManager uimanager;
	public LevelManager levelmanager;

	[SerializeField]
	private int tropas;												//esta variavel vai difinir o dinheiro do jogador 
	[SerializeField]
	private int orda;												//esta variavel vai definir a wave atual
	[SerializeField]
	private int vida;												//variavel que vai receber a vida do jogador

	//public Text healthLabel;										//componente Text responsavel por exibir a vida do jogador
	public GameObject[] healthIndicator;							//vetor que vai receber os germes que comem o biscoito

	public static int gameState = 0;
	private PlaceTower[] placesTower;

	public int Tropas {
		get {
			return uimanager.GetTropas();
		}
		set {
			tropas = value;
			uimanager.SetTropas (tropas);
		}
	}

	public int Orda {
		get {
			return uimanager.GetOrda();
		}
		set {
			orda = value;
			uimanager.SetOrda (orda);
		}
	}

	public int Vida {
		get {
			return uimanager.GetVida();
		}
		set {
			vida = value;
			if (Vida <= 1) {
				GameOver ();
			} else {				
				uimanager.SetVida (vida);
			}
		}
	}

	public void GameWon(){
		EventManager.ExecutarEvento ("GameWon", null, "");
	}

	public void GameOver(){
		EventManager.ExecutarEvento ("GameOver", null, "");
	}
		

	void Start(){
		uimanager = FindObjectOfType<UIManager> ();

		if (uimanager == null)
			Debug.Log ("UIManager didn't find out");
		
		levelmanager = FindObjectOfType<LevelManager> ();

		uimanager.SetTropas (tropas);
		uimanager.SetVida (vida);

		placesTower = FindObjectsOfType<PlaceTower> ();
		Debug.Log("Founded "+placesTower.Length+" places tower");

		Time.timeScale = 1f;
	}

	public void DisableAllTowers(){
		foreach (var item in placesTower) {
			item.CloseTowerBuildTree ();			
		}
	}
}
