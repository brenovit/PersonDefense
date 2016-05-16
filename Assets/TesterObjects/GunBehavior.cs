using UnityEngine;
using System.Collections;

public class GunBehavior : MonoBehaviour {
	public Transform boca;
	public Bullet bala;
	public float cadencia;
	public GameObject alvo;
	private float tempoUltimoTiro;
	// Use this for initialization
	void Start () {

	}

	void Update(){
		if(alvo != null){														//se o alvo não for  nulo
			if(Time.time - tempoUltimoTiro > cadencia){	//se o tempo atual menos o tmepo do ultimo tiro for menor que a cadencia do monstro
				Atira (alvo);						//executa o método de atirar, passando o collider do inimigo
				tempoUltimoTiro = Time.time;										//o tempo do ultimo tiro receber o segundo atual
			}

			Vector3 direction = 												//cria-se um vector3d que vai passar a direção da bala
				gameObject.transform.position - alvo.transform.position;		//recebendo a posição atual do monstro menos a posição do inimigo
		}
	}

	public void Atira (GameObject alvo){

		bala = Instantiate (bala);
		bala.alvo = alvo;
	}

}
