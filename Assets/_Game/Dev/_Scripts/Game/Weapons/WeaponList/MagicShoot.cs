using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShoot : Weapons
{
    [SerializeField] GameObject shootObj;
    [SerializeField] float proyectileSpeed;
    public override void Shoot()
    {
        counter = weaponType.stats.attackRate;
        StartCoroutine(ShootMagic());
    }
    IEnumerator ShootMagic()
    {
        Transform objetive = PlayerManager.instance.FOV.GetRandomTarget;
        if (objetive == null) objetive = PlayerManager.instance.shootController.directionObj;
        Vector3 direction = transform.position - objetive.position;
        direction.Normalize();
        GameObject shootedFireball = Instantiate(shootObj, transform.position, Quaternion.identity);
        Rigidbody2D fireballRb = shootedFireball.GetComponent<Rigidbody2D>();
        shootedFireball.SetActive(true);
        float duration = 5;
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            fireballRb.velocity = -direction * proyectileSpeed;
            yield return null;
        }
        Destroy(shootedFireball);
        // print("Destroying proyectile");
    }
}
