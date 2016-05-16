using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	private float vidaMaxima;			//armazena a vida maxima do inimigo
	private float originalScale;		//armazena o tamanho original da barra de vida

	// Use this for initialization
	void Start () {
		Mosquito mosquito = GetComponentInParent<Mosquito> ();
		originalScale = gameObject.transform.localScale.x;			//o tamanho original da vida vai ser igual a scale local do objeto definido no editor
		vidaMaxima = mosquito.vida;
	}

	public void AlteraVida(float vidaAtual){
		Vector3 tmpScale = gameObject.transform.localScale;			//cria-se um vector que vai conter a escala temporaria da barra de vida
		tmpScale.x = vidaAtual / vidaMaxima * originalScale;		//defini que o tamanho deste vector em x, igual ao resultado da seguinte expressão
												 					//valor do parametro vidaAtual dividido pelo valor da vidamaxima multiplicado pelo tamanho original
		gameObject.transform.localScale = tmpScale;					//por fim defini que a escala atual da barra de vida, será igual a escala temporaria;
	}	
}
