using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private string damageTag;
    [SerializeField] private string ignoreTag;
    [Space]
    [SerializeField] private GameObject HitWall;
    [SerializeField] private GameObject HitFlesh;

    [HideInInspector] public float damage;

    private bool hasCollided = false;

    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(hasCollided == true)
        {
            return;
        }

        if (collision.gameObject.tag.Contains("Props"))
        {
            int r = Random.Range(0, 100);
            //Debug.Log(r);
            if(r < 50)
            {
                hasCollided = true;
                SpawnHitEffect(HitWall);
                return;
            }
            return;
        }

        if (collision.gameObject.tag.Contains(ignoreTag))
        {
            return;
        }
        else if (collision.gameObject.tag.Contains(damageTag))
        {
            hasCollided = true;
            collision.gameObject.GetComponent<HitInterface>().TakeDamage(damage);

            SpawnHitEffect(HitFlesh);
        }
        else
        {
            hasCollided = true;
            SpawnHitEffect(HitWall);
        }
    }

    private void SpawnHitEffect(GameObject HitEffect)
    {
        GameObject hitEffect = Instantiate(HitEffect, new Vector3(this.transform.position.x, this.transform.position.y, 0f), Quaternion.identity);
        Destroy(hitEffect, 1f);

        Destroy(this.gameObject);
    }
}
