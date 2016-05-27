using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour {

	public GameObject PainelMenu;
	public Button btnPause;

	public Text lblInfo;
	public bool jogoPausado = false;

	public static PauseSystem instance = null;

	void Awake (){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}

	public void PauseJogo(){
		jogoPausado = true;
		lblInfo.text = "Jogo Pausado";
		Time.timeScale = 0;
		PainelMenu.SetActive (true);
		btnPause.interactable = false;
	}

	public void RetomaJogo() {
		jogoPausado = false;
		Time.timeScale = 1;
		PainelMenu.SetActive (false);
		btnPause.interactable = true;
	}

	public void ReiniciarCena(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void IrMenu(){
		SceneManager.LoadScene ("Menu");
	}
}
