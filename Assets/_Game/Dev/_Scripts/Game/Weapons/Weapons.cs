using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapons : MonoBehaviour
{
    public WeaponsSO weaponSO;
    public bool initialized;
    protected float counter;
    protected WeaponType weaponType;
    protected virtual void Awake()
    {
        weaponType = weaponSO.weaponType;
        weaponType.stats.Upgrade(weaponType);
    }
    public virtual void Initialize()
    {
        counter = weaponType.stats.attackRate;
        initialized = true;
    }
    public abstract void Shoot();
    public virtual void LevelUp(int amount = 1)
    {
        weaponType.level += amount;
        weaponType.stats.Upgrade(weaponType);
    }
    protected virtual void Update()
    {
        // print(gameObject.name + " counter: " + counter);
        if (initialized) counter -= Time.deltaTime;
        if (counter < 0) Shoot();
    }
}

