using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	//esta classe manipula o menu principal
	//estas variaveis se referem as 3 telas que o menu tera
    public GameObject panelOpcoes;
	public GameObject panelMenu;
	public GameObject panelSelecao;
	public GameObject telaCarregando;
	public Image loadBar;
	public Text percentText;

    public void AbrirCena(string cena){					//este metodo vai carregar a cena de acordo com o valor passado em parametro pelo inspetor
		//telaCarregando.SetActive (true);
		//StartCoroutine (LevelCoroutine (cena));
		SceneManager.LoadScene(cena);
    }

    public void MeuNumero(int numero)  {						//este metodo contrala os paineis do menu se um ativo os outros 2 apagam
        switch (numero){                    //selecao == 1    //opcao == 2    //menu == 3
            case 1:
                panelOpcoes.SetActive(false);
                panelMenu.SetActive(false);
                panelSelecao.SetActive(true);
                //panel3 e 2 recebe ativo e panel 1 recebe !ativo
                break;
            case 2:
                panelOpcoes.SetActive(true);
                panelMenu.SetActive(false);
                panelSelecao.SetActive(false);
                //panel1 e 3 recebe ativo e panel 2 recebe !ativo
                break;
            case 3:
                panelOpcoes.SetActive(false);
                panelMenu.SetActive(true);
                panelSelecao.SetActive(false);
                //panel1 e 2 recebe ativo e panel 3 recebe !ativo
                break;
        }
    }

    public void Sair() {										
        Application.Quit();										//fechar o jogo
    }

    IEnumerator LevelCoroutine(string cena){
		AsyncOperation async = SceneManager.LoadSceneAsync (cena);	//Carrega a cena, de forma unica, isto é, deleta as outras cenas.
		while(!async.isDone){
			loadBar.fillAmount = async.progress / 0.9f;
			percentText.text = loadBar.fillAmount * 100 + "%";
			yield return null;
		}
    }
}
