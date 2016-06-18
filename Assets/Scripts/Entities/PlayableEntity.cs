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

}
