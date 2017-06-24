using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Heal all units in specified radius
/// </summary>
public class AoeHealer : MonoBehaviour
{
	// Amount of healed hp
	public int healAmount = 1;
	// Healing radius
	public float healRadius = 2f;
	// Delay between healing
	public float cooldown = 3f;
	// Visual effect for healing
	public GameObject healVisualPrefab;
	// Duration for heal visual effect
	public float healVisualDuration = 1f;
	// Allowed objects tags for collision detection
	public List<string> tags = new List<string>();

	// Counter for cooldown
	private float cooldownCounter;
	// Animator component
	private Animator anim;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start()
	{
		anim = GetComponent<Animator>();
		cooldownCounter = cooldown;
	}

	/// <summary>
	/// Fixeds the update.
	/// </summary>
	void FixedUpdate()
	{
		if (cooldownCounter < cooldown)
		{
			cooldownCounter += Time.fixedDeltaTime;
		}
		else
		{
			cooldownCounter = 0f;
			// Try to heal somebody
			if (AoeHeal() == true)
			{
				if (anim != null)
				{
					// Play animation
					anim.SetTrigger("heal");
				}
			}
		}
	}

	/// <summary>
	/// Determines whether this instance is tag allowed the specified tag.
	/// </summary>
	/// <returns><c>true</c> if this instance is tag allowed the specified tag; otherwise, <c>false</c>.</returns>
	/// <param name="tag">Tag.</param>
	private bool IsTagAllowed(string tag)
	{
		bool res = false;
		if (tags.Count > 0)
		{
			foreach (string str in tags)
			{
				if (str == tag)
				{
					res = true;
					break;
				}
			}
		}
		else
		{
			res = true;
		}
		return res;
	}

	/// <summary>
	/// Heal all targets in radius.
	/// </summary>
	private bool AoeHeal()
	{
		bool res = false;
		// Searching for units
		Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, healRadius);
		foreach (Collider2D col in cols)
		{
			if (IsTagAllowed(col.tag) == true)
			{
				// If it has Damege Taker component
				DamageTaker target = col.gameObject.GetComponent<DamageTaker>();
				if (target != null)
				{
					// If target injured
					if (target.currentHitpoints < target.hitpoints)
					{
						res = true;
						target.TakeDamage(-healAmount);
						if (healVisualPrefab != null)
						{
							// Create visual healing effect on target
							GameObject effect = Instantiate(healVisualPrefab, target.transform);
							// And destroy it after specified timeout
							Destroy(effect, healVisualDuration);
						}
					}
				}
			}
		}
		return res;
	}
}
