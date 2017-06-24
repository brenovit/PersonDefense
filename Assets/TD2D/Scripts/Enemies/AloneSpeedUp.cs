using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unit will speed up if there is no any units in close.
/// </summary>
public class AloneSpeedUp : MonoBehaviour
{
	// Radius for searching other units
	public float aloneRadius = 1f;
	// Spedd up amount when alone
	public float speedUpAmount = 0.5f;
	// Cooldown between alone checing and for speed up duration
	public float cooldown = 1f;
	// Allowed objects tags for collision detection
	public List<string> tags = new List<string>();
	
	// Counter for cooldown
	private float cooldownCounter;
	// NavAgent of this instance
	private NavAgent navAgent;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start()
	{
		navAgent = GetComponent<NavAgent>();
		Debug.Assert(navAgent, "Wrong initial settings");
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
			if (AmIAlone() == true)
			{
				StartCoroutine(SpeedUpCoroutine());
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
	/// Check if there no other targets inside specified radius.
	/// </summary>
	/// <returns><c>true</c>, if I alone was amed, <c>false</c> otherwise.</returns>
	private bool AmIAlone()
	{
		bool alone = true;
		Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, aloneRadius);
		foreach (Collider2D col in cols)
		{
			if (IsTagAllowed(col.tag) == true && col.gameObject != gameObject)
			{
				alone = false;
				break;
			}
		}
		return alone;
	}

	/// <summary>
	/// Speeds up for cooldown time.
	/// </summary>
	/// <returns>The up coroutine.</returns>
	private IEnumerator SpeedUpCoroutine()
	{
		navAgent.speed += speedUpAmount;
		yield return new WaitForSeconds(cooldown);
		navAgent.speed -= speedUpAmount;
	}

	/// <summary>
	/// Raises the destroy event.
	/// </summary>
	void OnDestroy()
	{
		StopAllCoroutines();
	}
}
