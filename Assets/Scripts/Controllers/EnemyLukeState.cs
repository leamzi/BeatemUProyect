public class EnemyLukeIdleState : iEnemyState
{
    public void Enter(EnemyEntity enemy_entity)
    {
        if (enemy_entity.animator != null)
            enemy_entity.animator.Play("IDLE");
    }

    public iEnemyState HandleInput(EnemyEntity enemy_entity)
    {
        return null;
    }
}

public class EnemyLukeHitState : iEnemyState
{
    public void Enter(EnemyEntity enemy_entity)
    {
        if (enemy_entity.animator != null)
            enemy_entity.animator.Play("HIT");
    }

    public iEnemyState HandleInput(EnemyEntity enemy_entity)
    {
        if (enemy_entity.animator != null)
        {
            if (enemy_entity.animator.IsPlaying("HIT") == false)
            {
                return new EnemyLukeIdleState();
            }
        }
        else
        {
            return new EnemyLukeIdleState();
        }

        return null;
    }
}
