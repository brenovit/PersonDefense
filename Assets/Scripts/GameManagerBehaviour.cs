using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerBehaviour : MonoBehaviour {

	public Text goldLabel;											//esta variavel vai receber um componente Text, responsavel por exibir o dinheiro do jogador
	private int gold;												//esta variavel vai difinir o dinheiro do jogador 

	public Text waveLabel;											//componente Text, responsavel pela contagem da wave
	public GameObject[] nextWaveLabels;								//este vetor vai definir 2 elementos para a animação do next waves
	public int wave;												//esta variavel vai definir a wave atual

	public Text healthLabel;										//componente Text responsavel por exibir a vida do jogador
	public GameObject[] healthIndicator;							//vetor que vai receber os germes que comem o biscoito
	private int health;												//variavel que vai receber a vida do jogador

	public bool gameOver = false;

	public int Gold {												//retornar ou definir o valor do dinheiro(encapsulamento)
		get{
			return gold;											//retorna o valor do dinheiro
		}
		set{ 
			gold = value;											//definir que um dinheiro é uma variavel do tipo de valor; --duvida
			goldLabel.GetComponent <Text>().text 
											= "GOLD: " + gold;		//altera a label de dinheiro no jogo;
		}
	}

	public int Wave {												//encapsulamento da variavel wave;
		get{
			return wave;
		}
		set {
			wave = value;											//define que a variavel wave vai receber um valor
			if(!gameOver){											
				for(int i = 0; i < nextWaveLabels.Length; i++){		//se o jogo não acabar
					nextWaveLabels [i].GetComponent <Animator> ().SetTrigger ("nextWave");	//escutar a animação de nextWave
				}
			}
			waveLabel.text = "WAVE: " + (wave + 1);
		}
	}

	public int Health {
		 get{
			return health;
		 }
		 set{
		 	if(value < health){
		 		Camera.main.GetComponent<CameraShake>().Shake ();
		 	}

			health = value;

			healthLabel.text = "HEALTH: " + health;

			if(health <= 0 && !gameOver){
				gameOver = true;
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

	void Start () {
		Gold = 100;
		Wave = 0;
		Health = 5;
	}
}
