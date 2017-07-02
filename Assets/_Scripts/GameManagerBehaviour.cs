using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerBehaviour : MonoBehaviour {

	public UIManager uimanager;
	public LevelManager levelmanager;

	public int tropas;												//esta variavel vai difinir o dinheiro do jogador 
	public int orda;												//esta variavel vai definir a wave atual
	public int vida;								//variavel que vai receber a vida do jogador

	//public Text healthLabel;										//componente Text responsavel por exibir a vida do jogador
	public GameObject[] healthIndicator;							//vetor que vai receber os germes que comem o biscoito

	public static int gameState = 0;

	public void Venceu(){
		gameState = 2;									//ativa o game over do sistema
		//GameObject gameOverText = GameObject.FindGameObjectWithTag ("GameWon");		//procura o objeto com a tag GamoWon
		//gameOverText.GetComponent<Animator> ().SetBool ("gameOver",true);			//inicia a animação do texto de game over
	}

	void Awake(){
		UIManager[] uis = Resources.FindObjectsOfTypeAll<UIManager> ();
		foreach (UIManager ui in uis) {
			if(ui.CompareTag("UIManager")){
				uimanager = ui;
			}
		}
		//uimanager = FindObjectOfType<UIManager> ();
	}

	void Start () {
		Time.timeScale = 1f;
		orda = 0;
	}
}
