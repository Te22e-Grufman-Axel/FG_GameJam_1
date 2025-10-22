using UnityEngine;
using Pathfinding;
using System.IO;

public class Target : MonoBehaviour
{
    [SerializeField] private AIDestinationSetter destinationSetter;
    [SerializeField] private GameObject targetPrefab;
    private Transform target;

    private AIDestinationSetter aIDestinationSetter;
    private AIPath aIPath;
    private Shoting shoting;

    void Awake()
    {
        aIPath = this.GetComponent<AIPath>();
        shoting = this.GetComponent<Shoting>();
        aIDestinationSetter = this.GetComponent<AIDestinationSetter>();

        target = Instantiate(targetPrefab, this.transform.position, Quaternion.identity).transform;
        destinationSetter.target = target;
    }

    private void Update()
    {
        if (aIDestinationSetter.target != null)
        {
            if (aIDestinationSetter.target.gameObject.tag == "Enemy")
            {
                aIPath.canMove = !shoting.ableToAttack;
            }
            else
            {
                aIPath.canMove = true;
            }
        }
    }

    public void setTargetPos(Vector3 newPos)
    {
        Debug.Log("setTargetPos");
        destinationSetter.target = target;
        target.position = newPos;
    }

    public void setTransformAsTarget(Transform newTarget)
    {
        Debug.Log("setTransformAsTarget");
        destinationSetter.target = newTarget;
    }
}
