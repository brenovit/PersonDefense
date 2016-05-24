﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerBehaviour : MonoBehaviour {

	public Text tropasLabel;											//esta variavel vai receber um componente Text, responsavel por exibir o dinheiro do jogador
	private int tropas;												//esta variavel vai difinir o dinheiro do jogador 

	public Text waveLabel;											//componente Text, responsavel pela contagem da wave
	public GameObject[] nextWaveLabels;								//este vetor vai definir 2 elementos para a animação do next waves
	public int orda;												//esta variavel vai definir a wave atual

	public Text healthLabel;										//componente Text responsavel por exibir a vida do jogador
	public GameObject[] healthIndicator;							//vetor que vai receber os germes que comem o biscoito
	private int health;												//variavel que vai receber a vida do jogador

	public bool gameOver = false;

	public int Tropas {												//retornar ou definir o valor do dinheiro(encapsulamento)
		get{
			return tropas;											//retorna o valor do dinheiro
		}
		set{ 
			tropas = value;											//definir que um dinheiro é uma variavel do tipo de valor; --duvida
			tropasLabel.text = "TROPAS: " + tropas;		//altera a label de dinheiro no jogo;
		}
	}

	public int Orda {												//encapsulamento da variavel wave;
		get{
			return orda;
		}
		set {
			orda = value;											//define que a variavel wave vai receber um valor
			if(!gameOver){											
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
		 		Camera.main.GetComponent<CameraShake>().Shake ();
		 	}

			health = value;

			healthLabel.text = "PESQUISADORES: " + health;

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
		Tropas = 100;
		Orda = 0;
		Health = 5;
	}
}
