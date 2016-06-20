using UnityEngine;
using System.Collections;

public enum eWorldDirection
{
    left = 1,
    right = -1
}

public class CharacterEntity : MovableEntity {

    protected float _direction_factor;

    protected void ChangeDirection (float dir_factor)
    {
        if (dir_factor != _direction_factor)
        {
            Vector3 local_scale = transform.localScale;

            if (local_scale.x != dir_factor )
            {
                local_scale.x = dir_factor;
                transform.localScale = local_scale;
            }
            _direction_factor = dir_factor;
        } 
    }

    public virtual eWorldDirection GetDirection() { return (eWorldDirection) _direction_factor; }

    protected override void OnLateUpdate(){}
    protected override void OnAwake() {}
    protected override void OnStart() {}
    protected override void OnUpdate() {}
}
