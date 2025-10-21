using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, HitInterface, IncreaseHealth
{
    [SerializeField] private bool soldier = false;
    [SerializeField] private RectTransform healthBar;
    private Image image;
    [Space]

    [SerializeField] private Animator animator;

    [SerializeField] private float health = 10f;
    private float maxHealth;

    public bool Dead = false;

    private void Awake()
    {
        maxHealth = health;

        Image image = healthBar.GetComponent<Image>();

        Color color;

        if (soldier)
        {
            ColorUtility.TryParseHtmlString("#438D2B", out color);
        }
        else
        {
            ColorUtility.TryParseHtmlString("#8D2B2B", out color);
        }

        image.color = color;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        UpdatehealthBar();

        if (health <= 0f)
        {
            Dead = false;
            health = 0f;

            if (animator != null)
            {
                animator.SetBool("Dead", true);
            }

            this.transform.GetChild(0).transform.parent = null;
            Destroy(this.gameObject);
        }
    }

    void IncreaseHealth.Health(float healthIncrease)
    {
        if(Dead == false)
        {
            health += healthIncrease;

            if(health >= maxHealth)
            {
                health = maxHealth;
            }

            UpdatehealthBar();
        }
    }

    private void UpdatehealthBar()
    {
        float procentage = health / maxHealth;

        healthBar.localScale = new Vector3(procentage, 1f, 1f);
    }
}
