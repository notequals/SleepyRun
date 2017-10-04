﻿using UnityEngine;

public class CircularSpell : Spell
{
    //public GameObject particleOnHit;

    void Start()
    {
        DamageMonsters(transform.position);
    }

    void DamageMonsters(Vector3 hitPos)
    {
        var monsterObjects = Physics.OverlapSphere(hitPos, radius, LayerConstants.monsterMask);
        foreach (var monsterObject in monsterObjects)
        {
            var monster = monsterObject.GetComponent<Monster>();
            if (monster)
            {
                var monsterPos = monster.transform.position;

                GameManager.instance.comboManager.IncreaseComboCount();

                var dir = (monsterPos - hitPos).normalized;
                //var dir = Vector3.Cross(hitPos, monsterPos).normalized;
                //dir = transform.rotation * dir;

                dir.y = .15f;

                var force = dir * 1000;

                HitInfo hitInfo = new HitInfo
                {
                    hitStart = hitPos,
                    hitEnd = monsterPos,
                    force = force,
                };

                monster.Death(hitInfo);
            }
        }
    }

    public override void Death()
    {
        isDead = true;
        Destroy(transform.gameObject);
    }
}
