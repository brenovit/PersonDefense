using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public GameObject panelOpcoes;
    public GameObject panelMenu;
    public GameObject panelSelecao;

    public void ChamaCenaFases(string cena){
        SceneManager.LoadScene(cena);
    }

    public void MeuNumero(int numero)  {
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

    public void bt_exit() {
        Application.Quit();
    }
}
