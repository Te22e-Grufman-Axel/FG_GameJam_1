using UnityEngine;

public class Health : MonoBehaviour, HitInterface
{
    [SerializeField] private Animator animator;

    [SerializeField] private float health = 10f;

    public bool Dead = false;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0f)
        {
            Dead = false;

            if (animator != null)
            {
                animator.SetBool("Dead", true);
            }

            this.transform.GetChild(0).transform.parent = null;
            Destroy(this.gameObject);
        }
    }
}
