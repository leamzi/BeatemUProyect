using UnityEngine;
using System.Collections;

public abstract class EnemyController : MonoBehaviour {

    private iEnemyState _state;

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

    protected abstract iEnemyState GetDefaultState();

    public abstract void SetHit(PlayableEntity dealer);
}
