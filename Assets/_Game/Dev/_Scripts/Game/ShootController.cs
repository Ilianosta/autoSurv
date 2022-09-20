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
        foreach (Weapons weapon in weapons)
        {
            GameObject _newWeapon = Instantiate(weapon.gameObject, transform);
            _newWeapon.GetComponent<Weapons>().Initialize();
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
        directionObj.position = new Vector3(transform.position.x + PlayerManager.instance.movement.JoystickDirection.x, transform.position.y + PlayerManager.instance.movement.JoystickDirection.y, 0);
        if (Input.GetMouseButtonDown(0))
        {
            InitializeWeapons();
            // print("Initializing Weapons");
        }
    }

}
