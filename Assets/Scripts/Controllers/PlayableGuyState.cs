﻿using UnityEngine;
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
            playable_entity.Move(actions.Move.Value);
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

    private void SpawnHitFX(PlayableEntity playable_entity, BoxCollider2D attack_collider, BoxCollider2D other_collider)
    {
        if ( playable_entity.hitfx_prefabs != null && playable_entity.hitfx_prefabs.Length > 0)
        {
            int id = Random.Range(0, playable_entity.hitfx_prefabs.Length);
            Transform fx_prefab = playable_entity.hitfx_prefabs[id];

            Vector2 min_point = new Vector2();
            min_point.x = attack_collider.offset.x > other_collider.offset.x ? attack_collider.offset.x : other_collider.offset.x;
            min_point.y = attack_collider.offset.x > other_collider.offset.y ? attack_collider.offset.y : other_collider.offset.y;

            Vector2 max_point = new Vector2();
            max_point.x = attack_collider.offset.x + attack_collider.size.x < other_collider.offset.x + other_collider.size.x ? attack_collider.offset.x + attack_collider.size.x : other_collider.offset.x + other_collider.size.x;
            max_point.y = attack_collider.offset.y + attack_collider.size.y < other_collider.offset.y + other_collider.size.y ? attack_collider.offset.y + attack_collider.size.y : other_collider.offset.y + other_collider.size.y;

            Vector3 fx_position = Vector2.Lerp(min_point, max_point, Random.value);
            fx_position = fx_position + playable_entity.transform.position;
            //fx_position.z = (attack_collider.transform.position.z < other_collider.transform.position.z ? attack_collider.transform.position.z : other_collider.transform.position.z) + ;

            Object.Instantiate(fx_prefab, fx_position, new Quaternion());
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

        BoxCollider2D attack_collider = playable_entity.attack_collider;
        if (attack_collider != null && attack_collider.enabled == true)
        {
            BoxCollider2D[] overlapped_colliders = new BoxCollider2D[8];

            Vector2 min_point = attack_collider.offset;
            min_point.x *= (float) playable_entity.GetDirection(); 
            min_point = min_point + (Vector2) playable_entity.transform.position - attack_collider.size / 2.0f;
            Vector2 max_point = min_point + attack_collider.size;

            if (Physics2D.OverlapAreaNonAlloc(min_point, max_point, overlapped_colliders) > 0)
            {
                for (int i = 0; i < overlapped_colliders.Length; i++)
                {
                    BoxCollider2D other_collider = overlapped_colliders[i]; 
                    if (other_collider != null && LayerMask.LayerToName(other_collider.gameObject.layer) == "HitZoneCollider")
                    {
                        GameObject hit_gameobject = other_collider.transform.parent.parent.gameObject;
                        if (playable_entity.gameObject != hit_gameobject)
                        {
                            string layer_name = LayerMask.LayerToName(hit_gameobject.layer);
                            switch (layer_name)
                            {
                                case "Enemy":
                                    EnemyController controller = hit_gameobject.GetComponent<EnemyController>();
                                    if (controller != null)
                                    {
                                        controller.SetHit(playable_entity.transform);
                                    }
                                    break;
                                default:

                                    break;
                            }
                        }
                    }
                }
            }
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
