using UnityEngine;
using System.Collections;
using System;

public class PlayableGuyController : PlayableController
{
    protected override iPlayableState GetDefaultState()
    {
        return new GuyIdleState();
    }
}
