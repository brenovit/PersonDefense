using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerBehaviour : MonoBehaviour {

	public UIManager uimanager;
	public LevelManager levelmanager;

	//public Text tropasLabel;										//esta variavel vai receber um componente Text, responsavel por exibir o dinheiro do jogador
	[SerializeField]private int tropas;								//esta variavel vai difinir o dinheiro do jogador 

	//public Text waveLabel;											//componente Text, responsavel pela contagem da wave
	public GameObject[] nextWaveLabels;								//este vetor vai definir 2 elementos para a animação do next waves
	public int horda;												//esta variavel vai definir a wave atual

	//public Text healthLabel;										//componente Text responsavel por exibir a vida do jogador
	public GameObject[] healthIndicator;							//vetor que vai receber os germes que comem o biscoito
	[SerializeField]private int health;								//variavel que vai receber a vida do jogador

	public static int gameState = 0;

	public int Tropas {												//retornar ou definir o valor do dinheiro(encapsulamento)
		get{
			return tropas;											//retorna o valor do dinheiro
		}
		set{ 
			tropas = value;											//definir que um dinheiro é uma variavel do tipo de valor; --duvida
		}
	}

	public int Orda {												//encapsulamento da variavel wave;
		get{
			return horda;
		}
		set {
			horda = value;											//define que a variavel wave vai receber um valor
			if(gameState == 0){											
				for(int i = 0; i < nextWaveLabels.Length; i++){		//se o jogo não acabar
					nextWaveLabels [i].GetComponent <Animator> ().SetTrigger ("nextWave");	//executar a animação de nextWave
				}
			}		
		}
	}

	public int Health {
		 get{
			return health;
		 }
		 set{		 	
			health = value;											//a vida recebe um valor

			if(health <= 0){										//se a vida for menor ou igual a zero
				gameState = 1;										//o estado do jogo muda para 1 -> perdeu
				Time.timeScale = 0.2f;								//o jogo fica mais lento
				GameObject gameOverText = GameObject.FindGameObjectWithTag ("GameOver");
				gameOverText.GetComponent<Animator> ().SetBool ("gameOver", true);
			}

			for(int i = 0; i < healthIndicator.Length; i++){		//procura os germes comendo biscoito
				if(i < health){										//se o valor de i for menor que a vida
					healthIndicator [i].SetActive (true);			//deixa o germe
				} else {											//senão
					healthIndicator [i].SetActive (false);			//apaga o germe.
				}
			}
		 }
	}

	public void Venceu(){
		gameState = 2;									//ativa o game over do sistema
		GameObject gameOverText = GameObject.FindGameObjectWithTag ("GameWon");		//procura o objeto com a tag GamoWon
		gameOverText.GetComponent<Animator> ().SetBool ("gameOver",true);			//inicia a animação do texto de game over
	}

	void Awake(){
		//uimanager = FindObjectOfType<UIManager> ();
	}

	void Start () {
		Time.timeScale = 1f;
		Orda = 0;
	}
}
