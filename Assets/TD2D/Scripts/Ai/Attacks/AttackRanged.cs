using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attack with ranged weapon
/// </summary>
public class AttackRanged : MonoBehaviour, IAttack
{
    // Damage amount
    public int damage = 1;
    // Cooldown between attacks
    public float cooldown = 1f;
    // Prefab for arrows
    public GameObject arrowPrefab;
    // From this position arrows will fired
    public Transform firePoint;

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
        Debug.Assert(arrowPrefab && firePoint, "Wrong initial parameters");
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
            Fire(target);
        }
    }

    /// <summary>
    /// Make ranged attack
    /// </summary>
    /// <param name="target">Target.</param>
    private void Fire(Transform target)
    {
        if (target != null)
        {
            // Create arrow
            GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
            IBullet bullet = arrow.GetComponent<IBullet>();
            bullet.SetDamage(damage);
            bullet.Fire(target);
            if (anim != null)
            {
				anim.SetTrigger("attackRanged");
            }
        }
    }
}
