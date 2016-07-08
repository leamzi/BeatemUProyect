﻿using UnityEngine;
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

    public void SetHit(Transform dealer_transform, int hit_damage)
    {
        ApplyHitState(dealer_transform);
        if ( OnHit != null)
        {
            OnHit(dealer_transform, hit_damage);
        }
    }

    protected abstract iEnemyState GetDefaultState();
    protected abstract void ApplyHitState(Transform dealer_transform);
}
