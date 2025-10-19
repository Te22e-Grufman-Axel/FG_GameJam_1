using UnityEngine;

public class Shoting : MonoBehaviour
{
    [SerializeField] private DetectTarget detectTarget;

    [SerializeField] private float damage;
    [SerializeField] private float fireRate;
    [SerializeField] private float spread;
    [SerializeField] private float range;

    private float timer;

    [SerializeField] private GameObject bullet;

    [SerializeField] private Transform firePos;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (detectTarget.targetInView == true)
            {
                float dist = Vector2.Distance(detectTarget.target.transform.position, this.transform.position);

                if (dist <= range)
                {
                    timer = fireRate;

                    Vector3 dir = firePos.InverseTransformPoint(detectTarget.target.transform.position);
                    float angel = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                    GameObject newBullet = Instantiate(bullet, firePos.position, Quaternion.Euler(0f, 0f, angel + Random.Range(-spread, spread)));
                    newBullet.GetComponent<Bullet>().damage = damage;
                }
            }
        }
    }
}