public interface iEnemyState
{
    void Enter(EnemyEntity enemy_entity);
    iEnemyState HandleInput(EnemyEntity enemy_entity);
}
