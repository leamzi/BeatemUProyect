using UnityEngine;
using System.Collections;

public class PlayableEntity : CharacterEntity {

    private tk2dSpriteAnimator _sprite_animator;

    public tk2dSpriteAnimator animator { get { return _sprite_animator; } }
    public Transform[] hitfx_prefabs;

    protected override void OnAwake ()
    {
        base.OnAwake();
        _sprite_animator = GetComponent<tk2dSpriteAnimator>();
    }
}
