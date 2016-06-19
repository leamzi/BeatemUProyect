using UnityEngine;
using System.Collections;

public class PlayableEntity : CharacterEntity {

    private tk2dSpriteAnimator _sprite_animator;

    public tk2dSpriteAnimator animator { get { return _sprite_animator; } }

    public eWorldDirection start_direction;
 
    protected override void OnStart()
    {
        base.OnStart();

        _sprite_animator = GetComponent<tk2dSpriteAnimator>();
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
