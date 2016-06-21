using UnityEngine;
using System.Collections;

public abstract class MovableEntity : MonoBehaviour {

    private Collider2D[] _surronding_colliders;

    protected int _layer_mask;
    protected Vector2 _velocity;

    public float speed;
    public float up_speed_factor;

    public string[] collider_layers = null;
    public BoxCollider2D move_collider = null;

    private void Awake ()
    {
        OnAwake();
    }

    private void Start ()
    {
        _velocity = new Vector2();
        _surronding_colliders = new Collider2D[16];

        // Clamp initial position to integer values
        Warp (ClampPosition(transform.position));

        //Update Layer mask
        _layer_mask = 0;
        if (collider_layers != null)
        {
            for (int i = 0; i < collider_layers.Length; i++)
            {
                _layer_mask |= 1 << LayerMask.NameToLayer(collider_layers[i]);
            }
        }

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

            //if movement has an impact on colliders 
            if ( UpdateSurroundingColliders(clamped_movement) > 0)
            {
                if (CanMove(clamped_movement) == false)
                {
                    bool move_x = CanMoveX(clamped_movement.x);
                    bool move_y = CanMoveY(clamped_movement.y);

                    if (move_x == false)
                    {
                        clamped_movement.x = 0.0f;
                    }

                    if (move_y == false)
                    {
                        clamped_movement.y = 0.0f;
                    }
                }
            }

            if (clamped_movement != Vector2.zero)
            {
                //Move to the new position
                Warp(Translate(clamped_movement));
            }
            
        }

        OnLateUpdate();
    }
    //Returns the number of the colliders impact by movement (also updates _surrounding_colliders list)
    private int UpdateSurroundingColliders (Vector2 movement)
    {
        if ( move_collider != null )
        {
            Vector2 move_center = move_collider.offset;
            move_center.x *= transform.localScale.x;
            move_center += (Vector2)transform.position + movement;
            Vector2 move_min_point = move_center - move_collider.size / 2.0f;
            Vector2 move_max_point = move_center + move_collider.size / 2.0f;

            return Physics2D.OverlapAreaNonAlloc(move_min_point, move_max_point, _surronding_colliders, _layer_mask);
        }
        return 0;
    }

    private bool CanMove (Vector2 movement)
    {
        if (move_collider != null && _surronding_colliders != null)
        {
            Vector2 move_center = move_collider.offset;
            move_center.x *= transform.localScale.x;
            move_center += (Vector2)transform.position + movement;

            Vector2 move_min_point = move_center - move_collider.size / 2.0f;
            Vector2 move_max_point = move_center + move_collider.size / 2.0f;

            for (int i = 0; i < _surronding_colliders.Length; i++)
            {
                //checked tat we're not dealing with our own collider
                if ( _surronding_colliders[i] != null && _surronding_colliders[i].gameObject != gameObject)
                {
                    BoxCollider2D other_collider = (BoxCollider2D)_surronding_colliders[i];
                    Vector2 other_center = other_collider.offset;

                    other_center += (Vector2)other_collider.transform.position;
                    Vector2 other_size = other_collider.size;
                    Vector2 other_min_point = other_center - other_size / 2.0f;
                    Vector2 other_max_point = other_center + other_size / 2.0f;

                    if ( (move_min_point.x < other_max_point.x && move_max_point.x > other_min_point.x 
                        && move_min_point.y < other_max_point.y && move_max_point.y > other_min_point.y) == true)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    private bool CanMoveX (float x)
    {
        return CanMove(new Vector2(x, 0));
    }

    private bool CanMoveY (float y)
    {
        return CanMove(new Vector2(0, y));
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
        Vector3 position = (Vector3)ClampPosition(warp_pos);
        position.z = position.y;
        transform.position = position;
    }

    public virtual void GoLeft (float factor = 1.0f)
    {
        _velocity.x -= speed * Time.deltaTime * factor;
    }

    public virtual void GoRight(float factor = 1.0f)
    {
        _velocity.x += speed * Time.deltaTime * factor;
    }

    public virtual void GoUp(float factor = 1.0f)
    {
        _velocity.y += speed * Time.deltaTime * factor * up_speed_factor;
    }

    public virtual void GoDown(float factor = 1.0f)
    {
        _velocity.y -= speed * Time.deltaTime * factor * up_speed_factor;
    }

    public virtual void Move(Vector2 move_factor)
    {
        if (move_factor.x > 0 )
        {
            GoRight();
        }
        else if (move_factor.x < 0)
        {
            GoLeft();
        }

        if (move_factor.y > 0)
        {
            GoUp();
        }
        else if (move_factor.y < 0)
        {
            GoDown();
        }
    }

    // --- ABSTACT METHODS --- //
    protected abstract void OnStart();
    protected abstract void OnAwake();
    protected abstract void OnUpdate();
    protected abstract void OnLateUpdate();

}
