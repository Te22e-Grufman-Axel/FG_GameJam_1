using Pathfinding;
using UnityEngine;

public class Shoting : MonoBehaviour
{
    [SerializeField] private DetectTarget detectTarget;

    [SerializeField] private AIPath aIPath;

    [SerializeField] private Animator animator;
    [Space]

    [SerializeField] private bool soldier = false;
    [SerializeField] private float alarmRadius = 10f;
    [SerializeField] private LayerMask alarmLayerMask;
    [Space]

    [SerializeField] private bool meele;
    [SerializeField] private bool shooting;
    [Space]

    [SerializeField] private float damage;
    [SerializeField] private float attackRate;
    [SerializeField] private float spread;
    [SerializeField] private float range;
    [Space]

    [SerializeField] private float meeleDamage;
    [SerializeField] private float meeleAttackRate;
    [SerializeField] private float meeleRange;
    [SerializeField] private GameObject meeleHitEffect;
    [Space]

    public bool ableToAttack = false;

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

                    if (meele)
                    {
                        if (dist <= meeleRange)
                        {
                            ableToAttack = true;
                            Meele();
                        }
                        else if (shooting)
                        {
                            if (dist <= range)
                            {
                                ableToAttack = true;
                                Shot();
                            }
                            else
                            {
                                NotAttacking();
                            }
                        }
                        else
                        {
                            NotAttacking();
                        }
                    }
                    else if (shooting)
                    {
                        if (dist <= range)
                        {
                            ableToAttack = true;
                            Shot();
                        }
                        else
                        {
                            NotAttacking();
                        }
                    }
                    else
                    {
                        NotAttacking();
                    }
                }
                else
                {
                    NotAttacking();
                }
            }
            else
            {
                NotAttacking();
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

    private void NotAttacking()
    {
        ableToAttack = false;

        if (animator != null)
        {
            animator.SetBool("Attacking", false);
        }
    }

    private void FlipSprite()
    {
        if (ableToAttack)
        {
            if (detectTarget.target != null)
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

    private void AlarmNerbyEnemys()
    {
        Collider2D[] allColliders = Physics2D.OverlapCircleAll(firePos.position, alarmRadius, alarmLayerMask);

        foreach(Collider2D collider in allColliders)
        {
            collider.gameObject.GetComponent<DetectShot>().Alarm(firePos.position);
        }
    }

    private void Shot()
    {
        if (animator != null)
        {
            animator.SetBool("Meele", false);
            animator.SetBool("Attacking", true);
        }

        timer = attackRate;
        Vector3 dir = firePos.InverseTransformPoint(detectTarget.target.transform.position);
        float angel = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        GameObject newBullet = Instantiate(bullet, firePos.position, Quaternion.Euler(0f, 0f, angel + Random.Range(-spread, spread)));
        newBullet.GetComponent<Bullet>().damage = damage;

        if (soldier)
        {
            AlarmNerbyEnemys();
        }
    }

    private void Meele()
    {
        if (animator != null)
        {
            animator.SetBool("Meele", true);
            animator.SetBool("Attacking", true);
        }
        
        timer = meeleAttackRate;
        detectTarget.target.GetComponent<HitInterface>().TakeDamage(damage);
        SpawnHitEffect();
    }

    private void SpawnHitEffect()
    {
        GameObject hitEffect = Instantiate(meeleHitEffect, new Vector3(detectTarget.target.transform.position.x, detectTarget.target.transform.position.y, 0f), Quaternion.identity);
        Destroy(hitEffect, 1f);
    }
}