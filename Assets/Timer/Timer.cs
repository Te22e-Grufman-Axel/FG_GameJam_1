using UnityEngine;
using Pathfinding;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections;

public class Timer : MonoBehaviour, StopTimerInteface
{
    [SerializeField] private Animator buttonAnimator;
    [SerializeField] private Animator shakeAnimator;

    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private float DeactivateDistance = 1f;

    private bool timer = true;

    [SerializeField] float time = 100f;

    private AIDestinationSetter aIDestinationSetter;

    [SerializeField] private string endSceneName = "EndScene";

    [System.Obsolete]
    private void Update()
    {
        if (aIDestinationSetter != null)
        {
            if (aIDestinationSetter.target == this.transform)
            {
                float dist = Vector2.Distance(aIDestinationSetter.transform.position, this.transform.position);

                if (dist <= DeactivateDistance)
                {
                    timer = false;
                    buttonAnimator.SetBool("Press", true);
                    GetComponent<CircleCollider2D>().enabled = false;

                    StartCoroutine(NextSceneCoroutine());

                    aIDestinationSetter.GetComponent<Target>().setTargetPos(aIDestinationSetter.transform.position);
                }
            }
            else
            {
                aIDestinationSetter = null;
            }
        }

        if (timer)
        {
            if(time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                shakeAnimator.SetBool("TimeOut", true);
                KillEverything();
                time = 0;
            }
        }

        text.text = time.ToString("F2");
    }

    void StopTimerInteface.Stop(Transform Soldier)
    {
        aIDestinationSetter = Soldier.gameObject.GetComponent<AIDestinationSetter>();
    }

    [System.Obsolete]
    private void KillEverything()
    {
        var objectsWithHealth = FindObjectsOfType<MonoBehaviour>().OfType<HitInterface>();

        foreach(var go in objectsWithHealth)
        {
            go.Kill();
        }

        var objectsWithkill = FindObjectsOfType<MonoBehaviour>().OfType<KillInterface>();

        foreach (var go in objectsWithkill)
        {
            go.Kill();
        }
    }

    IEnumerator NextSceneCoroutine()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(endSceneName);
    }
}
