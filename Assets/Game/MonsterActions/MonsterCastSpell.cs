﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class MonsterCastSpell : MonsterAction
{
    public Spell spell;

    public override bool Execute()
    {
        var newSpell = Instantiate(spell, monster.transform.position, monster.transform.rotation);
        newSpell.source = monster;

        return true;
    }
}
