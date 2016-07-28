using UnityEngine;
using System.Collections;

[RequireComponent(typeof(tk2dSpriteAnimator))]
public class SentryScrew : HitParticle
{
    private tk2dSpriteAnimator _animator;
    private float _start_time;
    private Vector2 _acceleration;
    private Vector2 _velocity;
    private float _ttl;

    protected override void OnStart()
    {
        _animator = GetComponent<tk2dSpriteAnimator>();
        if (_animator != null)
        {
            _animator.Play("SentryScrew");
        }

        _start_time = Time.time;

        float ax = Random.Range(min_acceleration.x, max_acceleration.x);
        float ay = Random.Range(min_acceleration.y, max_acceleration.y);

        _acceleration = new Vector2(ax, ay);
        //Set random direction
        _acceleration.x *= (float)(Random.value > 0.5f ? eWorldDirection.left : eWorldDirection.right);
        _velocity = new Vector2();
        _ttl = Random.Range(min_ttl, max_ttl);
    }

    protected override void OnUpdate()
    {
        if (_animator.IsPlaying("SentryScrew"))
        {
            float t = (Time.time - _start_time);

            Vector2 a = _acceleration * Time.deltaTime;
            _velocity.x += a.x;
            _velocity.y += a.y - t; //Apply gravity on Y axis

            Vector2 clamped_movement = new Vector2((int)_velocity.x, (int)_velocity.y);
            if (clamped_movement.magnitude >= 1.0f)
            {
                _velocity = _velocity - clamped_movement;
                transform.position = transform.position + (Vector3)clamped_movement;
            }

            if (t >= _ttl)
            {
                _animator.Stop();
            }
        }
    }

    protected override void OnLateUpdate() {}
    protected override void OnAwake() {}
}
