using UnityEngine;
using System.Collections;

public abstract class EnemyController : EntityController {

    protected iEnemyState _state;
    public EnemyEntity enemy_entity;

    private void Start ()
    {
        _state = GetDefaultState();
        _state.Enter(enemy_entity);
    }

    private void Update ()
    {
        iEnemyState new_state = _state.HandleInput(enemy_entity);
        if ( new_state != null)
        {
            _state = new_state;
            _state.Enter(enemy_entity);
        }
    }

    public void SetState(iEnemyState new_state)
    {
        _state = new_state;
        _state.Enter(enemy_entity);
    }

    public override void SetHit(Transform dealer_transform, int hit_damage, Vector3 hit_position)
    {
        ApplyHitState(dealer_transform);
        if ( OnHit != null)
        {
            OnHit(dealer_transform, hit_damage, hit_position);
        }
    }

    public override void SetDeath()
    {
        ApplyDeathState();

        if (OnDeath != null)
        {
            OnDeath();
        }
    }

    protected abstract iEnemyState GetDefaultState();
    protected abstract void ApplyHitState(Transform dealer_transform);
    protected abstract void ApplyDeathState();
}
