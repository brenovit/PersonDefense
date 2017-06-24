using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Area Of Effect damage on destroing.
/// </summary>
public class AOE : MonoBehaviour
{
    // Area radius
    public float radius = 0.3f;
    // Damage amount
    public int damage = 3;
    // Explosion prefab
    public GameObject explosion;
    // Explosion visual duration
    public float explosionDuration = 1f;

    // Scene is closed now. Forbidden to create new objects on destroy
    private bool isQuitting;

    /// <summary>
    /// Raises the enable event.
    /// </summary>
    void OnEnable()
    {
        EventManager.StartListening("SceneQuit", SceneQuit);
    }

    /// <summary>
    /// Raises the disable event.
    /// </summary>
    void OnDisable()
    {
        EventManager.StopListening("SceneQuit", SceneQuit);
    }

    /// <summary>
    /// Raises the application quit event.
    /// </summary>
    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    /// <summary>
    /// Raises the destroy event.
    /// </summary>
    void OnDestroy()
    {
        // If scene is in progress
        if (isQuitting == false)
        {
            // Find all colliders in specified radius
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D col in cols)
            {
                // If collision allowed by scene
                if (LevelManager.IsCollisionValid(gameObject.tag, col.gameObject.tag) == true)
                {
                    // If target can receive damage
                    DamageTaker damageTaker = col.gameObject.GetComponent<DamageTaker>();
                    if (damageTaker != null)
                    {
                        damageTaker.TakeDamage(damage);
                    }
                }
            }
            if (explosion != null)
            {
                // Create explosion visual effect
                Destroy(Instantiate<GameObject>(explosion, transform.position, transform.rotation), explosionDuration);
            }
        }
    }

    /// <summary>
    /// Raises on scene quit.
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <param name="param">Parameter.</param>
    private void SceneQuit(GameObject obj, string param)
    {
        isQuitting = true;
    }
}
