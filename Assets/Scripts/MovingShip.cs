using UnityEngine;
using System.Collections;

public class MovingShip : MonoBehaviour {
	//esta classe vai mover o navio paa um lado e outro.
	[SerializeField]
	GameObject ship = null;									//navio

	[SerializeField]
	Transform startTransform = null;						//posição inicial

	[SerializeField]	
	Transform endTransform = null;							//destino

	[SerializeField]
	float platformSpeed = 0f;								//velocidade que vai se mover

	Vector3 direction;										//direção que vai se mover
	Transform destination;									//destino que tem de se mover
	bool shipHere = false;									//verificador da existencia de um navio

	void FixedUpdate(){										//vai executar o tempo todo
		if (ship == null){									//se não tiver navio
			ship = GameObject.FindGameObjectWithTag ("Ship");	//procura o gameobject com a tag ship
			shipHere = true;								//diz que o navio ta aqui
			return;
		}
		if(shipHere){										//se o navio estiver presente
			SetDestination(startTransform);					//informa que o destino dele sera o local da variavel startTransform
			shipHere = false;								//diz que o navio não esta mais aqui
		}
		ship.GetComponent<Rigidbody>().MovePosition(ship.transform.position + direction * platformSpeed * Time.fixedDeltaTime);
		//move o corpo do navio de acordo com a direção, a velocidade, e uma variação fixa de tempo

		if(Vector3.Distance (ship.transform.position, destination.position) < platformSpeed * Time.fixedDeltaTime){
			SetDestination(destination == startTransform ? endTransform : startTransform);
		}
	}

	void SetDestination(Transform dest){
		destination = dest;
		direction = (destination.position - ship.transform.position).normalized;
	}

}
