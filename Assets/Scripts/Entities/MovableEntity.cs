using UnityEngine;
using System.Collections;

public abstract class MovableEntity : MonoBehaviour {

    protected Vector2 _velocity;

    public float speed;
    public float up_speed_factor;

    private void Start ()
    {
        _velocity = new Vector2();

        Vector3 initial_position = transform.position;
        // Clamp initial position to integer values
        transform.position = new Vector3((int)initial_position.x, (int)initial_position.y, initial_position.z);

        OnStart();
    }

    private void Update ()
    {
        OnUpdate();
    }

    private void LateUpdate ()
    {
        //Clamp the current velocity
        Vector2 clamped_movement = new Vector2((int)_velocity.x, (int)_velocity.y);
        // Check if a movement is needed (more than 1 px to move)
        if ( clamped_movement.magnitude >= 1.0f)
        {
            //Update velocity, removing the actual movement
            _velocity = _velocity - clamped_movement;
            //Move to new position
            Warp(Translate(clamped_movement));
        }

        OnLateUpdate();
    }

    private Vector2 Translate (Vector2 move_pos)
    {
        return move_pos + (Vector2)transform.position;
    }

    private Vector2 ClampPosition (Vector2 vector_pos)
    {
        vector_pos.x = (int)vector_pos.x;
        vector_pos.y = (int)vector_pos.y;
        return vector_pos;
    }

    private void Warp( Vector2 warp_pos)
    {
        transform.position = (Vector3)ClampPosition(warp_pos);
    }

    // --- ABSTACT METHODS --- //
    protected abstract void OnStart();
    protected abstract void OnUpdate();
    protected abstract void OnLateUpdate();

}
