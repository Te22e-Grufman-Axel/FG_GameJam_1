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

    [SerializeField] private bool patrol = false;
    [SerializeField] private List<Transform> patrolList = new List<Transform>();

    private void Update()
    {
        if(detectTarget.target != null)
        {
            destinationSetter.target = detectTarget.target.transform;

            aIPath.canMove = !shoting.ableToShot;
        }
        else
        {
            destinationSetter.target = null;
            aIPath.canMove = true;
        }
    }
}
