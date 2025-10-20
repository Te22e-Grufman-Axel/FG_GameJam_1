using UnityEngine;
using Pathfinding;

public class Target : MonoBehaviour
{
    [SerializeField] private AIDestinationSetter destinationSetter;
    [SerializeField] private AIPath aIPath;
    [SerializeField] private GameObject targetPrefab;
    private Transform target;

    void Awake()
    {
        target = Instantiate(targetPrefab, this.transform.position, Quaternion.identity).transform;
        destinationSetter.target = target;
    }

    private void Update()
    {
        
    }

    public void setTargetPos(Vector3 newPos)
    {
        destinationSetter.target = target;
        target.position = newPos;
    }

    public void setTransformAsTarget(Transform newTarget)
    {
        destinationSetter.target = newTarget;
    }
}
