using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Button handler.
/// </summary>
public class ButtonHandler : MonoBehaviour
{
	/// <summary>
	/// Buttons pressed.
	/// </summary>
	/// <param name="buttonName">Button name.</param>
	public void ButtonPressed(string buttonName)
	{
		// Send global event about button preesing
		EventManager.TriggerEvent("ButtonPressed", gameObject, buttonName);
	}
}
