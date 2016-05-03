using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	public float maxHealth = 100;		//armazena a vida maxima do inimigo
	public float currentHealth = 100;	//armazena a vida atual do inimigo
	private float originalScale;		//armazena o tamanho original da barra de vida

	// Use this for initialization
	void Start () {
		originalScale = gameObject.transform.localScale.x;			//o tamanho original da vida vai ser igual a scale local do objeto definido no editor
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tmpScale = gameObject.transform.localScale;			//cria-se um vector que vai conter a escala temporaria da barra de vida
		tmpScale.x = currentHealth / maxHealth * originalScale;		//defini que o tamanho deste vector em x, é igual a vida atual do inimigo dividida pela vida maxima vezes o tamanho original
		gameObject.transform.localScale = tmpScale;					//por fim defini que a escala atual da barra de vida, será igual a escala temporaria;
	}		
}
