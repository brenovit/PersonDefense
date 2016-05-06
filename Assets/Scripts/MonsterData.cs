using UnityEngine;
using System.Collections;
using System.Collections.Generic;										//usar lista

[System.Serializable]													//permite que as variaveis abaixo possam ser alteradas pelo editor
public class MonsterLevel {												//esta clase define que cada mosntro ao ser atualizado tera um custo e uma aparencia.
	public int cost;													//custo do monstro
	public GameObject visualizacao;										//aparencia do monstro
	public GameObject bullet;											//bala que o monstro vai atirar
	public float fireRate;												//cadencia de tiro
}

public class MonsterData : MonoBehaviour {								//classe de dados do monstro
	public List<MonsterLevel> levels;									//cria uma lista para os levels dos monstros
	private MonsterLevel currentLevel;									//cria uma variavel que vai tratar o level atual do mosntro

	public MonsterLevel CurrentLevel {									//criamos um comportamento para retornar ou definir um level para o monstro
		get {															//retornar o level
			return currentLevel;										//retorna o level atual
		}
		set {															//definir um level
			currentLevel = value;										//definir um valor para o level atual
			int currentLevelIndex = levels.IndexOf (currentLevel);		//variavel que vai receber o valor do indice do level atual do monstro

			GameObject levelVizualizationn = 							//este gameobject vai ter sua aparecia alterada pela
							levels [currentLevelIndex].visualizacao;	//a aparencia que esta definida no atributo da classe de levels do monstro
			for(int i = 0; i < levels.Count; i++){						//laço que vai até o ultimo level definido (item da lista)
				if(levelVizualizationn != null) {						//se o gameobject de aparece não estiver vazio
					if(i == currentLevelIndex){							//se o valor da volta do laço 'i' for igual ao indice do level atual do monstro
						levels [i].visualizacao.SetActive (true);		//o item da lista na posição 'i', tera sua aparecia ativada, 
					} else {
						levels [i].visualizacao.SetActive (false);		//o item da lista na posição 'i', tera sua aparecia desativada, 
					}
				}
			}
		}
	}

	public  MonsterLevel getNextLevel(){								//procedimento do tipo do Monsterlevel que vai pegar o level atual do monstro
		int currentLevelIndex = levels.IndexOf (currentLevel);			//variavel do tipo inteiro que vai receber o indice do level atual do monstro
		int maxLevelIndex = levels.Count - 1;							//variavel que vai definir o level maximo do monstro de acordo com a quantidade especificada no inspetor menos 1
		if (currentLevelIndex < maxLevelIndex){							//se o level atual for menor que o level maximo
			return levels [currentLevelIndex + 1];						//retorna o valor que estiver na lista de levels na posição do level atual +1
		} else {														//se o level não for menor
			return null;												//retorna vazio
		}
	}

	public void increaseLevel(){										//procedimento que vai aumentar o level do monstro
		int currentLevelIndex = levels.IndexOf (currentLevel);			//variavel do tipo inteiro que vai receber o indice do level atual do monstro
		if (currentLevelIndex < levels.Count - 1){						//se o indice do level atual for menor que a quantidade de levels - 1
			CurrentLevel = levels[currentLevelIndex + 1];				//o level atual do monstro é aumentado de acordo com o valor que estiver na lista com indice + 1
		}
	}

	void OnEnable(){													//quando o sitema for ativado
		CurrentLevel = levels [0];										//o level atual será igual a lista de levels na posição 0
	}
}
