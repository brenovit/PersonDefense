using UnityEngine;
using System.Collections;

namespace InGame{
	[System.Serializable]
	public class Wave {						//classe responsavel por conter os dados das ondas
		public GameObject enemyPrefab;		//objeto que será instanciado em jogo na onda
		public float spawnInterval = 2;		//intervalo de tempo que cada objeto será instanciado
		public int maxEnemies = 20;			//quantidade maxima de objeto que será instancia no intervalo de tempo.
	}
}
