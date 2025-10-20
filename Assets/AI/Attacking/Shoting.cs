using Pathfinding;
using UnityEngine;

public class Shoting : MonoBehaviour
{
    [SerializeField] private DetectTarget detectTarget;

    [SerializeField] private AIPath aIPath;

    [SerializeField] private Animator animator;

    [SerializeField] private bool meele;
    [SerializeField] private float damage;
    [SerializeField] private float AttackRate;
    [SerializeField] private float spread;
    [SerializeField] private float range;

    public bool ableToShot = false;

    private float timer;

    [SerializeField] private GameObject bullet;

    [SerializeField] private Transform firePos;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (detectTarget.target != null)
            {
                if (detectTarget.targetInView == true)
                {
                    float dist = Vector2.Distance(detectTarget.target.transform.position, this.transform.position);

                    if (dist <= range)
                    {
                        timer = AttackRate;
                        ableToShot = true;

                        if (!meele)
                        {
                            Vector3 dir = firePos.InverseTransformPoint(detectTarget.target.transform.position);
                            float angel = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                            GameObject newBullet = Instantiate(bullet, firePos.position, Quaternion.Euler(0f, 0f, angel + Random.Range(-spread, spread)));
                            newBullet.GetComponent<Bullet>().damage = damage;
                        }
                        else
                        {
                            detectTarget.target.GetComponent<HitInterface>().TakeDamage(damage);
                        }

                        if (animator != null)
                        {
                            animator.SetBool("Attacking", true);
                        }
                    }
                    else
                    {
                        ableToShot = false;

                        if (animator != null)
                        {
                            animator.SetBool("Attacking", false);
                        }
                    }
                }
                else
                {
                    ableToShot = false;

                    if (animator != null)
                    {
                        animator.SetBool("Attacking", false);
                    }
                }
            }
            else
            {
                ableToShot = false;

                if (animator != null)
                {
                    animator.SetBool("Attacking", false);
                }
            }
        }


        FlipSprite();

        if (animator != null)
        {
            if (aIPath.velocity.x > 0f || aIPath.velocity.x < 0f)
            {
                animator.SetBool("Walking", true);
            }
            else
            {
                animator.SetBool("Walking", false);
            }
        }
    }

    private void FlipSprite()
    {
        if (ableToShot)
        {
            Vector3 dir = firePos.position - detectTarget.target.transform.position;
            dir.Normalize();

            if (dir.x > 0f)
            {
                spriteRenderer.flipX = true;
            }
            else if (dir.x < 0f)
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            if (aIPath.velocity.x > 0f)
            {
                spriteRenderer.flipX = false;
            }
            else if (aIPath.velocity.x < 0f)
            {
                spriteRenderer.flipX = true;
            }
        }
    }
}