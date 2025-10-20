using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class AITarget : MonoBehaviour
{
    [SerializeField] private AIDestinationSetter destinationSetter;
    [SerializeField] private AIPath aIPath;
    [SerializeField] private DetectTarget detectTarget;
    [SerializeField] private Shoting shoting;
    [Space]

    [SerializeField] private GameObject targetPrefab;
    private Transform target;
    private bool movingToLastSeenPos = false;
    [Space]

    [SerializeField] private bool patrol = false;
    private bool currentlyPatroling = false;
    private bool reversePatrol = false;
    private int patrolIndex;
    [SerializeField] private bool loopPatrol = false;
    [SerializeField] private Transform Waypoints;
    private List<Transform> patrolList = new List<Transform>();

    private void Awake()
    {
        patrolList = GetWaypoints();
        target = Instantiate(targetPrefab, this.transform.position, Quaternion.identity).transform;
    }

    private void Update()
    {
        if (detectTarget.target != null)
        {
            currentlyPatroling = false;

            destinationSetter.target = detectTarget.target.transform;

            aIPath.canMove = !shoting.ableToShot;
        }
        else
        {
            aIPath.canMove = true;

            if (movingToLastSeenPos == true)
            {
                float dist = Vector2.Distance(this.transform.position, target.position);

                if (dist <= aIPath.pickNextWaypointDist)
                {
                    movingToLastSeenPos = false;
                }
            }
            else if (patrol)
            {
                if (currentlyPatroling == true)
                {
                    float dist = Vector2.Distance(this.transform.position, destinationSetter.target.position);

                    if (dist <= aIPath.pickNextWaypointDist)
                    {
                        destinationSetter.target = getNextWaypoint();
                    }
                }
                else
                {
                    destinationSetter.target = getClosestWaypoint();
                    currentlyPatroling = true;
                }
            }
        }
    }

    private Transform getClosestWaypoint()
    {
        float dist = 0f;
        float closestdist = 0f;
        Transform closestWaypoint = null;

        foreach (Transform waypoint in patrolList)
        {
            dist = Vector2.Distance(waypoint.position, this.transform.position);

            if (closestWaypoint == null || dist < closestdist)
            {
                closestWaypoint = waypoint;
                closestdist = dist;
            }
        }

        return closestWaypoint;
    }

    private Transform getNextWaypoint()
    {
        if (loopPatrol == true)
        {
            if (patrolIndex == patrolList.Count)
            {
                patrolIndex = 0;
                return patrolList[0];
            }
            else
            {
                return patrolList[patrolIndex++];
            }
        }
        else
        {
            if (patrolIndex >= patrolList.Count - 1)
            {
                reversePatrol = !reversePatrol;
                patrolIndex = patrolList.Count - 2;
                return patrolList[patrolIndex];
            }
            else if (patrolIndex == 0 && reversePatrol == true)
            {
                reversePatrol = !reversePatrol;
                patrolIndex++;
                return patrolList[patrolIndex];
            }

            if (!reversePatrol)
            {
                patrolIndex++;
                return patrolList[patrolIndex];
            }
            else
            {
                patrolIndex--;
                return patrolList[patrolIndex];
            }
        }
    }

    private List<Transform> GetWaypoints()
    {
        List<Transform> listOfChildren = new List<Transform>();

        foreach (Transform child in Waypoints)
        {
            listOfChildren.Add(child);
        }

        return listOfChildren;
    }

    public void GetToLastSeenPos(Vector3 pos)
    {
        movingToLastSeenPos = true;
        target.position = pos;
        destinationSetter.target = target;
    }
}
