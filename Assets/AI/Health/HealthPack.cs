using Pathfinding;
using TMPro;
using UnityEngine;

public class HealthPack : MonoBehaviour, HealthPackInterface
{
    [SerializeField] private float distance = 1f;
    [SerializeField] private float healingAmmount = 1f;

    private AIDestinationSetter aIDestinationSetter;

    private void Update()
    {
        if (aIDestinationSetter != null)
        {
            if (aIDestinationSetter.target = this.transform)
            {
                float dist = Vector2.Distance(aIDestinationSetter.transform.position, this.transform.position);

                if (dist <= distance)
                {
                    aIDestinationSetter.gameObject.GetComponent<IncreaseHealth>().Health(healingAmmount);

                    Destroy(this.gameObject);
                }
            }
            else
            {
                aIDestinationSetter = null;
            }
        }
    }

    void HealthPackInterface.HealthPack(Transform Soldier)
    {
        aIDestinationSetter = Soldier.gameObject.GetComponent<AIDestinationSetter>();
    }
}
