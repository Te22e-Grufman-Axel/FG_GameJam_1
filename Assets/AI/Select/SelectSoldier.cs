using UnityEngine;
using UnityEngine.InputSystem;

public class SelectSoldier : MonoBehaviour
{
    public GameObject selectedSoldier;

    [SerializeField] private InputActionAsset inputActions;
    private InputAction selectAction;
    private InputAction mousePosAction;

    private bool pressed = false;

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        selectAction = InputSystem.actions.FindAction("Attack");
        mousePosAction = InputSystem.actions.FindAction("MousePos");
    }

    void Update()
    {
        float leftClick = selectAction.ReadValue<float>();

        if (leftClick == 1)
        {
            if(pressed == false)
            {
                pressed = true;

                Vector3 mousePosition = mousePosAction.ReadValue<Vector2>();

                Ray mouseRay = Camera.main.ScreenPointToRay(mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);

                //checks if is pressing a soldier if it is then select that soldier else if selected soldier is not null set that pressed location as target
                if (hit.collider != null && hit.collider.gameObject.tag == "Soldier")
                {
                    selectedSoldier = hit.collider.gameObject;
                }
                else if (selectedSoldier != null)
                {
                    if (hit.collider != null && hit.collider.gameObject.tag == "Enemy")
                    {
                        selectedSoldier.GetComponent<Target>().setTransformAsTarget(hit.collider.transform);
                    }
                    else if (hit.collider != null && hit.collider.gameObject.tag == "StopButton")
                    {
                        selectedSoldier.GetComponent<Target>().setTransformAsTarget(hit.collider.transform);
                        hit.collider.gameObject.GetComponent<StopTimerInteface>().Stop(selectedSoldier.transform);
                    }
                    else if (hit.collider != null && hit.collider.gameObject.tag == "HealthPack")
                    {
                        selectedSoldier.GetComponent<Target>().setTransformAsTarget(hit.collider.transform);
                        hit.collider.gameObject.GetComponent<HealthPackInterface>().HealthPack(selectedSoldier.transform);
                    }
                    else
                    {
                        selectedSoldier.GetComponent<Target>().setTargetPos(mouseRay.origin);
                    }
                }
            }
        }
        else if(leftClick == 0)
        {
            pressed = false;
        }
    }
}
