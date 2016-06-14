using UnityEngine;
using System.Collections;

public class ManipulaBotoes : MonoBehaviour {
	//esta classe vai se responsabilizar por mostrar os botões certos dependendo do que o jogador va fazer (contruir ou melhorar/destruir) na torre.
	//estas variaveis receber os 3 botoes
	public GameObject btnContruir;
	public GameObject btnDestruir;
	public GameObject btnMelhorar;

	public void ModoConstruir(bool estado){	//este metodo vai ativar 1 botão e desativar os outros 2 e vice-versa
		btnContruir.SetActive (estado);
		btnDestruir.SetActive (!estado);
		btnMelhorar.SetActive (!estado);
	}
}
