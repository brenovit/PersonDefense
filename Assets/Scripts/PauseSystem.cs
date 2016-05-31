using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour {

	public GameObject PainelMenu;					//painel de pause
	public Button btnPause;							//botão de pause

	public Text lblInfo;							//label de informação
	public bool jogoPausado = false;				//variavel que define se o jogo ta pausado ou não

	public static PauseSystem instance = null;		//Singleton

	void Awake (){									//este método vai fazer com que só exista apenas 1 objeto deste no sistema
		if (instance == null) {						//se não existir
			instance = this;						//ele cria
		} else if (instance != this) {				//se ja existir
			Destroy (gameObject);					//destroy este
		}
	}

	public void PauseJogo(){						//este metodo vai pausar o jogo
		jogoPausado = true;							//altera a variavel do jogo
		lblInfo.text = "Jogo Pausado";				
		Time.timeScale = 0;							//altera o tempo de execução para 0
		PainelMenu.SetActive (true);				//mostra o painel de pause
		btnPause.interactable = false;				//desbalita o botão de pause
	}

	public void RetomaJogo() {						//este metodo vai fazer o jogo voltar ao normal
		jogoPausado = false;
		Time.timeScale = 1;
		PainelMenu.SetActive (false);
		btnPause.interactable = true;
	}

	public void ReiniciarCena(){					//este metodo reinicia a cena atual
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void IrMenu(){							//este metodo muda para a cena do menu
		SceneManager.LoadScene ("Menu");
	}
}
