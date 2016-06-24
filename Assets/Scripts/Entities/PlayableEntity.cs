using UnityEngine;
using System.Collections;

public class PlayableEntity : CharacterEntity {

    private tk2dSpriteAnimator _sprite_animator;

    public tk2dSpriteAnimator animator { get { return _sprite_animator; } }

    public eWorldDirection start_direction;

    public BoxCollider2D attack_collider;

    protected override void OnAwake ()
    {
        base.OnAwake();
        _sprite_animator = GetComponent<tk2dSpriteAnimator>();
    }
 
    protected override void OnStart()
    {
        base.OnStart();

        _sprite_animator = GetComponent<tk2dSpriteAnimator>();
        ChangeDirection((float)start_direction);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        bool attack_found = false;

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
                            attack_found = true;

                            Vector3 origin = collider_def.origin;
                            origin.x *= (float)GetDirection();
                            attack_collider.offset = origin;
                            attack_collider.size = collider_def.Size;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        if (attack_collider != null)
        {
            attack_collider.enabled = attack_found;
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
        if (Utils.show_gizmos == true)
        {
            base.OnDrawGizmos();
            if (_sprite != null)
            {
                tk2dSpriteColliderDefinition[] sprite_colliders = _sprite.CurrentSprite.customColliders;
                for (int i = 0; i < sprite_colliders.Length; i++)
                {
                    tk2dSpriteColliderDefinition collider_def = sprite_colliders[i];
                    if (collider_def != null)
                    {
                        switch (collider_def.name)
                        {
                            case "AttackCollider":
                                Vector3 origin = collider_def.origin;
                                origin.x *= (float)GetDirection();
                                origin = transform.position + origin;
                                origin.z -= 1;
                                Vector3 size = collider_def.Size;
                                Gizmos.color = new Color(1, 1, 0, 0.5f);
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
