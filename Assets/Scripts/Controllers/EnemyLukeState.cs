using UnityEngine;

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
    private string[] _hit_animations = new string[2] { "HIT_01", "HIT_02" };
    private string _current_animation = "";

    public void Enter(EnemyEntity enemy_entity)
    {
        if (_current_animation == "")
        {
            int id = Random.Range(0, _hit_animations.Length);
            _current_animation = _hit_animations[id];
        }
        else
        {
            int id = Random.Range(0, _hit_animations.Length - 1);
            if (_hit_animations[id] == _current_animation)
            {
                id += 1;
            }
            _current_animation = _hit_animations[id];
        }

        if (enemy_entity.animator != null)
        {
            enemy_entity.animator.Play(_current_animation);
        }
    }

    public iEnemyState HandleInput(EnemyEntity enemy_entity)
    {
        if (enemy_entity.animator != null || enemy_entity.animator.IsPlaying(_current_animation) == false)
        {
            if (enemy_entity.animator.IsPlaying(_current_animation) == false)
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

//public class EnemyLukeDeathState : iEnemyState
//{
//    public void Enter(EnemyEntity enemy_entity)
//    {
//        if (enemy_entity.animator != null)
//        {
//            enemy_entity.animator.Play("DEATH");
//        }
//        else
//        {
//            SelfDestroy(enemy_entity.gameObject);
//        }

//        if (enemy_entity.shadow != null)
//        {
//            GameObject.Destroy(enemy_entity.shadow.gameObject);
//        }
//    }

//    public iEnemyState HandleInput(EnemyEntity enemy_entity)
//    {
//        if (enemy_entity.animator != null || enemy_entity.animator.IsPlaying("DEATH") == false)
//        {
//            SelfDestroy(enemy_entity.gameObject);
//            return null;
//        }
//        return null;
//    }

//    private void SelfDestroy(GameObject self)
//    {
//        GameObject.Destroy(self);
//    }
//}
