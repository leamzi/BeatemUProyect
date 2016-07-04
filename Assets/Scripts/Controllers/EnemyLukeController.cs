using UnityEngine;
using System.Collections;
using System;

public class EnemyLukeController : EnemyController
{
    protected override iEnemyState GetDefaultState()
    {
        return new EnemyLukeIdleState();
    }

    public override void SetHit(PlayableEntity dealer)
    {
        if ( dealer.transform.position.x < enemy_entity.transform.position.x)
        {
            enemy_entity.ChangeDirection((float) eWorldDirection.left);
        }
        else
        {
            enemy_entity.ChangeDirection((float)eWorldDirection.right);

        }

        if (_state == null || _state.GetType() != typeof(EnemyLukeHitState))
        {
            SetState(new EnemyLukeHitState());
        }
        else
        {
            _state.Enter(enemy_entity);
        }

    }
}
