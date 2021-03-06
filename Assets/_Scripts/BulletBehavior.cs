﻿using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour {

	[SerializeField]	private float velocidade = 10;			//velocidade que a bala vai se movimentar
	public GameObject efeito;
	private int dano;					//dano que a bala vai causar
	public GameObject alvo;				//alvo da bala
	public Vector3 posicaoInicial;		//posição inicial da bala
	public Vector3 posicaoAlvo;			//posição do alva da bala

	private float  distancia;			//vai medir a distancia entre o inimigo e a torreia
	private float tempoInicio;			//tempo que ela vai iniciar
	
	private GameManagerBehaviour gameManager;		//este Objeto vai tratar de alterar os dados do player na classe gameObject

	public int Dano {
		get{return dano;}
		set{dano = value;}		
	}

	void Start () {
		tempoInicio = Time.time;											//a variavel tempoInicio vai receber o tempo de inicio da bala
		distancia = Vector3.Distance (posicaoInicial, posicaoAlvo);			//esta variavel vai receber a distancia entre a posição inicial da bala e do alvo
		gameManager = FindObjectOfType<GameManagerBehaviour>();				//o objeto gameManager vai receber o GameObject gm, passando o seu componente GameManagerBehaviour
	}

	void Update () {

		float intervaloTempo = Time.time - tempoInicio;						//instavelo de tempo para a proxima bala sair
		gameObject.transform.position = 									//a posição da bala vai ser alterada de acordo com a 
			Vector3.Lerp(posicaoInicial, posicaoAlvo, 						//interpolação linear entre dois pontosl evando em consideração o tempo, 
			intervaloTempo * velocidade / distancia);						//que neste caso traduz o intervalo que a bala sai, vezes a velocidade dividido pela distancia
		if (gameObject.transform.position.Equals(posicaoAlvo)) {			//se a posição da bala for igual a posição do inimigo
			if (alvo != null) {												//se o alvo não for nulo
				Mosquito inimigo = alvo.GetComponent<Mosquito> ();			//Cria-se um objeto do tipo mosquito recebendo o compoente Mosquito presente no gameObject alvo 
				inimigo.RecebeuDano (dano);									//esse objeto executa o metodo recebeuDano
				Instantiate (efeito,gameObject.transform.position,Quaternion.identity);	//instancia o efeito de explosão
			}
			Destroy(gameObject);											//por fim detroi a bala
		}	
	}
}
