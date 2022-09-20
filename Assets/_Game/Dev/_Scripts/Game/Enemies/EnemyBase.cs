using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    Transform target;
    protected virtual void Start()
    {
        target = PlayerManager.instance.transform;
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void Update()
    {
        Vector3 direction = transform.position - target.position;
        direction.Normalize();
        rb.velocity = -direction * speed;
    }
}
