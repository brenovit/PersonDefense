using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pathway for enemy moving.
/// </summary>
[ExecuteInEditMode]
public class Pathway : MonoBehaviour
{
    /// <summary>
    /// Update this instance.
    /// </summary>
    void Update()
    {
        Waypoint[] waypoints = GetComponentsInChildren<Waypoint>();
        if (waypoints.Length > 1)
        {
            int idx;
            for (idx = 1; idx < waypoints.Length; idx++)
            {
                // Draw blue lines along pathway in edit mode
                Debug.DrawLine(waypoints[idx - 1].transform.position, waypoints[idx].transform.position, Color.blue);
            }
        }
    }

    /// <summary>
    /// Gets the nearest waypoint for specified position.
    /// </summary>
    /// <returns>The nearest waypoint.</returns>
    /// <param name="position">Position.</param>
    public Waypoint GetNearestWaypoint(Vector3 position)
    {
        float minDistance = float.MaxValue;
        Waypoint nearestWaypoint = null;
        foreach (Waypoint waypoint in GetComponentsInChildren<Waypoint>())
        {
            if (waypoint.GetHashCode() != GetHashCode())
            {
                // Calculate distance to waypoint
                Vector3 vect = position - waypoint.transform.position;
                float distance = vect.magnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestWaypoint = waypoint;
                }
            }
        }
        return nearestWaypoint;
    }

    /// <summary>
    /// Gets the next waypoint on this pathway.
    /// </summary>
    /// <returns>The next waypoint.</returns>
    /// <param name="currentWaypoint">Current waypoint.</param>
    /// <param name="loop">If set to <c>true</c> loop.</param>
    public Waypoint GetNextWaypoint(Waypoint currentWaypoint, bool loop)
    {
        Waypoint res = null;
        int idx = currentWaypoint.transform.GetSiblingIndex();
        if (idx < (transform.childCount - 1))
        {
            idx += 1;
        }
        else
        {
            idx = 0;
        }
        if (!(loop == false && idx == 0))
        {
            res = transform.GetChild(idx).GetComponent<Waypoint>(); 
        }
        return res;
    }

    public float GetPathDistance(Waypoint fromWaypoint)
    {
        Waypoint[] waypoints = GetComponentsInChildren<Waypoint>();
        bool hitted = false;
        float pathDistance = 0f;
        int idx;
        for (idx = 0; idx < waypoints.Length; ++idx)
        {
            if (hitted == true)
            {
                Vector2 distance = waypoints[idx].transform.position - waypoints[idx - 1].transform.position;
                pathDistance += distance.magnitude;
            }
            if (waypoints[idx] == fromWaypoint)
            {
                hitted = true;
            }
        }
        return pathDistance;
    }
}
