using UnityEngine;

public class DetectTarget : MonoBehaviour, DetectShot
{
    [SerializeField] private float range;

    [SerializeField] private LayerMask DetectLayerMask;
    [SerializeField] private LayerMask ViewLayerMask;

    [SerializeField] public GameObject target;

    [SerializeField] public bool targetInView = false;
    [SerializeField] public bool LostTarget = false;

    private AITarget aiTarget;

    private void Awake()
    {
        aiTarget = GetComponent<AITarget>();
    }

    void Update()
    {
        if (target != null)
        {
            float dist = Vector2.Distance(target.transform.position, this.transform.position);

            if (dist > range)
            {
                if (aiTarget != null)
                {
                    aiTarget.GetToLastSeenPos(target.transform.position);
                }
                target = null;
            }
            else
            {
                targetInView = CheckIfInView(target.transform);
            }
        }
        else
        {
            targetInView = false;

            GameObject detectedTarget = CheckForEnemys();

            if (detectedTarget != null)
            {
                targetInView = CheckIfInView(detectedTarget.transform);

                if (targetInView == true)
                {
                    target = detectedTarget;
                }
            }
        }
    }

    private GameObject CheckForEnemys()
    {
        Collider2D[] allhits = Physics2D.OverlapCircleAll(this.transform.position, range, DetectLayerMask);

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

            if (Closest != null)
            {
                return Closest.gameObject;
            }
        }
        return null;
    }

    private bool CheckIfInView(Transform target)
    {
        Vector3 dir = target.position - this.transform.position;

        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, dir, dir.magnitude, ViewLayerMask);

        Debug.DrawRay(this.transform.position, dir);

        if (hit.collider != null)
        {
            return false;
        }

        return true;
    }

    void DetectShot.Alarm(Vector3 pos)
    {
        if (target == null)
        {
            if (aiTarget != null)
            {
                aiTarget.GetToLastSeenPos(pos);
            }
        }
    }
}
