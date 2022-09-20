using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public Transform directionObj;
    [SerializeField] List<Weapons> weapons = new List<Weapons>();
    List<GameObject> weaponsGo = new List<GameObject>();
    public void InitializeWeapons()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].initialized) continue;
            GameObject _newWeapon = Instantiate(weapons[i].gameObject, transform);
            weapons[i] = _newWeapon.GetComponent<Weapons>();
            weapons[i].Initialize();
        }
    }
    public void LevelUpWeapon(Weapons weaponToUpgrade, int amount = 1)
    {
        foreach (Weapons weapon in weapons)
        {
            if (weapon.name == weaponToUpgrade.name) weapon.LevelUp(amount);
        }
    }
    void Update()
    {
        Vector3 newPos = new Vector3(transform.position.x + PlayerManager.instance.movement.JoystickDirection.x, transform.position.y + PlayerManager.instance.movement.JoystickDirection.y, 0);
        directionObj.position = newPos;
        if (Input.GetMouseButtonDown(0))
        {
            InitializeWeapons();
            // print("Initializing Weapons");
        }
    }

}
