using UnityEngine;
using System.Collections;

public class GuyIdleState : iPlayableState
{
    public void Enter (PlayableEntity playable_entity)
    {
        if (playable_entity.animator != null)
            playable_entity.animator.Play("IDLE");
    }

    public iPlayableState HandleInput(PlayableEntity playable_entity, PlayableActions actions)
    {
        if (actions.Move.IsPressed == true)
        {
            return new GuyMovingState();
        }

        return null;
    }
}

public class GuyMovingState : iPlayableState
{
    public void Enter(PlayableEntity playable_entity)
    {
        if (playable_entity.animator != null)
        {
            playable_entity.animator.Play("WALK");

        }
    }

    public iPlayableState HandleInput(PlayableEntity playable_entity, PlayableActions actions)
    {
        if (actions.Move.IsPressed == false)
        {
            return new GuyIdleState();
        }
        else
        {
            playable_entity.Move(actions.Move);
        }

        return null;
    }
}
