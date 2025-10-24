using System.Collections.Generic;
using UnityEngine;

public class ExplosiveSpawner : MonoBehaviour
{
    [SerializeField] private float timer = 0.2f;
    private float currentTime;

    [SerializeField] private List<GameObject> explosions = new List<GameObject>();

    private void Awake()
    {
        currentTime = 3f;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if(currentTime <= 0)
        {
            float x = Random.Range(0.05f, 0.95f);
            float y = Random.Range(0.05f, 0.95f);
            Vector3 pos = new Vector3(x, y, 10.0f);
            pos = Camera.main.ViewportToWorldPoint(pos);

            int i = Random.Range(0, explosions.Count);

            GameObject newExplosion = Instantiate(explosions[i], pos, Quaternion.identity);
            Destroy(newExplosion, 1.5f);

            currentTime = timer;
        }
    }
}
