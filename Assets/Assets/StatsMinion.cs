using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsMinion : MonoBehaviour
{

    [Header("Base Stats")]
    public float health;
    public float damage;

    public float damageLerpDuration;
    private float currentHealth;
    private float targetHealth;
    private Coroutine damageCoroutine;

    HealthUIMinion healthUI;
    // Start is called before the first frame update
    private void Awake()
    {
        healthUI = GetComponent<HealthUIMinion>();

        currentHealth = health;
        targetHealth = health;

        healthUI.Start3DSlider(health);
    }

    // Update is called once per frame
    private void TakeDamage(GameObject target, float damageAmount)
    {
        StatsMinion targetStats = target.GetComponent<StatsMinion>();
        if (targetStats.CompareTag("AllyMinion") && targetStats.targetHealth <= 0)
        {
            Destroy(target.gameObject);
        }
        else if (targetStats.damageCoroutine == null)
        {
            targetStats.StartLerpHealth();
        }

    }

    public void StartLerpHealth()
    {
        if (damageCoroutine == null)
        {
            damageCoroutine = StartCoroutine(LerpHealth());
        }
    }

    private IEnumerator LerpHealth()
    {
        float elapsedTime = 0;
        float initialHealth = currentHealth;
        float target = targetHealth;

        while (elapsedTime < damageLerpDuration)
        { 
            currentHealth = Mathf.Lerp(initialHealth, target, elapsedTime / damageLerpDuration);
            UpdateHealthUI();

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentHealth = target;
        UpdateHealthUI();

        damageCoroutine = null;
    }

    private void UpdateHealthUI()
    {
        healthUI.Update3DSlider(currentHealth);
    }
}
