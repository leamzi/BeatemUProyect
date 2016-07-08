using UnityEngine;
using System.Collections;
using System;

public class EnemyLukeController : EnemyController
{
    protected override iEnemyState GetDefaultState()
    {
        return new EnemyLukeIdleState();
    }

    protected override void ApplyHitState(Transform dealer_transform)
    {
        //Change direction to face the hit dealer 
        if (dealer_transform.transform.position.x < enemy_entity.transform.position.x)
        {
            enemy_entity.ChangeDirection((float) eWorldDirection.left);
        }
        else
        {
            enemy_entity.ChangeDirection((float)eWorldDirection.right);

        }

        //TODO: Maybe a new HitState call should be enough to randomize animations
        //TODO: it's not THAT bad that the same animation is played twice of more
        // Update the current state to HitState if it's different
        if (_state == null || _state.GetType() != typeof(EnemyLukeHitState))
        {
            SetState(new EnemyLukeHitState());
        }
        else // Else just re-enter the HitState to play another randomized animation
        {
            _state.Enter(enemy_entity);
        }

    }
}
