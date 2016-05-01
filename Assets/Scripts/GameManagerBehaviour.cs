using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerBehaviour : MonoBehaviour {

	public Text goldLabel;
	private int gold;

	public Text waveLabel;
	public GameObject[] nextWaveLabels;
	public int wave;

	public Text healthLabel;
	public GameObject[] healthIndicator;
	private int health;

	public bool gameOver = false;

	public int Gold {					//retornar ou definir o valor do dinheiro
		get{
			return gold;				//retorna o valor do dinheiro
		}
		set{ 
			gold = value;				//definir que um dinheiro é uma variavel do tipo de valor; --duvida
			goldLabel.GetComponent <Text>().text = "GOLD: " + gold;	//altera a label de dinheiro no jogo;
		}
	}

	public int Wave {
		get{
			return wave;
		}
		set {
			wave = value;
			if(!gameOver){
				for(int i = 0; i < nextWaveLabels.Length; i++){
					nextWaveLabels [i].GetComponent <Animator> ().SetTrigger ("nextWave");
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
