﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public abstract class MonsterAction : MonoBehaviour
{
    [NonSerialized]
    public Monster monster;

    public abstract bool Execute();
}
