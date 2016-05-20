using UnityEngine;
using System.Collections;

public class ManipulaBotoes : MonoBehaviour {

	public GameObject btnContruir;
	public GameObject btnDestruir;
	public GameObject btnMelhorar;

	public void ModoConstruir(bool estado){
		btnContruir.SetActive (estado);
		btnDestruir.SetActive (!estado);
		btnMelhorar.SetActive (!estado);
	}
}
