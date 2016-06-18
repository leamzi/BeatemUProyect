using UnityEngine;
using System.Collections;
using System;

public class PlayableEntity : CharacterEntity {

    public eWorldDirection start_direction;
 
    protected override void OnStart()
    {
        base.OnStart();
        ChangeDirection((float)start_direction);
    }

    public override void GoLeft(float factor = 1.0f)
    {
        base.GoLeft(factor);
        ChangeDirection((float)eWorldDirection.left);
    }

    public override void GoRight(float factor = 1.0f)
    {
        base.GoRight(factor);
        ChangeDirection((float)eWorldDirection.right);
    }
}
