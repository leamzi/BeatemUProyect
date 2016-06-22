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
        else if (actions.Attack.IsPressed == true)
        {
            return new GuyAttackingState();
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

public class GuyAttackingState :iPlayableState
{
    private float _last_action;
    private bool _continue_anim;

    public void Enter(PlayableEntity playable_entity)
    {
        _last_action = Time.time;
        _continue_anim = true; 
        if (playable_entity.animator != null)
        {
            playable_entity.animator.Play("ATTACK");
            playable_entity.animator.AnimationEventTriggered = CheckAnimationEvent;
        }
    }

    public iPlayableState HandleInput(PlayableEntity playable_entity, PlayableActions actions)
    {
        if (_continue_anim == false)
        {
            return new GuyIdleState();
        }
        if (actions.Attack.WasReleased == true)
        {
            _last_action = Time.time;
        }
        return null;
    }

    private void CheckAnimationEvent (tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip, int frameNo)
    {
        tk2dSpriteAnimationFrame frame = clip.GetFrame(frameNo);
        //Debug.Log(frame.eventInfo);

        if (frame.eventInfo == "AnimOver")
        {
            if (Time.time - _last_action >= 0.2f)
            {
                _continue_anim = false;
            }
        }
        
    }
}
