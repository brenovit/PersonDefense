using UnityEngine;
using System.Collections;

namespace InGame{
	public class Mosquito : MonoBehaviour {
		//esta classe vai centralizar todos os dados do mosquito
		public float vida = 100f;
		[SerializeField]	private float velocidade = 2f;
		//[SerializeField]	private float resistencia = 2f;
		[SerializeField]	private int recompensa = 10;
		[SerializeField]	private GameObject blood  = null;								//efeito de sangue
		private HealthBar barraVida;														//Criase uma variavel do tipo HealthBar

		private GameManagerBehaviour gameManager;											//este Objeto vai tratar de alterar os dados do player na classe gameObject

		void Start () {
			MoveEnemy andador = this.gameObject.GetComponent<MoveEnemy> ();					//cria-se um objeto do tipo move enemy, 
			andador.Speed = velocidade;														//para definir sua velocidade de acordo com o valor da variavel velocidade
			barraVida = this.gameObject.transform.FindChild ("HealthBar")					//2- GameObject filho deste gameObject na hierarchy com o nome de "HealthBar"
				.GetComponent<HealthBar> ();												//1- a barra de vida recebe o componente healthbar presente no

			gameManager = GameObject.FindGameObjectWithTag("GameManager")					//2- GameObject na hierarchy com o nome GameManager
				.GetComponent<GameManagerBehaviour>();										//1- o gameManager o componente GameManagerBehaviour presente no
		}                                                                                	

		public void RecebeuDano(int dano){
			//logica de tirar vida do inimigo
			this.vida -= dano;																//reduz a vida
			barraVida.AlteraVida (vida);													//altera a barra de vida
			if (vida <= 0)																	//checa se a vida é menor ou igual a 0
				Morreu ();																	//mata o mosquito
		}

		private void Morreu(){
			AudioSource audioSource = this.gameObject.GetComponent<AudioSource>();			//um som que esta presente no inimgio
			AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);				//é tocado
						
			gameManager.Tropas += recompensa;												//o jogador aumenta o dinheiro de ocordo com o valor da remponsa
			Destroy (gameObject);															//destroi este gameObject
			if(blood != null)																//se o efeito de sangue não for vazio
				Instantiate (blood,gameObject.transform.position,Quaternion.identity);		//instancia o efeito
		}
	}
}
