using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows AI to move with specified path.
/// </summary>
public class AiStatePatrol : MonoBehaviour, IAiState
{
    // Specified path
    public Pathway path;
    // Need to loop path after last point is reached?
    public bool loop = false;
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
    // Current destination
    private Waypoint destination;

    /// <summary>
    /// Awake this instance.
    /// </summary>
    void Awake ()
    {
        aiBehavior = GetComponent<AiBehavior>();
        navAgent = GetComponent<NavAgent>();
        anim = GetComponent<Animation>();
        Debug.Assert (aiBehavior && navAgent, "Wrong initial parameters");
    }

    /// <summary>
    /// Raises the state enter event.
    /// </summary>
    /// <param name="previousState">Previous state.</param>
    /// <param name="newState">New state.</param>
    public void OnStateEnter (string previousState, string newState)
    {
        if (path == null)
        {
            // If I have no path - try to find it
            path = FindObjectOfType<Pathway>();
            Debug.Assert (path, "Have no path");
        }
        if (destination == null)
        {
            // Get next waypoint from my path
            destination = path.GetNearestWaypoint (transform.position);
        }
        // Set destination for navigation agent
        navAgent.destination = destination.transform.position;
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
        if (destination != null)
        {
            // If destination reached
            if ((Vector2)destination.transform.position == (Vector2)transform.position)
            {
                // Get next waypoint from my path
                destination = path.GetNextWaypoint (destination, loop);
                if (destination != null)
                {
                    // Set destination for navigation agent
                    navAgent.destination = destination.transform.position;
                }
            }
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
        aiBehavior.ChangeState(agressiveAiState);
    }

    /// <summary>
    /// Triggers the exit.
    /// </summary>
    /// <param name="my">My.</param>
    /// <param name="other">Other.</param>
    public void TriggerExit(Collider2D my, Collider2D other)
    {

    }

    /// <summary>
    /// Gets the remaining distance on pathway.
    /// </summary>
    /// <returns>The remaining path.</returns>
    public float GetRemainingPath()
    {
        Vector2 distance = destination.transform.position - transform.position;
        return (distance.magnitude + path.GetPathDistance(destination));
    }
}
