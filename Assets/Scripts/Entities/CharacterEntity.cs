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

    public virtual eWorldDirection GetDirection() { return (eWorldDirection) _direction_factor; }

#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        if (Utils.show_gizmos)
        {
            if (move_collider != null) //Display Move Collider
            {
                Vector3 origin = (Vector3)move_collider.offset;
                origin.x *= (float)GetDirection();
                origin = transform.position + origin;
                origin.z -= 1;
                Vector3 size = move_collider.size;

                Gizmos.color = new Color(0, 1, 0, 0.5f);
                Gizmos.DrawCube(origin, size);
            }
        }
    }
#endif

    protected override void OnLateUpdate(){}
    protected override void OnAwake() {}
    protected override void OnStart() {}
    protected override void OnUpdate() {}
}
