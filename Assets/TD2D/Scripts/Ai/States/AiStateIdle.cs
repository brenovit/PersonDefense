using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows AI to operate idle state.
/// </summary>
public class AiStateIdle : AiState
{
    /// <summary>
    /// Raises the state enter event.
    /// </summary>
    /// <param name="previousState">Previous state.</param>
    /// <param name="newState">New state.</param>
	public override void OnStateEnter(AiState previousState, AiState newState)
    {
		if (anim != null)
		{
			// Play animation
			anim.SetTrigger("idle");
		}
    }
}
