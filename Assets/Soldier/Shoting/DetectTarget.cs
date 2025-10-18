using UnityEngine;

public class DetectTarget : MonoBehaviour
{
    [SerializeField] private float range;

    [SerializeField] private LayerMask layerMask;

    [SerializeField] public GameObject enemy;

    void Update()
    {
        if(enemy != null)
        {
            float dist = Vector2.Distance(enemy.transform.position, this.transform.position);

            if(dist > range)
            {
                enemy = null;
            }
        }
        else
        {
            enemy = CheckForEnemys();
        }
    }

    private GameObject CheckForEnemys()
    {
        Collider2D[] allhits = Physics2D.OverlapCircleAll(this.transform.position, range, layerMask);

        if (allhits.Length > 0)
        {
            float dist = 0f;
            float closestdist = 0f;
            Collider2D Closest = null;

            foreach (Collider2D hit in allhits)
            {
                dist = Vector2.Distance(hit.transform.position, this.transform.position);

                if (Closest == null || dist < closestdist)
                {
                    if (dist <= range)
                    {
                        Closest = hit;
                        closestdist = dist;
                    }
                }
            }

            if(Closest != null)
            {
                return Closest.gameObject;
            }
        }
        return null;
    }
}
