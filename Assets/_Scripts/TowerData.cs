using UnityEngine;
using System.Collections;
using System.Collections.Generic;										//usar lista

[System.Serializable]													//permite que as variaveis abaixo possam ser alteradas pelo editor
public class TowerLevel {												//esta clase define que cada mosntro ao ser atualizado tera um custo e uma aparencia.
	public GameObject visualizacao;										//aparencia do monstro
	public float cadencia;												//cadencia de tiro
	public int dano;
	public float campoVisao;											//Campo de visão da torre
	public int tropas;													//custo do monstro
	public Transform muzzle;
}

public class TowerData : MonoBehaviour {								//classe de dados do monstro
	public string nome;
	public GameObject bala;												//bala que o monstro vai atirar
	[SerializeField]private CircleCollider2D raio;						//Collider que vai especificar o campo de visão da torre
	public List<TowerLevel> levels;										//cria uma lista para os levels dos monstros
	private TowerLevel currentLevel;									//cria uma variavel que vai tratar o level atual do mosntro

	public TowerLevel CurrentLevel {									//criamos um comportamento para retornar ou definir um level para o monstro
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

	void Start(){														
		if(raio != null)												//se não tiver um raio
			raio = gameObject.GetComponent<CircleCollider2D> ();		//procura no gameObject o componente CircleCollider2D e atribui ao raio
	}

	public int getCurrentLevel(){										//retorna o level atual da torre
		int currentLevelIndex = levels.IndexOf (currentLevel);
		return currentLevelIndex;
	}

	public TowerLevel getNextLevel(){									//procedimento do tipo do Monsterlevel que vai pegar o level atual do monstro
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
			if(raio != null)											//se o raio não for nulo
				raio.radius = CurrentLevel.campoVisao;					//aumenta o valor do raio do CircleCollider2D de acordo com o valor do campo de visão da torre
		}
	}

	void OnEnable(){													//quando o sitema for ativado
		CurrentLevel = levels [0];										//o level atual será igual a lista de levels na posição 0
	}

	/*void OnDrawGizmosSelected(){
		Vector3 position = transform.position;
		position.z = 2;
		UnityEditor.Handles.color = Color.blue;
		UnityEditor.Handles.DrawWireDisc (position,transform.forward,raio.radius);
	}*/
}
