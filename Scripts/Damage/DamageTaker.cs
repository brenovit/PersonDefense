using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This target can receive damage.
/// </summary>
public class DamageTaker : MonoBehaviour
{
    // Start hitpoints
    public int hitpoints = 1;
    // Remaining hitpoints
    public int currentHitpoints;
    // Hit visual effect duration
    public float hitDisplayTime = 0.2f;
    // Helth bar object
    public Transform healthBar;

    // Image of this object
    private SpriteRenderer sprite;
    // Visualisation of hit
    private bool hitCoroutine;
	// Original width of health bar (full hp)
    private float originHealthBarWidth;

    /// <summary>
    /// Awake this instance.
    /// </summary>
    void Awake()
    {
        currentHitpoints = hitpoints;
        sprite = GetComponentInChildren<SpriteRenderer>();
        Debug.Assert(sprite && healthBar, "Wrong initial parameters");
    }

	/// <summary>
	/// Start this instance.
	/// </summary>
    void Start()
    {
        originHealthBarWidth = healthBar.localScale.x;
    }

    /// <summary>
    /// Take damage.
    /// </summary>
    /// <param name="damage">Damage.</param>
    public void TakeDamage(int damage)
    {
        if (currentHitpoints > damage)
        {
            // Still alive
            currentHitpoints -= damage;
            // If no coroutine now
            if (hitCoroutine == false)
            {
                // Damage visualisation
                StartCoroutine(DisplayDamage());
            }
            UpdateHealthBar();
        }
        else
        {
            // Die
            currentHitpoints = 0;
            Die();
        }
    }

	/// <summary>
	/// Updates the health bar width.
	/// </summary>
    private void UpdateHealthBar()
    {
        float healthBarWidth = originHealthBarWidth * currentHitpoints / hitpoints;
        healthBar.localScale = new Vector2(healthBarWidth, healthBar.localScale.y);
    }

    /// <summary>
    /// Die this instance.
    /// </summary>
    public void Die()
    {
        EventManager.TriggerEvent("UnitKilled", gameObject, null);
        Destroy(gameObject);
    }

    /// <summary>
    /// Damage visualisation.
    /// </summary>
    /// <returns>The damage.</returns>
    IEnumerator DisplayDamage()
    {
        hitCoroutine = true;
        Color originColor = sprite.color;
        float counter;
        // Set color to black and return to origin color over time
		for (counter = 0f; counter < hitDisplayTime; counter += Time.fixedDeltaTime)
        {
            sprite.color = Color.Lerp(originColor, Color.black, Mathf.PingPong(counter, hitDisplayTime));
			yield return new WaitForFixedUpdate();
        }
        sprite.color = originColor;
        hitCoroutine = false;
    }

	/// <summary>
	/// Raises the destroy event.
	/// </summary>
	void OnDestroy()
	{
		EventManager.TriggerEvent ("UnitDie", gameObject, null);
		StopAllCoroutines();
	}
}
