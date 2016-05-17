using UnityEngine;
using System.Collections;

public class EnemyDestructionDelegate : MonoBehaviour {
	/// <summary>
	/// Utilize delegates quando você quer um objeto de jogo notifique ativamente outros objetos do jogo sobre alterações
	/// Servindo como um ponteiro para a função
	/// </summary>
	public delegate void EnemyDelegate(GameObject inimigo);	//delegate sem retorno, recebendo um gameobject como parametro

	public EnemyDelegate enemyDelegate;						//ponteiro para um método

	void OnDestroy(){										//metodo o qual o delegate vai chammar(apontar)
		if (enemyDelegate != null)							//se o ponteiro não estiver vazio
			enemyDelegate (gameObject);						//ele recebe o game object passoado coomo parametro
	}
}
