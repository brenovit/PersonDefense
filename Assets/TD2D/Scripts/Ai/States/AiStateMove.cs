using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows AI to operate move towards destination.
/// </summary>
public class AiStateMove : AiState
{
	[Space(10)]
    // End point for moving
    public Transform destination;
    // Go to this state if passive event occures
	public AiState passiveAiState;

    // Navigation agent of this gameobject
    NavAgent navAgent;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	public override void Awake()
	{
		base.Awake();
		navAgent = GetComponent<NavAgent>();
		Debug.Assert (navAgent, "Wrong initial parameters");
	}

    /// <summary>
    /// Raises the state enter event.
    /// </summary>
    /// <param name="previousState">Previous state.</param>
    /// <param name="newState">New state.</param>
	public override void OnStateEnter(AiState previousState, AiState newState)
    {
        // Set destination for navigation agent
        navAgent.destination = destination.position;
		// Start moving
		navAgent.move = true;
		navAgent.turn = true;
        if (anim != null)
        {
            // Play animation
			anim.SetTrigger("move");
        }
    }

    /// <summary>
    /// Raises the state exit event.
    /// </summary>
    /// <param name="previousState">Previous state.</param>
    /// <param name="newState">New state.</param>
	public override void OnStateExit(AiState previousState, AiState newState)
    {
		// Stop moving
		navAgent.move = false;
		navAgent.turn = false;
    }

    /// <summary>
    /// Fixed update for this instance.
    /// </summary>
    void FixedUpdate()
    {
        // If destination reached
        if ((Vector2)transform.position == (Vector2)destination.position)
        {
            // Look at required direction
            navAgent.LookAt(destination.right);
            // Go to passive state
            aiBehavior.ChangeState(passiveAiState);
        }
    }
}
