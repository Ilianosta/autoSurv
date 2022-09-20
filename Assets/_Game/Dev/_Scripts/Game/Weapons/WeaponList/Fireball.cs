using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class Fireball : Weapons
{
    [SerializeField] GameObject fireball;
    [SerializeField] float fireballSpeed;
    public override void Initialize()
    {
        counter = weaponType.stats.attackRate;
        initialized = true;
        // print("Fireball initialized: " + initialized);
    }
    public override void Shoot()
    {
        // print("Shooting");
        counter = weaponType.stats.attackRate;
        StartCoroutine(ShootFireball());
    }
    IEnumerator ShootFireball()
    {
        Vector3 direction = transform.position - PlayerManager.instance.shootController.directionObj.position;
        direction.Normalize();
        GameObject shootedFireball = Instantiate(fireball, transform.position, Quaternion.identity);
        Rigidbody2D fireballRb = shootedFireball.GetComponent<Rigidbody2D>();
        shootedFireball.SetActive(true);
        float duration = 5;
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            fireballRb.velocity = -direction * fireballSpeed;
            yield return null;
        }
        Destroy(shootedFireball);
        // print("Destroying proyectile");
    }
}
