
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    public GameObject holder;
    [SerializeField] private float maxHealth;
    private float currentHealth;
    // effects
    [SerializeField] Image damageEffect;
    [SerializeField] Image healedEffect;
    [SerializeField] float effectTime;
    [SerializeField] float effectDelay;
    [SerializeField] LeanTweenType easeType;
    int healedEffect_Id;
    int damageEffect_Id;
    void Awake()
    {
        Initialize(maxHealth);
    }
    public void Initialize(float health)
    {
        maxHealth = health;
        currentHealth = health;
    }
    public void SetHealth(float health)
    {
        currentHealth = health;
        healthBar.fillAmount = health / maxHealth;
    }
    public void Heal(float amount)
    {
        GetHealed(amount);
    }
    public void Damage(float amount)
    {
        GetDamaged(amount);
    }
    void GetHealed(float amount)
    {
        LeanTween.cancel(damageEffect_Id);

        healedEffect.gameObject.SetActive(true);
        damageEffect.gameObject.SetActive(false);

        currentHealth += amount;
        ClampHealth();
        float newHealth = currentHealth / maxHealth;
        float healthbarfill = healthBar.fillAmount;
        damageEffect.fillAmount = newHealth;
        healedEffect.fillAmount = newHealth;
        healedEffect.gameObject.SetActive(true);
        healedEffect_Id = LeanTween.value(0, 1, effectTime).setEase(easeType).setOnUpdate((float val) =>
        {
            float fill = Mathf.Lerp(healthbarfill, newHealth, val);
            healthBar.fillAmount = fill;
        }).uniqueId;
    }
    void GetDamaged(float amount)
    {
        LeanTween.cancel(healedEffect_Id);

        healedEffect.gameObject.SetActive(false);
        damageEffect.gameObject.SetActive(true);

        currentHealth -= amount;
        ClampHealth();
        float newHealth = currentHealth / maxHealth;
        float healthbarfill = damageEffect.fillAmount;
        healedEffect.fillAmount = newHealth;
        healthBar.fillAmount = newHealth;
        //damageEffect.fillAmount = healthBar.fillAmount;
        damageEffect.gameObject.SetActive(true);
        damageEffect_Id = LeanTween.value(0, 1, effectTime).setEase(easeType).setOnUpdate((float val) =>
        {
            float fill = Mathf.Lerp(healthbarfill, newHealth, val);
            damageEffect.fillAmount = fill;
        }).uniqueId;
    }
    void ClampHealth()
    {
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        if (currentHealth < 0) currentHealth = 0;
    }
}
