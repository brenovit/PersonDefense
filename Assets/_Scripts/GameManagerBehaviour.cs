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
	private int vida;								//variavel que vai receber a vida do jogador

	//public Text healthLabel;										//componente Text responsavel por exibir a vida do jogador
	public GameObject[] healthIndicator;							//vetor que vai receber os germes que comem o biscoito

	public static int gameState = 0;

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
			uimanager.SetVida (vida);
		}
	}

	public void Venceu(){
		gameState = 2;									//ativa o game over do sistema
		//GameObject gameOverText = GameObject.FindGameObjectWithTag ("GameWon");		//procura o objeto com a tag GamoWon
		//gameOverText.GetComponent<Animator> ().SetBool ("gameOver",true);			//inicia a animação do texto de game over
	}

	void Start(){
		uimanager = FindObjectOfType<UIManager> ();
		if (uimanager == null)
			Debug.Log ("UIManager didn't find out");
		Time.timeScale = 1f;

		uimanager.SetTropas (tropas);
		uimanager.SetVida (vida);
	}
}
