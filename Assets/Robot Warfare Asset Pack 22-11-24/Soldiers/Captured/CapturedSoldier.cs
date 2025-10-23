using Pathfinding;
using UnityEngine;

public class CapturedSoldier : MonoBehaviour, CapturedSoldierInterface, KillInterface
{
    [SerializeField] private float distance = 1f;
    [SerializeField] GameObject soldierPrefab;

    private AIDestinationSetter aIDestinationSetter;

    [SerializeField] private Animator animator;

    private void Update()
    {
        if (aIDestinationSetter != null)
        {
            if (aIDestinationSetter.target == this.transform)
            {
                float dist = Vector2.Distance(aIDestinationSetter.transform.position, this.transform.position);

                if (dist <= distance)
                {
                    Instantiate(soldierPrefab, this.transform.position, Quaternion.identity);

                    aIDestinationSetter.GetComponent<Target>().setTargetPos(aIDestinationSetter.transform.position);

                    Destroy(this.gameObject);
                }
            }
            else
            {
                aIDestinationSetter = null;
            }
        }
    }

    public void Rescue(Transform Soldier)
    {
        aIDestinationSetter = Soldier.gameObject.GetComponent<AIDestinationSetter>();
    }

    public void Kill()
    {
        animator.SetBool("Dead", true);
    }
}
