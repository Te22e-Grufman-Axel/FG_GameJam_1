using UnityEngine;
using Pathfinding;
using TMPro;

public class Timer : MonoBehaviour, StopTimerInteface
{
    [SerializeField] private Animator buttonAnimator;
    [SerializeField] private Animator shakeAnimator;

    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private float DeactivateDistance = 1f;

    private bool timer = true;

    [SerializeField] float time = 100f;

    private AIDestinationSetter aIDestinationSetter;

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
                time = 0;
            }
        }

        text.text = time.ToString("F2");
    }

    void StopTimerInteface.Stop(Transform Soldier)
    {
        aIDestinationSetter = Soldier.gameObject.GetComponent<AIDestinationSetter>();
    }
}
