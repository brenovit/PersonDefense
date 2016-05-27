using UnityEngine;
using System.Collections;

public class Mosquito : MonoBehaviour {
	public float vida = 100f;
	[SerializeField]	private float velocidade = 2f;
	//[SerializeField]	private float resistencia = 2f;
	[SerializeField]	private int recompensa = 10;
	[SerializeField]	private GameObject blood  = null;
	private HealthBar barraVida;														//Criase uma variavel do tipo HealthBar

	private GameManagerBehaviour gameManager;											//este Objeto vai tratar de alterar os dados do player na classe gameObject

	void Start () {
		MoveEnemy andador = this.gameObject.GetComponent<MoveEnemy> ();
		andador.Speed = velocidade;
		barraVida = this.gameObject.transform.FindChild ("HealthBar")					//2- GameObject filho deste gameObject na hierarchy com o nome de "HealthBar"
			.GetComponent<HealthBar> ();												//1- a barra de vida recebe o componente healthbar presente no

		gameManager = GameObject.FindGameObjectWithTag("GameManager")
			.GetComponent<GameManagerBehaviour>();										//2- GameObject na hierarchy com o nome GameManager
	}                                                                                	//1- o gameManager o componente GameManagerBehaviour presente no

	public void RecebeuDano(int dano){
		//logica de tirar vida do inimigo
		this.vida -= dano;																//reduz a vida
		barraVida.AlteraVida (vida);													//altera a barra de vida
		if (vida <= 0)																	//checa se morreu
			Morreu ();
	}

	private void Morreu(){
		AudioSource audioSource = this.gameObject.GetComponent<AudioSource>();			//um som que esta presente no inimgio
		AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);				//é tocado
					
		gameManager.Tropas += recompensa;												//o jogador aumenta o dinheiro de ocordo com o valor da remponsa
		Destroy (gameObject);															//destroi este gameObject
		if(blood != null)
			Instantiate (blood,gameObject.transform.position,Quaternion.identity);
	}

	void Update(){
		barraVida.AlteraVida (vida);
		if (vida < 1)
			Morreu ();
	}
}

