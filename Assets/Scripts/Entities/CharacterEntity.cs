using UnityEngine;
using System.Collections;

public enum eWorldDirection
{
    left = 1,
    right = -1
}

public class CharacterEntity : MovableEntity
{
    private tk2dSpriteAnimator _sprite_animator;

    protected float _direction_factor;

    public BoxCollider2D attack_collider;
    public BoxCollider2D hitzone_collider;

    public Transform shadow;

    public tk2dSpriteAnimator animator { get { return _sprite_animator; } }

    public eWorldDirection start_direction;

    public virtual eWorldDirection GetDirection() { return (eWorldDirection)_direction_factor; }

    protected override void OnAwake()
    {
        _sprite_animator = GetComponent<tk2dSpriteAnimator>();
    }

    protected override void OnStart()
    {
        base.OnStart();
        ChangeDirection((float)start_direction);
    }

    public void ChangeDirection (float dir_factor)
    {
        if (dir_factor != _direction_factor)
        {
            Vector3 local_scale = transform.localScale;

            if (local_scale.x != dir_factor )
            {
                local_scale.x = dir_factor;
                transform.localScale = local_scale;

                if (move_collider != null)
                {
                    Vector3 pos = transform.position;
                    pos.x += -local_scale.x * move_collider.offset.x * 2.0f;
                    transform.position = pos;
                }
               
            }
            _direction_factor = dir_factor;
        } 
    }

    protected override void OnUpdate()
    {
        if (attack_collider != null)
        {
            attack_collider.enabled = false;
        }

        if (hitzone_collider != null)
        {
            hitzone_collider.enabled = false;
        }

        tk2dSpriteColliderDefinition[] sprite_colliders = _sprite.CurrentSprite.customColliders;
        for (int i = 0; i < sprite_colliders.Length; i++)
        {
            tk2dSpriteColliderDefinition collider_def = sprite_colliders[i];
            if (collider_def != null)
            {
                switch (collider_def.name)
                {
                    case "AttackCollider":
                        if (attack_collider != null)
                        {
                            Vector3 origin = collider_def.origin;
                            attack_collider.offset = origin;
                            attack_collider.size = collider_def.Size;
                            attack_collider.enabled = true;
                        }
                        break;
                    case "HitZoneCollider":
                        if (hitzone_collider != null)
                        {
                            Vector3 origin = collider_def.origin;
                            origin.z -= 1;
                            hitzone_collider.offset = origin;
                            hitzone_collider.size = collider_def.Size;
                            hitzone_collider.enabled = true;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
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


#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if (Utils.show_gizmos == true)
        {
            if (_sprite != null)
            {
                tk2dSpriteColliderDefinition[] sprite_colliders = _sprite.CurrentSprite.customColliders;
                for (int i = 0; i < sprite_colliders.Length; i++)
                {
                    tk2dSpriteColliderDefinition collider_def = sprite_colliders[i];
                    if (collider_def != null)
                    {
                        Vector3 origin;
                        Vector3 size;
                        switch (collider_def.name)
                        {
                            case "AttackCollider":
                                origin = collider_def.origin;
                                origin.x *= (float)GetDirection();
                                origin = transform.position + origin;
                                origin.z -= 1;
                                size = collider_def.Size;
                                Gizmos.color = new Color(1, 1, 0, 0.8f);
                                Gizmos.DrawCube(origin, size);
                                break;
                            case "HitZoneCollider":
                                origin = collider_def.origin;
                                origin.x *= (float)GetDirection();
                                origin = transform.position + origin;
                                origin.z -= 1;
                                size = collider_def.Size;
                                Gizmos.color = new Color(0, 1, 1, 0.8f);
                                Gizmos.DrawCube(origin, size);
                                break;
                            case "MoveCollider":
                                origin = collider_def.origin;
                                origin.x *= (float)GetDirection();
                                origin = transform.position + origin;
                                origin.z -= 1;
                                size = collider_def.Size;
                                Gizmos.color = new Color(0, 1, 0, 0.8f);
                                Gizmos.DrawCube(origin, size);
                                break;
                            default:
                                break;
                        }
                    }
                }
               }
            }
        }
#endif

    
}
