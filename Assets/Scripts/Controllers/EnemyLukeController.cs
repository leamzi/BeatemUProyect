﻿using UnityEngine;
using System.Collections;
using System;

public class EnemyLukeController : EnemyController
{
    protected override iEnemyState GetDefaultState()
    {
        return new EnemyLukeIdleState();
    }

    public override void SetHit()
    {
        Debug.Log(gameObject.name + " is Hit");
    }
}
