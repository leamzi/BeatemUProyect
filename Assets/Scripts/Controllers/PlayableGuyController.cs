using UnityEngine;
using System.Collections;

public class PlayableGuyController : PlayableController
{
    protected override iPlayableState GetDefaultState()
    {
        return new GuyIdleState();
    }
}
