using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows AI to move with specified path.
/// </summary>
public class AiStatePatrol : AiState
{
	[Space(10)]
	[HideInInspector]
    // Specified path
    public Pathway path;
    // Need to loop path after last point is reached?
    public bool loop = false;

    // Navigation agent of this gameobject
    NavAgent navAgent;
    // Current destination
    private Waypoint destination;

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
        if (path == null)
        {
            // If I have no path - try to find it
            path = FindObjectOfType<Pathway>();
            Debug.Assert(path, "Have no path");
        }
        if (destination == null)
        {
            // Get next waypoint from my path
            destination = path.GetNearestWaypoint (transform.position);
        }
        // Set destination for navigation agent
        navAgent.destination = destination.transform.position;
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
    /// Gets the remaining distance on pathway.
    /// </summary>
    /// <returns>The remaining path.</returns>
    public float GetRemainingPath()
    {
        Vector2 distance = destination.transform.position - transform.position;
        return (distance.magnitude + path.GetPathDistance(destination));
    }
}
