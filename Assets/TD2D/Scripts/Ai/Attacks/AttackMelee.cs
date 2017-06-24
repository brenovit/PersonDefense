using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attack with melee weapon
/// </summary>
public class AttackMelee : MonoBehaviour, IAttack
{
    // Damage amount
    public int damage = 1;
    // Cooldown between attacks
    public float cooldown = 1f;

    // Animation controller for this AI
	private Animator anim;
    // Counter for cooldown calculation
    private float cooldownCounter;

    /// <summary>
    /// Awake this instance.
    /// </summary>
    void Awake()
    {
		anim = GetComponentInParent<Animator>();
        cooldownCounter = cooldown;
    }

    /// <summary>
    /// Update this instance.
    /// </summary>
    void FixedUpdate()
    {
        if (cooldownCounter < cooldown)
        {
			cooldownCounter += Time.fixedDeltaTime;
        }
    }

    /// <summary>
    /// Attack the specified target if cooldown expired
    /// </summary>
    /// <param name="target">Target.</param>
    public void Attack(Transform target)
    {
        if (cooldownCounter >= cooldown)
        {
            cooldownCounter = 0f;
            Smash(target);
        }
    }

    /// <summary>
    /// Make melee attack
    /// </summary>
    /// <param name="target">Target.</param>
    private void Smash(Transform target)
    {
        if (target != null)
        {
            // If target can receive damage
            DamageTaker damageTaker = target.GetComponent<DamageTaker>();
            if (damageTaker != null)
            {
                damageTaker.TakeDamage(damage);
            }
            if (anim != null)
            {
				anim.SetTrigger("attackMelee");
            }
        }
    }
}
