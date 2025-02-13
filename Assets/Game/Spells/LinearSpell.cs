﻿using UnityEngine;

public class LinearSpell : Spell
{
    [System.NonSerialized]
    public Vector3 start;
    [System.NonSerialized]
    public Vector3 end;
    
    public int maxDistance = 500;
        
    public bool wallCollision = true;
        
    void Start()
    {
        Initialize();
        SetVelocity();
    }

    void Update()
    {
        if (Vector3.Distance(start, transform.position) > maxDistance)
        {
            Destroy(transform.gameObject);
        }
    }

    public void SetVelocity()
    {
        if (start == Vector3.zero)
        {
            start = transform.position;
        }

        if (end == Vector3.zero)
        {
            end = transform.position + transform.forward * maxDistance;
        }
        
        var dir = (end - start).normalized;
        dir.y = 0;

        if (speed > 0)
        {
            rigidbody.AddForce(dir * speed, ForceMode.Impulse);
        }

        if (dir != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(dir);
            rigidbody.MoveRotation(newRotation);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (isDead) return;

        if (wallCollision && collision.gameObject.layer == LayerConstants.wallLayer)
        {
            Death();
            return;
        }

        var unit = collision.GetUnit();
        if (unit != null)
        {
            var dir = (transform.position - start);
            dir.y = .15f;

            var force = dir * 100;

            unit.TakeDamage(InitializeHitInfo(unit, start, transform.position, force));
        }
    }

    public override void Death()
    {
        isDead = true;
        Destroy(transform.gameObject);
    }
}
