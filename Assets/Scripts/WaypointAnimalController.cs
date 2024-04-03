using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointAnimalController : AnimalController
{
    [SerializeField] Transform[] waypoints;
    [SerializeField] private float speed;

    private int currentWaypointIndex = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);
        if (Mathf.Abs(transform.position.x - waypoints[currentWaypointIndex].position.x) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex > waypoints.Length - 1) currentWaypointIndex = 0;
            
            transform.LookAt(waypoints[currentWaypointIndex], Vector3.up);
            transform.right = transform.forward;
        }
    }

    public void OverrideWaypoints(Transform[] newWaypoints)
    {
        currentWaypointIndex = 0;
        waypoints = newWaypoints;
    }

    public void OverrideWaypoint(Transform newWaypoint, int overrideIndex)
    {
        waypoints[overrideIndex] = newWaypoint;
    }

    public override void EnableMovement(bool enable)
    {
        base.EnableMovement(enable);
        
    }
}
