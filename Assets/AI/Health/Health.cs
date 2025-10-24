using System.Collections.Generic;
using Unity.VisualScripting;
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
            UnityEngine.ColorUtility.TryParseHtmlString("#438D2B", out color);
        }
        else
        {
            UnityEngine.ColorUtility.TryParseHtmlString("#8D2B2B", out color);
        }

        image.color = color;
    }

    [System.Obsolete]
    public void TakeDamage(float damage)
    {
        health -= damage;

        UpdatehealthBar();

        if (health <= 0f)
        {
            Dead = true;
            health = 0f;

            if (animator != null)
            {
                animator.SetBool("Dead", true);
            }

            if(soldier == true)
            {
                Health[] healthScript = FindObjectsOfType<Health>();

                List<Health> soldierHealthscripts = new List<Health>();

                foreach (Health soldierHealth in healthScript)
                {
                    if (soldierHealth.soldier == true)
                    {
                        soldierHealthscripts.Add(soldierHealth);
                    }
                }

                if (soldierHealthscripts.Count - 1 <= 0)
                {
                    FindObjectOfType<SelectSoldier>().End();
                }
            }

            this.gameObject.GetComponent<DrawPath>().drawNewPath = true;

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

    public void Kill()
    {
        Dead = true;
        health = 0f;

        if (animator != null)
        {
            animator.SetBool("Dead", true);
        }

        this.transform.GetChild(0).transform.parent = null;
        Destroy(this.gameObject);
    }
}
