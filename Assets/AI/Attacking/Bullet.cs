using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private string damageTag;
    [SerializeField] private string ignoreTag;

    [HideInInspector] public float damage;

    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains(ignoreTag))
        {
            return;
        }
        else if (collision.gameObject.tag.Contains(damageTag))
        {
            collision.gameObject.GetComponent<HitInterface>().TakeDamage(damage);

            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
