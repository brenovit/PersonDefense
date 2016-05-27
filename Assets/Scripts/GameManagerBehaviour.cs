using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerBehaviour : MonoBehaviour {

	public Text tropasLabel;											//esta variavel vai receber um componente Text, responsavel por exibir o dinheiro do jogador
	[SerializeField]private int tropas;												//esta variavel vai difinir o dinheiro do jogador 

	public Text waveLabel;											//componente Text, responsavel pela contagem da wave
	public GameObject[] nextWaveLabels;								//este vetor vai definir 2 elementos para a animação do next waves
	public int orda;												//esta variavel vai definir a wave atual

	public Text healthLabel;										//componente Text responsavel por exibir a vida do jogador
	public GameObject[] healthIndicator;							//vetor que vai receber os germes que comem o biscoito
	[SerializeField]private int health;												//variavel que vai receber a vida do jogador

	public static int gameState = 0;

	public int Tropas {												//retornar ou definir o valor do dinheiro(encapsulamento)
		get{
			return tropas;											//retorna o valor do dinheiro
		}
		set{ 
			tropas = value;											//definir que um dinheiro é uma variavel do tipo de valor; --duvida
			tropasLabel.text = "TROPAS: " + tropas;					//altera a label de dinheiro no jogo;
		}
	}

	public int Orda {												//encapsulamento da variavel wave;
		get{
			return orda;
		}
		set {
			orda = value;											//define que a variavel wave vai receber um valor
			if(gameState == 0){											
				for(int i = 0; i < nextWaveLabels.Length; i++){		//se o jogo não acabar
					nextWaveLabels [i].GetComponent <Animator> ().SetTrigger ("nextWave");	//escutar a animação de nextWave
				}
			}
			waveLabel.text = "ORDA: " + (orda + 1);
		}
	}

	public int Health {
		 get{
			return health;
		 }
		 set{
		 	if(value < health){
		 		//se o valor infomador for menor do que o valor da vida atual;
		 	}

			health = value;

			healthLabel.text = "PESQUISADORES: " + health;

			if(health <= 0){
				gameState = 1;
				Time.timeScale = 0.2f;
				GameObject gameOverText = GameObject.FindGameObjectWithTag ("GameOver");
				gameOverText.GetComponent<Animator> ().SetBool ("gameOver", true);
			}

			for(int i = 0; i < healthIndicator.Length; i++){
				if(i < health){
					healthIndicator [i].SetActive (true);
				} else {
					healthIndicator [i].SetActive (false);
				}
			}
		 }
	}

	public void Venceu(){
		gameState = 2;									//ativa o game over do sistema
		GameObject gameOverText = GameObject.FindGameObjectWithTag ("GameWon");		//procura o objeto com a tag GamoWon
		gameOverText.GetComponent<Animator> ().SetBool ("gameOver",true);			//inicia a animação do texto de game over
	}

	void Start () {
		Time.timeScale = 1f;
		Orda = 0;
		waveLabel.text = "ORDA: " + (orda + 1);
		tropasLabel.text = "TROPAS: " + tropas;					//altera a label de dinheiro no jogo;
		healthLabel.text = "PESQUISADORES: " + health;
	}
}
