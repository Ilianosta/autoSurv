using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapons : MonoBehaviour
{
    public WeaponsSO weaponSO;
    protected bool initialized;
    protected float counter;
    protected WeaponType weaponType;
    protected virtual void Awake()
    {
        weaponType = weaponSO.weaponType;
        weaponType.stats.Upgrade(weaponType);
    }
    public abstract void Initialize();
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

