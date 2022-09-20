using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons", fileName = "New Weapon")]
public class WeaponsSO : ScriptableObject
{
    public Sprite image;
    public string description;
    public WeaponType weaponType;
}
[System.Serializable]
public class WeaponType
{
    public string name;
    public int level;
    public float attackLvlMultiplier, attackRateLvlMultiplier, onHitLvlMultiplier;
    public WeaponStats stats;
}
[System.Serializable]
public class WeaponStats
{
    [SerializeField] float damageBase, attackRateBase;
    [HideInInspector] public float damage, attackRate;
    public WeaponOnHitEffects[] onHitEffects;
    public void Upgrade(WeaponType weapon)
    {
        damage = damageBase + ((damageBase * weapon.level) * weapon.attackLvlMultiplier);
        attackRate = attackRateBase + ((attackRateBase * weapon.level) * weapon.attackRateLvlMultiplier);
        foreach (WeaponOnHitEffects onHitEffect in onHitEffects) onHitEffect.Upgrade(weapon.onHitLvlMultiplier * weapon.level);
    }
}
[System.Serializable]
public class WeaponOnHitEffects
{
    public OnHitEffects effectType;
    public float amountBase;
    float amount = 0;

    public void Upgrade(float multiplier)
    {
        if (amountBase != 0)
            amount = amountBase + (amountBase * multiplier);
    }
}
public enum OnHitEffects
{
    SLOW, POISON, FIRE, THUNDER, WEAKNESS
}
