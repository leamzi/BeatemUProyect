using UnityEngine;
using System.Collections;
using System;

public class PlayableGuyController : PlayableController
{
    protected override iPlayableState GetDefaultState()
    {
        return new GuyIdleState();
    }

    public override void SetDeath()
    {
        throw new NotImplementedException();
    }

    public override void SetHit(Transform dealer_transform, int hit_damage, Vector3 hit_position)
    {
        throw new NotImplementedException();
    }
}
