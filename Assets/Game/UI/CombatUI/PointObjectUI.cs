﻿using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PointObjectUI : CombatUI
{
    public Trap trap;

    public override void Initialize(Weapon weapon)
    {
        this.weapon = weapon;
    }

    public override void OnTouchStart(Touch touch)
    {
        var pos = GameManager.instance.GetTouchPosition(touch.position, 1f);
        pos = GetLanePostion(pos);

        var newSpell = Instantiate(trap.gameObject, pos, Quaternion.identity);

        if(callBack != null)
        {
            callBack();
        }
    }

    public Vector3 GetLanePostion(Vector3 pos)
    {
        if(pos.x < -0.75f)
        {
            pos.x = -1.5f;
        }
        else if(pos.x > 0.75f)
        {
            pos.x = 1.5f;
        }
        else
        {
            pos.x = 0f;
        }

        pos.y = 0.5f;

        return pos;
    }
}
