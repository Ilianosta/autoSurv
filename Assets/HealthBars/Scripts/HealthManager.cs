using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthManager
{
    // CONSTRUCTOR ------------------------------------------------------------
    public HealthManager(float _maxHealth, HealthBar _lifebar = null)
    {
        maxHealth = _maxHealth;
        lifebar = _lifebar;
        Initialize();
    }
    // ------------------------------------------------------------------------
    public HealthBar lifebar;
    public float maxHealth = 100f;
    float actualHealth;
    public float ActualHealth => actualHealth;
    public bool isDead => actualHealth <= 0;
    void Initialize()
    {
        actualHealth = maxHealth;
        if (lifebar != null) lifebar.Initialize(maxHealth);
    }
    public void TakeDamage(float damage)
    {
        actualHealth -= damage;
        if (lifebar != null) lifebar.Damage(damage);
        ClampHealth();
    }
    public void Heal(float heal)
    {
        actualHealth += heal;
        if (lifebar != null) lifebar.Heal(heal);
        ClampHealth();
    }
    public void SetHealth(float amount)
    {
        actualHealth = amount;
        if (lifebar != null) lifebar.SetHealth(amount);
    }
    void ClampHealth()
    {
        if (actualHealth > maxHealth) actualHealth = maxHealth;
        else if (actualHealth < 0) actualHealth = 0;
    }
}
