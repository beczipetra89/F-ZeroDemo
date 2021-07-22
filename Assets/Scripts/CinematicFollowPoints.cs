using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicFollowPoints : MonoBehaviour
{
    public GameObject[] waypoints;
    int current = 0;
    float rotateSpeed;
    public float speed;
    float WPRadius = 1;
    public bool followTrack = false;
    public CinematicDriving cinematicDriving;

    
    void Update()
    {
        if (cinematicDriving.isActiveAndEnabled)
        {
            followTrack = true;
        }

        if (followTrack)
        {
            DriveOnTrack();
        }
    }

    void DriveOnTrack()
    {
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPRadius)
        {
            current++;
            if (current >= waypoints.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
    }
}
