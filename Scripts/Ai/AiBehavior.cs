using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main script to operate all AI states
/// </summary>
public class AiBehavior : MonoBehaviour
{
    // This state will be activate on start
    public string defaultState;

    // List with all states for this AI
    private List<IAiState> aiStates = new List<IAiState>();
    // The state that was before
    private IAiState previousState;
    // Active state
    private IAiState currentState;

    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start()
    {
        // Get all AI states from this gameobject
        IAiState[] states = GetComponents<IAiState>();
        if (states.Length > 0) 
        {
            foreach (IAiState state in states)
            {
                // Add state to list
                aiStates.Add(state);
            }
            if (defaultState != null)
            {
                // Set active and previous states as default state
                previousState = currentState = GetComponent(defaultState) as IAiState;
                if (currentState != null)
                {
                    // Go to active state
                    ChangeState(currentState.GetType().ToString());
                }
                else
                {
                    Debug.LogError("Incorrect default AI state " + defaultState);
                }
            }
            else
            {
                Debug.LogError("AI have no default state");
            }
        } 
        else 
        {
            Debug.LogError ("No AI states found");
        }
    }

    /// <summary>
    /// Set AI to defalt state.
    /// </summary>
    public void GoToDefaultState()
    {
        previousState = currentState;
        currentState = GetComponent(defaultState) as IAiState;
        NotifyOnStateExit();
        DisableAllStates();
        EnableNewState();
        NotifyOnStateEnter();
    }

    /// <summary>
    /// Change Ai state.
    /// </summary>
    /// <param name="state">State.</param>
    public void ChangeState(string state)
    {
        if (state != "")
        {
            // Try to find such state in list
            foreach (IAiState aiState in aiStates)
            {
                if (state == aiState.GetType().ToString())
                {
                    previousState = currentState;
                    currentState = aiState;
                    NotifyOnStateExit();
                    DisableAllStates();
                    EnableNewState();
                    NotifyOnStateEnter();
                    return;
                }
            }
            Debug.Log("No such state " + state);
            // If have no such state - go to default state
            GoToDefaultState();
            Debug.Log("Go to default state " + aiStates[0].GetType().ToString());
        }
    }

    /// <summary>
    /// Turn off all AI states components.
    /// </summary>
    private void DisableAllStates()
    {
        foreach (IAiState aiState in aiStates) 
        {
            MonoBehaviour state = GetComponent(aiState.GetType().ToString()) as MonoBehaviour;
            state.enabled = false;
        }
    }

    /// <summary>
    /// Turn on active AI state component.
    /// </summary>
    private void EnableNewState()
    {
        MonoBehaviour state = GetComponent(currentState.GetType().ToString()) as MonoBehaviour;
        state.enabled = true;
    }

    /// <summary>
    /// Send OnStateExit notification to previous state.
    /// </summary>
    private void NotifyOnStateExit()
    {
        string prev = previousState.GetType().ToString();
        string active = currentState.GetType().ToString();
        IAiState state = GetComponent(prev) as IAiState;
        state.OnStateExit(prev, active);
    }

    /// <summary>
    /// Send OnStateEnter notification to new state.
    /// </summary>
    private void NotifyOnStateEnter()
    {
        string prev = previousState.GetType().ToString();
        string active = currentState.GetType().ToString();
        IAiState state = GetComponent(active) as IAiState;
        state.OnStateEnter(prev, active);
    }

    /// <summary>
    /// Triggers the enter2d.
    /// </summary>
    /// <param name="my">My.</param>
    /// <param name="other">Other.</param>
    public void TriggerEnter2D(Collider2D my, Collider2D other)
    {
		if (other.gameObject == null) {
			Debug.Log("Other is null");
		}
		if (my.gameObject == null) {
			Debug.Log("My is null");
		}
		if (gameObject == null) {
			Debug.Log("GO is null");
		}
        if (LevelManager.IsCollisionValid(gameObject.tag, other.gameObject.tag) == true)
        {
			if (currentState == null) {
				Debug.Log ("Current sate is null");
			}
            currentState.TriggerEnter(my, other);
        }
    }

    /// <summary>
    /// Triggers the stay2d.
    /// </summary>
    /// <param name="my">My.</param>
    /// <param name="other">Other.</param>
    public void TriggerStay2D(Collider2D my, Collider2D other)
    {
        if (LevelManager.IsCollisionValid(gameObject.tag, other.gameObject.tag) == true)
        {
            currentState.TriggerStay(my, other);
        }
    }

    /// <summary>
    /// Triggers the exit2d.
    /// </summary>
    /// <param name="my">My.</param>
    /// <param name="other">Other.</param>
    public void TriggerExit2D(Collider2D my, Collider2D other)
    {
        if (LevelManager.IsCollisionValid(gameObject.tag, other.gameObject.tag) == true)
        {
            currentState.TriggerExit(my, other);
        }
    }
}
