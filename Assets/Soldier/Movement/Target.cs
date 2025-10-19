using UnityEngine;
using Pathfinding;

public class Target : MonoBehaviour
{
    [SerializeField] private AIDestinationSetter destinationSetter;
    [SerializeField] private GameObject targetPrefab;
    private Transform target;

    void Awake()
    {
        target = Instantiate(targetPrefab, this.transform.position, Quaternion.identity).transform;
        destinationSetter.target = target;
    }

    public void setTargetPos(Vector3 newPos)
    {
        destinationSetter.target = target;
        target.position = newPos;
    }

    public void setEnemyAsTarget(Transform newTarget)
    {
        destinationSetter.target = newTarget;
    }
}
