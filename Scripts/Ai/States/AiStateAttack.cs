using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows AI to attack targets.
/// </summary>
public class AiStateAttack : MonoBehaviour, IAiState
{
    // Attack target closest to the capture point
    public bool useTargetPriority = false;
    // Go to this state if agressive event occures
    public string agressiveAiState;
    // Go to this state if passive event occures
    public string passiveAiState;


    // AI behavior of this object
    private AiBehavior aiBehavior;
    // Target for attack
    private GameObject target;
    // List with potential targets finded during this frame
    private List<GameObject> targetsList = new List<GameObject>();
    // My melee attack type if it is
    private IAttack meleeAttack;
    // My ranged attack type if it is
    private IAttack rangedAttack;
    // Type of last attack is made
    private IAttack myLastAttack;
    // My navigation agent if it is
    NavAgent nav;
    // Allows to await new target for one frame before exit from this state
    private bool targetless;

    /// <summary>
    /// Awake this instance.
    /// </summary>
    void Awake ()
    {
        aiBehavior = GetComponent<AiBehavior> ();
        meleeAttack = GetComponentInChildren<AttackMelee>() as IAttack;
        rangedAttack = GetComponentInChildren<AttackRanged>() as IAttack;
        nav = GetComponent<NavAgent>();
        Debug.Assert ((aiBehavior != null) && ((meleeAttack != null) || (rangedAttack != null)), "Wrong initial parameters");
    }

    /// <summary>
    /// Raises the state enter event.
    /// </summary>
    /// <param name="previousState">Previous state.</param>
    /// <param name="newState">New state.</param>
    public void OnStateEnter (string previousState, string newState)
    {

    }

    /// <summary>
    /// Raises the state exit event.
    /// </summary>
    /// <param name="previousState">Previous state.</param>
    /// <param name="newState">New state.</param>
    public void OnStateExit (string previousState, string newState)
    {
        LoseTarget();
    }

    /// <summary>
    /// FixedUpdate for this instance.
    /// </summary>
    void FixedUpdate ()
    {
        // If have no target - try to get new target
        if ((target == null) && (targetsList.Count > 0))
        {
            target = GetTopmostTarget();
            if ((target != null) && (nav != null))
            {
                // Look at target
                nav.LookAt(target.transform);
            }
        }
        // There are no targets around
        if (target == null)
        {
            if (targetless == false)
            {
                targetless = true;
            }
            else
            {
                // If have no target more than one frame - exit from this state
                aiBehavior.ChangeState(passiveAiState);
            }
        }
    }

    /// <summary>
    /// Gets top priority target from list.
    /// </summary>
    /// <returns>The topmost target.</returns>
    private GameObject GetTopmostTarget()
    {
        GameObject res = null;
        if (useTargetPriority == true) // Get target with minimum distance to capture point
        {
            float minPathDistance = float.MaxValue;
            foreach (GameObject ai in targetsList)
            {
                if (ai != null)
                {
                    AiStatePatrol aiStatePatrol = ai.GetComponent<AiStatePatrol>();
                    float distance = aiStatePatrol.GetRemainingPath();
                    if (distance < minPathDistance)
                    {
                        minPathDistance = distance;
                        res = ai;
                    }
                }
            }
        }
        else // Get first target from list
        {
            res = targetsList[0];
        }
        // Clear list of potential targets
        targetsList.Clear();
        return res;
    }

    /// <summary>
    /// Loses the current target.
    /// </summary>
    private void LoseTarget()
    {
        target = null;
        targetless = false;
        myLastAttack = null;
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
        if (target == null) // Add new target to potential targets list
        {
            targetsList.Add(other.gameObject);
        }
        else // Attack current target
        {
            // If this is my current target
            if (target == other.gameObject)
            {
                if (my.name == "MeleeAttack") // If target is in melee attack range
                {
                    // If I have melee attack type
                    if (meleeAttack != null)
                    {
                        // Remember my last attack type
                        myLastAttack = meleeAttack as IAttack;
                        // Try to make melee attack
                        meleeAttack.Attack(other.transform);
                    }
                }
                else if (my.name == "RangedAttack") // If target is in ranged attack range
                {
                    // If I have ranged attack type
                    if (rangedAttack != null)
                    {
                        // If target not in melee attack range
                        if ((meleeAttack == null)
                            || ((meleeAttack != null) && (myLastAttack != meleeAttack)))
                        {
                            // Remember my last attack type
                            myLastAttack = rangedAttack as IAttack;
                            // Try to make ranged attack
                            rangedAttack.Attack(other.transform);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Triggers the exit.
    /// </summary>
    /// <param name="my">My.</param>
    /// <param name="other">Other.</param>
    public void TriggerExit(Collider2D my, Collider2D other)
    {
        if (other.gameObject == target)
        {
            // Lose my target if it quit attack range
            LoseTarget();
        }
    }
}
