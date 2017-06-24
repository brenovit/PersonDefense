using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows AI to operate move towards destination.
/// </summary>
public class AiStateMove : MonoBehaviour, IAiState
{
    // End point for moving
    public Transform destination;
    // Go to this state if agressive event occures
    public string agressiveAiState;
    // Go to this state if passive event occures
    public string passiveAiState;

    // Animation controller for this AI
    private Animation anim;
    // AI behavior of this object
    private AiBehavior aiBehavior;
    // Navigation agent of this gameobject
    NavAgent navAgent;

    /// <summary>
    /// Awake this instance.
    /// </summary>
    void Awake ()
    {
        aiBehavior = GetComponent<AiBehavior>();
        navAgent = GetComponent<NavAgent>();
        anim = GetComponentInParent<Animation>();
        Debug.Assert (aiBehavior && navAgent, "Wrong initial parameters");
    }

    /// <summary>
    /// Raises the state enter event.
    /// </summary>
    /// <param name="previousState">Previous state.</param>
    /// <param name="newState">New state.</param>
    public void OnStateEnter (string previousState, string newState)
    {
        // Set destination for navigation agent
        navAgent.destination = destination.position;
        if (anim != null)
        {
            // Start moving
            navAgent.move = true;
            // Play animation
            anim.Play("Move");
        }
    }

    /// <summary>
    /// Raises the state exit event.
    /// </summary>
    /// <param name="previousState">Previous state.</param>
    /// <param name="newState">New state.</param>
    public void OnStateExit (string previousState, string newState)
    {
        if (anim != null)
        {
            // Stop moving
            navAgent.move = false;
            // Stop animation
            anim.Stop();
        }
    }

    /// <summary>
    /// Fixed update for this instance.
    /// </summary>
    void FixedUpdate ()
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

    /// <summary>
    /// Triggers the enter.
    /// </summary>
    /// <param name="my">My.</param>
    /// <param name="other">Other.</param>
    public void TriggerEnter(Collider2D my, Collider2D other)
    {

    }

    /// <summary>
    /// Triggers the stay.
    /// </summary>
    /// <param name="my">My.</param>
    /// <param name="other">Other.</param>
    public void TriggerStay(Collider2D my, Collider2D other)
    {

    }

    /// <summary>
    /// Triggers the exit.
    /// </summary>
    /// <param name="my">My.</param>
    /// <param name="other">Other.</param>
    public void TriggerExit(Collider2D my, Collider2D other)
    {

    }
}
