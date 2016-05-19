using UnityEngine;
using System.Collections;

public class CarregarCena : MonoBehaviour {

    private bool ativo = false;
    public GameObject panelOpcoes;
    public GameObject panelMenu;
    public GameObject panelSelecao;

    /*public void ChamaCenaFases(string cena){
         Application.LoadLevel(cena);
       }*/

    public void MeuNumero(int numero){
        if (ativo)
            ativo = false;
        else
            ativo = true;

        switch (numero){                    //selecao == 1    //opcao == 2    //menu == 3
            case 1:
                panelOpcoes.SetActive(ativo);
                panelMenu.SetActive(ativo);
                panelSelecao.SetActive(!ativo);
                //panel3 e 2 recebe ativo e panel 1 recebe !ativo
                break;
            case 2:
                panelOpcoes.SetActive(!ativo);
                panelMenu.SetActive(ativo);
                panelSelecao.SetActive(ativo);
                //panel1 e 3 recebe ativo e panel 2 recebe !ativo
                break;
            case 3:
                panelOpcoes.SetActive(ativo);
                panelMenu.SetActive(!ativo);
                panelSelecao.SetActive(ativo);
                //panel1 e 2 recebe ativo e panel 3 recebe !ativo
                break;
        }
    }
}
