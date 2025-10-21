using UnityEngine;
using Pathfinding;
using static UnityEditor.PlayerSettings;
using System.Collections.Generic;

public class DrawPath : MonoBehaviour
{
    private AIDestinationSetter aIDestinationSetter;

    private AIPath aiPath;

    [SerializeField] private GameObject pathPrefab;
    [SerializeField] private GameObject destinationPrefab;
    private Queue<GameObject> pathMarkers = new Queue<GameObject>();

    private Path path;

    public bool drawNewPath = false;

    void Awake()
    {
        aIDestinationSetter = this.gameObject.GetComponent<AIDestinationSetter>();
        aiPath = this.gameObject.GetComponent<AIPath>();
    }

    void Update()
    {
        /*
        float distToTarget = Vector2.Distance(aIDestinationSetter.target.position, this.transform.position);

        if (aIDestinationSetter.target != null || distToTarget >= 1)
        {
            InvokeRepeating("GenerateGraphics", 0f, 0.2f);
        }
        else
        {
            CancelInvoke("GenerateGraphics");
        }
        */

        if(path != aiPath.GetPath())
        {
            GenerateGraphics();
        }

        if(pathMarkers.Count > 0)
        {
            float distToWaypoint = Vector2.Distance(pathMarkers.Peek().transform.position, this.transform.position);

            if(distToWaypoint <= aiPath.pickNextWaypointDist)
            {
                GameObject go = pathMarkers.Dequeue();

                Destroy(go);
            }
        }
    }

    public void GenerateGraphics()
    {
        drawNewPath = false;

        foreach(GameObject go in pathMarkers)
        {
            Destroy(go);
        }

        pathMarkers.Clear();

        path = aiPath.GetPath();

        float dist = Vector2.Distance(aIDestinationSetter.target.position, this.transform.position);

        if (dist >= 1)
        {
            for (int i = 0; i < path.vectorPath.Count; i++)
            {
                if(i + 1 < path.vectorPath.Count)
                {
                    Vector3 dir = path.vectorPath[i + 1] - path.vectorPath[i];
                    dir.Normalize();

                    //dir *= 2f;

                    float angel = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                    pathMarkers.Enqueue(Instantiate(pathPrefab, path.vectorPath[i], Quaternion.Euler(0f, 0f, angel -90f)));
                }
            }

            pathMarkers.Enqueue(Instantiate(destinationPrefab, aIDestinationSetter.target.position, Quaternion.identity));
        }
    }
}
