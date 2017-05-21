using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for all bullets.
/// </summary>
public interface IBullet
{
    void SetDamage(int damage);
    void Fire(Transform target);
}
