using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Teams : MonoBehaviour
{
    [SerializeField] private Image[] healthBars;
    [SerializeField] private Image[] hitEffects;
    [SerializeField] private float maxHealth;
    private float totalHealth;
    private float currentHealth_T1;
    private float currentHealth_T2;
    [SerializeField] Color team_1_color;
    [SerializeField] Color hit_team_1_color;
    [SerializeField] Color team_2_color;
    [SerializeField] Color hit_team_2_color;

    [Header("Effects")]
    [SerializeField] Image damageEffect;
    [SerializeField] Image healedEffect;
    [SerializeField] float effectTime;
    [SerializeField] LeanTweenType easeType;
    [Header("Debug")]
    [SerializeField] Team attacker;
    void Awake()
    {
        SetMaxHealth(maxHealth);
        SetMaxHealth(maxHealth);
    }
    void Start()
    {
        healthBars[0].color = team_1_color;
        healthBars[1].color = team_2_color;
        hitEffects[0].color = hit_team_1_color;
        hitEffects[1].color = hit_team_2_color;
    }
    public void SetMaxHealth(float health)
    {
        currentHealth_T1 = health;
        currentHealth_T2 = health;
        totalHealth = health * 2;
        // Set fill amount for each health bar
        for (int i = 0; i < healthBars.Length; i++)
        {
            healthBars[i].fillAmount = currentHealth_T1 / (totalHealth);
            hitEffects[i].fillAmount = healthBars[i].fillAmount;
        }
    }
    // Comment this function if u dont want to use it
    public void DebugDamage()
    {
        float amount = 25;
        Team team = attacker;
        StartCoroutine(HealthBarPush(amount, team));
    }
    public void Damage(float amount, Team team)
    {
        StartCoroutine(HealthBarPush(amount, team));
    }
    void UpdateBars(float fill1, float fill2)
    {
        healthBars[0].fillAmount = fill1 / totalHealth;
        healthBars[1].fillAmount = fill2 / totalHealth;
    }

    IEnumerator HealthBarPush(float amount, Team team)
    {
        if (team == Team.enemies) amount = -amount;
        float newHealth_T1 = currentHealth_T1 + amount;
        float auxHealth_T1 = currentHealth_T1;
        float newHealth_T2 = currentHealth_T2 - amount;
        float auxHealth_T2 = currentHealth_T2;
        currentHealth_T1 += amount;
        currentHealth_T2 -= amount;
        ClampHealth();
        hitEffects[0].fillAmount = newHealth_T1 / totalHealth;
        hitEffects[1].fillAmount = newHealth_T2 / totalHealth;
        LeanTween.value(0, 1, effectTime).setEase(easeType).setOnUpdate((float val) =>
        {
            float fill1 = Mathf.Lerp(auxHealth_T1, newHealth_T1, val);
            float fill2 = Mathf.Lerp(auxHealth_T2, newHealth_T2, val);
            UpdateBars(fill1, fill2);
        });
        yield return null;
    }
    void ClampHealth()
    {
        if (currentHealth_T1 > totalHealth) currentHealth_T1 = totalHealth;
        if (currentHealth_T2 > totalHealth) currentHealth_T2 = totalHealth;
        if (currentHealth_T1 < 0) currentHealth_T1 = 0;
        if (currentHealth_T2 < 0) currentHealth_T2 = 0;
    }
}
public enum Team
{
    allies,
    enemies
}
