using UnityEngine;

public class Health : MonoBehaviour, HitInterface
{
    [SerializeField] private float health = 10f;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}
