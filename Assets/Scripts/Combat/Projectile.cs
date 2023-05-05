using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] bool isHoming = true;
    Health target = null;
    float damage = 0;

    void Start()
    {
        transform.LookAt(GetAimLocation());
    }
    
    void Update()
    {
        if (target == null) return;
        if (isHoming && !target.IsDead)
        {
            transform.LookAt(GetAimLocation());
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    public void SetTarget(Health _target, float _damage)
    {
        this.target = _target;
        this.damage = _damage;
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if (targetCapsule == null)
        {
            return target.transform.position;
        }
        return target.transform.position + Vector3.up * targetCapsule.height / 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != target) return;

        if (target.IsDead) return;
        target.TakeDamge(damage);
        Destroy(gameObject);
    }
}
