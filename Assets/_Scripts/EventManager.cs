using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

[System.Serializable]
public class MyEvent : UnityEvent<GameObject, string>
{
}

public class EventManager : MonoBehaviour
{
	public static EventManager instancia;
	private Dictionary<string, MyEvent> eventos = new Dictionary<string,MyEvent> ();

	public void OnDestroy ()
	{
		instancia = null;
	}

	public static void CriarEvento (string nome, UnityAction<GameObject, string> evento)
	{
		if (instancia == null) {
			instancia = FindObjectOfType (typeof(EventManager)) as EventManager;
			if (instancia == null) {
				Debug.Log ("There is no EventManager");
				return;
			}
		}
		MyEvent esteEvento = null;
		if (instancia.eventos.TryGetValue (nome, out esteEvento)) {
			esteEvento.AddListener (evento);
		} else {
			esteEvento = new MyEvent ();
			esteEvento.AddListener (evento);
			instancia.eventos.Add (nome, esteEvento);
		}
	}

	public static void RemoverEvento (string nome, UnityAction<GameObject, string> evento)
	{
		if (instancia == null) {
			return;
		}
		MyEvent esteEvento = null;
		if (instancia.eventos.TryGetValue (nome, out esteEvento)) {
			esteEvento.RemoveListener (evento);
		}
	}

	public static void ExecutarEvento (string nome, GameObject obj, string param)
	{
		if (instancia == null) {
			return;
		}
		MyEvent esteEvento = null;
		if (instancia.eventos.TryGetValue (nome, out esteEvento)) {
			esteEvento.Invoke (obj, param);
		}
	}
}
