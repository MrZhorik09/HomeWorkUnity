using UnityEngine;
public class ChaseState : AIState
{
    public ChaseState(EnemyCharacter enemy) : base(enemy)
    {
        StateType = AIStateType.Chase;
    }

    public override void Enter()
    {
        Debug.Log($"{enemy.Data.CharacterName} перешел в состояние Chase");
    }

    public override void Update()
    {
        if (enemy.Target == null || !enemy.Target.Data.IsAlive())
        {
            enemy.ChangeState(new IdleState(enemy));
            return;
        }

        float distanceToTarget = Vector3.Distance(enemy.transform.position, enemy.Target.transform.position);

        
        if (distanceToTarget <= enemy.AttackRange)
        {
            enemy.ChangeState(new AttackState(enemy));
        }
        else if (distanceToTarget > enemy.ChaseRange)
        {
            enemy.ChangeState(new IdleState(enemy));
        }
        
        else
        {
            Vector3 direction = (enemy.Target.transform.position - enemy.transform.position).normalized;
            enemy.transform.position += direction * enemy.Data.MoveSpeed * Time.deltaTime;
        }
    }

    public override void Exit()
    {
    }
}
